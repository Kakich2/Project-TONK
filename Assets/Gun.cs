using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;    // точка, откуда вылетает снаряд
    public GameObject projectilePrefab; // префаб снаряда
    public float projectileSpeed = 30f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // например, левая кнопка мыши
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Создаём снаряд в точке выстрела с поворотом оружия
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Получаем компонент Rigidbody снаряда
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Задаём скорость вперёд (относительно направления оружия)
            rb.linearVelocity = firePoint.forward * projectileSpeed;
        }
    }
}