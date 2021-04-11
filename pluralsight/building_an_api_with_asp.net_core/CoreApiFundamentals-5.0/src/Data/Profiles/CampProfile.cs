using AutoMapper;
using CoreCodeCamp.models;

namespace CoreCodeCamp.Data.Profiles
{
    public class CampProfile : Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModel>()
                .ForMember(c => c.Venue, o => o.MapFrom(m => m.Location.VenueName));                
        }
    }
}
