using UnityEngine;

public class HidingSpot : InteractableObject<HidingSpotSO>
{
    [Header("Instance")]
    [SerializeField] private Transform _hidePoint; // where the player is teleported to
    [SerializeField] private Override<HidingVisibility> _visibility;

    public HidingVisibility Visibility => _visibility.Resolve(_data.Visibility);
    public Vector3 HidePosition => _hidePoint != null ? _hidePoint.position : transform.position;

    public override void Interact(PlayerInteractor interactor) {
        // Leaving is handled by the interactor, so this only runs when entering.
        if (interactor.Hiding.Enter(this))
            PlayUseSound();
    }

    public override void OnMouseHoverEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnMouseHoverExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayerExited() => PlayUseSound();
}