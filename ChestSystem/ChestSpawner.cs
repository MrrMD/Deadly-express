using Mirror;
using NUnit.Framework.Interfaces;
using UnityEngine;

public class ChestSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject commonChest;
    [SerializeField] private GameObject commonChest2;
    [SerializeField] private GameObject foodChest;
    [SerializeField] private GameObject weaponChest;
    [SerializeField] private GameObject medChest;

    public override void OnStartServer()
    {
        ServerSpawnItem(new Vector3(2.5f, 0.264f, 3));
    }

    [Server]
    public void ServerSpawnItem(Vector3 spawnPosition)
    {
        if (commonChest2)
        {
            GameObject itemInstance = Instantiate(commonChest2, spawnPosition, Quaternion.identity);

            NetworkServer.Spawn(itemInstance);
            Debug.Log("Заспавнен");
        }
        else
        {
            Debug.LogWarning("Префаб не задан в ScriptableObject.");
        }
    }
}
