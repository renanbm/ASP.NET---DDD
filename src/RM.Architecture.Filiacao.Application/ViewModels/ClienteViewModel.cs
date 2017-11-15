using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ValidationResult = DomainValidation.Validation.ValidationResult;

namespace RM.Architecture.Filiacao.Application.ViewModels
{
    public class ClienteViewModel
    {
        public ClienteViewModel()
        {
            CodCliente = Guid.NewGuid();
            Enderecos = new List<EnderecoViewModel>();
        }

        [Key]
        public Guid CodCliente { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caractéres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caractéres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo CPF")]
        [MaxLength(11, ErrorMessage = "Máximo {0} caractéres")]
        [DisplayName("CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Preencha o campo E-mail")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caractéres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caractéres")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataNascimento { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [ScaffoldColumn(false)]
        public bool Ativo { get; set; }

        [ScaffoldColumn(false)]
        public ValidationResult ValidationResult { get; set; }

        public virtual ICollection<EnderecoViewModel> Enderecos { get; set; }
    }
}