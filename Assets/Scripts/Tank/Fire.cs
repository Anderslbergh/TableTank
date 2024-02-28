using Assets.Scripts.data;
using Assets.Scripts.misc;
using Assets.Scripts.Tank.misc;
using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Tank
{
    public class Fire : MonoBehaviour
    {
        private Rigidbody rb;
        private bool tryFire = false;


        [SerializeField] Transform projectileStart;
        [SerializeField] private data.Projectile defaultProjectile;

        private VisualEffect currentMuzzle;
        private data.Projectile currentProjectile;
        private AudioClip[] currentAudioClips;
        [SerializeField] private Transform currentMuzzleTransform;
        [SerializeField] private data.Projectile[] projectiles;
        [SerializeField] AudioSource audioSource;

        private Boolean canFire = true;
       

        private void Start()
        {

            rb = GetComponent<Rigidbody>();

            if (projectiles.Length == 0)
            {
                throw new Exception("No projectiles");
            }

            if(defaultProjectile == null)
            {
                defaultProjectile = projectiles[0];
            }
            SetProjectile(defaultProjectile);
        }

        void SetProjectile(data.Projectile projectile)
        {
            currentProjectile = projectile;
            if(currentMuzzleTransform != null)
            {
                Destroy(currentMuzzleTransform.gameObject);
            }
            currentMuzzleTransform = Instantiate(projectile.MuzzlePrefab, projectileStart.position, projectileStart.rotation, projectileStart);
            currentMuzzle = currentMuzzleTransform.GetComponentInChildren<VisualEffect>();
            currentAudioClips = projectile.fireSounds;

        }
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                tryFire = true;

            }
        }

        private void Update()
        {
            if (tryFire)
            {
                if (canFire)
                {

                    StartCoroutine(FireProjectile());
                }
                tryFire = false;
            }
        }

        private IEnumerator FireProjectile()
        {
           
            canFire = false;
            if(projectileStart == null) { throw new Exception("No Projectile start transform"); }

            // Play sound
            audioSource.PlayOneShot(currentAudioClips[Random.Range(0, currentAudioClips.Length - 1)]);

            // setup projectile
            Transform projetileTransform = Instantiate(currentProjectile.Prefab, projectileStart.position, projectileStart.rotation);
            projetileTransform.GetComponent<misc.Projectile>().projectile = currentProjectile;


            // backward force
            rb.AddForceAtPosition(projectileStart.transform.forward * -1 * currentProjectile.backwardForce, projectileStart.position);


            // trigger muzzle
            if (currentMuzzle != null)
            {
                currentMuzzle.Play();
            }
            yield return new WaitForSeconds(currentProjectile.reloadTime);

            canFire = true;
           
        }
    }
}