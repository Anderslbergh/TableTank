using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Menu
{
    public class PauseSettings : PauseMenuController
    {
        private GameSpeedController gameSpeedController;

        public override void Start()
        {
            base.Start();
            Hide();
        }

        public void OnBack()
        {
            Hide();
            mainMenu.GetComponent<PauseMenuController>().Show();
        }

        private void Awake()
        {
            gameSpeedController = FindObjectOfType<Controllers.GameSpeedController>();
        }

        public void onChangeGameSpeed(int i)
        {
            switch (i)
            {
                case (0):
                    gameSpeedController.SetSettingsTimeScale(GameSpeedController.TimeScale.Normal);
                    break;
                case (1):
                    gameSpeedController.SetSettingsTimeScale(GameSpeedController.TimeScale.Miniature);
                    break;
            }
        }
    }
}