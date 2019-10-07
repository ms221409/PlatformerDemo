using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedDetector : MonoBehaviour
{ 
    private PlayerMotor _playerMotor;
    private bool _canGrounded = true;


    private void Start()
    {
        _playerMotor = GetComponentInParent<PlayerMotor>();
    }


    /// <summary>
    /// Respond to trigger.
    /// </summary>
    /// <param name="other">Other.</param>
    private void OnTriggerEnter(Collider other)
    {
        _playerMotor.OnGrounded();
    }


    public void OnJump ()
    {
        _canGrounded = false;
        Invoke("EnableGrounded", _playerMotor.jumpMinTime);
    }


    //
    void EnableGrounded ()
    {
        _canGrounded = true;
    }
}
