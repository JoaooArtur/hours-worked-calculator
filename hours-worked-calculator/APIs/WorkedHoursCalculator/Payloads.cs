namespace APIs.WorkedHoursCalculator
{
    public static class Payloads
    {
        public record CalculateWorkedHours(string Start, string Finish, List<Break> Breaks);
        public record Break(string Start, string Finish);
    }
}
