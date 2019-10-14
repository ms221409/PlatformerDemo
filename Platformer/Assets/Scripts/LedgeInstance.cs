using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeInstance : MonoBehaviour
{
    public Transform hangPosTransform;
    public Transform upPosTransform;

    public enum LedgeSide
    {
        Left,
        Right
    }
    public LedgeSide ledgeSide;

    [HideInInspector]
    public bool _sideCollided;
    [HideInInspector]
    public bool _topCollided;

    //
    private PlayerMotor _playerMotor;


    //
    private void Start()
    {
        _playerMotor = FindObjectOfType<PlayerMotor>();
    }

    //
    private void LateUpdate()
    {
        if (_sideCollided && _topCollided && _playerMotor.GetplayerMovementVector ().y >= _playerMotor.ledgeGrabVelocityMax)
        {
            if (ledgeSide == LedgeSide.Right)
            {
                _playerMotor._onLedgeRight = true;
                _playerMotor._onLedgeLeft = false;
            }
            else
            {
                _playerMotor._onLedgeRight = false;
                _playerMotor._onLedgeLeft = true;
            }
            _playerMotor._currentLedge = this;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 12)
            _sideCollided = true;
        else if (other.gameObject.layer == 13)
            _topCollided = true;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 12)
            _sideCollided = false;
        else if (other.gameObject.layer == 13)
            _topCollided = false;
    }
}
