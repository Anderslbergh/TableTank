using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class PauseMain : PauseMenuController
    {

        [SerializeField] Image backgroundImage;
        private float bgColorAlpha;
        [SerializeField] private float currentBackgroundAplha;
        private float bgColorAlphaSmoothTime = 0.2f;
        private float bgColorAlphaVel;

        bool isShown = false;
        private GameSpeedController gameSpeedController;

        public override void Start()
        {
            base.Start();
            Hide();
        }

        private void Awake()
        {
            gameSpeedController = FindObjectOfType<Controllers.GameSpeedController>();
        }

        public void OnPause()
        {
            if (isShown)
            {
                Disappear();
                this.Hide();
                isShown = false;
                gameSpeedController.ResumeTimeScale();
            }
            else
            {
                Appear();
                this.Show();
                gameSpeedController.SetTimeScale(GameSpeedController.TimeScale.Paused);
                isShown = true;

            }
        }

        private void Update()
        {
            if(currentBackgroundAplha != bgColorAlpha)
            {
                currentBackgroundAplha = Mathf.SmoothDamp(currentBackgroundAplha, bgColorAlpha, ref bgColorAlphaVel, bgColorAlphaSmoothTime);
                Color tmpColor = backgroundImage.color;
                tmpColor.a = currentBackgroundAplha;
                backgroundImage.color = tmpColor;
            }
        }

        void Appear()
        {
            bgColorAlpha = .8f;
        }

        void Disappear()
        {
            bgColorAlpha = 0;
        }

        override public void Show()
        {
            base.Show();
        }

        override public void Hide()
        {
            base.Hide();
        }

        public void ShowSettings()
        {
            Hide();
            settings.GetComponent<PauseSettings>().Show();
        }

        public void OnQuit()
        {
            SceneManager.LoadScene(0);
        }


    }
}