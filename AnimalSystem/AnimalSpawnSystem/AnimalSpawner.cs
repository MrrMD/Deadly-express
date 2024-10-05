using System.Collections.Generic;
using System.Linq;
using AnimalSystem.AnimalSpawnSystem.Utils;
using Mirror;
using UnityEngine;
using Utils;

namespace AnimalSystem.AnimalSpawnSystem
{
    public class AnimalSpawner : NetworkBehaviour
    {
        [SerializeField] private AnimalSpawnPoint[] animalSpawnPoints;
        [SerializeField] private GameObject animalSpawnPointsParent;
        [SerializeField] private Animal[] animals;
        [SerializeField] private int totalAnimalCountOnTheMap = 0;

        public override void OnStartClient()
        {
            if (!isServer)
            {
                Destroy(this);
            }
            base.OnStartClient();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            animalSpawnPoints = animalSpawnPointsParent.GetComponentsInChildren<AnimalSpawnPoint>();
            
            //Вынести в метод
            foreach (var point in animalSpawnPoints)
            {
                SpawnAnimal(point);
            }
        }

        [Server]
        private void SpawnAnimal(AnimalSpawnPoint point)
        {
            var animalsForSpawn = SelectAnimalsForSpawn(point);
            
            if (animalsForSpawn == null || animalsForSpawn.Length == 0)
            {
                Debug.Log("No animals available for spawn at this point.");
                return;
            }

            var animalCount = Random.Range(point.MinAnimalCount, point.MaxAnimalCount);
            for (var i = 0; i < animalCount; i++)
            {
                var number = Random.Range(0, animals.Length-1);
                var animalForSpawn = animalsForSpawn[number];
                if (animalForSpawn == null)
                {
                    Debug.Log("Animal for spawn == null");
                    return;
                }

                var itemInstance = Instantiate(animalForSpawn.gameObject, point.transform, true);

                // Спавн на всех клиентах
                NetworkServer.Spawn(itemInstance);
                totalAnimalCountOnTheMap++;
            }
        }

        private Animal[] SelectAnimalsForSpawn(AnimalSpawnPoint point)
        {

            return animals.Where(animal => AnimalSpawnHelper.CanSpawnThisAnimal(point, animal)).ToArray();

        }
        
    }
}