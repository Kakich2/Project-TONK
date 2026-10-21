using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform TankTower; // Ссылка на башню танка
    public Camera MainCamera; // Ссылка на камеру
    
    public Vector3 GunOffset = new Vector3(0, 1.5f, 0); // Смещение орудия от центра башни
    public float RotationSpeed = 5f; // Скорость вращения орудия

    void Start()
    {
        // Автоматически находим главную камеру, если не задана
        if (MainCamera == null)
            MainCamera = Camera.main;
    }

    void Update()
    {
        if (TankTower == null || MainCamera == null) return;

        // Позиция башни С УЧЕТОМ МЕСТОПОЛОЖЕНИЯ ОТНОСИТЕЛЬНО КОРПУСА
        // Важно: умножаем смещение на вращение корпуса
        Vector3 worldOffset = TankTower.rotation * GunOffset;
        transform.position = TankTower.position + worldOffset;

        // Башня следует за направлением камеры (горизонтально)
        FollowCameraDirection();
    }

    void FollowCameraDirection()
    {
        // Получаем направление, куда смотрит камера (проекция на горизонтальную плоскость)
        Vector3 cameraForward = MainCamera.transform.forward;
				// Оставляем вертикальную составляющую
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
                RotationSpeed * Time.deltaTime
            );
        }
		
    }
}