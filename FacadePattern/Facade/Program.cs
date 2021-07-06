using System;
using Facade.Entities;
using Facade.Services;

namespace Facade
{
    public static class Program
    {
        static void Main(string[] args)
        {
            const string zipCode = "98074";

            //// call to service 1
            //GeoLookupService geoLookupService = new GeoLookupService();
            //City city = geoLookupService.GetCityForZipCode(zipCode);
            //State state = geoLookupService.GetStateForZipCode(zipCode);

            //// call to service 2
            //WeatherService weatherService = new WeatherService();
            //int fahrenheit = weatherService.GetTempFahrenheit(city, state);

            //// call to service 3
            //ConverterService metricConverter = new ConverterService();
            //int celcius = metricConverter.ConvertFahrenheitToCelcious(fahrenheit);

            //// bring the result of all service calls together
            //Console.WriteLine("The current temperature is {0} F / {1} C in {2}, {3}",
            //                    fahrenheit,
            //                    celcius,
            //                    city.Name,
            //                    state.Name);

            IWeatherFacade weatherFacade = new WeatherFacade();

            WeatherFacadeResults results = weatherFacade.GetTempInCity(zipCode);

			Console.WriteLine("The current temperature is {0} F / {1} C in {2}, {3}",
                                results.Fahrenheit,
                                results.Celsius,
                                results.City.Name,
                                results.State.Name);

		}
    }
}