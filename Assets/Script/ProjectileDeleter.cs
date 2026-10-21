using UnityEngine;

public class ProjectileDeleter : MonoBehaviour
{
    private GameObject ProjectileClone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileClone = GameObject.Find("Projectile(Clone)");
        if (ProjectileClone != null)
        {
            ProjectileScript Script = ProjectileClone.GetComponent<ProjectileScript>();
            if (Script != null && Script.Alivetime >= Script.Lifetime)
            {
                GameObject.Destroy(ProjectileClone);
            }
        }
        
    }
}
