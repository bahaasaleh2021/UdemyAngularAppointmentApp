using System.Linq;
using API.DTO;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,MemberDto>()
            .ForMember(dest=>dest.PhotoUrl,opt=>opt.MapFrom(src=>src.Photos.FirstOrDefault().Url))
            .ForMember(dest=>dest.Age,opt=>opt.MapFrom(src=>src.DateOfBirth.CalculateAge())) ;
            CreateMap<Photo,PhotoDto>();
            CreateMap<MemberUpdateDTO,AppUser>();
            CreateMap<RegisterDTO,AppUser>();

            CreateMap<Message,MessageDTO>()
            .ForMember(dest=>dest.SenderPhotoUrl,
            memberOptions=>memberOptions.MapFrom(src=>src.Sender.Photos.FirstOrDefault(x=>x.IsMain).Url))
            .ForMember(dest=>dest.RecepientPhotoUrl,
            memberOptions=>memberOptions.MapFrom(src=>src.Recepient.Photos.FirstOrDefault(x=>x.IsMain).Url));


        }
    }
}