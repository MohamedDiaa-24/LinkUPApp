using System;
using AutoMapper;
using LinkUpApp.API.DTOs;
using LinkUpApp.API.Entities;
using LinkUpApp.API.Extensions;

namespace LinkUpApp.API.Helpers;

public class AutoMapperProfiles :Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDto>()
        .ForMember(d=>d.Age,o=>o.MapFrom(s=>s.DateOfBirth.CalculateAge()))
        .ForMember(d => d.PhotoUrl, o => o.MapFrom(s=>s.Photos.FirstOrDefault(x=>x.IsMain)!.Url));
        CreateMap<Photo, PhotoDto>();

    }
}
