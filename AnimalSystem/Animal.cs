using Mirror;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(Inventory))]
public class Animal : NetworkBehaviour, IEntity
{
    [Header("Animal Settings")]
    [SerializeField] private bool isEnemy;
    [SerializeField] private int meatCount;

    [SerializeField] private HealthSystem healthSystem;

    [SerializeField] private float atackValue;

    [SerializeField] private Inventory inventory;

    public float AtackValue { get => atackValue; set => atackValue = value; }
    public HealthSystem HealthSystem { get => healthSystem; set => healthSystem = value; }

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        healthSystem = GetComponent<HealthSystem>();
    }
}
