using Assets.Scripts.Controllers;
using Assets.Scripts.data;
using Assets.Scripts.PowerUp;
using System;
using System.Collections;
using UnityEngine;
using static Assets.Scripts.data.PowerUp;

namespace Assets.Scripts.PowerUp
{
    public class PowerUpBase : MonoBehaviour, PowerUpInterface
    {

        public data.PowerUp powerUpData;
        public Player player;

        private void OnCollisionEnter(Collision collision)
        {
            player = collision.gameObject.GetComponentInParent<Player>();
            if (player)
            {
                TriggerPowerUp();
            }
        }


        private void DisableMe()
        {
            foreach(Collider collider in GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
            foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.enabled = false;
            }
        }


        private void TriggerPowerUp()
        {
            DisableMe();
            StartCoroutine(MakeHappen());
        }


  
        public virtual IEnumerator MakeHappen()
        {
            yield return new WaitForSeconds(0);
        }

        public virtual void UnMakeHappen()
        {
            
        }
    }
}