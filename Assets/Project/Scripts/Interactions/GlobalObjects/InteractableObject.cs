using UnityEngine;

public abstract class InteractableObject<TData> : MonoBehaviour, IInteractable
    where TData : InteractableSO
{
    [SerializeField] protected TData _data;
    [SerializeField] private Transform _visualRoot; // falls back to this transform
    [SerializeField] private GameObject _placeholder;

    private GameObject _visualInstance;

    public string PromptText => _data.PromptText;

    // IInteractable Functions
    public abstract void Interact(PlayerInteractor interactor);
    public abstract void OnMouseHoverEnter();
    public abstract void OnMouseHoverExit();


    protected virtual void Awake() => RefreshVisuals();

    protected virtual GameObject GetVisualPrefab() => _data.WorldPrefab;

    protected void RefreshVisuals() {
        if (_visualInstance != null)
            Destroy(_visualInstance);

        GameObject prefab = GetVisualPrefab();
        if (prefab == null) return;

        Destroy(_placeholder);

        Transform parent = _visualRoot != null ? _visualRoot : transform;
        _visualInstance = Instantiate(prefab, parent); // keeps the prefab's local position and rotation
    }

    protected void PlayUseSound() {
        if (_data.InteractAudioClip != null)
            AudioSource.PlayClipAtPoint(_data.InteractAudioClip, transform.position);
    }
}