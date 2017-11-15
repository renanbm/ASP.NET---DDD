using AutoMapper;
using RM.Architecture.Filiacao.Application.ViewModels;
using RM.Architecture.Filiacao.Domain.Entities.Cliente;
using RM.Architecture.Filiacao.Domain.Entities.Endereco;

namespace RM.Architecture.Filiacao.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Cliente, ClienteViewModel>()
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.Numero))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Endereco));

            CreateMap<Cliente, ClienteEnderecoViewModel>();
            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<Endereco, ClienteEnderecoViewModel>();
        }
    }
}