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
    }
}
