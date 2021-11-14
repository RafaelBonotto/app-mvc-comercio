using System;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Display(Name = "CÓDIGO")]
        [Required(ErrorMessage = ("O campo código é obrigatório"))]
        [MinLength(6, ErrorMessage = "O campo código deve ter no mínimo 6 dígitos")]
        public string Codigo { get; set; }

        [Display(Name = "PRODUTO")]
        [Required(ErrorMessage = ("A descrição do produto é obrigatória"))]
        public string Descricao { get; set; }

        [Display(Name = "SETOR")]
        public string Setor { get; set; }

        [Display(Name = "CUSTO")]
        [Required(ErrorMessage = ("O preço de custo do produto é obrigatório"))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public decimal Preco_custo { get; set; }

        [Display(Name = "VENDA")]
        [Required(ErrorMessage = ("O preço de venda do produto é obrigatório"))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public decimal Preco_venda { get; set; }

        [Display(Name = "FABRICAÇÃO")]
        [DataType(DataType.Date)]
        public DateTime Data_fabricacao { get; set; }

        [Display(Name = "VALIDADE")]
        [DataType(DataType.Date)]
        public DateTime Data_validade { get; set; }
        public int Setor_id { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }
    }
}
