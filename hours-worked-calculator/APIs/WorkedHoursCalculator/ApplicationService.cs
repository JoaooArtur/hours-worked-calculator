using Abstractions;
using APIs.WorkedHoursCalculator.Validators;

namespace APIs.WorkedHoursCalculator
{
    public class ApplicationService : ApplicationServiceBase
    {
        public TimeSpan CalculateHoursWorked(Payloads.CalculateWorkedHours payload)
        {
            ValidatePayload(payload, new CalculateWorkedHoursValidator());

            var parsedStart = ConvertStringToTimeSpan(payload.Start);
            var parsedFinish = ConvertStringToTimeSpan(payload.Finish);

            if (parsedFinish < parsedStart) 
            {
                var aux = parsedFinish;
                parsedFinish = parsedStart;
                parsedStart = aux;
            }


            var hoursWorked =  parsedFinish - parsedStart;
            
            foreach (var @break in payload.Breaks)
            {
                var parsedBreakStart = ConvertStringToTimeSpan(@break.Start); 
                var parsedBreakFinish = ConvertStringToTimeSpan(@break.Finish);

                var breakDuration = parsedBreakFinish - parsedBreakStart;

                hoursWorked = hoursWorked - breakDuration;
            }

            return hoursWorked;
        }

        public TimeSpan ConvertStringToTimeSpan(string time) 
            => TimeSpan.Parse(time);
    }
}
