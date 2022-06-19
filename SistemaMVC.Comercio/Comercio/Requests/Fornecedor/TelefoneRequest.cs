﻿using System.ComponentModel.DataAnnotations;

namespace Comercio.Requests.Fornecedor
{
    public class TelefoneRequest
    {
        public int Fornecedor_id { get; set; }

        [MaxLength(3)]
        [Required(ErrorMessage = "Campo Ddd obrigatório")]
        public string Ddd { get; set; }

        [MaxLength(15)]
        [Required(ErrorMessage = "Campo Numero obrigatório")]
        public string Nummero{ get; set; }

        public string TipoTelefone { get; set; }
    }
}