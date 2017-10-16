using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Therapist_Simulator
{
    public class Actions
    {
        private List<Condition> SuspectedCondition = new List<Condition>();

        /// <summary>
        /// For when Player suspect patient having a condition.
        /// </summary>
        /// <param name="pSusConditions"></param>
        /// <param name="pCondition"></param>
        public static void AddSuspectCondition(List<Condition> pSusConditions, Condition pCondition)
        {
            pSusConditions.Add(pCondition);
        }
        public static void RemoveSuspectCondition(List<Condition> pSusConditions, Condition pCondition)
        {
            pSusConditions.Remove(pCondition);
        }


        public static void Investigate(Patient pPatient)
        {
            if(pPatient.PatientData["Trust"] <= 5)
            {

                //Reveal one hidden statistic among the chosen ones.
                //Requires trust 5,10,15,20
                Random r = new Random();
                var a = r.Next(pPatient.PatientData.Keys.Count);
                var b = pPatient.PatientData.Keys.ElementAt(a);
                //TODO: Display b visually.
                Console.WriteLine(b);
            }
            else
            {
                Console.WriteLine("I do not trust you enough to peek into my personal life like that");
            }
            pPatient.SessionQuestions -= 1;
        }

        //If client responds well to player.
        public static void GainTrust(Patient pPatient, int plus)
        {
            //pPatient gain increased trust.
            pPatient.PatientData["Trust"] += plus;
        }

        //if client responds bad to player.
        public static void LoseTrust(Patient pPatient, int minus)
        {
            pPatient.PatientData["Trust"] -= minus;

        }
        public static Response AskQuestion(Patient pPatient, Question pQuestion)
        {
            //Depending on pPatient conditions and question asked response may vary.
            //GenereteResponse();
            var response = new Response(pPatient, pQuestion);
            pPatient.SessionQuestions -= 1;
            
            return response;
        }

        public static void SessionOver(Patient pPatient)
        {
            if(pPatient.SessionQuestions != 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("Session with " + pPatient.Name + " is over");
                //Add experience.
                //TODO: Determine verdict among Results.
                
            }

        }

        //Compare correct results to suspected ones.
        public static int CompareResult(List<Result> pSuspected, List<Result> pCorrect)
        {
            var results = pSuspected.Intersect(pCorrect);
            return results.ToList().Count;
               
        }

        //Always called at end of session unless "Rescheduele happens". 
        public static float CalculatePoints(Patient pPatient)
        {
            var t = pPatient.PatientData["Trust"];
            var i = pPatient.PatientData["Insanity"];
            var a = pPatient.PatientData["Aggression"];

            float score = t - ((i + a) / 2f);
            score -= pPatient.TriggerCounter + CompareResult(pPatient.SuspectedResults, pPatient.CorrectResults);

            return score;
        }
    }
}
