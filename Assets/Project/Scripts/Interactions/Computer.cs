using UnityEngine;

public class Computer : InteractableObject<ComputerSO>
{
    //[SerializeField] private LoreScreen _loreScreen; // UI element

    public override void Interact(PlayerInteractor interactor) {
       // _loreScreen.Show(_data.LoreTitle, _data.LoreBody);
    }
}