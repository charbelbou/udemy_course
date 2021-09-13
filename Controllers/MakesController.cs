using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using udemy.Models;
using udemy.Persistence;
using udemy.Controllers.Resources;

namespace udemy.Controllers
{
    public class MakesController : Controller
    {
        private readonly UdemyDbContext context;
        private readonly IMapper mapper;

        //Injecting UdemyDbContext and Imapper into the constructor, and initializing the fields.
        public MakesController(UdemyDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }
        //Function responds to GET on the 'api/makes' endpoint, function gets the makes.
        [HttpGet("api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            // Uses UdemyDbContext to get Makes (and include the models that belong to it)
            // and returns them as a list.
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();
            // Maps the List of Makes to a list of MakeResources, made possible through
            // AutoMapper, configured in the MappingProfile file.
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}