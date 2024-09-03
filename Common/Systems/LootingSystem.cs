using Mirror;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class LootingSystem : NetworkBehaviour
{
    [SerializeField] Inventory curentInventory;

    public override void OnStartClient()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        base.OnStartClient();

        curentInventory = GetComponent<Inventory>();
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

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit, 5f))
        {
            LootingSystem lootingSystem = hit.collider.GetComponent<LootingSystem>();
            if (lootingSystem != null)
            {
                lootingSystem.GetLooted();
                return;
            }
            else if (hit.collider.GetComponent<Item>() != null)
            {
                Item Item = hit.collider.GetComponent<Item>();
                curentInventory.CmdAddItem(Item);
                return;
            }
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
