using Assets.Scripts.data;
using Assets.Scripts.Menu;
using Assets.Scripts.Tank;
using System;
using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Controllers
{
    public class Player : MonoBehaviour
    {
        public PlayerColor color;

        [SerializeField]
        public bool isReady = false;

        private Aim aim;
        private CatapilarFeet catapilarFeet;
        private Transform menuPlayerTransform;
        private MainCharacterSelector mainCharacterSelector;

        public void initMenuPlayer()
        {
            mainCharacterSelector = FindAnyObjectByType<MainCharacterSelector>();
            Transform menuPrefab = mainCharacterSelector.menuPlayerPrefab;
            menuPlayerTransform = Instantiate(menuPrefab);
            menuPlayerTransform.position = transform.position + Vector3.up * 10;
            menuPlayerTransform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0,360), 0f);
            color = mainCharacterSelector.GetNextAvailableColor();
            menuPlayerTransform.GetComponent<MenuPlayerTank>().SetColor(color);

        }

        public void onCharacterChange(InputAction.CallbackContext context)
        {
            if (isReady) return;
            if (context.performed)
            {

                print(context.ReadValue<Vector2>());
                float upDownValue = context.ReadValue<Vector2>().y;

                float LeftRightValue = context.ReadValue<Vector2>().x;
                if (upDownValue != 0f)
                {
                    color = mainCharacterSelector.GetNextAvailableColor(color, (int)upDownValue);
                    menuPlayerTransform.GetComponent<MenuPlayerTank>().SetColor(color);
                }
                if(LeftRightValue != 0f)
                {
                    // change outfit
                }
            }
        }

        public void onOkey(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                isReady = true;
            }
        }

        public void onCancel(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                isReady = false;
            }
        }

        private void Awake()
        {
            aim = GetComponent<Aim>();
            catapilarFeet = GetComponent<CatapilarFeet>();
        }

        public void setAdditionalSpeed(float value) {
            catapilarFeet.SetSpeed(value);
            aim.SetSpeed(3);
        }
    }
}