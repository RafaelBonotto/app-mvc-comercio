using System.Collections.Generic;

namespace Comercio.Entities
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string DataNasc { get; set; }
        public string Genero { get; set; }
        public List<Telefone> Telefones { get; set; }
        public List<Endereco> Endereco { get; set; }
    }
}
