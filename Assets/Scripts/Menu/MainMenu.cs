using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class MainMenu : MainMenuController
    {
        public override void Start()
        {
            base.Start();
            Show();
        }
        public void OnStart()
        {
            Hide();
            characterSelector.GetComponent<MainMenuController>().Show();
        }

        public void ShowSettings()
        {
            Hide();
            settings.GetComponent<MainMenuController>().Show();
        }
        public void OnQuit()
        {
            Application.Quit();
        }
    }
}
