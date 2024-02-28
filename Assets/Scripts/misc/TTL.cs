using System.Collections;
using UnityEngine;

namespace Assets.Scripts.misc
{
    public class TTL : MonoBehaviour
    {
        public float ttl = 10;

        private void Start()
        {
            Destroy(gameObject, ttl);

        }

    }
}