namespace WPFWeatherForecast
{
    public class DailyForecast
    {
        public DailyForecast(DayOfWeek weekday, double lowTemp, double highTemp, WeatherCondition weatherCondition)
        {
            this.WeekDay = weekday;
            this.LowTemp = lowTemp;
            this.HighTemp = highTemp;
            this.WeatherCondition = weatherCondition;
        }

        public DayOfWeek WeekDay { get; }
        public double LowTemp { get; private set; }
        public double HighTemp { get; private set; }
        public WeatherCondition WeatherCondition { get; private set; }
    }
}