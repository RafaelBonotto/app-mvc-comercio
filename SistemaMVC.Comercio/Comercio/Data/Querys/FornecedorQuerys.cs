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
    }
}
