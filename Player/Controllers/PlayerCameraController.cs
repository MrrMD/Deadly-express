using Mirror;
using UnityEngine;

public class PlayerCameraController : NetworkBehaviour
{

    [SerializeField] private Camera newCamera;
    [SerializeField] private bool isCameraBlocked = false;
    [SerializeField] private float sensitivity = 2.0f; // Чувствительность мыши
    [SerializeField] private float maxYAngle = 80.0f; // Максимальный угол вращения по вертикали

    private float rotationX = 0.0f;

    public bool IsCameraBlocked { get => isCameraBlocked; set => isCameraBlocked = value; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (isLocalPlayer)
        {

            if (isCameraBlocked) return;
            // Получаем ввод от мыши
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Вращаем персонажа в горизонтальной плоскости
            transform.parent.Rotate(Vector3.up * mouseX * sensitivity);

            // Вращаем камеру в вертикальной плоскости
            rotationX -= mouseY * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
            transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void LockCamera()
    {
        isCameraBlocked = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void UnlockCamera()
    {
        isCameraBlocked = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
