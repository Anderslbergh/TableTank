using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class Level : MonoBehaviour
    {

        [SerializeField] TMPro.TextMeshProUGUI text;
        [SerializeField] Image bgSprite;
        Controllers.LevelsController.Level levelControllerLevel;

        public void SetLevel(Controllers.LevelsController.Level levelControllerLevel)
        {
            bgSprite.sprite = levelControllerLevel.image;

            this.levelControllerLevel = levelControllerLevel;
            text.text = $"Chapter {levelControllerLevel.level} - {levelControllerLevel.name}";

            if (!levelControllerLevel.open)
            {
                GetComponent<Button>().interactable = false;
            }

        }


        public void OnLoadScene()
        {
            SceneManager.LoadScene(levelControllerLevel.sceneId);
        }
    }
}