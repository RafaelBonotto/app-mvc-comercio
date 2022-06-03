namespace Comercio.Requests.Fornecedor
{
    public class AdicionarVendedorRequest
    {
        public int Fornecedor_id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Ddd { get; set; }
        public string Numero { get; set; }
        public string DddAdicional { get; set; }
        public string NumeroAdicional { get; set; }
    }
}
