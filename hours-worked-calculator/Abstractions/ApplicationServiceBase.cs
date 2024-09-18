using FluentValidation;

namespace Abstractions
{
    public abstract class ApplicationServiceBase
    {
        protected void ValidatePayload<TPayload, TValidator>(TPayload payload, TValidator validator)
            where TValidator : AbstractValidator<TPayload>
        {
            var result = validator.Validate(payload);

            if (!result.IsValid)
                foreach (var failure in result.Errors)
                    throw new Exception(failure.ErrorMessage);
        }
    }
}
