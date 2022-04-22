using System.Collections.Generic;

namespace Comercio.Entities
{
    public class Vendedor
    {
        public Pessoa Pessoa { get; set; } 
        public List<Telefone> Telefones { get; set; }
    }
}
