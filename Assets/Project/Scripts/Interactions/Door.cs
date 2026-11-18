using System.Collections;
using UnityEngine;

public class Door : RepairableObject<DoorSO>
{
    private bool _isOpen;
    private float _currentAngle;
    private Coroutine _animationCoroutine;
    private Vector3 _closedPosition;

    protected override void Awake() {
        base.Awake();
        _currentAngle = transform.rotation.y;
        _isOpen = _data.StartsOpen;
    }

    public override void Interact(PlayerInteractor interactor)
    {
        if (!EnsureRepaired(interactor)) return;

        _isOpen = !_isOpen;

        // Stop any currently running door animation before starting the new one
        if (_animationCoroutine != null) {
            StopCoroutine(_animationCoroutine);
        }

        _animationCoroutine = StartCoroutine(AnimateDoor(_isOpen));
    }
    private IEnumerator AnimateDoor(bool open) {
        float elapsed = 0f;
        float duration = OpenDuration;

        if (IsSlidingDoor) { // If dor is a sliding door
            Vector3 startPosition = transform.localPosition;
            Vector3 targetPosition = open ? _closedPosition + SlideOffset : _closedPosition;

            // While time played is less then duration 
            while (elapsed < duration) {
                float timeStep = TimeStep(elapsed, duration);

                transform.localPosition = Vector3.Lerp(startPosition, targetPosition, timeStep);
                yield return null; // Wait until the next frame
            }

            transform.localPosition = targetPosition;
        }
        else { // If door is a rotating door.
            float startAngle = _currentAngle;
            float targetAngle = open ? 90f : 0f;

            while (elapsed < duration) {
                float timeStep = TimeStep(elapsed, duration);

                _currentAngle = Mathf.LerpAngle(startAngle, targetAngle, timeStep);
                transform.localRotation = Quaternion.Euler(0f, _currentAngle, 0f);
                yield return null; // Wait until the next frame
            }

            _currentAngle = targetAngle;
            transform.localRotation = Quaternion.Euler(0f, _currentAngle, 0f);
        }

        _animationCoroutine = null;
    }

    private float TimeStep(float elapsed, float duration) {
        elapsed += Time.deltaTime;
        float timeStep = Mathf.Clamp01(elapsed / duration);
        // SmoothStep adds a nice ease-in and ease-out feel
        timeStep = Mathf.SmoothStep(0f, 1f, timeStep);
        return timeStep;
    }
}