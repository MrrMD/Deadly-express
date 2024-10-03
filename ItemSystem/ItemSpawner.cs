using Mirror;
using UnityEngine;

namespace ItemSystem
{
    public class ItemSpawner : NetworkBehaviour
    {
        public static ItemSpawner Instance { get; private set; }


        public ItemData itemData;
        public ItemData itemData2;

        private void Awake()
        {

            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            itemData = Resources.Load<ItemData>("ScriptableObjects/items/Coal");
            if (itemData == null)
            {
                Debug.LogError("Item not found in Resources folder!");
            }
            itemData2 = Resources.Load<ItemData>("ScriptableObjects/items/Wood");
            if (itemData == null)
            {
                Debug.LogError("Item not found in Resources folder!");
            }
        }
        public override void OnStartClient()
        {
            ServerSpawnItem(itemData, new Vector3(0, 0.109f, 0));
            ServerSpawnItem(itemData, new Vector3(1, 0.109f, 0));
            ServerSpawnItem(itemData, new Vector3(2, 0.109f, 0));
            ServerSpawnItem(itemData, new Vector3(3, 0.109f, 0));
            ServerSpawnItem(itemData, new Vector3(4, 0.109f, 0));
            ServerSpawnItem(itemData, new Vector3(5, 0.109f, 0));

            ServerSpawnItem(itemData2, new Vector3(0, 0.0699f, -1));
            ServerSpawnItem(itemData2, new Vector3(1, 0.0699f, -1));
            ServerSpawnItem(itemData2, new Vector3(2, 0.0699f, -1));
            ServerSpawnItem(itemData2, new Vector3(3, 0.0699f, -1));
            ServerSpawnItem(itemData2, new Vector3(4, 0.0699f, -1));
            ServerSpawnItem(itemData2, new Vector3(5, 0.0699f, -1));
        }

        [Server]
        public void ServerSpawnItem(ItemData data, Vector3 spawnPosition)
        {
            if (itemData.ItemPrefab != null)
            {
                // Спавн префаба на сервере
                GameObject itemInstance = Instantiate(data.ItemPrefab, spawnPosition, data.ItemPrefab.transform.rotation);

                // Спавн на всех клиентах
                NetworkServer.Spawn(itemInstance);
                Debug.Log("Заспавнен");
            }
            else
            {
                Debug.LogWarning("Префаб не задан в ScriptableObject.");
            }
        }

        [Server]
        public void ServerSpawnItemByData(ItemData data, int count, Vector3 position)
        {
            for(int i  = 0; i < count; i++)
            {
                // Спавн префаба на сервере

                Debug.Log(data + " " + count + " " + position);
                GameObject itemInstance = Instantiate(data.ItemPrefab, position + Vector3.down * (i * 0.1f), data.ItemPrefab.transform.rotation);

                // Спавн на всех клиентах
                NetworkServer.Spawn(itemInstance);
                Debug.Log("Заспавнен");
            }

        }
    }
}
