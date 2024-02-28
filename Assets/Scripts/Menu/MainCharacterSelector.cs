using Assets.Scripts.Controllers;
using Assets.Scripts.data;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class MainCharacterSelector : MainMenuController
    {

        [Header("Character Selection")]
        public Transform menuPlayerPrefab;

        public PlayerColor[] playerColors = new PlayerColor[0]; 
            //[ColorUsage(true, true)]
        //public Color[] playerLaserColors = new Color[4];

        public Sprite keyboardSprite;
        public Sprite gamepadSprite;
        public float menyPlayerHeight;
        private bool isPopulated = false;
        private MainCharacterSelectionController characterSelectionController;

        private PlayerInputManager playerInputManager;

        private void Awake()
        {
            characterSelectionController = FindObjectOfType<MainCharacterSelectionController>();
            playerInputManager = FindObjectOfType<PlayerInputManager>();
            
        }
        public override void Start()
        {

            base.Start();
            Hide();
           
        }

        PlayerColor[] GetUnusedColors()
        {
            PlayerColor[] availableColors = playerColors;
            Player[] players = FindObjectsOfType<Player>();
            foreach (Player p in players)
            {
                availableColors = availableColors.Where(color => color != p.color).ToArray();
            }
            return availableColors;
        }
        public PlayerColor GetNextAvailableColor()
        {
           
            PlayerColor playerColor = GetUnusedColors()[0];
            return playerColor;

        }

        public PlayerColor GetNextAvailableColor(PlayerColor currentColor, int direction)
        {
            PlayerColor[] colors = GetUnusedColors();
            PlayerColor playerColor;

            int currentIndex = 0;
            for (int i = 0; i < playerColors.Length; i++)
            {
                if (playerColors[i].color == currentColor.color)
                {
                    currentIndex = i;
                }
            }

            int nextAvailabelIndex = ((currentIndex + direction) % playerColors.Length);
            nextAvailabelIndex = nextAvailabelIndex == -1 ? playerColors.Length - 1 : nextAvailabelIndex;

            playerColor = playerColors[nextAvailabelIndex];

            return playerColor;

        }

        public void OnBack()
        {
            Hide();
            mainMenu.GetComponent<MainMenuController>().Show();
        }

        public void OnSelectLevel()
        {
            Hide();
            levelSelector.GetComponent<MainMenuController>().Show();
        }

        override public void Show()
        {
            if (!isPopulated)
            {
                //Populate();
            }
            playerInputManager.EnableJoining();
            base.Show();
        }
        override public void Hide()
        {
            playerInputManager.DisableJoining();

            base.Hide();
        }

        //private void Populate()
        //{
        //    int i = 0;

        //    foreach (InputDevice device in InputSystem.devices)
        //    {
        //        if ((device is not Gamepad) && (device is not Keyboard)){
        //            continue;
        //        }
        //        RectTransform tempMenuPlayer =  Instantiate(menuPlayerPrefab, menuPlayersParent);
        //        Vector3 tempPos = tempMenuPlayer.localPosition;
        //        tempPos.y -= (i * menyPlayerHeight);
        //        tempMenuPlayer.localPosition = tempPos;

        //        MainMenuPlayer menuPlayer = tempMenuPlayer.GetComponent<MainMenuPlayer>();
        //        menuPlayer.SetColor(playerColors[i], playerLaserColors[i]);
        //        menuPlayer.SetPlayerIndex(i);
        //        Sprite sprite = gamepadSprite;

        //        if (device is Keyboard)
        //        {
        //            sprite = keyboardSprite;
        //        } else if(device is Gamepad)
        //        {
        //            sprite = gamepadSprite;
        //        }
        //        menuPlayer.SetDevice(device, sprite);
        //        i++;
        //    }
        //    characterSelectionController.SpawnCharacters();
        //    isPopulated = true;
        //}
    }
}