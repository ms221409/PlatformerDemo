using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [Header("Values")]
    public float movementSpeed;
    public float movementRedux;
    public float jumpForce;
    public float fallRate;
    public float jumpMinTime = 0.2f;

    //
    private float _currentVerticalVector = 0;
    private float _currentHorizontalVector = 0;
    private bool _isGrounded = false;
    private GroundedDetector _groundedDetector;


    private void Start()
    {
        _groundedDetector = GetComponentInChildren<GroundedDetector>();
    }


    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        InputLoop();
        VectorLoop();
        MovementLoop();
    }


    /// <summary>
    /// Collect user input
    /// </summary>
    void InputLoop ()
    {
        _currentHorizontalVector = Input.GetAxis("Horizontal");

        if (_isGrounded)
        {
            if (Input.GetKeyDown (KeyCode.Space))
            {
                Jump();
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    void Jump ()
    {
        _groundedDetector.OnJump();
        _currentVerticalVector = jumpForce;
        _isGrounded = false;
    }


    /// <summary>
    /// Vectors the loop.
    /// </summary>
    void VectorLoop ()
    {
        // Fall if we aren't grounded
        if (!_isGrounded)
        {
            _currentVerticalVector -= Time.deltaTime * fallRate;
        }

        //
        _currentHorizontalVector = Mathf.Lerp(_currentHorizontalVector, 0, Time.deltaTime * movementRedux);
    }


    /// <summary>
    /// 
    /// </summary>
    void MovementLoop ()
    {
        transform.Translate(Vector3.right * _currentHorizontalVector * Time.deltaTime);
        transform.Translate(Vector3.up * _currentVerticalVector * Time.deltaTime);
    }


    /// <summary>
    /// Called from GroundedDetector.
    /// </summary>
    public void OnGrounded ()
    {
        _isGrounded = true;
        _currentVerticalVector = 0;
        AdjustToGround();
    }


    /// <summary>
    /// Snap player to ground
    /// </summary>
    void AdjustToGround ()
    {

    }
}
