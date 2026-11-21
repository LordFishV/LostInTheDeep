using UnityEngine;

[CreateAssetMenu(menuName = "Interactables/Hiding Spot", fileName = "New Hiding Spot")]
public class HidingSpotSO : InteractableSO
{
    [Header("Defaults (instances can override)")]
    [SerializeField] private HidingVisibility _visibility = HidingVisibility.Invisible;

    public HidingVisibility Visibility => _visibility;
}