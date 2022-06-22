using System.ComponentModel.DataAnnotations;

namespace Comercio.Requests.Fornecedor
{
    public class TelefoneRequest
    {
        public int Fornecedor_id { get; set; }

        [MaxLength(3)]
        [Required(ErrorMessage = "Campo DDD obrigatório")]
        public string Ddd { get; set; }

        [MaxLength(15)]
        [Required(ErrorMessage = "Campo Número obrigatório")]
        public string Numero{ get; set; }

        public string TipoTelefone { get; set; }
    }
}
