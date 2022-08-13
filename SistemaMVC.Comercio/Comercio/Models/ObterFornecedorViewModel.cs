using System.Collections.Generic;

namespace Comercio.Models
{
    public class ObterFornecedorViewModel 
    {
        public int Produto_id { get; set; }
        public List<FornecedorDescricaoId> Fornecedores { get; set; } = new();
    }

    public class FornecedorDescricaoId
    {
        public int Fornecedor_id { get; set; }
        public string Descricao { get; set; }
    }
}
