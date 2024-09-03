using Mirror;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class LootingSystem : NetworkBehaviour
{
    [SerializeField] Inventory curentInventory;

    private void Start()
    {
        curentInventory = GetComponent<Inventory>();
        if (!isLocalPlayer)
        {
            return;
        }
    }
    public void GetLooted()
    {
        Debug.Log("Get looted");
        curentInventory = GetComponent<Inventory>();
        if (GetComponent<HealthSystem>() != null && GetComponent<HealthSystem>().IsAlive)
        {
            Debug.Log("Get looted return");
            return;
        }
        LootUI.Instance.Open(curentInventory);
    }

    private void LootTo()
    {
        Debug.Log("Loot to");
        RaycastHit hit;

        // Создание луча из центра экрана
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit, 5f))
        {
            LootingSystem lootingSystem = hit.collider.GetComponent<LootingSystem>();
            if (lootingSystem != null)
            {
                Debug.Log("Interact system != null");
                lootingSystem.GetLooted();
                return;
            }
            else if (hit.collider.GetComponent<Item>() != null)
            {
                Debug.Log("InventoryItem != null");
                Item Item = hit.collider.GetComponent<Item>();
                curentInventory.CmdAddItem(Item);
            }
            return;
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            LootTo();
        }
    }
}
