using UnityEngine;

[CreateAssetMenu(menuName = "Interactables/Scrap", fileName = "New Scrap")]
public class ScrapSO : InteractableSO
{
    [SerializeField] private Sprite _icon;

    public Sprite Icon => _icon;
}
