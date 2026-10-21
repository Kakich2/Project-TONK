using UnityEngine;
using TMPro;

public class VelocityDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private string rigidbodyTag = ""; // тег объекта с Rigidbody
    [SerializeField] private string rigidbodyName = "Centauro"; // или имя объекта
    [SerializeField] private TextMeshProUGUI text;
    
    [Header("Display Settings")]
    [SerializeField] private string prefix = "";
    [SerializeField] private string postfix = " км/ч.";
    [SerializeField] private string format = "F1";
    
    private Rigidbody rb;
    
    private void Start()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();
            
        // Пытаемся найти Rigidbody
        FindRigidbody();
    }
    
    private void FindRigidbody()
    {
        // Поиск по тегу
        if (!string.IsNullOrEmpty(rigidbodyTag))
        {
            GameObject go = GameObject.FindGameObjectWithTag(rigidbodyTag);
            if (go != null)
                rb = go.GetComponent<Rigidbody>();
        }
        
        // Если не нашли по тегу, ищем по имени
        if (rb == null && !string.IsNullOrEmpty(rigidbodyName))
        {
            GameObject go = GameObject.Find(rigidbodyName);
            if (go != null)
                rb = go.GetComponent<Rigidbody>();
        }
        
        // Если всё ещё не нашли, ищем любой Rigidbody в сценах
        if (rb == null)
        {
            rb = FindObjectOfType<Rigidbody>();
        }
        
        if (rb == null)
            Debug.LogWarning("Rigidbody не найден!");
    }
    
    private void Update()
    {
        if (rb != null && text != null)
        {
            float speed = rb.linearVelocity.magnitude * 3.6f;
            text.text = prefix + speed.ToString(format) + postfix;
        }
        else if (rb == null)
        {
            // Периодически пробуем найти заново (если объект появился позже)
            FindRigidbody();
        }
    }
}