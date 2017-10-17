using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static List<Result> AllResultsAsList()
        {
        List<Result> AllResult = new List<Result>();
        AllResult.Add(Result.CALL_THE_POLICE);
        AllResult.Add(Result.GROUP_THERAPY);
        AllResult.Add(Result.MEDITATION);
        AllResult.Add(Result.MENTAL_INSTITUTION);
        AllResult.Add(Result.PRESCRIPTION);
        AllResult.Add(Result.RESCHEDULE_MEETING);

        return AllResult;
        }

    }

    

