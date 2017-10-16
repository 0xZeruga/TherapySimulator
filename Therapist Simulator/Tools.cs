using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Therapist_Simulator
{
    public static class Tools
    {

        public static string GenerateName()
        {
            string name;
            List<string> AllNames = new List<string>();


            //TODO: Add more names.
            AllNames.Add("Lars");
            AllNames.Add("Fredrik");
            AllNames.Add("Hildur");


            Random r = new Random();
            var n = r.Next(AllNames.Count);
            name = AllNames.ElementAt(n);

            return name;
        }

        public static int GenerateAge()
        {
            Random r = new Random();
            return r.Next(16, 70);
        }
    }

    
}
