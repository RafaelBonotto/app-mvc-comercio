using Comercio.Enums;

namespace Comercio.Entities
{
    public class Endereco 
    {
        public int Id { get; set; }
        public TipoEndereco Tipo { get; set; }
        public string Logradouro { get; set; }
        public string NumeroLogradouro { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
