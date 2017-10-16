using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Therapist_Simulator.Question;

namespace Therapist_Simulator
{
    /// <summary>
    /// Depending on input data this class returns an AI response.
    /// </summary>
    public class Response
    {
        private Question Question;
        private QuestionType QuestionType;
        private Dictionary<string, int> Data;
        private List<Condition> Conditions;
        private Patient Patient;

        //Hostile, Neutral, Friendly
        //Different result depending on conditions.
        public Response(Patient pPatient, Question pQuestion)
        {
            //TODO: Not sure if question is needed.
            Question = pQuestion;
            QuestionType = pQuestion.questionType;
            Data = pPatient.PatientData;
            Conditions = pPatient.Conditions;
            Patient = pPatient;

            Initiate_AI_Loop(pQuestion);
        }
        /// <summary>
        /// This is where shit gets complex.
        /// </summary>
        private void Initiate_AI_Loop(Question pQuestion)
        {
            CheckExtremes(pQuestion);
            CheckQuestionType(QuestionType);

        }

        public void CheckExtremes(Question pQuestion)
        {
            if (Patient != null && pQuestion != null)
            {
                if (Data["Insanity"] <= 10)
                {
                    //TODO: Game over.
                }
                else if (Data["Trust"] >= 0)
                {
                    //Client leaves player.
                    Therapist.RemovePatient(Therapist.PatientList, Patient);
                }
                else if (Data["Aggression"] <= 10)
                {
                    //Client goes home for today. Put furthest back in PatientList.
                    Therapist.RemovePatient(Therapist.PatientList, Patient);
                    Therapist.PatientList.Add(Patient);
                    Patient.JustRaged = true;
                    Patient.TriggerCounter += 1;
                    Data["Aggression"] -= 5;
                    Data["Insanity"] += 1;
                }
                else if (Patient.JustRaged == true && !Patient.Conditions.Contains(Condition.PSYCHOPATHY))
                {
                    //Patient apologize for last weeks performance.
                    //TODO: Add dialogue.
                }
            }

        }
        public void CheckQuestionType(QuestionType pQuestionType)
        {
            int trust = Data["Trust"];

            switch (QuestionType)
            {
            
                case (QuestionType.ChildhoodTrauma):
                    {
                        TrustCheck(9,pQuestionType);
                        break;
                    }
                case (QuestionType.Duration):
                    {                      
                        break;
                    }
                case (QuestionType.FamilySituation):
                    {
                        TrustCheck(7,pQuestionType);
                        break;
                    }
                case (QuestionType.HaveYouTalkedToSomeone):
                    {
                        TrustCheck(4,pQuestionType);
                        break;
                    }
                case (QuestionType.HowDoYouFeel):
                    {
                        break;
                    }
                case (QuestionType.Relationship):
                    {
                        TrustCheck(8, pQuestionType);
                        break;
                    }
                case (QuestionType.Symptoms):
                    {
                        break;
                    }
                case (QuestionType.WorkSituation):
                    {
                        TrustCheck(6, pQuestionType);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("QuestionType doesn't exist");
                        break;
                    }
            }
        }

        public void TrustCheck(int pTrustThreshold, QuestionType pQuestionType)
        {
            if(Data["Trust"] > pTrustThreshold)
            {
                //TODO: Add more responses when trust isn't enough.
                SensitivityCheck(pQuestionType);
            }
            else
            {
                Console.WriteLine("I don't trust you enough to reveal that information yet");
                return;
            }
        }

        public void SensitivityCheck(QuestionType pQuestionType)
        {
            if(QuestionType == pQuestionType)
            {
                var s = Data["Sensitivity"];
                Random r = new Random();
                if (s <= r.Next(1, 10))
                {
                    Patient.TriggerCounter += 1;
                    switch (pQuestionType)
                    {
                        case (QuestionType.ChildhoodTrauma):
                            Console.WriteLine("You stay the fuck away from my past alright!");
                            Data["Aggression"] += 2;
                            break;
                        case (QuestionType.Relationship):
                            Console.WriteLine("My partner has absolutely nothing to do with this");
                            Data["Aggression"] += 2;
                            break;
                        case (QuestionType.WorkSituation):
                            Console.WriteLine("I have nothing to say about my work");
                            Data["Aggression"] += 1;
                            break;
                        case (QuestionType.FamilySituation):
                            Console.WriteLine("This concerns only me, not my family. Do you get that?");
                            Data["Aggression"] += 1;
                            break;
                        case (QuestionType.HaveYouTalkedToSomeone):
                            Console.WriteLine("Yes, I'm talking with you about it");
                            Data["Aggression"] += 1;
                            break;
                        default:
                            Console.WriteLine("Questiontype is null");
                            break;
                    }

                }
                else
                {
                    FinalResponse(Conditions, pQuestionType);
                }
            }
        }

        public void FinalResponse(List<Condition> pConditions, QuestionType pQuestionType)
        {
            var adhd = pConditions.Contains(Condition.ADHD);
            var anxiety = pConditions.Contains(Condition.ANXIETY);
            var autism = pConditions.Contains(Condition.AUTISM);
            var avoidant = pConditions.Contains(Condition.AVOIDANT);
            var bipolar = pConditions.Contains(Condition.BIPOLAR_DISORDER);
            var insomnia = pConditions.Contains(Condition.INSOMNIA);
            var paranoia = pConditions.Contains(Condition.PARANOIA);
            var psycho = pConditions.Contains(Condition.PSYCHOPATHY);
            var schiezo = pConditions.Contains(Condition.SCHIEZOFRENIA);
            var socio = pConditions.Contains(Condition.SOCIOPATHY);
            var stress = pConditions.Contains(Condition.STRESS);

            foreach (Condition c in pConditions)
            {
                SingleResponse(c, pQuestionType);
            }
            //TODO: Check conditions towards QuestionType. Pref as pair.





            Actions.SessionOver(Patient);
        }

        public string SingleResponse(Condition pCondition, QuestionType pQuestionType)
        {
            switch(pQuestionType)
            {
                case (QuestionType.ChildhoodTrauma):
                    return ChildhoodTrauma(pCondition);
                case (QuestionType.Duration):
                    return Duration(pCondition);
                case (QuestionType.FamilySituation):
                    return FamilySituation(pCondition);
                case (QuestionType.HaveYouTalkedToSomeone):
                    return HaveYouTalkedToSomeone(pCondition);
                case (QuestionType.HowDoYouFeel):
                    return HowDoYouFeel(pCondition);
                case (QuestionType.Relationship):
                    return Relationship(pCondition);
                case (QuestionType.Symptoms):
                    return Symptoms(pCondition);
                case (QuestionType.WorkSituation):
                    return WorkSituation(pCondition);
                default:
                    return "Wrong questiontype for Single Response";
            }
        }

        /// <summary>
        /// InBeforeTonsOfResponses
        /// TODO: Add string responses and effects to each.
        /// </summary>
        /// <param name="pCondition"></param>
        public string ChildhoodTrauma(Condition pCondition)
        {
            switch(pCondition)
            {
                case (Condition.ADHD):
                    return "Ever since I was child I had trouble concentrating, it's just recently I think it has gotten worse";
                case (Condition.ANXIETY):
                    return "Anxiety";
                case (Condition.AUTISM):
                    return "Autism";
                case (Condition.AVOIDANT):
                    return "Avoidant";
                case (Condition.BIPOLAR_DISORDER):
                    return "Bipolar";
                case (Condition.INSOMNIA):
                    return "Insomnia";
                case (Condition.PARANOIA):
                    return "Paranoia";
                case (Condition.PSYCHOPATHY):
                    return "Psycho";
                case (Condition.SCHIEZOFRENIA):
                    return "Schiezo";
                case (Condition.SOCIOPATHY):
                    return "Socio";
                case (Condition.STRESS):
                    return "Stress";
                default:
                    return "";
            }
        }
        public string Duration(Condition pCondition)
        {
            switch (pCondition)
            {
                case (Condition.ADHD):
                    return "Ever since I was child I had trouble concentrating, it's just recently I think it has gotten worse";
                case (Condition.ANXIETY):
                    return "Anxiety";
                case (Condition.AUTISM):
                    return "Autism";
                case (Condition.AVOIDANT):
                    return "Avoidant";
                case (Condition.BIPOLAR_DISORDER):
                    return "Bipolar";
                case (Condition.INSOMNIA):
                    return "Insomnia";
                case (Condition.PARANOIA):
                    return "Paranoia";
                case (Condition.PSYCHOPATHY):
                    return "Psycho";
                case (Condition.SCHIEZOFRENIA):
                    return "Schiezo";
                case (Condition.SOCIOPATHY):
                    return "Socio";
                case (Condition.STRESS):
                    return "Stress";
                default:
                    return "";
            }
        }
        public string FamilySituation(Condition pCondition)
        {
            switch (pCondition)
            {
                case (Condition.ADHD):
                    return "Ever since I was child I had trouble concentrating, it's just recently I think it has gotten worse";
                case (Condition.ANXIETY):
                    return "Anxiety";
                case (Condition.AUTISM):
                    return "Autism";
                case (Condition.AVOIDANT):
                    return "Avoidant";
                case (Condition.BIPOLAR_DISORDER):
                    return "Bipolar";
                case (Condition.INSOMNIA):
                    return "Insomnia";
                case (Condition.PARANOIA):
                    return "Paranoia";
                case (Condition.PSYCHOPATHY):
                    return "Psycho";
                case (Condition.SCHIEZOFRENIA):
                    return "Schiezo";
                case (Condition.SOCIOPATHY):
                    return "Socio";
                case (Condition.STRESS):
                    return "Stress";
                default:
                    return "";
            }
        }
        public string HaveYouTalkedToSomeone(Condition pCondition)
        {
            switch (pCondition)
            {
                case (Condition.ADHD):
                    return "Ever since I was child I had trouble concentrating, it's just recently I think it has gotten worse";
                case (Condition.ANXIETY):
                    return "Anxiety";
                case (Condition.AUTISM):
                    return "Autism";
                case (Condition.AVOIDANT):
                    return "Avoidant";
                case (Condition.BIPOLAR_DISORDER):
                    return "Bipolar";
                case (Condition.INSOMNIA):
                    return "Insomnia";
                case (Condition.PARANOIA):
                    return "Paranoia";
                case (Condition.PSYCHOPATHY):
                    return "Psycho";
                case (Condition.SCHIEZOFRENIA):
                    return "Schiezo";
                case (Condition.SOCIOPATHY):
                    return "Socio";
                case (Condition.STRESS):
                    return "Stress";
                default:
                    return "";
            }
        }
        public string HowDoYouFeel(Condition pCondition)
        {
            switch (pCondition)
            {
                case (Condition.ADHD):
                    return "Ever since I was child I had trouble concentrating, it's just recently I think it has gotten worse";
                case (Condition.ANXIETY):
                    return "Anxiety";
                case (Condition.AUTISM):
                    return "Autism";
                case (Condition.AVOIDANT):
                    return "Avoidant";
                case (Condition.BIPOLAR_DISORDER):
                    return "Bipolar";
                case (Condition.INSOMNIA):
                    return "Insomnia";
                case (Condition.PARANOIA):
                    return "Paranoia";
                case (Condition.PSYCHOPATHY):
                    return "Psycho";
                case (Condition.SCHIEZOFRENIA):
                    return "Schiezo";
                case (Condition.SOCIOPATHY):
                    return "Socio";
                case (Condition.STRESS):
                    return "Stress";
                default:
                    return "";
            }
        }
        public string Relationship(Condition pCondition)
        {
            switch (pCondition)
            {
                case (Condition.ADHD):
                    return "Ever since I was child I had trouble concentrating, it's just recently I think it has gotten worse";
                case (Condition.ANXIETY):
                    return "Anxiety";
                case (Condition.AUTISM):
                    return "Autism";
                case (Condition.AVOIDANT):
                    return "Avoidant";
                case (Condition.BIPOLAR_DISORDER):
                    return "Bipolar";
                case (Condition.INSOMNIA):
                    return "Insomnia";
                case (Condition.PARANOIA):
                    return "Paranoia";
                case (Condition.PSYCHOPATHY):
                    return "Psycho";
                case (Condition.SCHIEZOFRENIA):
                    return "Schiezo";
                case (Condition.SOCIOPATHY):
                    return "Socio";
                case (Condition.STRESS):
                    return "Stress";
                default:
                    return "";
            }
        }
        public string Symptoms(Condition pCondition)
        {
            switch (pCondition)
            {
                case (Condition.ADHD):
                    return "Ever since I was child I had trouble concentrating, it's just recently I think it has gotten worse";
                case (Condition.ANXIETY):
                    return "Anxiety";
                case (Condition.AUTISM):
                    return "Autism";
                case (Condition.AVOIDANT):
                    return "Avoidant";
                case (Condition.BIPOLAR_DISORDER):
                    return "Bipolar";
                case (Condition.INSOMNIA):
                    return "Insomnia";
                case (Condition.PARANOIA):
                    return "Paranoia";
                case (Condition.PSYCHOPATHY):
                    return "Psycho";
                case (Condition.SCHIEZOFRENIA):
                    return "Schiezo";
                case (Condition.SOCIOPATHY):
                    return "Socio";
                case (Condition.STRESS):
                    return "Stress";
                default:
                    return "";
            }
        }
        public string WorkSituation(Condition pCondition)
        {
            switch (pCondition)
            {
                case (Condition.ADHD):
                    return "Ever since I was child I had trouble concentrating, it's just recently I think it has gotten worse";
                case (Condition.ANXIETY):
                    return "Anxiety";
                case (Condition.AUTISM):
                    return "Autism";
                case (Condition.AVOIDANT):
                    return "Avoidant";
                case (Condition.BIPOLAR_DISORDER):
                    return "Bipolar";
                case (Condition.INSOMNIA):
                    return "Insomnia";
                case (Condition.PARANOIA):
                    return "Paranoia";
                case (Condition.PSYCHOPATHY):
                    return "Psycho";
                case (Condition.SCHIEZOFRENIA):
                    return "Schiezo";
                case (Condition.SOCIOPATHY):
                    return "Socio";
                case (Condition.STRESS):
                    return "Stress";
                default:
                    return "";
            }
        }

    }
}
