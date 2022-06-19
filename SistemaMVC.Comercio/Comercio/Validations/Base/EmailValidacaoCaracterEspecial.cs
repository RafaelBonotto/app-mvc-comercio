using System.ComponentModel.DataAnnotations;

namespace Comercio.Validations.Base
{
    public class EmailValidacaoCaracterEspecial : ValidationAttribute
    {
        public string Email { get; set; }
        public string GetErrorMessage() => $"Email Inválido: {Email}";

        protected override ValidationResult IsValid(object Value, ValidationContext validationContext)
        {
            var aux = (dynamic)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(aux.Email))
                return ValidationResult.Success;

            if (CaracterEspecial(aux.Email))
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        public bool CaracterEspecial(string email)
        {
            var caracterInvalidos = "!@#$%¨&*()-+=/*,?|~áéíóúãõàèìòùâêîôû[{]}äëïöü\"'< >^";
            foreach (var caracter in email)
                foreach (var charr in caracterInvalidos)
                    if (charr.Equals(caracter))
                        return true;
            return false;
        }
    }

}
