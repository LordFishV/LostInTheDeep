// Computer.cs
using UnityEngine;

public class Computer : InteractableObject<ComputerSO>
{
    [Header("Instance")]
    [SerializeField] private LoreEntrySO _lore;
    //[SerializeField] private LoreScreen _loreScreen; // your UI element

    public override void Interact(PlayerInteractor interactor) {/*
        if (_lore == null || _loreScreen == null)
        {
            Debug.LogWarning($"{name} has no lore entry or lore screen assigned.", this);
            return;
        }

        PlayUseSound();
        _loreScreen.Show(_lore.Title, _lore.Body);*/
    }

    public override void OnMouseHoverEnter() {

    }
    public override void OnMouseHoverExit() {

    }
}