using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class LevelController : MonoBehaviour
    {

        [SerializeField] private string levelName = "untitled";
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Collider[] deathTrigger;

        internal Vector3 getRandomSpawnPoints()
        {
            int index = UnityEngine.Random.Range(0, spawnPoints.Length - 1);
            Transform point = spawnPoints[index];
            spawnPoints = spawnPoints.Where(val => val != point).ToArray();
            return point.position;
        }
    }
}