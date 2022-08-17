using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Comercio.Responses.Produto
{
    public class AdicionarFornecedorProdutoViewResponse
    {
        public int Produto_id { get; set; } 
        public string Produto_codigo { get; set; }
        public string Produto_descricao { get; set; } 
        public IEnumerable<SelectListItem> FornecedoresBanco { get; set; }
    }
}
