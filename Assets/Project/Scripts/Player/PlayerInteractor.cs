using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask _interactionMask;
    [SerializeField] private float _interactRange = 2f;
    [SerializeField, Range(-1f, 1f)] private float _minimumFacingAlignment = 0.5f; // 0.5 is roughly 60 degrees either side
    [SerializeField] private PlayerWallet _wallet;
    public PlayerWallet Wallet => _wallet;
    private readonly Collider[] _nearbyColliders = new Collider[16];

    public IChargeable Gear { get; private set; }
    public PlayerHiding Hiding { get; private set; }

    private void Awake() {
        Gear = GetComponentInParent<IChargeable>();
        Hiding = GetComponentInParent<PlayerHiding>();
    }

    /// <summary>Located in the main player loop updated every frame but only passes through when player is pressing the interact key</summary>
    /// <param name="inputs">The player inputs to pass into this function</param>
    public void UpdateInteractor(PlayerInputs inputs) {
        if (!inputs.Interact) return;

        if (Hiding.IsHidden) {
            Hiding.Exit();
            return;
        }

        CallInteraction();
    }

    /// <summary>When player can interact call this function</summary>
    private void CallInteraction() {
        Vector3 origin = transform.position;
        Vector3 facingDirection = transform.forward;
        facingDirection.y = 0f;
        facingDirection.Normalize();

        int colliderCount = Physics.OverlapSphereNonAlloc(origin, _interactRange, _nearbyColliders,
                                                          _interactionMask, QueryTriggerInteraction.Collide);

        IInteractable bestTarget = null;
        float bestSquaredDistance = float.MaxValue;

        for (int i = 0; i < colliderCount; i++) {
            Collider nearbyCollider = _nearbyColliders[i];
            Vector3 offsetToTarget = GetClosestPoint(nearbyCollider, origin) - origin;
            offsetToTarget.y = 0f; // top-down: ignore height

            float squaredDistance = offsetToTarget.sqrMagnitude;

            // Directly on top of the player counts as "in front"
            Vector3 directionToTarget = squaredDistance > 0.0001f
                ? offsetToTarget / Mathf.Sqrt(squaredDistance)
                : facingDirection;

            if (Vector3.Dot(facingDirection, directionToTarget) < _minimumFacingAlignment) continue;
            if (squaredDistance >= bestSquaredDistance) continue;

            if (_nearbyColliders[i].GetComponentInParent<IInteractable>() is IInteractable candidate) {
                bestTarget = candidate;
                bestSquaredDistance = squaredDistance;
            }
        }

        if (bestTarget != null)
            bestTarget.Interact(this);
        else
            Log("Nothing in range.");
    }

    /// <summary>Find the closest object with a interaction trigger collider to interact with</summary>
    /// <param name="targetCollider"> The </param>
    /// <param name="origin">The transform.position of the gameObject using this sctipt</param>
    /// <returns>Gets the closes position to the player from the collider</returns>
    private static Vector3 GetClosestPoint(Collider targetCollider, Vector3 origin) {
        // ClosestPoint isn't supported on non-convex mesh colliders
        if (targetCollider is MeshCollider meshCollider && !meshCollider.convex)
            return targetCollider.bounds.center;

        return targetCollider.ClosestPoint(origin);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private static void Log(string message) => Debug.Log(message);
}