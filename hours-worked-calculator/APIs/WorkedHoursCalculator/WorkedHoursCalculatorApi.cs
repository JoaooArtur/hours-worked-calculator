using Asp.Versioning.Builder;

namespace APIs.WorkedHoursCalculator
{
    public static class WorkedHoursCalculatorApi
    {
        private const string BaseUrl = "/api/v{version:apiVersion}/worked-hours-calculator/";

        public static IVersionedEndpointRouteBuilder MapWorkedHoursCalculatorApiV1(this IVersionedEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

            group.MapPost("/calculate", ([AsParameters] Payloads.CalculateWorkedHours payload)
                => new ApplicationService().CalculateHoursWorked(payload));

            return builder;
        }
    }
}
