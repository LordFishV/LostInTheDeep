using UnityEngine;

public abstract class RepairableObject<TData> : InteractableObject<TData>
    where TData : RepairableInteractableSO
{
    protected bool _isBroken;

    protected override void Awake() {
        _isBroken = _data.StartsBroken; // must be set before the base call spawns visuals
        base.Awake();
    }

    protected override GameObject GetVisualPrefab() =>
        _isBroken && _data.WorldPrefabDamaged != null
            ? _data.WorldPrefabDamaged
            : _data.WorldPrefab;

    protected bool EnsureRepaired(PlayerInteractor interactor) {
        if (!_isBroken) return true;

        if (interactor.Wallet.TrySpendScrap(_data.RepairCostInScrap, _data.UseAudioClip)) {
            _isBroken = false;
            RefreshVisuals(); // swap damaged for repaired
            Debug.Log($"Repaired {_data.DisplayName}.");
        }
        else {
            Debug.Log($"Need {_data.RepairCostInScrap} scrap to repair {_data.DisplayName}.");
        }
        return false; // the repair used up this interaction
    }
}