// PickUp.cs
public class PickUp : InteractableObject<PickUpSO>
{
    public override void Interact(PlayerInteractor interactor)
    {
        if (_data.IsScrap)
            interactor.Scrap.AddScrap(_data.Amount);
        else
            Debug.Log($"Picked up {_data.DisplayName}"); // inventory goes here later

        Destroy(gameObject);
    }
}