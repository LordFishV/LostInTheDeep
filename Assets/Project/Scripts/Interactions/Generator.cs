// Generator.cs
using UnityEngine;

public class Generator : RepairableObject<GeneratorSO>
{
    [SerializeField] private Override<float> _maxStartingCharge;
    [SerializeField] private Override<float> _chargePerUse;

    private float _storedCharge;

    protected override void Awake() {
        base.Awake();
        _storedCharge = _maxStartingCharge.Resolve(_data.MaxStoredCharge);
    }

    public override void Interact(PlayerInteractor interactor) {
        if (!EnsureRepaired(interactor)) return;

        IChargeable gear = interactor.Gear;
        if (gear == null) return;

        if (_storedCharge <= 0f) {
            Debug.Log($"{_data.DisplayName} is empty.");
            return;
        }

        float offered = Mathf.Min(_chargePerUse.Resolve(_data.ChargePerUse), _storedCharge);
        float accepted = gear.AddCharge(offered);

        if (accepted <= 0f) {
            Debug.Log("Gear is already fully charged.");
            return; // don't drain the generator for nothing
        }

        _storedCharge -= accepted;
        PlayUseSound();
    }

    public override void OnMouseHoverEnter() {

    }
    public override void OnMouseHoverExit() {

    }
}