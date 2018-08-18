using OWRankTracker.Core.Model;
using System.Linq;

namespace OWRankTracker.Test.Fakers
{
    public class MapFaker
    {
        private static Bogus.Faker faker;

        static MapFaker()
        {
            faker = new Bogus.Faker();
        }

        public static string Random()
        {
            return faker.Random.ArrayElement(Maps.All);
        }
    }
}
