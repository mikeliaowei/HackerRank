using Facade.Entities;

namespace Facade.Services
{
    public class GeoLookupService
    {
        public City GetCityForZipCode(string zipCode)
        {
            return new City();
        }
        
        public State GetStateForZipCode(string zipCode)
        {
            return new State();
        }
    }
}