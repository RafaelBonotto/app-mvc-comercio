using System.Collections.Generic;

namespace Comercio.Entities
{
    public class Pessoa 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string DataNasc { get; set; }
        public string Genero { get; set; }
        public virtual List<Telefone> Telefones { get; set; }
        public virtual List<Endereco> Endereco { get; set; }
    }
}
