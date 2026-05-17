using UnityEngine;

[System.Serializable]
public class SlotSymbol
{
    public string symbolName;
    public Sprite sprite;
    public int payoutMultiplier;
}

[CreateAssetMenu(fileName = "ReelConfig", menuName = "SlotMachine/ReelConfig")]
public class ReelConfig : ScriptableObject
{
    public SlotSymbol[] symbolPool;
    public float symbolHeight = 150f;
    public float spinSpeed = 1800f;
    public float spinDuration = 2f;
}