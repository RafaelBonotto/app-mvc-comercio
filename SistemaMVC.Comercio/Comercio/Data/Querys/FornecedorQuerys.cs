namespace Comercio.Data.Querys
{
    public class FornecedorQuerys
    {
        public const string SELECT_POR_CNPJ = @"SELECT * 
                                                FROM tb_fornecedor 
                                                WHERE tb_fornecedor.cnpj = @cnpj";

        public const string SELECT_ID_TELEFONE_FORNECEDOR = @"SELECT telefone_id
                                                                FROM tb_fornecedor_telefone fornecTel
                                                                WHERE 
                                                                fornecTel.fornecedor_id = @fornecedor_id
                                                                AND 
                                                                fornecTel.ativo = 1";
        
        public const string SELECT_ID_ENDERECO_FORNECEDOR = @"SELECT endereco_id
                                                                FROM tb_fornecedor_endereco fornecEnd
                                                                WHERE 
                                                                fornecEnd.fornecedor_id = @fornecedor_id
                                                                AND 
                                                                fornecEnd.ativo = 1";

        public const string SELECT_ID_VENDEDOR_FORNECEDOR = @"SELECT pessoa_id
                                                                FROM tb_fornecedor_pessoa fornecPes
                                                                WHERE 
                                                                fornecPes.fornecedor_id = @fornecedor_id
                                                                AND
                                                                fornecPes.ativo = 1";
    }
}
