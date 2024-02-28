using Assets.Scripts.data;
using Assets.Scripts.misc;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Tank.misc
{
    public class Projectile : MonoBehaviour
    {

        public data.Projectile projectile;
        [SerializeField] VisualEffect impactEffect;
        [SerializeField] VisualEffect trailEffect;
        private Rigidbody rb;
        private Light glowLight;
        private float currentForce;
        private bool isDead;

        private void Start()
        {
            glowLight = GetComponentInChildren<Light>();
            rb = GetComponent<Rigidbody>();

            GetComponent<Rigidbody>().useGravity = projectile.useGravity;
            if(projectile.ttl > 0)
            {
                transform.AddComponent<TTL>().ttl = projectile.ttl;
            }

            if (projectile.instantVelocity)
            {
                rb.velocity = transform.forward * projectile.targetVelocity;
            }
            rb.freezeRotation = true;

        }
        // Update is called once per frame
        void Update()
        {
            if (!projectile.instantVelocity)
            {
                SetVeclocity();
            }
        }

        private void SetVeclocity()
        {

            if (projectile.targetVelocity > 0)
            {
                currentForce = Mathf.Lerp(0, projectile.targetVelocity, Time.deltaTime * projectile.accelerationMultiplier);
                rb.AddForce(transform.forward * currentForce, ForceMode.VelocityChange);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isDead) return;

            if(collision.rigidbody != null)
            {
                collision.rigidbody.AddForce(transform.forward * projectile.impactForce, ForceMode.Impulse);
            }

            if (projectile.destroyOnImpact)
            {
                if (projectile.impactSounds.Length > 0)
                {
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().PlayOneShot(projectile.impactSounds[Random.Range(0, projectile.impactSounds.Length - 1)]);
                }
                if (glowLight)
                {
                    glowLight.enabled = false;

                }
                if (impactEffect != null)
                {
                    impactEffect.Play();
                    trailEffect.Stop();
                   
                }
                GetComponent<MeshRenderer>().enabled = false;
                isDead = true;

            }
        }
    }
}