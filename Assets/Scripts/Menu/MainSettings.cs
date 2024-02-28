using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class MainSettings : MainMenuController
    {
        public override void Start()
        {
            base.Start();
            Hide();
        }

        public void OnBack()
        {
            Hide();
            mainMenu.GetComponent<MainMenuController>().Show();
        }


    }
}