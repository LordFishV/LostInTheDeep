using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bed : HideableObject<BedSO>
{
    private Transform locationBefore; // Before interacting
    private bool isHiden => IsPlayerHidingIn;
    public override void Interact(PlayerInteractor interactor) {
        locationBefore = interactor.transform;

        IsHiding();

        interactor.transform.position = transform.position;
    }

    public override void OnMouseHoverEnter() {

    }

    public override void OnMouseHoverExit() {

    }

    private void IsHiding() {
        if (HideState()) {
            isHiden = false;
        } else {
            isHiden = true;
        }
    }
}
