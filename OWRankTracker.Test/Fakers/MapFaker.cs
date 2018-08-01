using OWRankTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.Fakers
{
    class MapFaker
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
