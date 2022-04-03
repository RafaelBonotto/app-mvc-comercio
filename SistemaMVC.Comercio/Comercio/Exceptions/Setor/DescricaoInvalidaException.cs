using System;

namespace Comercio.Exceptions.Setor
{
    public class DescricaoInvalidaException : Exception
    {
        public DescricaoInvalidaException()
        {
        }
        public DescricaoInvalidaException(string mensagem) : base(mensagem)
        {
        }
    }
}
