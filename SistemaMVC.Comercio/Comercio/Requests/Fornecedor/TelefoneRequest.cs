using Comercio.Validations.Telefone;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Requests.Fornecedor
{
    public class TelefoneRequest
    {
        public int Telefone_id { get; set; }

        public int Fornecedor_id { get; set; }

        [MaxLength(3)]
        [Required(ErrorMessage = "Campo DDD obrigatório")]
        [TelefoneDddValidacao]
        public string Ddd { get; set; }

        [MaxLength(15)]
        [Required(ErrorMessage = "Campo Número obrigatório")]
        [TelefoneNumeroValidacao]
        public string Numero{ get; set; }

        public string Tipo_telefone { get; set; }

        public int Tipo_telefone_id { get; set; }
    }
}
