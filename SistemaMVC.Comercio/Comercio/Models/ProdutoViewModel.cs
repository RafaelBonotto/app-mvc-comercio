using System.ComponentModel.DataAnnotations;

namespace Comercio.Models
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "CÓDIGO")]
        public string Codigo { get; set; }

        [Display(Name = "PRODUTO")]
        public string Descricao { get; set; }

        [Display(Name = "CUSTO")]
        public string Preco_custo { get; set; }

        [Display(Name = "VENDA")]
        public string Preco_venda { get; set; }

        [Display(Name = "STATUS")]
        public string Ativo { get; set; } = "Ativo";

        [Display(Name = "SETOR")]
        public string Setor { get; set; }

        public int Setor_id { get; set; }
    }
}
