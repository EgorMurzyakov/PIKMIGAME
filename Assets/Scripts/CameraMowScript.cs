using UnityEngine;

public class CameraMowScript : MonoBehaviour
{
    [SerializeField] private Transform targetPlayer; // ���������� ������
    [SerializeField] private float mouseSen = 150f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked; // ���������� ������� � ������ ������ (��� ������ ��� ����������)
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSen * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSen * Time.deltaTime;
        xRotation -= mouseY;  // ������ ������ �����/����  
        xRotation = Mathf.Clamp(xRotation, -45f, 90f);  // �����������, ����� �� ����������� ������  
        yRotation += mouseX;  // ������� ������ �����/������  
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        Vector3 currentPosition = transform.position; // ������� ������� ������� (�������� � ������� ����������� ������)
        currentPosition = targetPlayer.position; // ����������� ������� ������
        transform.position = currentPosition + new Vector3(0f,1.5f,0f); // ��������� ��������� (������� � ��������)
    }
}
