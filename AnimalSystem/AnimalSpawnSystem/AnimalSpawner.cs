using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Utils;

namespace AnimalSystem.AnimalSpawnSystem
{
    public class AnimalSpawner : NetworkBehaviour
    {
        [SerializeField] private AnimalSpawnPoint[] animalSpawnPoints;
        [SerializeField] private int totalAnimalCountOnTheMap;

        public override void OnStartClient()
        {
            base.OnStartClient();
            if (!isServer)
            {
                Destroy(this);
            }
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            animalSpawnPoints = GetComponentsInChildren<AnimalSpawnPoint>();
            
            //Вынести в метод
            foreach (var point in animalSpawnPoints)
            {
                SpawnAnimal(point);
            }
        }

        [Server]
        private void SpawnAnimal(AnimalSpawnPoint point)
        {
            var animalsForSpawn = new List<GameObject>();
            if (point.isWolfSpawnPoint)
            {
                animalsForSpawn.Add(Resources.Load<GameObject>(AnimalSystemConstants.WOLF_PREFAB_PATH));
            }
            
            var animalCount = Random.Range(point.minAnimalCount, point.maxAnimalCount);
            for (int i = 0; i < animalCount; i++)
            {
                var number = Random.Range(0, animalsForSpawn.Count-1);
                var animalforSpawn = animalsForSpawn[number];
                if (animalforSpawn == null)
                {
                    Debug.Log("Animal for spawn == null");
                    return;
                }
                
                var itemInstance = Instantiate(animalforSpawn, point.transform, true);

                // Спавн на всех клиентах
                NetworkServer.Spawn(itemInstance);
                
                totalAnimalCountOnTheMap++;
            }
        }
    }
}