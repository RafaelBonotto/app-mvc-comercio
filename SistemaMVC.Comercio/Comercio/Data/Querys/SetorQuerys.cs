namespace Comercio.Data.Querys
{
    public class SetorQuerys
    {
        public const string SELECT_POR_DESCRICAO = @"SELECT * 
                                                        FROM tb_setor
                                                        WHERE tb_setor.descricao = @descricao";
    }
}
