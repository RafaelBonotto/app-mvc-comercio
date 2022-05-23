namespace Comercio.Data.Querys
{
    public class EnderecoQuerys
    {
        public const string SELECT_ID_ENDERECO_FORNECEDOR = @"SELECT endereco_id
                                                                FROM tb_endereco_fornecedor fornecEnd
                                                                WHERE 
                                                                fornecEnd.fornecedor_id = @fornecedor_id
                                                                AND 
                                                                fornecEnd.ativo = 1";

        public const string SELECT_ID_TIPO_ENDERECO = @"SELECT id 
                                                        FROM tb_tipo_endereco tpEnd
                                                        WHERE tpEnd.descricao = @tipoEndereco";

        public const string SELECT_TIPO_ENDERECO = @"SELECT id, descricao 
                                                    FROM tb_tipo_endereco";

        public const string DESATIVAR_ENDERECO_FORNECEDOR = @"UPDATE tb_endereco_fornecedor fornecEnd
                                                                SET fornecEnd.Ativo = 0
                                                                WHERE fornecEnd.fornecedor_id = @fornecedor_id
                                                                AND fornecEnd.endereco_id = @endereco_id";
    }
}
