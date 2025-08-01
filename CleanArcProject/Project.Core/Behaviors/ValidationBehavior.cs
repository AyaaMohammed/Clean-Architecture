﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Project.Core.Rescources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Behaviors
{
        public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
               where TRequest : IRequest<TResponse>
            {
            private readonly IEnumerable<IValidator<TRequest>> _validators;
            IStringLocalizer<SharedRescources> _stringLocalizer;

            public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IStringLocalizer<SharedRescources> _stringLocalizer)
            {
                _validators = validators;
                _stringLocalizer = _stringLocalizer;
        }

            public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
            {
                if (_validators.Any())
                {
                    var context = new ValidationContext<TRequest>(request);
                    var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                    var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                    if (failures.Count != 0)
                    {
                        var message = failures.Select(x => _stringLocalizer[x.PropertyName] + ": " + _stringLocalizer[x.ErrorMessage]).FirstOrDefault();

                        throw new ValidationException(message);

                    }
                }
                return await next();
            }
    }
}
