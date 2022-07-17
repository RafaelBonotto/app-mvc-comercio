namespace Comercio.Data.Querys
{
    public class FornecedorQuerys
    {
        public const string SELECT_POR_CNPJ = @"SELECT * 
                                                FROM tb_fornecedor 
                                                WHERE tb_fornecedor.cnpj = @cnpj";

        public const string SELECT_ID_VENDEDOR_FORNECEDOR = @"SELECT pessoa_contato_id
                                                                FROM tb_pessoa_contato_fornecedor fornecPes
                                                                WHERE 
                                                                fornecPes.fornecedor_id = @fornecedor_id
                                                                AND
                                                                fornecPes.ativo = 1";

        public const string SELECT_ID_TELEFONE_VENDEDOR = @"SELECT telefone_id
                                                                FROM tb_pessoa_contato_telefone tb
                                                                WHERE
                                                                tb.pessoa_contato_id = @vendedor_id
                                                                AND
                                                                tb.ativo = 1";

        public const string SELECT_VENDEDOR = @"SELECT *
                                                    FROM tb_pessoa_contato_fornecedor tb
                                                    WHERE tb.fornecedor_id = @fornecedor_id
                                                    AND tb.pessoa_contato_id = @vendedor_id";

        public const string DESATIVAR_FORNECEDOR = @"UPDATE tb_fornecedor tb
                                                        SET ativo = 0 
                                                        WHERE  tb.id = @id";

        public const string SELECT_PRODUTO_ID = @"SELECT produto_id 
                                                    FROM tb_fornecedor_produto tb
                                                    WHERE tb.fornecedor_id = @fornecedor_id";

        public const string SELECT_PRODUTOS = @"SELECT * 
                                                    FROM tb_produto tb
                                                    WHERE tb.id = @id";

        public const string SELECT_FORNECEDOR_ID_POR_SETOR = @"SELECT fornecedor_id 
                                                                FROM tb_fornecedor_produto TBFP
                                                                INNER JOIN tb_produto TBP
                                                                ON TBFP.produto_id = TBP.id
                                                                WHERE TBP.setor_id = @setor_id";

        public const string SELECT_ID_SETOR = @"SELECT id 
                                                FROM tb_setor TBS
                                                WHERE TBS.descricao = @setorDescricao";
    }
}
