using AutoMapper;
using StoreMarketApp.Contracts.Requests;
using StoreMarketApp.Contracts.Responses;
using StoreMarketApp.Models;

namespace StoreMarketApp.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            // CreateMap<первое - Product - из какого объекта будем конвертировать, второе - ProductResponse = в какой объект>
            CreateMap<Product, ProductResponse>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductDeleteRequest>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductUpdateRequest>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductCreateRequest>(MemberList.Destination).ReverseMap();
        }
    }
}
