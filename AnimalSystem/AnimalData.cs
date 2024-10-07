using System;
using AnimalSystem.AnimalSpawnSystem.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnimalSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "AnimalData", menuName = "AnimalSystem/AnimalData", order = 1)]
    public class AnimalData : ScriptableObject
    {
        [Header("Animal base settings")]
        [SerializeField] private AnimalType animalType; 
        [SerializeField] private bool isEnemy;
        [SerializeField] private int meatCount;
        
        [Header("Animal atack settings")]
        [SerializeField] private float attackDamage;

        [SerializeField] private float attackRadius;
        [SerializeField] private float detectionRadius;
        [SerializeField] private float attackCooldown;
        
        [Header("Animal navmesh settings")]
        [SerializeField] private float wanderRadius = 10f;  
        [SerializeField] private float wanderTimer = 5f;   
        
        public AnimalType AnimalType { get => animalType;}
        public bool IsEnemy { get => isEnemy;}
        public int MeatCount { get => meatCount;}
        public float AttackDamage { get => attackDamage;}
        public float AttackRadius { get => attackRadius;}
        public float DetectionRadius { get => detectionRadius; }
        public float AttackCooldown { get => attackCooldown; }
        
        public float WanderRadius { get => wanderRadius;}
        public float WanderTimer { get => wanderTimer;}
    }
}