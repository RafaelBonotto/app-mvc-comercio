namespace Comercio.Data.Repositories.Response
{
    public class TipoTelefoneResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public override string ToString()
            => Descricao.ToString();
    }
}
