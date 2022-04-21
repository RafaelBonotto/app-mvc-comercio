using System;

namespace Comercio.Exceptions.Fornecedor
{
    public class CnpjInvalidoException : Exception
    {
        public CnpjInvalidoException()
        {
        }
        public CnpjInvalidoException(string mensagem) : base(mensagem)
        {
        }
    }
}
