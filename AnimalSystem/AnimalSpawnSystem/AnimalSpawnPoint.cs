using Mirror;
using UnityEngine;

namespace AnimalSystem.AnimalSpawnSystem
{
    public class AnimalSpawnPoint : MonoBehaviour
    {
        public AnimalType spawnableAnimals = AnimalType.None;
        
        [SerializeField] private int animalCount;
        [SerializeField] private int minAnimalCount;
        [SerializeField] private int maxAnimalCount;
        [SerializeField] private float spawnRadius;
        
        public int AnimalCount { get => animalCount;}        
        public float SpawnRadius { get => spawnRadius;}
        public int MinAnimalCount { get => minAnimalCount;}
        public int MaxAnimalCount { get => maxAnimalCount;}




    }
}