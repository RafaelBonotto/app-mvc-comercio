using System.ComponentModel.DataAnnotations;

namespace Comercio.Validations.Produtos
{
    public class PrecoValidacao : ValidationAttribute
    {
        public string Preco_custo { get; set; } 
        public string Preco_venda { get; set; } 
        public string GetErrorMessage() => $"Valor inválido para preço produto";

        protected override ValidationResult IsValid(object Value, ValidationContext validationContext)
        {
            var aux = (dynamic)validationContext.ObjectInstance;
            Preco_custo = aux.Preco_custo;
            Preco_venda= aux.Preco_venda;

            if (!this.ValidaPreco(Preco_custo))
                return new ValidationResult(GetErrorMessage());

            if (!this.ValidaPreco(Preco_venda))
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }
        public bool ValidaPreco(string preco) => double.TryParse(preco, out _);        
    }
}
