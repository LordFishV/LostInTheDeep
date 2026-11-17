using UnityEngine;

public abstract class InteractableSO : ScriptableObject
{
    [SerializeField] private string _displayName;
    [SerializeField] private string _promptText = "Interact";
    [SerializeField] private GameObject _worldPrefab;
    [SerializeField] private AudioClip _useAudioClip;

    public string DisplayName => _displayName;
    public string PromptText => _promptText;
    public GameObject WorldPrefab => _worldPrefab;
    public AudioClip UseAudioClip => _useAudioClip;
}

public abstract class RepairableInteractableSO : InteractableSO
{
    [Header("Repair")]
    [SerializeField] private bool _startsBroken;
    [SerializeField, Min(0)] private int _repairCostInScrap = 5;
    [SerializeField] private GameObject _worldPrefabDamaged;

    public bool StartsBroken => _startsBroken;
    public int RepairCostInScrap => _repairCostInScrap;
    public GameObject WorldPrefabDamaged => _worldPrefabDamaged;
}

[CreateAssetMenu(menuName = "Interactables/Pick Up", fileName = "New Pick Up")]
public class PickUpSO : InteractableSO
{
    [SerializeField] private Sprite _icon;
    [SerializeField, Min(1)] private int _amount = 1;
    [SerializeField] private bool _isScrap; // scrap goes to the currency wallet, everything else to inventory
    

    public Sprite Icon => _icon;
    public int Amount => _amount;
    public bool IsScrap => _isScrap;
    


}

[CreateAssetMenu(menuName = "Interactables/Door", fileName = "New Door")]
public class DoorSO : RepairableInteractableSO
{
    [SerializeField] private bool _startsOpen;
    [SerializeField] private float _openDuration = 0.5f;

    public bool StartsOpen => _startsOpen;
    public float OpenDuration => _openDuration;
}

[CreateAssetMenu(menuName = "Interactables/Computer", fileName = "New Computer")]
public class ComputerSO : InteractableSO
{
    [SerializeField] private string _loreTitle;
    [SerializeField, TextArea(5, 20)] private string _loreBody;

    public string LoreTitle => _loreTitle;
    public string LoreBody => _loreBody;
}

[CreateAssetMenu(menuName = "Interactables/Generator", fileName = "New Generator")]
public class GeneratorSO : RepairableInteractableSO
{
    [SerializeField] private float _chargePerSecond = 10f;
    [SerializeField] private float _repairPerSecond = 5f;

    public float ChargePerSecond => _chargePerSecond;
    public float RepairPerSecond => _repairPerSecond;
}