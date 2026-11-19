using UnityEngine;

public abstract class InteractableSO : ScriptableObject
{
    [Header("Global Interactable Variables")]
    [SerializeField] private string _displayName;
    [SerializeField] private string _promptText = "Interact";
    [SerializeField] private GameObject _worldPrefab;
    [SerializeField] private AudioClip _interactAudioClip;

    public string DisplayName => _displayName;
    public string PromptText => _promptText;
    public GameObject WorldPrefab => _worldPrefab;
    public AudioClip InteractAudioClip => _interactAudioClip;
}