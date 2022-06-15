using Comercio.Data.Repositories.Response;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace Comercio.Entities
{
    [Table("tb_fornecedor")]
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Nome_empresa { get; set; }
        public string Email { get; set; } 
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }

        [Write(false)]
        public List<Produto> Produtos { get; set; } = new();

        [Write(false)]
        public List<Endereco> Endereco { get; set; } = new();

        [Write(false)]
        public List<Telefone> Telefone { get; set; } = new();

        [Write(false)]
        public List<PessoaContato> Vendedor { get; set; } = new();

        [Write(false)]
        public List<TipoTelefoneResponse> DescricaoTipoTelefone { get; set; } = new();

        [Write(false)]
        public List<TipoEnderecoResponse> DescricaoTipoEndereco { get; set; } = new();
    }
}
