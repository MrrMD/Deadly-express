using Mirror;
using System;
using UnityEngine;

public class PlayerPoisonSystem : NetworkBehaviour
{

    [SerializeField] private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void Poison(float poisonTime)
    {
    }

}
