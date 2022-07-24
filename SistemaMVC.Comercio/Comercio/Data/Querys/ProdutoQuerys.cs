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

        public const string SELECT_ID_FORNECEDOR_POR_PRODUTO = @"SELECT fornecedor_id 
                                                                    FROM tb_fornecedor_produto TBFP 
                                                                    WHERE TBFP.produto_id = @produto_id;";

        public const string SELECT_LISTA_FORNECEDORES = @"SELECT cnpj, nome_empresa 
                                                            FROM tb_fornecedor TBF 
                                                            WHERE TBF.ativo = 1;";

        public const string SELECT_FORNECEDOR_POR_CNPJ = @"SELECT id
                                                            FROM tb_fornecedor
                                                            WHERE tb_fornecedor.cnpj = @cnpj";
    }
}
