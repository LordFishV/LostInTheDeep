using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask _interactionMask;
    [SerializeField] private float _interactRange = 2f;
    [SerializeField, Range(-1f, 1f)] private float _minimumFacingAlignment = 0.5f; // 0.5 is roughly 60 degrees either side
    [SerializeField] private PlayerScrap _scrap;
    public PlayerScrap Scrap => _scrap;
    private readonly Collider[] _nearbyColliders = new Collider[16];

    public void UpdateInteractor(PlayerInputs inputs) {
        if (inputs.Interact)
            CallInteraction();
    }

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
            Vector3 offsetToTarget = _nearbyColliders[i].transform.position - origin;
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

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private static void Log(string message) => Debug.Log(message);
}