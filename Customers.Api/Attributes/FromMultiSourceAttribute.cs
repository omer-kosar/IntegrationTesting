﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Customers.Api.Attributes
{
    public sealed class FromMultiSourceAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource? BindingSource => CompositeBindingSource.Create(new[] { BindingSource.Path, BindingSource.Query }, nameof(FromMultiSourceAttribute));
    }
}
