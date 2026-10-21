using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
	public float minElevation;
	public float maxElevation;
	public Transform gunPivot;
	public Transform Turret;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Ray cameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;
		Vector3 targetPoint;
        
		if (Physics.Raycast(cameraRay, out hit, 1000f)) {
    // Если луч уперся во что-то, цель - точка попадания
		targetPoint = hit.point;
		} else {
    // Если луч в пустоту, цель - точка на большой дистанции по направлению камеры
		targetPoint = cameraRay.origin + cameraRay.direction * 500f;
		}
		UpdateGunElevation(targetPoint);
    }
	
	void UpdateGunElevation(Vector3 targetPoint) {
    // Направление от точки крепления орудия (gunPivot) к цели - В МИРОВЫХ КООРДИНАТАХ
    Vector3 directionFromGunToTarget = targetPoint - gunPivot.position;

    // Преобразуем это направление в ЛОКАЛЬНОЕ пространство башни.
    // Теперь ось X - это "вправо", Y - "вверх", Z - "вперед" для башни.
    Vector3 localDirection = Turret.InverseTransformDirection(directionFromGunToTarget);

    // Вычисляем вертикальный угол (Pitch) через арктангенс отношения высоты к горизонтальной дистанции.
    // Atan2 возвращает угол в радианах, конвертируем в градусы.
    float angle = Mathf.Atan2(localDirection.y, localDirection.z) * Mathf.Rad2Deg;

    // Ограничиваем угол, чтобы ствол не задирался/опускался слишком сильно (например, от -10 до +30 градусов)
    angle = Mathf.Clamp(angle, minElevation, maxElevation);

    // Применяем поворот ТОЛЬКО по локальной оси X к объекту GunPivot
    gunPivot.localRotation = Quaternion.Euler(-angle, 0f, 0f);
	}
}
