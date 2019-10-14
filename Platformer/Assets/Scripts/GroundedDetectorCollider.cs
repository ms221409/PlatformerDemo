using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedDetectorCollider : MonoBehaviour
{
    [HideInInspector]
    public bool _isCollided;

    //
    private void OnTriggerStay(Collider other)
    {
        _isCollided = true;
    }


    //
    private void OnTriggerExit(Collider other)
    {
        _isCollided = false;
    }
}
