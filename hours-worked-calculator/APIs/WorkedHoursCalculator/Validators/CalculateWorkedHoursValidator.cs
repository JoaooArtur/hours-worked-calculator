using FluentValidation;

namespace APIs.WorkedHoursCalculator.Validators
{
    public class CalculateWorkedHoursValidator : AbstractValidator<Payloads.CalculateWorkedHours>
    {
        public CalculateWorkedHoursValidator()
        {
            RuleFor(payload => payload.Start)
                .Must(BeAValidFormat).WithMessage("O horário de inicio deve estar no formato HH:mm ou HHmm.")
                .Must(BeAValidTime).WithMessage("O horário de inicio deve ser um horário valido.");

            RuleFor(payload => payload.Finish)
                .Must(BeAValidFormat).WithMessage("O horário de termino deve estar no formato HH:mm ou HHmm.")
                .Must(BeAValidTime).WithMessage("O horário de termino deve ser um horário valido.");

            RuleForEach(payload => payload.Breaks)
                .Must((payload, breaks) => BeAValidFormat(breaks.Start) && BeAValidFormat(breaks.Finish))
                .WithMessage("Os horários de intervalo devem estar no formato HH:mm ou HHmm.")
                .Must((payload, breaks) => BeAValidTime(breaks.Start) && BeAValidTime(breaks.Finish))
                .WithMessage("Os horários de intervalo devem ser horários validos.");
        }

        protected static bool BeAValidTime(string time)
        {
            TimeSpan parsedTime;
            if(time.Length == 4)
                time.Insert(2, ":");

            if (!TimeSpan.TryParse(time, out parsedTime))
                return false;

            return true;
        }

        protected static bool BeAValidFormat(string time)
        {
            if (time.Length == 4 && int.TryParse(time, out _))
                return true;
            else 
            {
                if (time.Length == 5 && time.Contains(':'))
                {
                    string[] parts = time.Split(':');

                    if (!int.TryParse(parts[0], out _) || !int.TryParse(parts[1], out _))
                        return false;

                    return true;
                }
                return false;
            }
        }
    }
}
