using Assets.Scripts.Controllers;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class MainLevelSelector : MainMenuController
    {
        [Header("Level Selection")]
        public RectTransform levelSelectorPrefab;
        public RectTransform levelSelectorParent;
        private bool isPopulated;
        private LevelsController levelsController;
        [SerializeField] private int menyLevelHeight;

        public override void Start()
        {
            base.Start();
            Hide();
            levelsController = FindObjectOfType<LevelsController>();
        }

        public void OnBack()
        {
            Hide();
            characterSelector.GetComponent<MainMenuController>().Show();
        }



        override public void Show()
        {
            if (!isPopulated)
            {
                Populate();
            }
            base.Show();
        }

        private void Populate()
        {
            int i = 0;

            foreach (LevelsController.Level level in levelsController.GetLevels())
            {

                RectTransform tempMenuLevel = Instantiate(levelSelectorPrefab, levelSelectorParent);
                Vector3 tempPos = tempMenuLevel.localPosition;
                tempPos.y -= i * menyLevelHeight;
                tempMenuLevel.localPosition = tempPos;

                
                tempMenuLevel.GetComponent<Level>().SetLevel(level);

                i++;
            }
            isPopulated = true;
        }
    }
}