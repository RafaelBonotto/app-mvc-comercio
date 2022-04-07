namespace Comercio.Data.Querys
{
    public class ProdutoQuerys
    {
        public const string SELECT_POR_CODIGO = @"SELECT* 
                                                    FROM 
                                                    tb_produto  
                                                    INNER JOIN
                                                    tb_setor 
                                                    ON tb_setor.id = tb_produto.setor_id
                                                    WHERE 
                                                    tb_produto.codigo = @Codigo";

        public const string SELECT_POR_DESCRICAO = @"SELECT* 
                                                        FROM 
                                                        tb_produto 
                                                        INNER JOIN
                                                        tb_setor        
                                                        ON tb_setor.id = tb_produto.setor_id
                                                        WHERE 
                                                        tb_produto.ativo = 1
                                                        AND
                                                        tb_produto.descricao LIKE CONCAT('%',@descricao,'%')
                                                        ORDER BY tb_produto.descricao";

        public const string SELECT_POR_SETOR = @"SELECT* 
                                                    FROM 
                                                    tb_produto
                                                    INNER JOIN
                                                    tb_setor 
                                                    ON tb_setor.id = tb_produto.setor_id
                                                    WHERE 
                                                    tb_produto.ativo = 1
                                                    AND
                                                    tb_setor.descricao = @setor
                                                    ORDER BY tb_produto.descricao";

        public const string SELECT_POR_ID = @"SELECT* 
                                                FROM 
                                                tb_produto 
                                                INNER JOIN
                                                tb_setor 
                                                ON tb_setor.id = tb_produto.setor_id
                                                WHERE 
                                                tb_produto.id = @produto_id";

        public const string SELECT_LISTAR_SETORES = @"SELECT * 
                                                        FROM tb_setor 
                                                        WHERE tb_setor.ativo = 1";

        public const string SELECT_ID_SETOR = @"SELECT id 
                                                    FROM
                                                    tb_setor
                                                    WHERE
                                                    tb_setor.descricao LIKE CONCAT('%',@descricao,'%')";

        public const string SELECT_LISTAR_FORNECEDORES = @"SELECT *
                                                            FROM
                                                            tb_fornecedor
                                                            INNER JOIN
                                                            tb_fornecedor_produto 
                                                            ON tb_fornecedor_produto.fornecedor_id = tb_fornecedor.id
                                                            WHERE
                                                            tb_fornecedor_produto.produto_id = @produto_id";
                                                        
    }
}
