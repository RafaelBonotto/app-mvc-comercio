using System;

namespace Comercio.Exceptions.Produto
{
    public class CodigoInvalidoException : Exception
    {
        public string Mensagem { get; set; }

        public CodigoInvalidoException()
        {
            Mensagem = "Código inválido!!!";
        }
        public CodigoInvalidoException(string mensagem) : base(mensagem)
        {
           
        }
    }
}
