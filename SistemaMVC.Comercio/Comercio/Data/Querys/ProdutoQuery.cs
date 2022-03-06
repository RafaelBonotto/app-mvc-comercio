namespace Comercio.Data.Querys
{
    public static class ProdutoQuery
    {
        public const string SELECT_POR_CODIGO = @"SELECT* 
                                                    FROM 
                                                    tb_produto  
                                                    INNER JOIN
                                                    tb_setor ON tb_setor.id = tb_produto.setor_id
                                                    WHERE 
                                                    codigo = @codigo";

        public const string SELECT_POR_DESCRICAO = @"SELECT * 
                                                        FROM 
                                                        tb_produto 
                                                        INNER JOIN
                                                        tb_setor ON tb_setor.id = tb_produto.setor_id
                                                        WHERE 
                                                        tb_produto.descricao LIKE CONCAT('%',@descricao,'%')";

        public const string SELECT_POR_SETOR_ID = @"SELECT* FROM 
                                                        tb_produto
                                                        INNER JOIN
                                                        tb_setor ON tb_setor.id = tb_produto.setor_id
                                                        WHERE 
                                                        tb_produto.setor_id = @setor_id";

        public const string SELECT_POR_ID = @"SELECT* FROM 
                                                tb_produto 
                                                INNER JOIN
                                                tb_setor ON tb_setor.id = tb_produto.setor_id
                                                WHERE 
                                                tb_produto.id = @produto_id";
    }
}
