using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Controllers
{
    public class GameSpeedController : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public enum TimeScale { Paused, SlowMo, Normal, Miniature};
        public TimeScale timeScale = TimeScale.Normal;
        public TimeScale settingsTimeScale = TimeScale.Miniature; // Change via settings
        public TimeScale lastTimeScale;

        public float slowMoTimeScale = 0.2f;
        public float normalTimeScale = 1f;
        public float minatureTimeScale = 2.2f;

        private float defaultTimeScale = 0.01f;
        public float currentTimeScale = 1;
        public float targetTimeScale = 1;
        public float smoothTime = 0.5f;
        private float currentVelocity;

        private void Update()
        {
            if(lastTimeScale != timeScale)
            {
                switch (timeScale)
                {
                    case TimeScale.Paused:
                        targetTimeScale = 0f;
                        break;
                    case TimeScale.SlowMo:
                        targetTimeScale = slowMoTimeScale;
                        break;
                    case TimeScale.Normal:
                        targetTimeScale = normalTimeScale;
                        break;
                    case TimeScale.Miniature:
                        targetTimeScale = minatureTimeScale;
                        break;
                }
            }

            if(currentTimeScale != targetTimeScale)
            {
                currentTimeScale = Mathf.SmoothDamp(currentTimeScale, targetTimeScale, ref currentVelocity, smoothTime);
                Time.timeScale = currentTimeScale;
                Time.fixedDeltaTime = defaultTimeScale * Time.timeScale;
                audioMixer.SetFloat("pitch_master", Time.timeScale);
            }
        }

        private void LateUpdate()
        {
            lastTimeScale = timeScale;
        }

        public void SetSettingsTimeScale(TimeScale timeScale)
        {
            settingsTimeScale = timeScale;
        }

        public void ResumeTimeScale()
        {
            timeScale = settingsTimeScale;
        }

        public void OnPause()
        {
            targetTimeScale = 0;
        }

        public void SetTimeScale(TimeScale timeScale)
        {
            this.timeScale = timeScale;
        }

    }
}