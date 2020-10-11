using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MvcExtensionsCore.ModelMetadata
{
    public static class ExpressionHelper
    {
        private static readonly ModelExpressionProvider ModelExpressionProvider = new ModelExpressionProvider(new EmptyModelMetadataProvider());

        public static string GetExpressionText<TEntity, TProperty>(this Expression<Func<TEntity, TProperty>> expression)
        {
            return ModelExpressionProvider.GetExpressionText(expression);
        }
    }
}
