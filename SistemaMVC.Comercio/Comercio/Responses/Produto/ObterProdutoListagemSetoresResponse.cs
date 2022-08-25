using Comercio.Entities;
using System.Collections.Generic;

namespace Comercio.Responses.Produto
{
    public class ObterProdutoListagemSetoresResponse
    {
        public Comercio.Entities.Produto Produto { get; set; }
        public List<Setor> Setores { get; set; }
    }
}
