using UnityEngine;

[CreateAssetMenu(menuName = "Interactables/Door", fileName = "New Door")]
public class DoorSO : RepairableInteractableSO
{
    [Header("Defaults (instances can override)")]
    [SerializeField, Min(0.01f)] private float _openDuration = 0.5f;
    [SerializeField] private float _openAngle = 90f;


    public float OpenDuration => _openDuration;
    public float OpenAngle => _openAngle;

}