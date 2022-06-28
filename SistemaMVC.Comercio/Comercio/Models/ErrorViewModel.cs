using System.Collections.Generic;
using System.Text;

namespace Comercio.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel() { }
        public ErrorViewModel(string msg) => Mensagem = msg;

        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Mensagem { get; set; }

        public ErrorViewModel ErroAoTentarCarregarPagina()
        {
            this.Mensagem = "Algo deu errado ao tentar carregar essa p�gina.";
            return this;
        }

        public ErrorViewModel ErroFiltroNaoEncontrado()
        {
            this.Mensagem = "N�o foram encontrados itens para esse filtro.";
            return this;
        }

        public ErrorViewModel ErroAoCarregarDetalhes()
        {
            this.Mensagem = "Algo deu errado ao tentar carregar a p�gina de detalhes.";
            return this;
        }

        public ErrorViewModel ErroDeValidacao()
        {
            this.Mensagem = "Erro de valida��o, verifique se os valores dos campos de entrada s�o v�lidos.";
            return this;
        }

        public ErrorViewModel ErroDeValidacao(List<string> erros)
        {
            StringBuilder msg = new();
            foreach (var erro in erros)
            {
                msg.Append(erro);
                if (erro != erros[erros.Count - 1])
                    msg.Append(", ");
            }
            this.Mensagem = $"Erro de valida��o, campo(s) inv�lido(s): {msg}";
            return this;
        }

        public ErrorViewModel ProdutoErroCodigoInvalido()
        {
            this.Mensagem = "N�o foi poss�vel inserir ou atualizar o produto, foi encontrado um produto ativo com o mesmo c�digo.";
            return this;
        }

        public ErrorViewModel ProdutoErroAoTentarInserir()
        {
            this.Mensagem = "N�o foi poss�vel inserir o produto.";
            return this;
        }

        public ErrorViewModel ProdutoErroAoTentarAtualizar()
        {
            this.Mensagem = "N�o foi poss�vel atualizar o produto.";
            return this;
        }

        public ErrorViewModel ProdutoErroAoTentarExcluir()
        {
            this.Mensagem = "N�o foi poss�vel excluir o produto.";
            return this;
        }

        public ErrorViewModel SetorErroAoTentarAtualizar()
        {
            this.Mensagem = "N�o foi poss�vel atualizar o setor.";
            return this;
        }

        public ErrorViewModel SetorErroAoTentarExcluir()
        {
            this.Mensagem = "N�o foi poss�vel excluir o setor.";
            return this;
        }

        public ErrorViewModel SetorErroAoTentarInserir()
        {
            this.Mensagem = "N�o foi poss�vel inserir o setor.";
            return this;
        }

        public ErrorViewModel SetorErroInserirDescricaoInvalida()
        {
            this.Mensagem = "N�o foi poss�vel inseir o setor. Foi encontrado um setor ativo com a mesma descri��o.";
            return this;
        }

        public ErrorViewModel ProdutoFornecedorNaoEncontrado()
        {
            this.Mensagem = "N�o foi encontrado nenhum fornecedor para esse produto.";
            return this;
        }

        public ErrorViewModel FornecedorErroInserirCnpjInvalido()
        {
            this.Mensagem = "N�o foi poss�vel inseir o fornecedor. Foi encontrado um fornecedor ativo com o mesmo CNPJ.";
            return this;
        }

        public ErrorViewModel FornecedorErroAoTentarInserir()
        {
            this.Mensagem = "Erro ao tentar inserir o vendedor.";
            return this;
        }

        public ErrorViewModel FornecedorErroAoTentarAtualizarNomeEmail()
        {
            this.Mensagem = "Erro ao tentar atualizar o nome e o email do fornecedor.";
            return this;
        }

        public ErrorViewModel FornecedorErroAoTentarInserirTelefone()
        {
            this.Mensagem = "Erro ao tentar inserir o telefone do fornecedor.";
            return this;
        }
        
        public ErrorViewModel FornecedorErroAoTentarCarregarTelefone()
        {
            this.Mensagem = "Erro ao tentar carregar o telefone do fornecedor.";
            return this;
        }

        public ErrorViewModel FornecedorErroAoTentarEditarTelefone()
        {
            this.Mensagem = "Erro ao tentar editar o telefone do fornecedor.";
            return this;
        }

        public ErrorViewModel FornecedorErroAoTentarExcluirTelefone()
        {
            this.Mensagem = "Erro ao tentar excluir o telefone do fornecedor.";
            return this;
        }
        public ErrorViewModel FornecedorErroAoTentarInserirEndereco()
        {
            this.Mensagem = "Erro ao tentar inserir endere�o do fornecedor.";
            return this;
        }

        public ErrorViewModel FornecedorErroAoTentarCarregarEndereco()
        {
            this.Mensagem = "Erro ao tentar carregar endere�o do fornecedor.";
            return this;
        }

    }
}
