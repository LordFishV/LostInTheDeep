using UnityEngine;

[CreateAssetMenu(menuName = "Interactables/Generator", fileName = "New Generator")]
public class GeneratorSO : RepairableInteractableSO
{
    [Header("Defaults (instances can override)")]
    [SerializeField, Min(0f)] private float _maxStoredCharge = 100f;
    [SerializeField, Min(1f)] private float _chargePerUse = 25f;


    public float MaxStoredCharge => _maxStoredCharge;
    public float ChargePerUse => _chargePerUse;


    [Header("Defaults (instances can not override)")]
    [SerializeField] private AudioClip _repairAmbientAudio;
    [SerializeField] private AudioClip _ambientAudio;

    public AudioClip AmbientAudio => _ambientAudio;
    private AudioClip RepairAmbientAudio => _repairAmbientAudio;
}
