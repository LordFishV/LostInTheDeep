using UnityEngine;

public abstract class RepairableObject<TData> : InteractableObject<TData>
    where TData : RepairableInteractableSO
{
    [Header("Instance")]
    [SerializeField] private bool _startsBroken;

    protected bool _isBroken;

    protected override void Awake() {
        _isBroken = _startsBroken; // before base.Awake spawns visuals
        base.Awake();
    }

    protected override GameObject GetVisualPrefab() =>
        _isBroken && _data.WorldPrefabDamaged != null
            ? _data.WorldPrefabDamaged
            : _data.WorldPrefab;

    protected bool EnsureRepaired(PlayerInteractor interactor) {
        if (!_isBroken) return true;

        if (interactor.Wallet.TrySpendScrap(_data.RepairAudioClip)) {
            _isBroken = false;
            RefreshVisuals();
        }
        else {
            Debug.Log("Need 1 scrap to repair.");
        }
        return false; // the repair used this interaction
    }
}