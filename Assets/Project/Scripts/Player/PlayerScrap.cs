using UnityEngine;

public class PlayerScrap : MonoBehaviour
{
    public int Scrap { get; private set; }

    public void AddScrap(int amount) => Scrap += amount;

    public bool TrySpendScrap(int amount)
    {
        if (Scrap < amount) return false;
        Scrap -= amount;
        return true;
    }
}