using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Цель для следования")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform targetToLeadRotation;	
	
    [Header("Смещение камеры относительно цели")]
    [SerializeField] private Vector3 positionOffset = new Vector3(0, 2, -5);

    [Header("Смещение фокусного положения камеры относительно цели")]
    [SerializeField] private Vector3 CAMpositionOffset = new Vector3(0, 2, -5);

    [Header("Настройки мыши")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float verticalAngleLimit = 80.0f; // Ограничение вертикального вращения

    [Header("Плавность движения")]
    [SerializeField] private float smoothSpeed = 100.0f;
	
	[Header("Настройки зума")]
	[SerializeField] private float zoomSpeed = 5f;
	[SerializeField] private float minZoom = 2f;
	[SerializeField] private float maxZoom = 20f;
private float currentZoom = 5f; // Начальный зум (должен совпадать с positionOffset.z)

	[Header("Следование объекта за камерой")]
[SerializeField] private bool rotateTargetWithCamera = true;
[SerializeField] private float targetRotationSpeed = 10f;

    public float rotationY = 0.0f; // Горизонтальное вращение
    public float rotationX = 0.0f; // Вертикальное вращение

    private Vector3 initialOffset;
    private bool isCameraActive = true;

    void Start()
    {
        Application.targetFrameRate = 120;
        // Инициализация смещения
        initialOffset = positionOffset;
		currentZoom = -positionOffset.z; // Отрицательное значение, так как offset обычно отрицательный
        
        // Скрываем курсор
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Если цель не задана, пытаемся найти игрока по тегу
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    void Update()
    {
if (isCameraActive)
{
    HandleMouseInput();
    
    // Зум колесом мыши
    float scroll = Input.GetAxis("Mouse ScrollWheel");
    if (scroll != 0)
    {
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        
        // Обновляем только Z-координату offset
        positionOffset.z = -currentZoom;
    }
}
        // Переключение активности камеры по нажатию Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCameraControl();
        }

        // Если камера активна - обрабатываем ввод мыши
        if (isCameraActive)
        {
            HandleMouseInput();
        }
    }

    void LateUpdate()
    {

        if (target == null)
            return;

    // Вычисляем желаемое положение камеры
    Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
    Vector3 desiredPosition = target.position + rotation * positionOffset + CAMpositionOffset;
    
    // Плавное перемещение камеры
    transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    
    // Направляем камеру на цель
    transform.LookAt(target.position + rotation * Vector3.up * 1.5f);
    }

    private void HandleMouseInput()
    {
        // Получаем ввод мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Обновляем вращение
        rotationY += mouseX;
        rotationX -= mouseY;

        // Ограничиваем вертикальное вращение
        rotationX = Mathf.Clamp(rotationX, -verticalAngleLimit, verticalAngleLimit);
    }

    private void ToggleCameraControl()
    {
        isCameraActive = !isCameraActive;

        if (isCameraActive)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Метод для изменения цели в runtime
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Метод для изменения смещения в runtime
    public void SetPositionOffset(Vector3 newOffset)
    {
        positionOffset = newOffset;
    }

    // Сброс вращения камеры
    public void ResetCameraRotation()
    {
        rotationX = 0;
        rotationY = 0;
    }
}