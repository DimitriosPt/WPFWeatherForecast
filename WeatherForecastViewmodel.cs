using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWeatherForecast
{
    public class WeatherForecastViewmodel
    {
        /// <summary>
        /// Creates a <see cref="WeatherForecastViewmodel\"/>.
        /// </summary>
        public WeatherForecastViewmodel()
        {
        }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public double Longitude { get; set; }

        public ObservableCollection<DailyForecast> DailyForecasts { get; set; } = new ObservableCollection<DailyForecast>();
    }
}
