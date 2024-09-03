using Mirror;
using UnityEngine;


public class PlayerItemSystem : NetworkBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ItemData activeItem;

    private void Start()
    {
        player = Player.Instance;
    }

    public ItemData ActiveItem
    {
        get { return activeItem; }
        private set { activeItem = value; }
    }

    public void CmdUseItem(IUsable item)
    {
        item.Use();
    }

    public void CmdDropItem(InventoryItem droppedItem)
    {
        player.Inventory.CmdRemoveItem(droppedItem);
    }

    public void CmdTakeItem(Item takenItem)
    {
        player.Inventory.CmdAddItem(takenItem);
    }
}