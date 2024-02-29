using Assets.Scripts.Controllers;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class MainMenuPlayer : MonoBehaviour
    {
        public MainCharacterSelectionController characterSelectionController;
        //private PlayerController playerController;

        public enum PlayerState { Inactive, SelectingCharacter, Ready };
        public PlayerState playerState = PlayerState.Inactive;

        public Image backgroundImage;
        public Image deviceImage;
        private Color playerColor;
        private Color playerLaserColor;
        public TextMeshProUGUI text;

        public PlayerInput playerInput;
        private int playerIndex;
        private Light playerLight;
        [SerializeField] private Color inactiveColor;
        private Transform currentPrefab;

        private void Awake()
        {
            characterSelectionController= FindObjectOfType<MainCharacterSelectionController>();
            //playerController = /*FindObjectOfType*/<Controllers.PlayerController>();
        }
        internal void SetColor(Color color, Color laserColor)
        {
            playerColor = color;
            playerLaserColor = laserColor;
        }

        void ActivatePlayer()
        {
            playerState = PlayerState.SelectingCharacter;
            Color playerSelectColor = playerColor;
            playerSelectColor.a = 0.3f;
            backgroundImage.color = playerSelectColor;
            text.text = "Select character";
            playerLight.color = playerSelectColor;
            playerLight.enabled = true;
            //playerController.AddPlayer(
            //    playerIndex,
            //    $"PlayerIndex {playerIndex}",
            //    playerInput.devices[0],
            //    currentPrefab,
            //    playerColor,
            //    playerLaserColor
            //    ); ;
            SelectNewCharacter();


        }
        void ReadyPlayer()
        {
            playerState = PlayerState.Ready;
            Color playerReadyColor = playerColor;
            playerReadyColor.a = 1f;
            backgroundImage.color = playerReadyColor;
            text.text = "Ready";
        }

        void InactivatePlayer()
        {
            playerState = PlayerState.Inactive;
            if (playerInput.currentControlScheme == "Keyboard&Mouse")
            {
                text.text = "Press space to join";
            } else if (playerInput.currentControlScheme == "Gamepad")
            {
                text.text = "Press south key to join";
            }
            Color playerInactiveColor = inactiveColor;
            backgroundImage.color = playerInactiveColor;

            if (characterSelectionController)
            {
                //characterSelectionController.OptOut(playerIndex);

            }
            playerLight.enabled = false;
            //playerController.RemovePlayer(playerIndex);

        }

        public void SetDevice(InputDevice device, Sprite selectedSprite)
        {
            deviceImage.sprite = selectedSprite;
            playerInput.SwitchCurrentControlScheme(device);
            InactivatePlayer();
        }


        internal void SetPlayerIndex(int i)
        {
            playerIndex = i;
            //playerLight = characterSelectionController.GetPlayerLight(playerIndex);
        }

        public void OnJoin(InputAction.CallbackContext context)
        {
            print("join");
            if (context.canceled)
            {
                switch (playerState)
                {
                    case PlayerState.Inactive:
                        ActivatePlayer();
                        break;
                    case PlayerState.SelectingCharacter:
                        ReadyPlayer();
                        break;
                    case PlayerState.Ready:
                        break;
                }
            }
        }

        public void OnNextCharacter(InputAction.CallbackContext context)
        {
            print("next char");

            if (context.canceled)
            {
                SelectNewCharacter();
            }
        }

        private void SelectNewCharacter()
        {
            //currentPrefab = characterSelectionController.SelectNextCharacter(playerIndex);
            //playerController.UpdatePrefab(playerIndex, currentPrefab);
        }

        public void OnPrevCharacter(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
               // characterSelectionController.SelectNextCharacter(playerIndex, false);
            }
        }

        public void OnUnJoin(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                switch (playerState)
                {
                    case PlayerState.Inactive:
                        break;
                    case PlayerState.SelectingCharacter:
                        InactivatePlayer();
                        break;
                    case PlayerState.Ready:
                        ActivatePlayer();
                        break;
                }
            }
        }

    }
}