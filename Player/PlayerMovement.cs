using Mirror;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private float moveSpeed; // Скорость движения персонажа
    [SerializeField] private bool isPlayerLooting = false;

    private CharacterController controller;

    public bool IsPlayerLooting { get => isPlayerLooting; set => isPlayerLooting = value; }

    private void Start()
    {
        moveSpeed = 5f;
        controller = GetComponent<CharacterController>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerLooting)
        {
            isPlayerLooting = false;
        }

        if (isPlayerLooting) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        moveDirection.y -= 15.81f * Time.deltaTime;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);



    }

}
