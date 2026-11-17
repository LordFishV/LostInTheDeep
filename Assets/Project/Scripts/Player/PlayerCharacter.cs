using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _rotateAround;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _smoothTime = 0.05f;
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _sprintSpeed = 10f;
    [SerializeField] private float _crouchSpeed = 3f;
    [SerializeField, Range(0f, 1f)] private float _gearSpeedMultiplier = 0.5f;
    [SerializeField] private float _rayLength = 3f;

    private float _currentVelocity;
    private float _currentAngle;
    private Stance _state;

    private void Reset() => _controller = GetComponent<CharacterController>();

    /// <summary>Initialise variables for use in update methods.</summary>
    public void Initialise() {
        _state = Stance.Idle;
        _currentAngle = _rotateAround.eulerAngles.y;
    }

    /// <summary>The main update cycle for the player character.</summary>
    public void CharacterUpdate(PlayerInputs inputs, bool isInGear) {
        if (inputs.Move.sqrMagnitude < 0.001f) {
            _state = Stance.Idle;
            _currentVelocity = 0f;
            return;
        }

        // Update character methods.
        UpdateVelocity(inputs, isInGear);
        UpdateRotation(inputs);
    }

    /// <summary>Update the player characters velocity based on player inputs.</summary>
    private void UpdateVelocity(PlayerInputs inputs, bool isInGear) {

        // The current state the player is in.
        _state = inputs.Sprint ?
                            Stance.Sprint   : // IF SPRINT
                 inputs.Crouch && !isInGear ?
                            Stance.Crouch   : // IF CROUCH NOT IN GEAR
                            Stance.Walk;      // OTHERWISE WALK


        // The current speed at which the player will move at.
        float gear = isInGear ? _gearSpeedMultiplier : 1f;
        float speed = _state switch {
            Stance.Walk     => _walkSpeed    * gear,
            Stance.Sprint   => _sprintSpeed  * gear,
            Stance.Crouch   => _crouchSpeed,
            _ => throw new System.ArgumentOutOfRangeException(nameof(_state), _state, null)
        };

        // Input is the motion direction.
        Vector3 input = Vector3.ClampMagnitude(new Vector3(inputs.Move.x, 0f, inputs.Move.y), 1f);

        // Player motion projected on to the ground normal.
        Vector3 motion = Vector3.ProjectOnPlane(
            input,
            GetGroundNormal()
        ) * (speed * Time.deltaTime);

        _controller.Move(motion);
    }

    /// <summary>Update the player characters rotation based on player inputs.</summary>
    private void UpdateRotation(PlayerInputs inputs) {

        float targetAngle = Mathf.Atan2(inputs.Move.x, inputs.Move.y) * Mathf.Rad2Deg;
        _currentAngle = Mathf.SmoothDampAngle(_currentAngle, targetAngle, ref _currentVelocity, _smoothTime);
        _rotateAround.rotation = Quaternion.Euler(0f, _currentAngle, 0f);
    }

    /// <summary>Update ground normal for movement.</summary>
    private Vector3 GetGroundNormal() {

        Vector3 origin = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(origin, Vector3.down, out RaycastHit hit, _rayLength, _groundMask, QueryTriggerInteraction.Ignore)
            ? hit.normal  // HIT GROUND
            : Vector3.up; // HIT NOTHING
    }
}
