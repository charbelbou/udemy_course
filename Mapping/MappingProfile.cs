using AutoMapper;
using udemy.Models;
using udemy.Controllers.Resources;
using System.Linq;
using udemy_course.Controllers.Resources;
using udemy_course1.Controllers.Resources;
using udemy_course1.Core.Models;

namespace udemy.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            // Domain to API resource

            CreateMap(typeof(QueryResult<>),typeof(QueryResultResource<>));
            // Mapping Make to MakeResource
            CreateMap<Make,MakeResource>();
            
            // Mapping Make to KeyValuePairResource
            CreateMap<Make,KeyValuePairResource>();

            // Mapping Model to KeyValuePairResource
            CreateMap<Model,KeyValuePairResource>();

            // Mapping Feature to KeyValuePairResource
            CreateMap<Feature,KeyValuePairResource>();

            // Mapping Vehicle to VehicleResource
            CreateMap<Vehicle,SaveVehicleResource>()
                // Mapping Vehicle Contact fields to ContactResource fields
                .ForMember(vr=>vr.Contact,opt => opt.MapFrom(v => new ContactResource{
                    Name = v.ContactName,
                    Email = v.ContactEmail,
                    Phone = v.ContactPhone
                }))
                // Mapping Feature ID of Vehicle's features to VehicleResource's collection of IDs (integers)
                .ForMember(vr => vr.Features,opt => opt.MapFrom(v=>v.Features.Select(vf=>vf.FeatureId)));
            
            CreateMap<Vehicle,VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr=>vr.Contact,opt => opt.MapFrom(v => new ContactResource{
                    Name = v.ContactName,
                    Email = v.ContactEmail,
                    Phone = v.ContactPhone
                }))
                .ForMember(vr => vr.Features,opt => opt.MapFrom(v=>v.Features.Select(vf=> new KeyValuePairResource{
                    Id = vf.Feature.Id,
                    Name = vf.Feature.Name,
                })));



            // API Resource to Domain

            // Mapping VehicleQueryResource to VehicleQuery
            CreateMap<VehicleQueryResource,VehicleQuery>();
            // Mapping VehicleResource to Vehicle
            CreateMap<SaveVehicleResource,Vehicle>()
            // The property 'Vehicle.Id' is part of a key and so cannot be modified
            .ForMember(v => v.Id, opt => opt.Ignore())
            // Mapping the contact.name from VehicleResource's 'Contact' object to ContactName
            .ForMember(v=>v.ContactName,opt => opt.MapFrom(vr => vr.Contact.Name))
            // Mapping the contact.email from VehicleResource's 'Contact' object to ContactEmail
            .ForMember(v=>v.ContactEmail,opt => opt.MapFrom(vr => vr.Contact.Email))
            // Mapping the contact.phone from VehicleResource's 'Contact' object to ContactPhone
            .ForMember(v=>v.ContactPhone,opt => opt.MapFrom(vr => vr.Contact.Phone))
            // Ignoring Features, because they create a loop. (Features point back to Vehicle)
            .ForMember(v=>v.Features,opt => opt.Ignore())
            // Explain
            .AfterMap((vr,v) => {
                //Remove unselected Features

                // Finds the features in Vehicle object that arent in VehicleResource
                var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));
                // Delete these features
                foreach(var f in removedFeatures){
                    v.Features.Remove(f);
                }

                //Add new Features

                // Find the features in VehicleResource Object that aren't in Vehicle Object
                // These are new features
                // Add them to Vehicle Features
                var addedFeatures = vr.Features.Where(id => !v.Features.Any(f=>f.FeatureId == id)).Select(id=> new VehicleFeature{FeatureId=id});
                foreach(var f in addedFeatures){
                    v.Features.Add(f);
                }
            });
        }
    }
}