using UnityEngine;

public class PlayerMowScript : MonoBehaviour
{
    private float speed = 3f; // Скорость передвижения (КОСТЫЛЬ есть изменения скорости в коде)
    [SerializeField] private float rotationSpeed = 5f; // Скорость поворота
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform targetObject;
    private Animator animator;

    private float x;
    private float z;
    private bool shift;
    private bool walk = false;
    private bool back = false;
    private bool right = false;
    private bool left = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        x = Input.GetAxis("Horizontal"); // A (-1) и D (+1)  
        z = Input.GetAxis("Vertical");   // S (-1) и W (+1)  
        shift = Input.GetKey(KeyCode.LeftShift);
        if (z == 1) // p.s. не показывайте Куприянову
        {
            walk = true;
            back = false;
        }
        else if (z == -1)
        {
            walk = false;
            back = true;
        }
        else
        {
            walk = false;
            back = false;
        }
        if (x == 1)
        {
            right = true;
            left = false;
        }
        else if (z == -1)
        {
            right = false;
            left = true;
        }
        else
        {
            right = false;
            left = false;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        Vector3 currentEuler = transform.eulerAngles;
        Vector3 targetEuler = targetObject.eulerAngles;
        if (x != 0 || z != 0)
        {           
            if (currentEuler.y != targetEuler.y) // Поворачиваем игрока в направлении камеры в начале движения
            {
                // Плавно интерполируем угол по Y
                currentEuler.y = Mathf.LerpAngle(currentEuler.y, targetEuler.y, rotationSpeed * Time.deltaTime);
                transform.eulerAngles = currentEuler;
            }
            if (shift)
            {
                speed = 15f;
            }
            else
            {
                speed = 3f;
            }
        }
        animator.SetBool("IsWalk", walk);
        animator.SetBool("IsBack", back);
        animator.SetBool("IsRight", right);
        animator.SetBool("IsLeft", left);
        animator.SetBool("IsRun", shift);
        controller.Move(move * speed * Time.deltaTime); // Применяем изменения
    }
}
