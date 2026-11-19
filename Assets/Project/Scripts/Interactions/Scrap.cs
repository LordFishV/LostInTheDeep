using UnityEngine;

public class Scrap : InteractableObject<ScrapSO>
{
    public override void Interact(PlayerInteractor interactor) {
        if (!interactor.Wallet.TryAddScrap(_data.InteractAudioClip)) {
            Debug.Log("Scrap storage is full.");
            return; // leave the pickup in the world
        }

        Destroy(gameObject);
    }

    public override void OnMouseHoverEnter() {

    }
    public override void OnMouseHoverExit() {

    }
}
