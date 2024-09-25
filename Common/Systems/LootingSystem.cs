using Mirror;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class LootingSystem : NetworkBehaviour
{
    [SerializeField] Inventory curentInventory;
    [SyncVar]
    [SerializeField] private bool isLooted = false;
    [SyncVar]
    [SerializeField] private uint isLootedPlayerId;
    [SerializeField] private SphereCollider playerExitTrigerCollider;

    public override void OnStartClient()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        base.OnStartClient();

    }

    private void Start()
    {
        playerExitTrigerCollider = GetComponent<SphereCollider>();
        curentInventory = GetComponent<Inventory>();
    }

    public void GetLooted(uint identityId)
    {
        if(isLooted) return;
        Debug.Log("Get looted");
        if (GetComponent<HealthSystem>() != null && GetComponent<HealthSystem>().IsAlive)
        {
            Debug.Log("Get looted return");
            return;
        }
        CmdSetLootingActive(identityId);
        LootUI.Instance.Open(curentInventory);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetLootingActive(uint identityId)
    {
        isLooted = true;
        isLootedPlayerId = identityId;
        playerExitTrigerCollider.enabled = true;
    }

    [Command(requiresAuthority = false)]
    public void CmdSetLootingInactive()
    {
        NetworkIdentity playerIdentity = NetworkServer.spawned[isLootedPlayerId];
        if (playerIdentity != null)
        {
            playerIdentity.GetComponent<Inventory>().RpcCloseLootUI();
        }
        isLootedPlayerId = 0;
        isLooted = false;
        playerExitTrigerCollider.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NetworkIdentity>().netId == isLootedPlayerId)
        {
            CmdSetLootingInactive();
        }
    }

    private void LootTo()
    {
        Debug.Log("Loot to");
        RaycastHit hit;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit, 2f))
        {
            LootingSystem lootingSystem = hit.collider.GetComponent<LootingSystem>();
            if (lootingSystem != null)
            {
                lootingSystem.GetLooted(NetworkClient.localPlayer.netId);
                return;
            }
            if (hit.collider.GetComponent<Item>() != null)
            {
                Item Item = hit.collider.GetComponent<Item>();
                curentInventory.CmdAddItem(Item);
                return;
            }
        }
    }

    public void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("E")))
        {
            Debug.Log("E");
            LootTo();
        }
    }

}
