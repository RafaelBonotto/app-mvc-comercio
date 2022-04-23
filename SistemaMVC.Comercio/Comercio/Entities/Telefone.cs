﻿using Comercio.Enums;

namespace Comercio.Entities
{
    public class Telefone 
    {
        public int Id { get; set; }
        public TipoTelefone Tipo { get; set; }
        public string Ddd { get; set; }
        public string Numero { get; set; }
    }
}
