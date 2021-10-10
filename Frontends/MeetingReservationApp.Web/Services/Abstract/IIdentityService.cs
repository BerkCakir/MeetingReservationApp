using IdentityModel.Client;
using MeetingReservationApp.Shared.Utilities.Results.Abstract;
using MeetingReservationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Services.Abstract
{
    public interface IIdentityService
    {
        Task<IResult> SignIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
