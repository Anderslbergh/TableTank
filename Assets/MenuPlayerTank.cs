using Assets.Scripts.data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerTank : MonoBehaviour
{
    public MeshRenderer[] meshRenderers= null;
    public void SetColor(PlayerColor color)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.SetColor("_Color", color.color);

        }
    }
}
