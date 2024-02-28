using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Menu
{
    public class SettingsController : MonoBehaviour
    {


        [Header("Settings")]
        public AudioMixer audioMixer;
        private Resolution[] resolusions;
        public TMPro.TMP_Dropdown resolutionDropdown;

        public void Start()
        {
            resolusions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentResolutionIndex = 0;
            int i = 0;
            foreach (Resolution resolution in resolusions)
            {
                string option = $"{resolution.width} x {resolution.height}";
                options.Add(option);

                if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
                i++;
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
     

        public void SetVolume(float value)
        {
            audioMixer.SetFloat("MainVolume", Mathf.Lerp(-80, 0, value));
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFullScreen(bool fullScreen)
        {
            Screen.fullScreen = fullScreen;
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolusions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

    }
}