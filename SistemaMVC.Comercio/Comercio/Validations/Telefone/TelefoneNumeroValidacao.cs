using System.ComponentModel.DataAnnotations;

namespace Comercio.Validations.Telefone
{
    public class TelefoneNumeroValidacao : ValidationAttribute
    {
        public string Numero { get; set; }
        private string GetErrorMessage() => $"Número Inválido";

        protected override ValidationResult IsValid(object Value, ValidationContext validationContext)
        {
            var aux = (dynamic)validationContext.ObjectInstance;

            if (!ValidarNumero(aux.Numero))
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        public bool ValidarNumero(string numero)
        {
            if (string.IsNullOrEmpty(numero))
                return false;

            if (numero.Length < 8)
                return false;

            if (!numero.StartsWith("0800") && numero.Length > 9)
                return false;

            if(long.TryParse(numero.Length.ToString(), out _))
                return true;

            return false;
        }
    }
}
