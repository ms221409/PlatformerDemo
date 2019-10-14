using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideColliderInstance : MonoBehaviour
{
    public enum SideCollider
    {
        UpperLeft,
        UpperRight,
        LowerLeft,
        LowerRight
    }
    public SideCollider colliderType;
    public Renderer rend;
    [HideInInspector]
    public bool isCollided;


    //
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 14)
            return;

        isCollided = true;
        rend.material.color = Color.red;
    }


    //
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 14)
            return;

        isCollided = false;
        rend.material.color = Color.green;
    }
}
