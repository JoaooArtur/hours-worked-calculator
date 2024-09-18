
using APIs.WorkedHoursCalculator;
using static APIs.WorkedHoursCalculator.Payloads;

namespace ApplicationServiceTests.HoursWorkedCalculatorTests
{
    public class ApplicationServiceTest
    {
        private ApplicationService GenerateAppService()
            => new ApplicationService();

        [Fact]
        public void Should_Not_Have_Error_When_Hours_Are_Valid()
        {
            var work = new Payloads.CalculateWorkedHours("08:30", "18:00", new List<Break> { new("12:00", "13:30") });

            Assert.IsType<TimeSpan>(GenerateAppService().CalculateHoursWorked(work));
        }
    }
}
