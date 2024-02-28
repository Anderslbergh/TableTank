//using Assets.Scripts.Effects;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class MainMenuController : MenuController
    {
        [Header("Rect transforms")]
        public RectTransform levelSelector;
        public RectTransform characterSelector;
        public RectTransform settings;

        [Header("Camera settings")]
        public Cinemachine.CinemachineVirtualCamera sceneCamera;
        public Cinemachine.CinemachineTargetGroup targetGroup;

        public Transform focusTarget;
        //private FocalLength focalLength;

        private void Awake()
        {
            //focalLength= FindObjectOfType<Effects.FocalLength>();
        }

        public void ActivateCam()
        {
            Cinemachine.CinemachineVirtualCamera[] cameras = FindObjectsOfType<Cinemachine.CinemachineVirtualCamera>();
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].enabled = cameras[i] == sceneCamera;
            }

            //targetGroup.RemoveMember(targetGroup.m_Targets[0].target);
            targetGroup.AddMember(focusTarget, 1, 1);

            //focalLength.SetCMCam(sceneCamera);
        }

        override public void Show()
        {
            ActivateCam();
            base.Show();
        }
    }
}