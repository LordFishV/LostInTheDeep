public abstract class RepairableObject<TData> : InteractableObject<TData>
    where TData : RepairableInteractableSO
{
    protected bool _isBroken;

    protected virtual void Awake() => _isBroken = _data.StartsBroken;

    // Returns true if the object is working (already, or after paying).
    protected bool EnsureRepaired(PlayerInteractor interactor)
    {
        if (!_isBroken) return true;

        if (interactor.Scrap.TrySpendScrap(_data.RepairCostInScrap))
        {
            _isBroken = false;
            Debug.Log($"Repaired {_data.DisplayName}.");
        }
        else
        {
            Debug.Log($"Need {_data.RepairCostInScrap} scrap to repair {_data.DisplayName}.");
        }
        return false; // the repair itself uses the interaction
    }
}