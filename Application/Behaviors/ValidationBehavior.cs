using FluentValidation;
using FluentValidation.Results;
using LazZiya.ExpressLocalization;
using Mediator;

namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<
#nullable disable
            TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ISharedCultureLocalizer _localizer;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ISharedCultureLocalizer localizer)
        {
            _validators = validators;
            _localizer = localizer;
        }


        public ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
        {
            ValidationContext<TRequest> context = new(message);
            List<ValidationFailure> list = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null).ToList();
            if (list.Count > 0)
            {
                foreach (var failure in list)
                {
                    var value = _localizer.GetLocalizedString(failure.ErrorMessage);
                    failure.ErrorMessage = value;
                }
                throw new ValidationException(list);
            }

            return next(message, cancellationToken);
        }

    }
}