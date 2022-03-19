using System;

namespace Comercio.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel(){}
        public ErrorViewModel(string msg) => Mensagem = msg;

        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Mensagem { get; set; }
    }
}
