using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.XR;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int _maxScrap;

    public int Scrap;
    public int MaxScrap => _maxScrap;
    private void Reset() => _audioSource = GetComponent<AudioSource>();

    public void AddScrap(int amount, GameObject gameObject, AudioClip audioClip) {
        if (!(Scrap < MaxScrap)) return;

        if (audioClip != null) {
            _audioSource.PlayOneShot(audioClip, 0.75f);
        }

        Scrap += amount;
        Destroy(gameObject);
    }

    public bool TrySpendScrap(int amount, AudioClip audioClip) {
        if (Scrap < amount) return false;

        if (audioClip != null) {
            _audioSource.PlayOneShot(audioClip, 0.75f);
        }

        Scrap -= amount;
        return true;
    }
}