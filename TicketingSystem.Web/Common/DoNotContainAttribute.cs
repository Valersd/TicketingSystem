using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TicketingSystem.Common;

namespace TicketingSystem.Web.Common
{
    public class DoNotContainAttribute: ValidationAttribute, IClientValidatable
    {
        private string forbidden;
        private const string DefaultErrorMessage = GlobalConstants.DoNotContain;

        public DoNotContainAttribute(string forbidden)
            :base(DefaultErrorMessage)
        {
            this.forbidden = forbidden;
        }


        protected override ValidationResult IsValid(object propertyValue, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.forbidden))
            {
                return ValidationResult.Success;
            }

            if (propertyValue == null)
            {
                return ValidationResult.Success;
            }

            string propertyValueToLower = ((string)propertyValue).ToLower();

            if (propertyValueToLower.IndexOf(this.forbidden.ToLower()) >= 0)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }
            return ValidationResult.Success;
        }

        //public override bool IsValid(object propertyValue)
        //{
        //    if (string.IsNullOrEmpty(this.forbidden))
        //    {
        //        return true;
        //    }

        //    if (propertyValue == null)
        //    {
        //        return true;
        //    }

        //    string propertyValueToLower = ((string)propertyValue).ToLower();

        //    if (propertyValueToLower.IndexOf(this.forbidden.ToLower()) >= 0 )
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        public override string FormatErrorMessage(string name)
        {
            return String.Format(this.ErrorMessageString, name, this.forbidden);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule();
            rule.ValidationType = "donotcontain";
            rule.ErrorMessage = this.FormatErrorMessage(metadata.PropertyName);
            rule.ValidationParameters.Add("forbidden", this.forbidden);
            yield return rule;
        }
    }
}