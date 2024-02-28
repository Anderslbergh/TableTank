using Assets.Scripts.Controllers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PowerUp
{
    public class Speed : PowerUpBase, PowerUpInterface
    {
        public override IEnumerator MakeHappen()
        {
            player.setAdditionalSpeed(powerUpData.powerUpValue);

            yield return new WaitForSeconds(powerUpData.duration);
            UnMakeHappen();
        }

        public override void UnMakeHappen()
        {
            player.setAdditionalSpeed(0);
            base.UnMakeHappen();
        }

    }
}