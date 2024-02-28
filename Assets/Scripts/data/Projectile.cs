using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace Assets.Scripts.data
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Projectile", order = 1)]

    public class Projectile : ScriptableObject
    {

        public Transform Prefab;
        public Transform MuzzlePrefab;
        public AudioClip[] fireSounds;
        public AudioClip[] impactSounds;
        public float reloadTime = 1; 
        public float ttl = 3;
        public float damange = 100;
        public float impactForce = 100;
        public bool useGravity = false;
        public float targetVelocity = 400;
        public float accelerationMultiplier = 1;
        public bool instantVelocity = false;
        public bool destroyOnImpact = true;

        public float backwardForce = 400;

    }
}