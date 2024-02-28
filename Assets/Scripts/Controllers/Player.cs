using Assets.Scripts.data;
using Assets.Scripts.Menu;
using Assets.Scripts.Tank;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Controllers
{
    public class Player : MonoBehaviour
    {
        public PlayerColor color;

        private Aim aim;
        private CatapilarFeet catapilarFeet;
        private Transform menuPlayerTransform;
        private MainCharacterSelector mainCharacterSelecto;

        public void initMenuPlayer()
        {
            mainCharacterSelecto = FindAnyObjectByType<MainCharacterSelector>();
            Transform menuPrefab = mainCharacterSelecto.menuPlayerPrefab;
            menuPlayerTransform = Instantiate(menuPrefab);
            menuPlayerTransform.position = transform.position + Vector3.up * 10;
            menuPlayerTransform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0,360), 0f);
            color = mainCharacterSelecto.GetNextAvailableColor();
        }

        public void onFire(InputAction.CallbackContext context)
        {
            float upDownValue = context.ReadValue<Vector2>().y;

            float LeftRightValue = context.ReadValue<Vector2>().x;
            if (upDownValue != 0f)
            {
                print(context.ReadValue<Vector2>());
                color = mainCharacterSelecto.GetNextAvailableColor(color, (int)upDownValue);
                menuPlayerTransform.GetComponent<MenuPlayerTank>().SetColor(color);
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