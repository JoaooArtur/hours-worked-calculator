using Abstractions;
using APIs.WorkedHoursCalculator;
using APIs.WorkedHoursCalculator.Validators;
using System.ComponentModel.DataAnnotations;
using static APIs.WorkedHoursCalculator.Payloads;

namespace ValidatorTests.AutonomousTrainTests
{
    public class CalculateWorkedHoursValidatorTest : ValidatorTests<CalculateWorkedHoursValidator, Payloads.CalculateWorkedHours>
    {
        [Fact]
        public void Should_Have_Error_When_Start_Is_invalid()
        {
            Given()
                .When(query => query.With(x => x.Start, "AA:AA"))
                .ThenShouldHaveErrorWithCustomMessageFor((x => x.Start, "O horário de inicio deve estar no formato HH:mm ou HHmm."));
        }

        [Fact]
        public void Should_Have_Error_When_Start_Is_invalid_TimeSpan()
        {
            Given()
                .When(query => query.With(x => x.Start, "25:78"))
                .ThenShouldHaveErrorWithCustomMessageFor((x => x.Start, "O horário de inicio deve ser um horário valido."));
        }

        [Fact]
        public void Should_Have_Error_When_Start_Is_invalid_Not_Contains_Colon()
        {
            Given()
                .When(query => query.With(x => x.Start, "AAAA"))
                .ThenShouldHaveErrorWithCustomMessageFor((x => x.Start, "O horário de inicio deve estar no formato HH:mm ou HHmm."));
        }

        [Fact]
        public void Should_Have_Error_When_Finish_Is_invalid_Not_Contains_Colon()
        {
            Given()
                .When(query => query.With(x => x.Start, "08:00"))
                .When(query => query.With(x => x.Finish, "AAAA"))
                .ThenShouldHaveErrorWithCustomMessageFor((x => x.Finish, "O horário de termino deve estar no formato HH:mm ou HHmm."));
        }

        [Fact]
        public void Should_Have_Error_When_Finish_Is_invalid_TimeSpan()
        {
            Given()
                .When(query => query.With(x => x.Start, "08:00"))
                .When(query => query.With(x => x.Finish, "25:78"))
                .When(query => query.With(x => x.Breaks, new List<Break> { new("12:00","13:30") }))
                .ThenShouldHaveErrorWithCustomMessageFor((x => x.Finish, "O horário de termino deve ser um horário valido."));
        }

        [Fact]
        public void Should_Have_Error_When_Finish_Is_invalid()
        {
            Given()
                .When(query => query.With(x => x.Start, "08:00"))
                .When(query => query.With(x => x.Finish, "AA:AA"))
                .ThenShouldHaveErrorWithCustomMessageFor((x => x.Finish, "O horário de termino deve estar no formato HH:mm ou HHmm."));
        }

        [Fact]
        public void Should_Have_Error_When_Break_Is_invalid()
        {
            Given()
                .When(query => query.With(x => x.Start, "08:00"))
                .When(query => query.With(x => x.Finish, "18:00"))
                .When(query => query.With(x => x.Breaks, new List<Break> { new("AA:AA", "AA:AA") }))
                .ThenShouldHaveErrorWithCustomMessageFor((x => x.Breaks, "Os horários de intervalo devem estar no formato HH:mm ou HHmm."));
        }

        [Fact]
        public void Should_Not_Have_Error_When_Work_Is_Valid()
        {
            Given()
                .When(query => query.With(x => x.Start, "08:00"))
                .When(query => query.With(x => x.Finish, "18:00"))
                .When(query => query.With(x => x.Breaks, new List<Break> { new("12:00", "13:30") }))
                .ThenNothing();
        }
    }
}
