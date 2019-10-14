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
    public float maxFallRate = -30;
    public float jumpMinTime = 0.2f;
    public float ledgeGrabVelocityMax = -8f;

    public enum PlayerState
    {
        Grounded,
        Jumping,
        Falling,
        Hanging
    }
    public PlayerState currentPlayerState;

    //
    private float _currentVerticalVector = 0;
    private float _currentHorizontalVector = 0;
    private GroundedDetector _groundedDetector;
    private PlayerSideCollider _sideCollider;
    private TopColliderInstance _topCollider;
    private bool _jumpingFlag = false;
    [HideInInspector]
    public bool _onLedgeRight;
    [HideInInspector]
    public bool _onLedgeLeft;
    [HideInInspector]
    public LedgeInstance _currentLedge;


    private void Start()
    {
        _groundedDetector = GetComponentInChildren<GroundedDetector>();
        _sideCollider = GetComponent<PlayerSideCollider>();
        _topCollider = GetComponentInChildren<TopColliderInstance>();
    }


    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        InputLoop();
        VectorLoop();
        MovementLoop();

        _onLedgeRight = false;
        _onLedgeLeft = false;
    }


    /// <summary>
    /// Collect user input
    /// </summary>
    void InputLoop ()
    {
        _currentHorizontalVector = Input.GetAxis("Horizontal");
        switch (_sideCollider.sideCollisionStatus)
        {
            case PlayerSideCollider.SideCollisionStatus.Both:
                _currentHorizontalVector = 0;
                break;

            case PlayerSideCollider.SideCollisionStatus.None:

                break;

            case PlayerSideCollider.SideCollisionStatus.Right:
                _currentHorizontalVector = Mathf.Min(0, _currentHorizontalVector);
                break;

            case PlayerSideCollider.SideCollisionStatus.Left:
                _currentHorizontalVector = Mathf.Max(0, _currentHorizontalVector);
                break;
        }

        //
        if (currentPlayerState == PlayerState.Hanging)
        {
            _currentHorizontalVector = 0;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                LedgeGetUp();
            }
        }

        if (_groundedDetector.groundedState == GroundedDetector.GroundedState.Grounded && !_jumpingFlag)
        {
            if (Input.GetKeyDown (KeyCode.Space))
            {
                Jump();
            }
        }
    }


    //
    void LedgeGetUp ()
    {
        if (_currentLedge != null)
        {
            transform.position = _currentLedge.upPosTransform.position;
            currentPlayerState = PlayerState.Grounded;
            _groundedDetector.groundedState = GroundedDetector.GroundedState.Grounded;
            _jumpingFlag = true;
            Invoke("JumpFlagEnd", jumpMinTime);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    void Jump ()
    {
        _currentVerticalVector = jumpForce;
        _jumpingFlag = true;
        currentPlayerState = PlayerState.Jumping;
        Invoke("JumpFlagEnd", jumpMinTime);
    }


    void JumpFlagEnd ()
    {
        _jumpingFlag = false;
    }


    /// <summary>
    /// Vectors the loop.
    /// </summary>
    void VectorLoop ()
    {
        // Fall if we aren't grounded
        switch (_groundedDetector.groundedState)
        {
            case GroundedDetector.GroundedState.Airborne:
                if (currentPlayerState != PlayerState.Hanging)
                {
                    _currentVerticalVector -= Time.deltaTime * fallRate;
                    if (_currentVerticalVector < 0)
                        currentPlayerState = PlayerState.Falling;
                }
                break;

            case GroundedDetector.GroundedState.Grounded:
                if (!_jumpingFlag)
                    _currentVerticalVector = 0;
                break;
        }

        //
        if (currentPlayerState == PlayerState.Hanging)
            _currentVerticalVector = 0;
        else if (_topCollider.isCollided)
            _currentVerticalVector = Mathf.Min(_currentVerticalVector, 0);

        //
        _currentVerticalVector = Mathf.Max(_currentVerticalVector, maxFallRate);

        //
        _currentHorizontalVector = Mathf.Lerp(_currentHorizontalVector, 0, Time.deltaTime * movementRedux);
    }


    /// <summary>
    /// 
    /// </summary>
    void MovementLoop ()
    {
        transform.Translate(Vector3.right * _currentHorizontalVector * movementSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * _currentVerticalVector * Time.deltaTime);

        //
        if ((_onLedgeLeft || _onLedgeRight) && _currentVerticalVector < 0)
            OnLedgeGrab();
    }


    /// <summary>
    /// Called from GroundedDetector.
    /// </summary>
    public void OnGrounded ()
    {
        transform.position = new Vector3(transform.position.x, _groundedDetector.GetGroundYPosition() + _groundedDetector.offGroundDistance, transform.position.z);
        currentPlayerState = PlayerState.Grounded;
    }


    //
    void OnLedgeGrab ()
    {
        Debug.Log("G R A B");
        currentPlayerState = PlayerState.Hanging;
    }


    //
    public Vector2 GetplayerMovementVector ()
    {
        return new Vector2(_currentHorizontalVector, _currentVerticalVector);
    }
}
