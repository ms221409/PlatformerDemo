using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedDetector : MonoBehaviour
{
    private PlayerMotor _playerMotor;
    public enum GroundedState
    {
        Grounded,
        Airborne
    }
    public GroundedState groundedState = GroundedState.Airborne;

    public BoxCollider boxCast;
    public LayerMask groundLayers;
    public Transform groundedRaycastSource;
    public float groundedDistance = 0.5f;
    public float offGroundDistance = 0.5f;
    RaycastHit _downRayHit;
    GroundedDetectorCollider _detectorCollider;


    private void Start()
    {
        _playerMotor = GetComponentInParent<PlayerMotor>();
        _detectorCollider = GetComponentInChildren<GroundedDetectorCollider>();
    }


    //
    private void FixedUpdate()
    {
        // Raycast down
        if (Physics.BoxCast(groundedRaycastSource.position, boxCast.size / 2, Vector3.down, out _downRayHit, transform.rotation, groundedDistance, groundLayers))
        {
            if (groundedState != GroundedState.Grounded && _playerMotor.GetplayerMovementVector ().y < 0 && !_detectorCollider._isCollided)
            {
                OnLand();
            }
        }
        else
            groundedState = GroundedState.Airborne;
    }


    //
    private void OnLand()
    {
        groundedState = GroundedState.Grounded;
        _playerMotor.OnGrounded();
        Debug.Log("G R O U N D E D");
    }


    //
    public float GetGroundYPosition ()
    {
        return _downRayHit.point.y;
    }
}

