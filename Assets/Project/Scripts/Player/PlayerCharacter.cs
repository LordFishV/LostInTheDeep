using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _rotateAround;
    [SerializeField] private LayerMask _groundMask;


    [SerializeField] private float _gravity = -20f;
    [SerializeField] private float _groundedStickForce = -2f;
    [SerializeField] private float _smoothTime = 0.05f;
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _sprintSpeed = 10f;
    [SerializeField] private float _crouchSpeed = 3f;
    [SerializeField, Range(0f, 1f)] private float _gearSpeedMultiplier = 0.5f;
    [SerializeField] private float _rayLength = 3f;

    private float _currentVelocity;
    private float _verticalVelocity;
    private float _currentAngle;
    private float _deltaTime;
    private PlayerInputs _inputs;
    private bool _isInDiveGear;
    private Stance _state;

    private void Reset() => _controller = GetComponent<CharacterController>();

    /// <summary>Initialise variables for use in update methods.</summary>
    public void Initialise() {
        _state = Stance.Idle;
        _currentAngle = _rotateAround.eulerAngles.y;
    }

    /// <summary>The main update cycle for the player character.</summary>
    public void CharacterUpdate(PlayerInputs inputs, bool isInDiveGear) {
        
        _deltaTime = Time.deltaTime;
        _isInDiveGear = isInDiveGear;
        _inputs = inputs;

        // Update character methods.
        UpdateVelocity();

        if (_inputs.MoveInput) {
            UpdateRotation();
        }
    }

    /// <summary>Update the player characters velocity based on player inputs.</summary>
    private void UpdateVelocity() {
        (Vector3 groundNormal, bool isGrounded) = GroundInfo();
        if (isGrounded && _verticalVelocity < 0f) {
            _verticalVelocity = _groundedStickForce;
        }
        else {
            _verticalVelocity += _gravity * _deltaTime;
        }

        if (!_inputs.MoveInput)
            _state = Stance.Idle;
        else {
            // The current state the player is in.
            _state = _inputs.Sprint ? 
                                Stance.Sprint : // IF SPRINT
                     _inputs.Crouch && !_isInDiveGear ?
                                Stance.Crouch : // IF CROUCH NOT IN GEAR
                                Stance.Walk;    // OTHERWISE WALK
        }

        // The current speed at which the player will move at.
        float gear = _isInDiveGear ? _gearSpeedMultiplier : 1f;
        float speed = _state switch
        {
            Stance.Idle   => 0f,
            Stance.Walk   => _walkSpeed   * gear,
            Stance.Sprint => _sprintSpeed * gear,
            Stance.Crouch => _crouchSpeed,
            _ => throw new System.ArgumentOutOfRangeException(nameof(_state), _state, null)
        };

        // Input is the motion direction.
        Vector3 moveDirection = Vector3.ClampMagnitude(new Vector3(_inputs.Move.x, 0f, _inputs.Move.y), 1f);

        // Player motion projected on to the ground normal.
        Vector3 horizontalMotion = isGrounded
            ? Vector3.ProjectOnPlane(moveDirection, groundNormal) * speed
            : moveDirection * speed;

        Vector3 finalVelocity = horizontalMotion + (Vector3.up * _verticalVelocity);
        _controller.Move(finalVelocity * _deltaTime);
    }

    /// <summary>Update the player characters rotation based on player inputs.</summary>
    private void UpdateRotation() {

        float SmoothTime = _state switch {
            Stance.Walk     => _smoothTime,
            Stance.Sprint   => _smoothTime * 0.5f,
            Stance.Crouch   => _smoothTime * 1.25f,
            _ => 0f
        };

        float targetAngle = Mathf.Atan2(_inputs.Move.x, _inputs.Move.y) * Mathf.Rad2Deg;
        _currentAngle = Mathf.SmoothDampAngle(_currentAngle, targetAngle, ref _currentVelocity, SmoothTime);
        _rotateAround.rotation = Quaternion.Euler(0f, _currentAngle, 0f);
    }

    /// <summary>Update ground normal and hit conformation for movement.</summary>
    private (Vector3, bool) GroundInfo() {

        Vector3 origin = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(origin, Vector3.down, out RaycastHit hit, _rayLength, _groundMask, QueryTriggerInteraction.Ignore)
            ? (hit.normal, true)   // HIT GROUND
            : (Vector3.up, false); // HIT NULL OR DOESN'T MATCH MASK
    }
}
