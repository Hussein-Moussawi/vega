using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using vega.Controllers.Resources;
using vega.Models;

namespace vega.Controllers.Mapping
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            // Domain To API Resource
            CreateMap<Make, MakesResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(v => v.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactName, Phone = v.ContactPhone }))
                .ForMember(v => v.Features, opt => opt.MapFrom(v => v.VehicleFeatures.Select(f => f.FeatureId)));
           CreateMap<Vehicle, VehicleResource>()
                .ForMember(v => v.Make, opt => opt.MapFrom(m => m.Model.Make))
                .ForMember(v => v.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactName, Phone = v.ContactPhone }))
                .ForMember(v => v.Features, opt => opt.MapFrom(v => v.VehicleFeatures.Select(f => new KeyValuePairResource { Id = f.Feature.Id, Name = f.Feature.Name})));
            // API Resource to Domain
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.VehicleFeatures, opt => opt.Ignore())
                .AfterMap((vr,v) => {
                    //Remove unselected features
                    var featuresToRemove = v.VehicleFeatures.Where(vf => !vr.Features.Any(f => f == vf.FeatureId)).ToList();
                    foreach (var featurId in featuresToRemove)
                        v.VehicleFeatures.Remove(featurId);

                    //Add new Features
                    var featuresToAdd = vr.Features.Where(id => !v.VehicleFeatures.Any(vf => vf.FeatureId == id)).Select(feature => new VehicleFeature {FeatureId = feature});
                    foreach (var feature in featuresToAdd)
                        v.VehicleFeatures.Add(feature);
                });
        }
    }
}
