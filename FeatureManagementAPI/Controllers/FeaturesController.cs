using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeaturesController : ControllerBase
    {
        public FeaturesController()
        {
           
        }   
    }

    public class Feature
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string EstimatedComplexity { get; set; } // S, M, L, XL
        public string Status { get; set; } // New, Active, Closed, Abandoned
        public DateTime? TargetCompletionDate { get; set; }
        public DateTime? ActualCompletionDate { get; set; }
    }
}
