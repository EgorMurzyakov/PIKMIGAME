using UnityEngine;

public class CameraMowScript : MonoBehaviour
{
    [SerializeField] private Transform targetPlayer; // Координыты игрока
    [SerializeField] private float mouseSen = 150f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked; // Блокировка курсора в центре экрана (еще делает его прозрачным)
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSen * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSen * Time.deltaTime;
        xRotation -= mouseY;  // Наклон камеры вверх/вниз  
        xRotation = Mathf.Clamp(xRotation, -45f, 90f);  // Ограничение, чтобы не перевернуть голову  
        yRotation += mouseX;  // Поворот камеры влево/вправо  
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        Vector3 currentPosition = transform.position; // Текущая позиция объекта (пустышки к которой прикреплена камера)
        currentPosition = targetPlayer.position; // Присваиваем позицию игрока
        transform.position = currentPosition + new Vector3(0f,1.5f,0f); // Применяем изменения (КОСТЫЛЬ с вектором)
    }
}
