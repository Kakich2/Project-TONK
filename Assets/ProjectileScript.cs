using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [Header("Движение")]
    public float Speed = 150f;
    public float Gravity = 9.81f;
    public float Lifetime = 1f;

    [Header("Обнаружения")]
    public LayerMask HitMask;

    public float Alivetime;
    private Vector3 Velocity;
    private bool IsHit;

    void Start()
    {
        Velocity = transform.forward * Speed;
    }
    void FixedUpdate()
    {
        if (IsHit) return;
        Vector3 Start = transform.position;
        Vector3 End = Start + Velocity * Speed * Time.fixedDeltaTime;
        RaycastHit Hit;
        if (Physics.Linecast(Start, End, out Hit, HitMask))
        {
            IsHit = true;
            transform.position = Hit.point;
            Destroy(gameObject);
        }
        if (Alivetime > Lifetime && gameObject.name == "Projectile(Clone)")
        {
            Destroy(gameObject);
            Lifetime = 0;
        }

        if (gameObject.name == "Projectile(Clone)")
        {
            Alivetime += Time.fixedDeltaTime;
        }
        transform.position = End;
        Velocity.y -= Gravity * Time.fixedDeltaTime;
        
    }
}
