

namespace WPFWeatherForecast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Formatting;
    using System.Security.Policy;
    using Newtonsoft.Json;
    using WeatherDataTypes.JsonResponseObjects;

    /// <summary>
    /// A class that handles the retrieval of weather data from a web api.
    /// </summary>
    public class WebWeatherAggregator
    {
        private readonly string weatherStationDefaultEndpoint = "https://api.weather.gov/points";
        public WebWeatherAggregator()
        {
            this.Client = new HttpClient();
            this.Client.BaseAddress = new Uri(weatherStationDefaultEndpoint);

            // Add an Accept header for JSON format.
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Add a user agent so the weather service can contact me in case of odd behaviour.
            var productValue = new ProductInfoHeaderValue("WPFWeatherForecaster", "1.0");
            //var commentValue = new ProductInfoHeaderValue("ptdimitrios@gmail.com");

            this.Client.DefaultRequestHeaders.UserAgent.Add(productValue);
            //this.Client.DefaultRequestHeaders.UserAgent.Add(commentValue);
        }

        public HttpClient Client { get; private set; }

        /// <summary>
        /// Gets the daily forecast.
        /// </summary>
        /// <param name="latitude">The latitudinal coordinate.</param>
        /// <param name="longitude">The longitudinal coordinate.</param>
        /// <returns>The first daily forecast.</returns>
        public async Task<Root> GetForecastRootResponse(double latitude, double longitude)
        {
            // https://api.weather.gov/points/{lat},{lon}
            string dailyForecastEndpoint = $"https://api.weather.gov/points/{latitude},{longitude}";

            var response = await this.Client.GetAsync(dailyForecastEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var rootResponse = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());

                return rootResponse;
            }

            return null;
        }

        public async Task<DailyForecast> GetDailyForecastAsync(double latitude, double longitude)
        {
            var root = await GetForecastRootResponse(latitude, longitude);

            if (root != null)
            {
                var dailyForecastEndpoint = root.Properties.Forecast;

                var response = await this.Client.GetAsync(dailyForecastEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    // Log the JSON response
                    Console.WriteLine("JSON Response: " + json);

                    try
                    {
                        var content = JsonConvert.DeserializeObject<DailyForecastResponse>(json);

                        if (content?.Properties?.Periods != null && content.Properties.Periods.Any())
                        {
                            var forecast = content.Properties.Periods.First();
                            var dailyForecast = new DailyForecast(DateTime.Today.DayOfWeek, forecast.Temperature, forecast.Temperature, forecast.DetailedForecast);

                            return dailyForecast;
                        }
                    }
                    catch (JsonSerializationException ex)
                    {
                        // Log the deserialization error
                        Console.WriteLine($"Deserialization error: {ex.Message}");
                    }
                }

                return null;

            }

            return null;

        }

    }




}
