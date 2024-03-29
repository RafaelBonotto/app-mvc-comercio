﻿using Comercio.Entities;
using Comercio.Validations.Produtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Models
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "CÓDIGO")]
        [Required(ErrorMessage = ("O campo código é obrigatório"), AllowEmptyStrings = false)]
        [MinLength(6, ErrorMessage = "O campo código deve ter no mínimo 6 dígitos")]
        public string Codigo { get; set; }

        [Display(Name = "PRODUTO")]
        [MaxLength(50)]
        [Required(ErrorMessage = ("O campo descrição é obrigatório"), AllowEmptyStrings = false)]
        public string Descricao { get; set; }

        [Display(Name = "CUSTO")]
        [PrecoValidacao]
        [Required(ErrorMessage = ("O preço de custo do produto é obrigatório"))]
        public string Preco_custo { get; set; }

        [Display(Name = "VENDA")]
        [PrecoValidacao]
        [Required(ErrorMessage = ("O preço de venda do produto é obrigatório"))]
        public string Preco_venda { get; set; }

        [Display(Name = "STATUS")]
        public string Ativo { get; set; } 

        [Display(Name = "SETOR")]
        [Required]
        public string SetorDescricao { get; set; }

        public int Setor_id { get; set; }

        public List<Fornecedor> FornecedorProduto { get; set; }

        public IEnumerable<SelectListItem> SetoresBanco { get; set; } 
        public IEnumerable<SelectListItem> FornecedoresBanco { get; set; }  
    }
}