using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Rigidbody prefab;
    public Rigidbody Parent;

    public float Cooldown = 5.0f; //5 секунд
    public float ProjectileSpeed = 200.0f;
    public float ShootingEnergy = 50000.0f;
    private float CooldownLeft = 0.0f;
    public bool AutoShooting;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || AutoShooting)
        {
            if (CooldownLeft == 0)
            {
                Rigidbody projectile;
                projectile = Instantiate(prefab, transform.position, transform.rotation);
                projectile.linearVelocity = transform.TransformDirection(Vector3.forward * ProjectileSpeed) + Parent.linearVelocity;

                Parent.AddForceAtPosition(transform.TransformDirection(Vector3.back * ShootingEnergy), transform.position, ForceMode.Impulse);
                
                CooldownLeft = Cooldown;
            }
        }
        CooldownLeft = Mathf.Clamp(CooldownLeft - Time.deltaTime, 0f, CooldownLeft);
    }
}
