using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Therapist_Simulator
{
    public class Therapist
    {
        public string Name { get; set; }
        public int Empathy { get; set; }
        public int ProblemSolving { get; set; }
        public int Listening { get; set; }

        private Patient ActivePatient;
        public static List<Patient> PatientList { get; set; }

        public float Money;

        public Therapist()
        {
            Empathy = 5;
            ProblemSolving = 5;
            Listening = 5;

            PatientList = new List<Patient>();

            PatientList = AddPatient(PatientList, new Patient());
            ActivePatient = PatientList.ElementAt(0);
        }

        private static List<Patient> AddPatient(List<Patient> pPatientList, Patient pPatient)
        {
            pPatientList.Add(pPatient);
            return pPatientList;
        }
        public static List<Patient> RemovePatient(List<Patient> pPatientList, Patient pPatient)
        {
            pPatientList.Add(pPatient);
            return pPatientList;
        }
        public static void AddSuspectedResult(Result pResult, Patient pPatient)
        {
            if(!pPatient.SuspectedResults.Contains(pResult))
            pPatient.SuspectedResults.Add(pResult);
        }
        public static void RemoveSuspectedResult(Result pResult, Patient pPatient)
        {
            if(pPatient.SuspectedResults.Contains(pResult))
            {
                pPatient.SuspectedResults.Remove(pResult);
            }
            else
            {
                return;
            }
        }

        public void StartSession()
        {
            ActivePatient = PatientList.First();
        }
    }

}
