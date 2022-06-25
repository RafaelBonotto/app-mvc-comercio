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
            return false;
        }
    }
}
