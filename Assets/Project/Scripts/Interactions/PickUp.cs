using UnityEngine;

public class PickUp : InteractableObject<PickUpSO>
{
    public override void Interact(PlayerInteractor interactor) {
        if (_data.IsScrap)
            interactor.Wallet.AddScrap(_data.Amount, gameObject, _data.UseAudioClip);
        else
            Debug.Log($"Picked up {_data.DisplayName}"); // inventory goes here later
    }
}