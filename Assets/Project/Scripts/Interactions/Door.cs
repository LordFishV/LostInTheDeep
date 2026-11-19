using System.Collections;
using UnityEngine;

public class Door : RepairableObject<DoorSO>
{
    private enum DoorStyle { Rotating, Sliding }

    [SerializeField] private DoorStyle _style = DoorStyle.Rotating;
    [SerializeField] private bool _startsOpen;
    [SerializeField] private Vector3 _slideOffset = new Vector3(0f, 0f, 1.5f); // sliding only
    [SerializeField] private Override<float> _openDuration;
    [SerializeField] private Override<float> _openAngle;                       // rotating only
    [SerializeField] private Transform _root;

    private bool _isOpen;
    private Vector3 _closedPosition;
    private Quaternion _closedRotation;
    private Coroutine _animation;

    private float OpenDuration => _openDuration.Resolve(_data.OpenDuration);
    private float OpenAngle => _openAngle.Resolve(_data.OpenAngle);

    protected override void Awake() {
        base.Awake();

        _closedPosition = _root.localPosition;
        _closedRotation = _root.localRotation;

        _isOpen = _startsOpen;
        if (_isOpen) Apply(1f);
    }

    public override void Interact(PlayerInteractor interactor) {
        if (!EnsureRepaired(interactor)) return;

        _isOpen = !_isOpen;
        if (_animation != null) StopCoroutine(_animation);
        _animation = StartCoroutine(Animate(_isOpen ? 1f : 0f));
    }
    public override void OnMouseHoverEnter() {

    }
    public override void OnMouseHoverExit() {

    }

    private IEnumerator Animate(float target) {
        float start = _currentBlend;
        float elapsed = 0f;

        while (elapsed < OpenDuration) {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(elapsed / OpenDuration));
            Apply(Mathf.Lerp(start, target, t));
            yield return null;
        }

        Apply(target);
        _animation = null;
    }

    private float _currentBlend; // 0 = closed, 1 = open

    /// <summary>Apply rotation or position depending on the type of door</summary>
    /// <param name="blend">A 0 to 1 blend that from from one value to the other</param>
    private void Apply(float blend) {
        _currentBlend = blend;

        if (_style == DoorStyle.Sliding)
            _root.localPosition = _closedPosition + _slideOffset * blend;
        else
            _root.localRotation = _closedRotation * Quaternion.Euler(0f, OpenAngle * blend, 0f);
    }

    private void OnDrawGizmosSelected() {
        if (_style != DoorStyle.Sliding) return;
        Vector3 from = _root.position;
        Gizmos.DrawLine(from, from + _root.parent.TransformVector(_slideOffset));
    }
}