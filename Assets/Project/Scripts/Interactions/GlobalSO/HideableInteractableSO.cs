using UnityEngine;

public abstract class HideableInteractableSO : InteractableSO
{
    [Header("Global Hide Interactable Variables")]
    [SerializeField] private AudioClip _hideAudioClip;


    public AudioClip HideAudioClip => _hideAudioClip;

}