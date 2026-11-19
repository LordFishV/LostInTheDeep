using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField, Min(1)] private int _maxScrap = 100;
    [SerializeField, Range(0f, 1f)] private float _volume = 0.75f;

    public int Scrap { get; private set; }
    public int MaxScrap => _maxScrap;
    public bool IsFull => Scrap >= _maxScrap;
    public bool IsEmpty => Scrap <= 0;

    public event Action<int> ScrapChanged;

    private void Reset() => _audioSource = GetComponent<AudioSource>();

    public bool TryAddScrap(AudioClip audioClip) {
        if (IsFull) return false;

        Scrap++;
        PlayClip(audioClip);
        ScrapChanged?.Invoke(Scrap);
        return true;
    }

    public bool TrySpendScrap(AudioClip audioClip) {
        if (IsEmpty) return false;

        Scrap--;
        PlayClip(audioClip);
        ScrapChanged?.Invoke(Scrap);
        return true;
    }

    private void PlayClip(AudioClip audioClip) {
        if (audioClip != null && _audioSource != null)
            _audioSource.PlayOneShot(audioClip, _volume);
    }
}