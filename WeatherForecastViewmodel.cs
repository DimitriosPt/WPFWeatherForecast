using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWeatherForecast
{
    public class WeatherForecastViewmodel : INotifyPropertyChanged
    {
        private WebWeatherAggregator webAggregator;
        private double latitude;

        private double longitude;

        /// <summary>
        /// Creates a <see cref="WeatherForecastViewmodel\"/>.
        /// </summary>
        public WeatherForecastViewmodel()
        {
            this.webAggregator = new WebWeatherAggregator();
        }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public double Latitude
        {
            get
            {
                return this.latitude;
            }

            set
            {
                this.latitude = value;

                #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                this.GetNewForecastAsync();
                #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                this.OnPropertyChanged(nameof(this.Latitude));
            }
        }

        private async Task GetNewForecastAsync()
        {
            var dailyForecast = await this.webAggregator.GetDailyForecastAsync(this.Latitude, this.Longitude);

            this.DailyForecasts.Clear();

            this.DailyForecasts.Add(dailyForecast);
        }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public double Longitude
        {
            get
            {
                return this.longitude;
            }

            set
            {
                this.longitude = value;
                
                #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                this.GetNewForecastAsync();
                #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                this.OnPropertyChanged(nameof(this.Latitude));
            }
        }

        /// <summary>
        /// Gets the collection of daily forecasts.
        /// </summary>
        public ObservableCollection<DailyForecast> DailyForecasts { get; } = new ObservableCollection<DailyForecast>();

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Informs the UI of changes to a property.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
