using AutoMapper;
using Pr.API.Common.Models.DTO;
using Pr.API.Common.Domain;

namespace Pr.API.Common.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductEntity, ProductViewModel>();
        }
    }
}
