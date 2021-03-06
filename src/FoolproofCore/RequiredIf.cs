﻿namespace FoolproofCore
{
    using System.Collections.Generic;
    using System.Linq;

    public class RequiredIfAttribute : ContingentValidationAttribute
    {
        public Operator Operator { get; }
        public object DependentValue { get; }
        protected OperatorMetadata Metadata { get; }
        
        public RequiredIfAttribute(string dependentProperty, Operator @operator, object dependentValue)
            : base(dependentProperty)
        {
            Operator = @operator;
            DependentValue = dependentValue;
            Metadata = OperatorMetadata.Get(Operator);
        }

        public RequiredIfAttribute(string dependentProperty, object dependentValue)
            : this(dependentProperty, Operator.EqualTo, dependentValue) { }

        public override string FormatErrorMessage(string name)
        {
            if (string.IsNullOrEmpty(ErrorMessageResourceName) && string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = DefaultErrorMessage;

            return string.Format(ErrorMessageString, name, DependentPropertyDisplayName ?? DependentProperty, DependentValue);
        }

        public override string ClientTypeName => "RequiredIf";

        protected override IEnumerable<KeyValuePair<string, object>> GetClientValidationParameters()
        {
            return base.GetClientValidationParameters()
                .Union(new[] {
                    new KeyValuePair<string, object>("Operator", Operator.ToString()),
                    new KeyValuePair<string, object>("DependentValue", DependentValue)
                });
        }

        public override bool IsValid(object value, object dependentValue, object container)
        {
            if (Metadata.IsValid(dependentValue, DependentValue))
                return !string.IsNullOrEmpty(value?.ToString().Trim());

            return true;
        }

        public override string DefaultErrorMessage => "{0} is required due to {1} being " + Metadata.ErrorMessage + " {2}";
    }
}
