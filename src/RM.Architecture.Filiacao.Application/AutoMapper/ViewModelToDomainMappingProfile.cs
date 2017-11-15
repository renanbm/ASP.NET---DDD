using AutoMapper;
using RM.Architecture.Filiacao.Application.ViewModels;
using RM.Architecture.Filiacao.Domain.Entities.Cliente;
using RM.Architecture.Filiacao.Domain.Entities.Endereco;
using RM.Architecture.Filiacao.Domain.Value_Objects;
using RM.Architecture.Filiacao.Domain.Value_Objects.Documentos;

namespace RM.Architecture.Filiacao.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteViewModel, Cliente>()
                .ForMember(dest => dest.Cpf, input => input.ResolveUsing(src => new Cpf {Numero = src.Cpf}))
                .ForMember(dest => dest.Email, input => input.ResolveUsing(src => new Email {Endereco = src.Email}));

            CreateMap<ClienteEnderecoViewModel, Cliente>();
            CreateMap<EnderecoViewModel, Endereco>();
            CreateMap<ClienteEnderecoViewModel, Endereco>();
        }
    }
}