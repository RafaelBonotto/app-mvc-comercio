using System.ComponentModel.DataAnnotations;

namespace Comercio.Requests.Fornecedor
{
    public class EnderecoRequest
    {
        public int Endereco_id { get; set; } 
        public int Fornecedor_id { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Campo logradouro obrigátorio")]
        public string Logradouro { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Campo Numero obrigátorio")]
        public string Numero { get; set; }

        [MaxLength(30)]
        public string Complemento { get; set; }

        [MaxLength(9)]
        [RegularExpression(@"^\d{5}-\d{3}$")]
        public string Cep { get; set; }

        [MaxLength(30)]
        public string Bairro { get; set; }

        [MaxLength(30)]
        public string Cidade { get; set; }

        [MaxLength(30)]
        public string Estado { get; set; }

        [MaxLength(2)]
        public string Uf { get; set; }

        public string TipoEndereco { get; set; }
    }
}
