using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Controllers
{
    public class LevelsController : MonoBehaviour
    {

        [System.Serializable]
        public struct Stage
        {
            public int stage;
            public bool open;
            public string name;
            public int sceneId;
        }
        [System.Serializable]
        public class Level
        {
            public int level;
            public bool open;
            public bool enable;
            public int sceneId;
            public string name;
            public Sprite image;
            public Stage[] stages;
        }

        public bool canSelectNewLevel = false;

        [SerializeField]
        List<Level> levels = new List<Level>();
        private int currentLoadedLevelIndex;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public List<Level> GetLevels() => levels;

        public void LoadLevel(Level level)
        {
            currentLoadedLevelIndex = levels.IndexOf(level);
            SceneManager.LoadScene(level.sceneId);
        }

        public void LoadNextLevel()
        {
            int nextLevelIndex = currentLoadedLevelIndex + 1;
            Level nextLevel = levels[nextLevelIndex];
            if (nextLevel != null)
            {
                LoadLevel(nextLevel);
            }
        }

        public void EnableNextLevel()
        {
            canSelectNewLevel = true;
        }

        public void OnNextLevel(InputAction.CallbackContext callback)
        {
            if (canSelectNewLevel)
            {
                if (callback.canceled)
                {
                    canSelectNewLevel = false;
                    LoadNextLevel();
                }
            }
        }
    }
}