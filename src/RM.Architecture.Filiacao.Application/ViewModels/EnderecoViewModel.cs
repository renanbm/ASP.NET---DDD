using System;
using System.ComponentModel.DataAnnotations;

namespace RM.Architecture.Filiacao.Application.ViewModels
{
    public class EnderecoViewModel
    {
        public EnderecoViewModel()
        {
            CodEndereco = Guid.NewGuid();
        }

        [Key]
        public Guid CodEndereco { get; set; }

        [Required(ErrorMessage = "Preencha o campo Logradouro")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caractéres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caractéres")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Preencha o campo Número")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caractéres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caractéres")]
        public string Numero { get; set; }

        [MaxLength(150, ErrorMessage = "Máximo {0} caractéres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caractéres")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Preencha o campo Bairro")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caractéres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caractéres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Preencha o campo Cep")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caractéres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caractéres")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Preencha o campo Cidade")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caractéres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caractéres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Preencha o campo Estado")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caractéres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caractéres")]
        public string Estado { get; set; }

        [ScaffoldColumn(false)]
        public Guid CodCliente { get; set; }
    }
}