﻿using Comercio.Enum;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Models
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "CÓDIGO")]
        [Required(ErrorMessage = ("O campo código é obrigatório"))]
        [MinLength(6, ErrorMessage = "O campo código deve ter no mínimo 6 dígitos")]
        public string Codigo { get; set; }

        [Display(Name = "PRODUTO")]
        [Required(ErrorMessage = ("O campo descrição é obrigatório"))]
        public string Descricao { get; set; }

        [Display(Name = "CUSTO")]
        [Required(ErrorMessage = ("O preço de custo do produto é obrigatório"))]
        public string Preco_custo { get; set; }

        [Display(Name = "VENDA")]
        [Required(ErrorMessage = ("O preço de venda do produto é obrigatório"))]
        public string Preco_venda { get; set; }

        [Display(Name = "STATUS")]
        public string Ativo { get; set; } 

        [Display(Name = "SETOR")]
        [Required]
        [EnumDataType(typeof(Setores))]
        public Setores Setor { get; set; }
        //public string Setor { get; set; }

        //public int Setor_id { get; set; }
    }
}