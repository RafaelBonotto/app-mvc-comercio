using System;

namespace Comercio.Data.Repositories.Request
{
    public class ProdutoRepositorioRequest
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco_custo { get; set; }
        public decimal Preco_venda { get; set; }
        public int Setor_id { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_alteracao { get; set; }
    }
}
