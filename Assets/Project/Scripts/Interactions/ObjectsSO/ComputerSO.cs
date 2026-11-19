using UnityEngine;

[CreateAssetMenu(menuName = "Interactables/Computer", fileName = "New Computer")]
public class ComputerSO : InteractableSO
{
    [Header("Defaults (instances can not override)")]
    [SerializeField] private AudioClip _ambientAudio;

    public AudioClip AmbientAudio => _ambientAudio;
}