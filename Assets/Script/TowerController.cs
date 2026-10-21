using UnityEngine;

public class TowerController : MonoBehaviour
{
    public Transform tankBody; // Ссылка на корпус танка
    public Camera mainCamera; // Ссылка на камеру
    
    public Vector3 towerOffset = new Vector3(0, 1.5f, 0); // Смещение башни от центра корпуса
    public float rotationSpeed = 5f; // Скорость вращения башни

    void Start()
    {
        // Автоматически находим главную камеру, если не задана
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if (tankBody == null || mainCamera == null) return;

        // Позиция башни С УЧЕТОМ МЕСТОПОЛОЖЕНИЯ ОТНОСИТЕЛЬНО КОРПУСА
        // Важно: умножаем смещение на вращение корпуса
        Vector3 worldOffset = tankBody.rotation * towerOffset;
        transform.position = tankBody.position + worldOffset;

        // Башня следует за направлением камеры (горизонтально)
        FollowCameraDirection();
    }

    void FollowCameraDirection()
    {
        // Получаем направление, куда смотрит камера (проекция на горизонтальную плоскость)
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0; // Убираем вертикальную составляющую
        cameraForward.Normalize(); // Нормализуем вектор
        
        if (cameraForward != Vector3.zero)
        {
            // Создаем вращение по направлению камеры
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            
            // Плавно поворачиваем башню к направлению камеры
            // ВАЖНО: используем вращение от текущей позиции башни
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );
        }
    }
}