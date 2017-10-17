using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.UI
{

    /// <summary>
    /// TODO: 
    /// List of suspected conditions.
    /// List of result
    /// Money counter
    /// QuestionTypes.
    /// </summary>
    /// 
    

    public class HUDbase : MonoBehaviour
    {

        private SuspectedConditions SuspectedConditions;
        private Results Results;
        private Money Money;
        private QuestionPanel QuestionPanel;
        private Dictionary<Text, Toggle> TextChecker;

        public Vector3 m_Alignment;

        public HUDbase()
        {

            new Money();
        }

        protected void PushPop(GameObject pObj)
        {
            if(pObj.activeSelf == true) { pObj.SetActive(false); }
            else {pObj.SetActive(true);}
        }

        protected void FillTextChecker(TextChecker pTextChecker)
        {
            
        }
    }

    internal class QuestionPanel : HUDbase
    {

    }


    internal class Results : HUDbase
    {
        TextChecker TextChecker = new TextChecker();
        
        public Results()
        {
            FillTextChecker(TextChecker);
            
        }
        //TODO: Remember "ison" for toggle.


        /// <summary>
        /// Fills all TextCheckers and defaults them to false.
        /// </summary>
        /// <param name="pTextChecker"></param>
        private void FillTextChecker()
        {
            m_Alignment = new Vector3(0f, 0f, 0f);
            var res = Tools.AllResultsAsList();
            foreach(Result r in res)
            {
                var pTextChecker = new TextChecker();
                pTextChecker.Text.text = r.ToString();
                pTextChecker.Toggle.isOn = false;

                //Should cause text to appear slighty below eachother.
                m_Alignment = m_Alignment + new Vector3(0, -20f, 0);
                pTextChecker.Transform.position = m_Alignment;
            }

           
        }

    }

    internal class SuspectedConditions : HUDbase
    {
        TextChecker TextChecker = new TextChecker();
    }
    public class TextChecker : HUDbase
    {
        public Text Text;
        public Toggle Toggle;
        public Transform Transform;


        public TextChecker()
        {
            ///W.I.P
        }
        
    }


  
}
