using UnityEngine;

public abstract class RepairableInteractableSO : InteractableSO
{
    [Header("Global Repair Interactable Variables")]
    [SerializeField] private GameObject _worldPrefabDamaged;
    [SerializeField] private AudioClip _repairAudioClip;


    public GameObject WorldPrefabDamaged => _worldPrefabDamaged;
    public AudioClip RepairAudioClip => _repairAudioClip;

}