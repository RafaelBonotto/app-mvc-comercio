namespace Comercio.Data.Querys
{
    public static class ProdutoQuery
    {
        public const string SELECT_POR_CODIGO = @"SELECT* FROM 
                                                  tb_produto WHERE 
                                                  codigo = @codigo";

        public const string SELECT_POR_DESCRICAO = @"SELECT* FROM 
                                                        tb_produto WHERE 
                                                        descricao LIKE CONCAT('%',@descricao,'%')";

        public const string SELECT_POR_SETOR_ID = @"SELECT* FROM 
                                                        tb_produto WHERE 
                                                        setor_id = @setor_id";

        public const string SELECT_POR_ID = @"SELECT* FROM 
                                                tb_produto WHERE 
                                                id = @id";
    }
}
