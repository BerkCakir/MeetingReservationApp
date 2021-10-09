using AutoMapper;
using MeetingReservationApp.Data.Abstract;
using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Managers.Abstract;
using MeetingReservationApp.Shared.Utilities.Messages;
using MeetingReservationApp.Shared.Utilities.Results.Abstract;
using MeetingReservationApp.Shared.Utilities.Results.ComplexTypes;
using MeetingReservationApp.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingReservationApp.Managers.Concrete
{
    public class InventoryReservationManager : IInventoryReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InventoryReservationManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(InventoryReservationAddDto inventoryReservationAddDto, int locationId)
        {
            var newReservation = _mapper.Map<InventoryReservation>(inventoryReservationAddDto);

            #region Check the given reservation exists
            var roomReservation = await _unitOfWork.RoomReservations.GetAsync(x => x.RoomReservationGuid == newReservation.RoomReservationGuid, x=> x.InventoryReservations);
            if (roomReservation.Id == 0)
            {
                return new Result(ResultStatus.Error, Messages.InventoryReservation.RelatedReservationNotExists());
            }
            #endregion

            #region Check if another reservation exists for the inventory
            var result = await CheckHoursForInventory(roomReservation.MeetingStartTime, roomReservation.MeetingEndTime, newReservation.InventoryId);
            if (result.ResultStatus != ResultStatus.Success)
            {
                return result;
            }
            #endregion

            #region Another inventory exists for the same purpose
            if (roomReservation.InventoryReservations.Count > 0)
            {
                var newInventory = await _unitOfWork.Inventories.GetAsync(x => x.Id == newReservation.InventoryId);
                foreach (var inventoryReservation in roomReservation.InventoryReservations)
                {
                    var exists = await _unitOfWork.Inventories.AnyAsync(x => x.Id == inventoryReservation.InventoryId && x.InventoryPurpose == newInventory.InventoryPurpose);
                    if (exists)
                    {
                        return new Result(ResultStatus.Error, Messages.InventoryReservation.SamePurposeInventoryExists());
                    }
                }
            }
            #endregion

            #region Create new inventory reservation
            await _unitOfWork.InventoryReservations.AddAsync(newReservation);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.InventoryReservation.Success());
            #endregion
        }

        public async Task<IDataResult<IList<Inventory>>> GetAvailableInventories(AvailabilitySearchDto availabilitySearchDto, int locationId)
        {
            #region Create dates
            DateTime desiredStartDate = availabilitySearchDto.DesiredDate.Date.AddHours(availabilitySearchDto.StartHours).AddMinutes(availabilitySearchDto.StartMinutes);
            DateTime desiredEndDate = availabilitySearchDto.DesiredDate.Date.AddHours(availabilitySearchDto.EndHours).AddMinutes(availabilitySearchDto.EndMinutes);
            #endregion

            List<Inventory> availableInventories = new List<Inventory>();
            var inventories = await _unitOfWork.Inventories.GetAllAsync(x => x.Room.LocationId == locationId, x => x.Room);
            if (inventories.Count > 0)
            {
                foreach (var inventory in inventories)
                {
                    if(inventory.IsFixed)
                    {
                        continue; // fixed inventories can't get reserved
                    }
                    var result = await CheckHoursForInventory(desiredStartDate, desiredEndDate, inventory.Id);
                    if (result.ResultStatus == ResultStatus.Success)
                    {
                        inventory.Room = null; // the owner room object of the inventory not returned
                        availableInventories.Add(inventory);
                    }
                }
                if (availableInventories.Count > 0)
                {
                    return new DataResult<IList<Inventory>>(ResultStatus.Success, availableInventories);
                }
            }
            // not any office available
            return new DataResult<IList<Inventory>>(ResultStatus.Error, Messages.InventoryReservation.HoursNotAvailableForInventory(), null);
        }

        #region Private Bussiness Methods
        private async Task<IResult> CheckHoursForInventory(DateTime newStartTime, DateTime newEndTime, int inventoryId)
        {
            var exists = await _unitOfWork.InventoryReservations.GetAllAsync(x => x.InventoryId == inventoryId &&
                                                                      ((newStartTime >= x.RoomReservation.MeetingStartTime && newStartTime <= x.RoomReservation.MeetingEndTime) ||
                                                                       (newEndTime >= x.RoomReservation.MeetingStartTime && newEndTime <= x.RoomReservation.MeetingEndTime) ||
                                                                       (newStartTime.Date == x.RoomReservation.MeetingStartTime.Date &&
                                                                       newStartTime <= x.RoomReservation.MeetingStartTime && newEndTime >= x.RoomReservation.MeetingEndTime)),
                                                                       x => x.RoomReservation);

            if (exists.Count > 0)
            {
                // Another reservation exists at the desired time interval
                return new Result(ResultStatus.Error, Messages.InventoryReservation.AnotherReservationExists());
            }
            return new Result(ResultStatus.Success);
        }
        #endregion
    }
}
