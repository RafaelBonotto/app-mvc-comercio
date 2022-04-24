using Comercio.Enums;
using Dapper.Contrib.Extensions;

namespace Comercio.Entities
{
    [Table("tb_telefone")]
    public class Telefone 
    {
        public int Id { get; set; }
        public TipoTelefone Tipo { get; set; }
        public string Ddd { get; set; }
        public string Numero { get; set; }
    }
}
