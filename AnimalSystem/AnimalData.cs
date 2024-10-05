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
        [FormerlySerializedAs("animalName")]
        [Header("Animal Settings")]
        [SerializeField] private AnimalType animalType; 
        [SerializeField] private bool isEnemy;
        [SerializeField] private int meatCount;
        [SerializeField] private float atackValue;
        [SerializeField] private float atackRange;
        
        public AnimalType AnimalType { get => animalType;}
        public bool IsEnemy { get => isEnemy;}
        public int MeatCount { get => meatCount;}
        public float AtackValue { get => atackValue;}
        public float AtackRange { get => atackRange;}

    }
}