using System;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField, Min(0f)] private float _spottedGraceTime = 0.3f;
    [SerializeField, Range(0f, 1f)] private float _partialDetectionMultiplier = 0.4f;

    private Renderer[] _renderers;
    private Vector3 _returnPosition;
    private float _lastSeenTime = float.NegativeInfinity;
    private bool _spottedEntering;

    public HidingSpot CurrentSpot { get; private set; }
    public bool IsHidden => CurrentSpot != null;
    public event Action<bool> HiddenChanged;

    /// <summary>What enemies should treat the player as right now.</summary>
    public HidingVisibility EffectiveVisibility =>
        !IsHidden || _spottedEntering ? HidingVisibility.Visible : CurrentSpot.Visibility;

    /// <summary>Scales enemy sight range: 1 in the open or when compromised, 0 when fully hidden.</summary>
    public float DetectionMultiplier => EffectiveVisibility switch {
        HidingVisibility.Visible => 1f,
        HidingVisibility.PartiallyVisible => _partialDetectionMultiplier,
        _ => 0f
    };

    private void Reset() => _controller = GetComponent<CharacterController>();

    private void Awake() => _renderers = GetComponentsInChildren<Renderer>(true);

    private void Update() {
        // The spot was destroyed while the player was inside it.
        if (IsHidden && CurrentSpot == null)
            Exit();
    }

    /// <summary>Enemies call this every time their vision check sees the player.</summary>
    public void ReportSeen() => _lastSeenTime = Time.time;

    public bool Enter(HidingSpot spot) {
        if (IsHidden) return false;

        // Seen by an enemy just before or as they entered: the spot is compromised.
        _spottedEntering = Time.time - _lastSeenTime <= _spottedGraceTime;

        _returnPosition = transform.position;
        CurrentSpot = spot;

        // A CharacterController overwrites direct position changes unless disabled.
        _controller.enabled = false;
        transform.position = spot.HidePosition;

        SetRenderers(false);
        HiddenChanged?.Invoke(true);
        return true;
    }

    public void Exit() {
        if (!IsHidden) return;

        HidingSpot spot = CurrentSpot;
        CurrentSpot = null;
        _spottedEntering = false;

        transform.position = _returnPosition;
        _controller.enabled = true;

        SetRenderers(true);
        HiddenChanged?.Invoke(false);

        if (spot != null) spot.OnPlayerExited();
    }

    private void SetRenderers(bool visible) {
        foreach (Renderer renderer in _renderers)
            renderer.enabled = visible;
    }
}