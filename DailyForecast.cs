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

        /// <summary>
        /// Gets the day of the week the forecast is for.
        /// </summary>
        public DayOfWeek WeekDay { get; }

        /// <summary>
        /// Gets the low temp for the day.
        /// </summary>
        public double LowTemp { get; private set; }

        /// <summary>
        /// Gets the high temp for the day.
        /// </summary>
        public double HighTemp { get; private set; }

        /// <summary>
        /// Gets the weather condtion for the day.
        /// </summary>
        public WeatherCondition WeatherCondition { get; private set; }
    }
}