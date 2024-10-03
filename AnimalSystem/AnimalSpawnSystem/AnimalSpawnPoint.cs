using Mirror;
using UnityEngine;

namespace AnimalSystem.AnimalSpawnSystem
{
    public class AnimalSpawnPoint : MonoBehaviour
    {
        [SerializeField] public bool isWolfSpawnPoint = false;
        [SerializeField] public  bool isChickenSpawnPoint = false;
        [SerializeField] public  float spawnRadius;
        [SerializeField] public  int totalAnimalCount;
        [SerializeField] public  int minAnimalCount;
        [SerializeField] public  int maxAnimalCount;
    }
}