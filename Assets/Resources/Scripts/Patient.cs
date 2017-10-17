using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Patient
    {
        private string m_Name = "";
        private int m_Age;
        public Dictionary<string, int> PatientData = new Dictionary<string, int>();

        public int SessionQuestions;

        private bool m_JustRaged;
        public bool JustRaged
        {
            get
            {
                return m_JustRaged;
            }
            set
            {
                m_JustRaged = value;
            }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        //Counts the amount of times the Player overstepped their boundries.
        public int TriggerCounter;

        public List<Condition> Conditions;
        public List<Result> CorrectResults;
        public List<Result> SuspectedResults { get; set; }

        public Patient()
        {
            m_Name = Tools.GenerateName();
            m_Age = Tools.GenerateAge();
            Random r = new Random();

            Conditions = RandomizeConditions();
            CorrectResults = GetCorrectResults(Conditions);

            SuspectedResults = new List<Result>();

            int min = 1;
            int max = 9;
          
            PatientData.Add("Trust", r.Next(min,max));
            PatientData.Add("Sensitivity", r.Next(min, max));
            PatientData.Add("Insanity", r.Next(min, max));
            PatientData.Add("Aggression", r.Next(min, max));

            ConditionalStats();


            TriggerCounter = 0;

            //Test
            foreach(Result re in CorrectResults)
            {
                Console.WriteLine(re.ToString());
            }
            Console.WriteLine(PatientData["Trust"]);
            Console.WriteLine(PatientData["Sensitivity"]);
            Console.WriteLine(PatientData["Insanity"]);
            Console.WriteLine(PatientData["Aggression"]);
            SessionQuestions = 5;

            JustRaged = false;
        }

        //TODO: Delegate or something to chec when PatientData changes.
        public void ConditionalStats()
        {
            Random r = new Random();

            if (Conditions.Contains(Condition.PSYCHOPATHY))
            {
                PatientData["Trust"] = r.Next(1, 3);
                PatientData["Sensitivity"] = r.Next(1, 2);
                PatientData["Insanity"] = r.Next(5, 9);
                PatientData["Aggression"] = r.Next(5, 9);
            }
            if (Conditions.Contains(Condition.BIPOLAR_DISORDER))
            {
                PatientData["Insanity"] += 1;
                PatientData["Sensitivity"] = r.Next(6, 9);
            }
            if(Conditions.Contains(Condition.PARANOIA))
            {
                PatientData["Insanity"] += 2;
                PatientData["Aggression"] += 1;
            }
            if(Conditions.Contains(Condition.SOCIOPATHY))
            {
                PatientData["Sensitivity"] -= 3;
            }
        }

        //TODO: Remake this shit, it looks aweful.
        public static void PatientDataMinMax(Dictionary<string,int> pPatientData)
        {
            var t = pPatientData["Trust"];
            var s = pPatientData["Sensitivity"];
            var i = pPatientData["Insanity"];
            var a = pPatientData["Agression"];
            if (t < 1) { t = 1; }
            if (t > 10) { t = 10; }
            if (s < 1) { s = 1; }
            if (s > 10) { s = 10; }
            if (i < 1) { i = 1; }
            if (i > 10) { i = 10; }
            if (a < 1) { a = 1; }
            if (a > 10) { a = 10; }

        }


        public List<Condition> AllConditions = Enum.GetValues(typeof(Condition))
                                        .Cast<Condition>()
                                        .ToList();


        public bool ThreshHoldReached(int pStat, int questionThreshHold)
        {
            bool h;
            h = questionThreshHold <= pStat ? false : true;
            return h;
        }

        /// <summary>
        /// Randomizes 1-3 Random conditions for the patient. 
        /// </summary>
        /// <returns></returns>
        public List<Condition> RandomizeConditions()
        {
            var c = new List<Condition>();
            Random r = new Random();
            var CondAmount = r.Next(3) + 1;
            for (int i = 0; i < CondAmount; i++)
            {
                //10 is max number of condition
                var q = r.Next(AllConditions.Capacity);
                c.Add(AllConditions.ElementAt(q));
            }
            return c;
        }

        /// <summary>
        /// Checks correct results depending on Patient conditions.
        /// </summary>
        /// <param name="pConditions"></param>
        /// <returns></returns>
        public static List<Result> GetCorrectResults(List<Condition> pConditions)
        {
            //TODO: Compare Patient conditions to Result.
            //TODO: Point calculation depending on stats and correct Result.
            var results = new List<Result>();
            if (pConditions.Count != 0)
            {
                foreach (Condition c in pConditions)
                {
                    if (c == Condition.ADHD || c == Condition.ANXIETY || c == Condition.BIPOLAR_DISORDER || c == Condition.INSOMNIA)
                    {
                        if (results.Contains(Result.PRESCRIPTION))
                        {
                            break;
                        }
                        results.Add(Result.PRESCRIPTION);
                    }
                    else if (c == Condition.AUTISM || c == Condition.AVOIDANT || c == Condition.PARANOIA)
                    {
                        if (results.Contains(Result.GROUP_THERAPY))
                        {
                            break;
                        }
                        results.Add(Result.GROUP_THERAPY);
                    }
                    else if (c == Condition.STRESS)
                    {
                        if (results.Contains(Result.MEDITATION))
                        {
                            break;
                        }
                        results.Add(Result.MEDITATION);
                    }
                    else if (c == Condition.SCHIEZOFRENIA || c == Condition.SOCIOPATHY)
                    {
                        if (results.Contains(Result.MENTAL_INSTITUTION))
                        {
                            break;
                        }
                        results.Add(Result.MENTAL_INSTITUTION);
                    }
                    else if (c == Condition.PSYCHOPATHY)
                    {
                        if (results.Contains(Result.CALL_THE_POLICE))
                        {
                            break;
                        }
                        results.Add(Result.CALL_THE_POLICE);
                    }
                    else
                    {
                        if (results.Contains(Result.RESCHEDULE_MEETING))
                        {
                            break;
                        }
                        results.Add(Result.RESCHEDULE_MEETING);
                    }

                }

            }
            return results;
        }


    }

    public class Question
    {
        public QuestionType questionType;
        int ThreshHold;

        private string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public Question(int pThreshHold, QuestionType pQuestionType )
        {
            questionType = pQuestionType;
            ThreshHold = pThreshHold;
        }

        //Expand this.
        public enum QuestionType
        {

            Symptoms,
            Duration,
            HowDoYouFeel,
            HaveYouTalkedToSomeone,

            //Require trust 8
            ChildhoodTrauma,

            //Require trust 6
            FamilySituation,

            //Require trust 5
            WorkSituation,

            //Require trust 7
            Relationship,


        }

        
    }

    //TODO: Add more dialogue for insanity
    //TODO: Change stats depending on conditions.

    //Expand this.
    public enum Condition
    {
        ANXIETY,
        STRESS,
        PARANOIA,
        SCHIEZOFRENIA,
        PSYCHOPATHY,
        BIPOLAR_DISORDER,
        SOCIOPATHY,
        AUTISM,
        INSOMNIA,
        ADHD,
        AVOIDANT,

    }

    /// <summary>
    /// TODO: Add rating to results.
    /// </summary>
    public enum Result
    {
        PRESCRIPTION,
        //new meetings can be rescheuduled for ever.
        RESCHEDULE_MEETING,
        CALL_THE_POLICE,
        MENTAL_INSTITUTION,
        GROUP_THERAPY,
        MEDITATION,

    }



    //TODO
    //Introtext for each condition



















