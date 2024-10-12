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
                var number = Random.Range(0, animals.Length - 1);
                var animalForSpawn = animalsForSpawn[number];
                if (animalForSpawn == null)
                {
                    Debug.Log("Animal for spawn == null");
                    return;
                }

                // Попытка найти подходящую позицию для спавна
                Vector3 spawnPosition = GetValidSpawnPosition(point);
                
                if (spawnPosition == Vector3.zero)
                {
                    Debug.Log("Failed to find a valid spawn position.");
                    continue;
                }

                Debug.Log(spawnPosition);

                var itemInstance = Instantiate(animalForSpawn.gameObject, spawnPosition, Quaternion.identity);

                // Спавн на всех клиентах
                NetworkServer.Spawn(itemInstance);
                totalAnimalCountOnTheMap++;
            }
        }

        // Метод для выбора валидной позиции для спавна
        private Vector3 GetValidSpawnPosition(AnimalSpawnPoint point)
        {
            Vector3 spawnPosition;
            int attempts = 0;
            const int maxAttempts = 10; // Количество попыток найти свободную позицию

            do
            {
                // Генерация позиции для спавна в зависимости от радиуса
                if (point.SpawnRadius > 0)
                {
                    // Спавним случайным образом в пределах радиуса
                    Vector3 randomPosition = Random.insideUnitSphere * point.SpawnRadius;
                    randomPosition.y = 0; // Убираем смещение по высоте, чтобы спавнить на плоскости
                    spawnPosition = point.transform.position + randomPosition;
                }
                else
                {
                    // Спавним на самом поинте
                    spawnPosition = point.transform.position;
                }

                // Проверяем, свободна ли эта позиция от других объектов
                if (Physics.CheckSphere(spawnPosition, 0.5f))
                {
                    // Если что-то есть на месте спавна, пробуем заново
                    attempts++;
                    continue;
                }

                // Проверка, есть ли земля под спавн-точкой
                if (Physics.Raycast(spawnPosition + Vector3.up * 10, Vector3.down, out RaycastHit hit, 20f))
                {
                    // Корректируем позицию по высоте, чтобы спавнить на поверхности
                    spawnPosition = hit.point;
                    return spawnPosition; // Возвращаем валидную позицию
                }

                attempts++;

            } while (attempts < maxAttempts);

            return Vector3.zero; // Если не удалось найти подходящую позицию
        }

        [Server]
        private Animal[] SelectAnimalsForSpawn(AnimalSpawnPoint point)
        {
            return animals.Where(animal => AnimalSpawnHelper.CanSpawnThisAnimal(point, animal)).ToArray();
        }
    }
}
