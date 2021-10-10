using MeetingReservationApp.Shared.Utilities.Results.ComplexTypes;
using MeetingReservationApp.Web.Models;
using MeetingReservationApp.Web.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInInput signinInput)
        {
            if (signinInput is null)
            {
                throw new ArgumentNullException(nameof(signinInput));
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _identityService.SignIn(signinInput);

            if (response.ResultStatus != ResultStatus.Success)
            {
                ModelState.AddModelError(String.Empty, response.Message);

                return View();
            }

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
