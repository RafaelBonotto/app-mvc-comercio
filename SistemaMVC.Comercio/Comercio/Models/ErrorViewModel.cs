using System;

namespace Comercio.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel(){}
        public ErrorViewModel(string msg, int statusCode)
        {
            Mensagem = msg;
            StatusCode = statusCode;
        }
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Mensagem { get; set; }
        public int StatusCode { get; set; }
    }
}
