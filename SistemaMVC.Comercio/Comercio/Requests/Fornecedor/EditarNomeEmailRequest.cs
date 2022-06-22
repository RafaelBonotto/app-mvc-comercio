using Comercio.Validations.Base;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Requests.Fornecedor
{
    public class EditarNomeEmailRequest
    {
        public int Fornecedor_id { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Campo Nome obrigatório")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido")]
        [EmailValidacaoCaracterEspecial]
        public string Email { get; set; }
    }
}
