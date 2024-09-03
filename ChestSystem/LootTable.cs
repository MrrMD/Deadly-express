using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New LootTable", menuName = "Chest/LootTable")]
public class LootTable : ScriptableObject
{
    [SerializeField] private List<ChestItem> lootList;

    public List<ChestItem> LootList { get => lootList; set => lootList = value; }
}