using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backendapi.Models;
using backendapi.Services;

namespace backendapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DonationsController
    {
        private readonly DonationsService _donations;

        public DonationsController(DonationsService donationService)
        {
            _donations = donationService;
        }

        [HttpGet]
        public ActionResult<List<Donations>> Get() => _donations.Get();

    }
}
