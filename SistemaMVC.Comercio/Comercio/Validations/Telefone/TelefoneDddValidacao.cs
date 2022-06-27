using System.ComponentModel.DataAnnotations;

namespace Comercio.Validations.Telefone
{
    public class TelefoneDddValidacao : ValidationAttribute
    {
        public string Ddd { get; set; }
        private string GetErrorMessage() => $"DDD Inválido";

        protected override ValidationResult IsValid(object Value, ValidationContext validationContext)
        {
            var aux = (dynamic)validationContext.ObjectInstance;

            if (!ValidarDdd(aux.Ddd))
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        public bool ValidarDdd(string ddd)
        {
            if (string.IsNullOrEmpty(ddd))
                return false;

            if (ddd.Length > 3)
                return false;

            if (int.TryParse(ddd, out _))
                return true;

            return false;
        }
    }
}
