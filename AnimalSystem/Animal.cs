using AnimalSystem.AnimalSpawnSystem;
using InventorySystem;
using Mirror;
using UnityEngine;

namespace AnimalSystem
{
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(Inventory))]
    public class Animal : MonoBehaviour
    {
        [SerializeField] private AnimalData animalData;
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private Inventory inventory;

        public HealthSystem HealthSystem { get => healthSystem; set => healthSystem = value; }
        public AnimalData AnimalData { get => animalData;}


        private void Start()
        {
            inventory = GetComponent<Inventory>();
            healthSystem = GetComponent<HealthSystem>();
        }
    }
}
