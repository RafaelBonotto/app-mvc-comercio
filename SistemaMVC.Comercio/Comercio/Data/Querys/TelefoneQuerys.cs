namespace Comercio.Data.Querys
{
    public class TelefoneQuerys
    {
        public const string SELECT_ID_TIPO_TELEFONE = @"SELECT id 
                                                        FROM tb_tipo_telefone tpTel
                                                        WHERE tpTel.descricao = @tipoTelefone";

        public const string SELECT_TIPO_TELEFONE = @"SELECT id, descricao 
                                                    FROM tb_tipo_telefone";

        public const string DESATIVAR_TELEFONE_FORNECEDOR = @"UPDATE tb_telefone_fornecedor fornecTel
                                                                SET fornecTel.Ativo = 0
                                                                WHERE fornecTel.fornecedor_id = @fornecedor_id
                                                                AND fornecTel.telefone_id = @telefone_id";
    }
}
