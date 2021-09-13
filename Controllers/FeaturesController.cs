using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using udemy.Controllers.Resources;
using udemy.Models;
using udemy.Persistence;

namespace udemy_course1.Controllers
{
    public class FeaturesController
    {
        private readonly UdemyDbContext context;
        private readonly IMapper mapper;

        //Injecting UdemyDbContext and Imapper into the constructor, and initializing the fields.
        public FeaturesController(UdemyDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        //Function responds to GET on the 'api/features' endpoint, function gets the features.
        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures(){
            // Uses UdemyDbContext to get features and returns them as a list.
            var features = await context.Features.ToListAsync();
            // Maps the List of Features to a list of FeatureResources.
            // Made possible through AutoMapper, which was configured in the MappingProfile file.
            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features); 
        }
    }
}