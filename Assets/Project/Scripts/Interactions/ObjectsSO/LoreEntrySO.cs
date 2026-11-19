// LoreEntrySO.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Interactables/Lore Entry", fileName = "New Lore Entry")]
public class LoreEntrySO : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField, TextArea(5, 20)] private string _body;

    public string Title => _title;
    public string Body => _body;
}