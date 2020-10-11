using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Context;
using vega.Controllers.Resources;
using vega.Models;

namespace vega.Controllers
{
    [Route("api/feature")]
    [ApiController]
    public class FeatureController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public FeatureController(VegaDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }



        [HttpGet]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();
            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
        }

       
    }
}
