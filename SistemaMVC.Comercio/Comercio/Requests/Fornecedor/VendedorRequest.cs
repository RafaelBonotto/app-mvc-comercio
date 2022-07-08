using Comercio.Validations.Base;
using Comercio.Validations.Telefone;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Requests.Fornecedor
{
    public class VendedorRequest
    {
        public int Vendedor_id { get; set; }
        public int Fornecedor_id { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "campo Nome obrigatório")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [EmailValidacaoCaracterEspecial]
        public string Email { get; set; }

        [MaxLength(3)]
        [Required(ErrorMessage = "Campo Ddd obrigatório")]
        [TelefoneDddValidacao]
        public string Ddd { get; set; }

        [MaxLength(15)]
        [Required(ErrorMessage = "Campo Numero obrigatório")]
        [TelefoneNumeroValidacao]
        public string Numero { get; set; }

        [MaxLength(3)]
        [TelefoneDddValidacao]
        public string DddAdicional { get; set; }

        [MaxLength(15)]
        [TelefoneNumeroValidacao]
        public string NumeroAdicional { get; set; }
    }
}
