using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Comercio.Models
{
    public class TelefoneFornecedorViewModel
    {
        public int Fornecedor_id { get; set; }
        public int Telefone_id { get; set; }
        public int Tipo_telefone_id { get; set; }
        public string Ddd { get; set; }
        public string Numero { get; set; }
        public string Tipo_telefone { get; set; }
        public IEnumerable<SelectListItem> TiposTelefoneBanco { get; set; }
    }
}
