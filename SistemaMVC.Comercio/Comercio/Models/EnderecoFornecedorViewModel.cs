using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Comercio.Models
{
    public class EnderecoFornecedorViewModel
    {
        public int Fornecedor_id { get; set; }
        public int Endereco_id { get; set; }
        public int Tipo_endereco_id { get; set; }
        public string Tipo_endereco { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string UF { get; set; }
        public IEnumerable<SelectListItem> TiposEnderecoBanco { get; set; }
    }
}
