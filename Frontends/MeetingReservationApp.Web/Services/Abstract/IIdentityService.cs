using IdentityModel.Client;
using MeetingReservationApp.Web.Models;
using MeetingReservationApp.Web.Results.Abstract;
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
