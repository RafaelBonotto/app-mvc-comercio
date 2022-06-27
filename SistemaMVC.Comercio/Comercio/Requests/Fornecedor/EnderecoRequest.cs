using System.ComponentModel.DataAnnotations;

namespace Comercio.Requests.Fornecedor
{
    public class EnderecoRequest
    {
        public int Endereco_id { get; set; } 
        public int Fornecedor_id { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Campo logradouro obrigátorio", AllowEmptyStrings = false)]
        public string Logradouro { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Campo Número obrigátorio", AllowEmptyStrings = false)]
        public string Numero { get; set; }

        [MaxLength(30)]
        public string Complemento { get; set; }

        [MaxLength(9)]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve estar no formato: 00000-000")]
        [Required(ErrorMessage = "Campo Cep obrigátorio", AllowEmptyStrings = false)]
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
