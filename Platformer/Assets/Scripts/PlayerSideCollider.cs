using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideCollider : MonoBehaviour
{
    public PlayerSideColliderInstance URInstance;
    public PlayerSideColliderInstance ULInstance;
    public PlayerSideColliderInstance LLInstance;
    public PlayerSideColliderInstance LRInstance;

    public enum SideCollisionStatus
    {
        Right,
        Left,
        Both,
        None
    }
    public SideCollisionStatus sideCollisionStatus;


    //
    private void Update()
    {
        bool r = false;
        bool l = false;
        if (URInstance.isCollided || LRInstance.isCollided)
        {
            r = true;
        }
        if (ULInstance.isCollided || LLInstance.isCollided)
        {
            l = true;
        }

        if (r && l)
            sideCollisionStatus = SideCollisionStatus.Both;
        else if (!r && !l)
            sideCollisionStatus = SideCollisionStatus.None;
        else if (r)
            sideCollisionStatus = SideCollisionStatus.Right;
        else if (l)
            sideCollisionStatus = SideCollisionStatus.Left;
    }
}
