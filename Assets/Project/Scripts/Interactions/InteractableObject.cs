using UnityEngine;

public abstract class InteractableObject<TData> : MonoBehaviour, IInteractable
    where TData : InteractableSO
{
    [SerializeField] protected TData _data;
    [SerializeField] private Transform _visualRoot; // falls back to this transform
    [SerializeField] private GameObject _placeholder;
    [SerializeField] private float _openDuration;
    [SerializeField] private Vector3 _slideOffset;
    [SerializeField] private bool _isSlidingDoor;

    private GameObject _visualInstance;

    public string PromptText => _data.PromptText;
    public abstract void Interact(PlayerInteractor interactor);

    public void OnMouseHoverEnter() {
        throw new System.NotImplementedException();
    }

    public void OnMouseHoverExit() {
        throw new System.NotImplementedException();
    }

    protected virtual void Awake() => RefreshVisuals();

    protected virtual GameObject GetVisualPrefab() => _data.WorldPrefab;
    protected float OpenDuration => _openDuration;
    protected Vector3 SlideOffset => _slideOffset;
    protected bool IsSlidingDoor => _isSlidingDoor;

    protected void RefreshVisuals() {
        if (_visualInstance != null)
            Destroy(_visualInstance);

        GameObject prefab = GetVisualPrefab();
        if (prefab == null) return;

        Destroy(_placeholder);

        Transform parent = _visualRoot != null ? _visualRoot : transform;
        _visualInstance = Instantiate(prefab, parent); // keeps the prefab's local position and rotation
    }
}