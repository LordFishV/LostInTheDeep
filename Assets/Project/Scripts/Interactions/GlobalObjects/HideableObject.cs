using UnityEngine;
using UnityEngine.UIElements;

public abstract class HideableObject<TData> : InteractableObject<TData>
    where TData : HideableInteractableSO
{
    [Header("Instance")]
    public bool IsPlayerHidingIn;

    protected override void Awake() {
        base.Awake();
    }



    //protected override GameObject GetVisualPrefab() =>

    protected bool HideState() {

    }
}