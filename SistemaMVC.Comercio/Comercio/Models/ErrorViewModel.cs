namespace Comercio.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel(){}
        public ErrorViewModel(string msg) => Mensagem = msg;

        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Mensagem { get; set; }

        public ErrorViewModel ErroAoTentarCarregarPagina()
        {
            this.Mensagem = "Algo deu errado ao tentar carregar essa página.";
            return this;
        }

        public ErrorViewModel ErroFiltroNaoEncontrado()
        {
            this.Mensagem = "Não foram encontrados itens para esse filtro.";
            return this;
        }

        public ErrorViewModel ErroAoCarregarDetalhes()
        {
            this.Mensagem = "Algo deu errado ao tentar carregar a página de detalhes.";
            return this;
        }

        public ErrorViewModel ErroDeValidacao()
        {
            this.Mensagem = "Erro de validação, verifique se os valores dos campos de entrada são válidos.";
            return this;
        }

        public ErrorViewModel ProdutoErroCodigoInvalido()
        {
            this.Mensagem = "Não foi possível inserir ou atualizar o produto, foi encontrado um produto ativo com o mesmo código.";
            return this;
        }

        public ErrorViewModel ProdutoErroAoTentarInserir()
        {
            this.Mensagem = "Não foi possível inserir o produto.";
            return this;
        }

        public ErrorViewModel ProdutoErroAoTentarAtualizar()
        {
            this.Mensagem = "Não foi possível atualizar o produto.";
            return this;
        }

        public ErrorViewModel ProdutoErroAoTentarExcluir()
        {
            this.Mensagem = "Não foi possível excluir o produto.";
            return this;
        }

        public ErrorViewModel SetorErroAoTentarAtualizar()
        {
            this.Mensagem = "Não foi possível atualizar o setor.";
            return this;
        }

        public ErrorViewModel SetorErroAoTentarExcluir()
        {
            this.Mensagem = "Não foi possível excluir o setor.";
            return this;
        }
    }
}
