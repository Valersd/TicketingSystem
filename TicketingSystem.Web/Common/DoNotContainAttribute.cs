using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketingSystem.Web.Common
{
    public class DoNotContainAttribute: ValidationAttribute, IClientValidatable
    {
        private string forbidden;

        public DoNotContainAttribute(string forbidden)
        {
            this.forbidden = forbidden;
        }

        public override bool IsValid(object propertyValue)
        {
            if (string.IsNullOrEmpty(this.forbidden))
            {
                return true;
            }

            if (propertyValue == null)
            {
                return true;
            }

            string propertyValueToLower = ((string)propertyValue).ToLower();

            if (propertyValueToLower.IndexOf(this.forbidden.ToLower()) >= 0 )
            {
                return false;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("{0} should not contains \"{1}\" !", name, this.forbidden);
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