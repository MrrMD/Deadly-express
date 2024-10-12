using Mirror;
using System;
using UnityEngine;

public class PlayerPoisonSystem : NetworkBehaviour
{

    [SerializeField] private Player.Player player;

    private void Start()
    {
        player = GetComponent<Player.Player>();
    }

    public void Poison(float poisonTime)
    {
    }

}
