using Comercio.Entities;
using System.Collections.Generic;

namespace Comercio.Responses.Produto
{
    public class ObterFornecedoresDadosProdutoResponse
    {
        public int IdProduto { get; set; }
        public string CodigoProduto { get; set; }
        public string DescircaoProduto { get; set; }
        public List<Fornecedor> Fornecedores { get; set; } = new();
    }
}
