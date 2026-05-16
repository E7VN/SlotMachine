using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SlotSymbol
{
    public string symbolName;
    public Sprite sprite;
    public int payoutMultiplier; // e.g., 7=10x, Cherry=2x, Bar=5x
}