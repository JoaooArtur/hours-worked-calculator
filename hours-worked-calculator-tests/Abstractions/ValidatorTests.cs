using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using FluentValidation.TestHelper;
using FluentValidation;
using AutoFixture.Dsl;
using AutoFixture;
using AutoFixture.Kernel;

namespace Abstractions
{
    public abstract class ValidatorTests<TValidator, TRequest>
        where TValidator : AbstractValidator<TRequest>
    {
        protected TValidator ValidatorRoot;
        private Func<TestValidationResult<TRequest>>? _validation;
        protected Fixture _fixture;
        protected IPostprocessComposer<TRequest> _requestBuilder;

        protected ValidatorTests()
        {
            _fixture = new Fixture();
            _requestBuilder = _fixture.Build<TRequest>();
        }

        protected ValidatorTests<TValidator, TRequest> Given()
        {
            ValidatorRoot = _fixture.Create<TValidator>();

            return this;
        }

        public ValidatorTests<TValidator, TRequest> When(params Func<IPostprocessComposer<TRequest>, IPostprocessComposer<TRequest>>[] fields)
        {
            foreach (var field in fields)
                _requestBuilder = field.Invoke(_requestBuilder);

            _validation = () => ValidatorRoot.TestValidate(_requestBuilder.Create());

            return this;
        }

        public ValidatorTests<TValidator, TRequest> ThenShouldHaveErrorFor<TProperty>(params Expression<Func<TRequest, TProperty>>[] validations)
        {
            var result = _validation.Invoke();

            foreach (var validation in validations)
                result.ShouldHaveValidationErrorFor(validation);

            return this;
        }

        public ValidatorTests<TValidator, TRequest> ThenShouldHaveErrorWithCustomMessageFor<TProperty>(params (Expression<Func<TRequest, TProperty>> ValidationExpression, string ErrorMessage)[] validations)
        {
            var result = _validation.Invoke();

            foreach (var validation in validations)
                result.ShouldHaveValidationErrorFor(validation.ValidationExpression).WithErrorMessage(validation.ErrorMessage);

            return this;
        }

        public void ThenNothing()
        {
            var result = _validation.Invoke();

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}