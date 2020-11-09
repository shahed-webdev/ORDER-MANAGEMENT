using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{

    public class DateGreaterThan : ValidationAttribute
    {
        private string _startDatePropertyName;
        public DateGreaterThan(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            if (propertyInfo == null)
            {
                return new ValidationResult(string.Format("Unknown property {0}", _startDatePropertyName));
            }
            var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);
            if (value == null)
            {
                return new ValidationResult(string.Format("Unknown property {0}", _startDatePropertyName));
            }
            if ((DateTime)value > (DateTime)propertyValue)
            {
                return ValidationResult.Success;
            }
            else
            {
                var startDateDisplayName = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().Single().Name;
                return new ValidationResult(validationContext.DisplayName + " must be greater than " + startDateDisplayName + ".");
            }
        }
    }
}