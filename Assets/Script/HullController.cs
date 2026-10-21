using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public float turnSpeed = 20f;

    void Update()
    {
        // Движение вперед/назад
        float moveInput = Input.GetAxis("Vertical");
        transform.Translate(0, 0, moveInput * speed * Time.deltaTime);

        // Поворот вправо/влево
        float turnInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, turnInput * turnSpeed * Time.deltaTime, 0);
    }
}