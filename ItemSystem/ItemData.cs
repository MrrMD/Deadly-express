using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData : ScriptableObject
{
    #region Fields

    [Header("Item Settings")]
    [SerializeField] private string itemName = "Item";
    [SerializeField] private Sprite itemIcon = null;
    [SerializeField] private GameObject itemPrefab = null;
    [SerializeField] private bool isUsable = false;
    [SerializeField] private List<ItemData> craftItems = new List<ItemData>();
    [SerializeField] private bool isLootable = false;
    [SerializeField] private bool isConsumable = false;
    [SerializeField] private int stackSize = 1;

    #endregion

    #region Properties

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    public Sprite ItemIcon
    {
        get { return itemIcon; }
        protected set { itemIcon = value; }
    }

    public GameObject ItemPrefab
    {
        get { return itemPrefab; }
        protected set { itemPrefab = value; }
    }

    public bool IsUsable
    {
        get { return isUsable; }
        protected set { isUsable = value; }
    }

    public bool IsLootable
    {
        get { return isLootable; }
        protected set { isLootable = value; }
    }

    public bool IsConsumable
    {
        get { return isConsumable; }
        protected set { isConsumable = value; }
    }

    public int StackSize
    {
        get { return stackSize; }
        protected set { stackSize = value; }
    }

    public List<ItemData> CraftItems { get => craftItems; }

    #endregion

   
}