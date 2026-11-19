using UnityEngine;

public class PickUp : InteractableObject<PickUpSO>
{
    public override void Interact(PlayerInteractor interactor) {
        Debug.Log($"Picked up {_data.DisplayName}"); // inventory goes here later
        Destroy(gameObject);
    }

    public override void OnMouseHoverEnter() {

    }
    public override void OnMouseHoverExit() {

    }
}