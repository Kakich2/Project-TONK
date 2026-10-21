using UnityEngine;

public class SimpleWoTController : MonoBehaviour
{
    [Header("Настройки движения")]
    public float maxSpeed = 10f;        // Макс. скорость вперед
    public float reverseSpeed = 5f;     // Макс. скорость назад  
    public float acceleration = 5f;     // Ускорение
    public float rotationSpeed = 50f;   // Скорость поворота
	
	[Header("Гусеницы танка")]
	public GameObject LeftTrack;
	public GameObject RightTrack;
    
    [Header("Текущая скорость (для отладки)")]
    public float currentSpeed = 0f;
	public float realspeed = 0f;
    
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Настройки для WoT-стиля физики
        rb.mass = 40000f;              // Тяжелый танк
        rb.linearDamping = 2.0f;                // Сопротивление движению
        rb.angularDamping = 0.05f;           // Сопротивление вращению
    }
    
    void FixedUpdate()
    {
        // ПРОСТОЕ УПРАВЛЕНИЕ КАК В WoT:
        // W/S - вперед/назад
        // A/D - поворот на месте или в движении
        
        float vertical = Input.GetAxis("Vertical");   // W/S
        float horizontal = Input.GetAxis("Horizontal"); // A/D
		rb = GetComponent<Rigidbody>();
		Vector3 Velocity = rb.linearVelocity;
		float RealSpeed = Velocity.magnitude;
		Vector3 Direction = Velocity.normalized;
		
        
        // --- 1. ДВИЖЕНИЕ ВПЕРЕД/НАЗАД ---
        float targetSpeed = 0f;
		if (currentSpeed > 0 && currentSpeed < 0.05 && vertical > -0.4 && vertical < 0.4) {
			currentSpeed = 0;
		}
		if (currentSpeed < 0 && currentSpeed > -0.05 && vertical > -0.4 && vertical < 0.4) {
			currentSpeed = 0;
		}
        
        if (vertical > 0.1f) // W - вперед
        {
            targetSpeed = maxSpeed * vertical;
        }
        else if (vertical < -0.1f) // S - назад
        {
            targetSpeed = reverseSpeed * vertical;
        }
        
        // Плавно меняем скорость
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);
        
        // Двигаем танк вперед/назад
        Vector3 moveDirection = transform.forward * currentSpeed;
        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
        
        // --- 2. ПОВОРОТ ---
        if (Mathf.Abs(currentSpeed) > 0.1f) // Если движемся
        {
            // Поворот в движении
            float turnAmount = horizontal * rotationSpeed * Time.fixedDeltaTime * Mathf.Sign(currentSpeed);
            transform.Rotate(0, turnAmount, 0);
        }
        else // Если стоим на месте
        {
            // Поворот на месте (медленнее)
            float turnAmount = horizontal * rotationSpeed * 1f * Time.fixedDeltaTime;
            transform.Rotate(0, turnAmount, 0);
        }
        
        // --- 3. ТОРМОЖЕНИЕ (Space) ---
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, 10f * Time.fixedDeltaTime);
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
		
    }
}