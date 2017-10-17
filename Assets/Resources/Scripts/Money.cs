using Assets.Resources.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts
{
    public class Money : HUDbase
    {
        public Text MoneyText;

        public Money()
        {
            // MoneyText = GameObject.FindObjectOfType(MoneyText);
            MoneyText.text = "FUCK YEAH I'M RICH I'M THE BEST!";
           
        }
    }
}

