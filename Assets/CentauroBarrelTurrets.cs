using UnityEngine;

public class BarrelTiltWithParent : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // объект, за которым следим (камера, игрок)
    [SerializeField] private float rotationSpeed = 90f; // скорость вращения град/сек

    void Update()
    {
        if (cameraTransform == null) return;

        // Направление камеры в мировом пространстве
        Vector3 targetWorldDir = cameraTransform.forward;

        // Ось вращения башни — её текущая вертикаль (локальная Y)
        Vector3 rotationAxis = transform.right;

        // Проецируем направление цели на плоскость, перпендикулярную оси вращения
        Vector3 projectedDir = Vector3.ProjectOnPlane(targetWorldDir, rotationAxis).normalized;

        // Защита от нулевого вектора (если смотрим строго вдоль оси вращения)
        if (projectedDir == Vector3.zero) return;

        // Создаём целевой поворот в мировом пространстве с сохранением текущего up
        Quaternion targetWorldRotation = Quaternion.LookRotation(projectedDir, rotationAxis);

        // Плавно вращаемся от текущего поворота к целевому
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetWorldRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}