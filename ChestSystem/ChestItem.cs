using Mirror;
using UnityEngine;

[System.Serializable]
public class ChestItem
{
    [SerializeField] private Item item;
    [SerializeField] private int weight;
    [SerializeField] private int minCount;
    [SerializeField] private int maxCount;
    [SerializeField] private NetworkIdentity myChest;

    public Item Item { get => item; set => item = value; }
    public int Weight { get => weight; set => weight = value; }
    public int MinCount { get => minCount; set => minCount = value; }
    public int MaxCount { get => maxCount; set => maxCount = value; }
}
