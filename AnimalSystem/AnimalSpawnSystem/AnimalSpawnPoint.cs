using Mirror;
using UnityEngine;

namespace AnimalSystem.AnimalSpawnSystem
{
    public class AnimalSpawnPoint : MonoBehaviour
    {
        public AnimalType spawnableAnimals = AnimalType.None;
        
        [SerializeField] private bool isWolfSpawnPoint = false;
        [SerializeField] private bool isChickenSpawnPoint = false;
        [SerializeField] private float spawnRadius;
        [SerializeField] private int animalCount;
        [SerializeField] private int minAnimalCount;
        [SerializeField] private int maxAnimalCount;
        
        public bool IsWolfSpawnPoint { get => isWolfSpawnPoint;}
        public bool IsChickenSpawnPoint { get => isChickenSpawnPoint;}
        public int AnimalCount { get => animalCount;}
        public int MinAnimalCount { get => minAnimalCount;}
        public int MaxAnimalCount { get => maxAnimalCount;}




    }
}