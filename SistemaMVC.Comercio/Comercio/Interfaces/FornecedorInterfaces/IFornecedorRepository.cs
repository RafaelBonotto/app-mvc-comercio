﻿using Comercio.Entities;
using Comercio.Requests.Fornecedor;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorRepository
    {
        Task<Fornecedor> GetFornecedorAsync(int id, MySqlConnection connection);
        Task<Fornecedor> InserirVendedor(int fornecedor_id, PessoaContato vendedor, List<Telefone> telefones);
        Task<Fornecedor> InserirEndereco(EnderecoRequest req);
        Task<Fornecedor> AtualizarVendedor(VendedorRequest vendedor);
        Task<bool> ExcluirFornecedor(int id);
        Task<Fornecedor> ExcluirVendedor(int fornecedor_id, int vendedor_id);
        Task<Fornecedor> ExcluirTelefone(int fornecedor_id, int telefone_id);
        Task<Fornecedor> ExcluirEndereco(int fornecedor_id, int endereco_id);
        Task<PessoaContato> GetVendedor(int vendedor_id);
        Task<List<Telefone>> GetTelefoneVendedor(int vendedor_id, MySqlConnection connection = null, MySqlTransaction transaction = null);
        Task<Fornecedor> InserirTelefone(int fornecedor_id, Telefone telefone, MySqlConnection connection = null);
        Task<Fornecedor> EditarTelefone(TelefoneRequest telefone, MySqlConnection connection = null);
        Task<Fornecedor> EditarEndereco(EnderecoRequest endereco);
        Task<List<Produto>> ListarProdutos(int fornecedor_id);
        Task<List<Setor>> ObterSetores();
        Task<List<Fornecedor>> FiltarPorSetor(string setor);
        Task<List<Fornecedor>> FiltarPorNome(string nome);
    }
}
