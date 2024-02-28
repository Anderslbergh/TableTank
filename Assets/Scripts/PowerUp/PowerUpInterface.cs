using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PowerUp
{
    public interface PowerUpInterface
    {
        IEnumerator MakeHappen();
        void UnMakeHappen();
    }
}