using UnityEngine;

public abstract class InteractableObject<TData> : MonoBehaviour, IInteractable
    where TData : InteractableSO
{
    [SerializeField] protected TData _data;

    public string PromptText => _data.PromptText;
    public abstract void Interact(PlayerInteractor interactor);

    public void OnMouseHoverEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnMouseHoverExit()
    {
        throw new System.NotImplementedException();
    }
}