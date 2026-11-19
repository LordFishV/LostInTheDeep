using UnityEngine;

[CreateAssetMenu(menuName = "Interactables/Pick Up", fileName = "New Pick Up")]
public class PickUpSO : InteractableSO
{
    [SerializeField] private Sprite _icon;

    public Sprite Icon => _icon;
}