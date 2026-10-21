using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IconPanelManager : MonoBehaviour
{
    [Header("Настройки панели")]
    [SerializeField] private GameObject iconPrefab; // СЮДА ПЕРЕТАСКИВАЕМ ПРЕФАБ
    [SerializeField] private Transform contentPanel; // СЮДА ПЕРЕТАСКИВАЕМ Content
    
    [Header("Настройки иконок")]
    [SerializeField] private Sprite defaultIconSprite; // Можно назначить спрайт по умолчанию
    
    private List<GameObject> currentIcons = new List<GameObject>();
    
    void Start()
    {
        // Тестовое добавление иконок при старте
        AddNewIcon();
        AddNewIcon();
        AddNewIcon();
    }
    
    // ОСНОВНОЙ МЕТОД - добавляет новый префаб в панель
    public void AddNewIcon()
    {
        if (iconPrefab == null)
        {
            Debug.LogError("Не назначен префаб иконки!");
            return;
        }
        
        // 1. СОЗДАЁМ НОВЫЙ ЭКЗЕМПЛЯР ПРЕФАБА
        GameObject newIcon = Instantiate(iconPrefab, contentPanel);
        
        // 2. Добавляем в список для управления
        currentIcons.Add(newIcon);
        
        // 3. Настраиваем иконку (можно расширить)
        SetupNewIcon(newIcon);
        
        Debug.Log($"Добавлена новая иконка. Всего: {currentIcons.Count}");
    }
    
    // Метод для добавления иконки с определенным спрайтом
    public void AddNewIconWithSprite(Sprite sprite)
    {
        GameObject newIcon = Instantiate(iconPrefab, contentPanel);
        currentIcons.Add(newIcon);
        
        // Настраиваем изображение
        Image iconImage = newIcon.GetComponent<Image>();
        if (iconImage == null)
        {
            iconImage = newIcon.GetComponentInChildren<Image>();
        }
        
        if (iconImage != null)
        {
            iconImage.sprite = sprite ?? defaultIconSprite;
        }
    }
    
    private void SetupNewIcon(GameObject iconObject)
    {
        // Даем уникальное имя
        iconObject.name = $"Icon_{currentIcons.Count:000}";
        
        // Настраиваем изображение если есть спрайт по умолчанию
        if (defaultIconSprite != null)
        {
            Image img = iconObject.GetComponent<Image>();
            if (img != null)
            {
                img.sprite = defaultIconSprite;
            }
        }
    }
    
    // Добавить иконку с пользовательскими данными
    public void AddCustomIcon(string iconText, Sprite iconSprite, Color color)
    {
        GameObject newIcon = Instantiate(iconPrefab, contentPanel);
        currentIcons.Add(newIcon);
        
        // Настройка Image
        Image image = newIcon.GetComponent<Image>();
        if (image != null && iconSprite != null)
        {
            image.sprite = iconSprite;
            image.color = color;
        }
        
        // Если у префаба есть Text компонент
        Text textComponent = newIcon.GetComponentInChildren<Text>();
        if (textComponent != null)
        {
            textComponent.text = iconText;
        }
        
        newIcon.name = $"Icon_{iconText}";
    }
    
    // Удалить последнюю иконку
    public void RemoveLastIcon()
    {
        if (currentIcons.Count > 0)
        {
            GameObject lastIcon = currentIcons[currentIcons.Count - 1];
            currentIcons.RemoveAt(currentIcons.Count - 1);
            Destroy(lastIcon);
        }
    }
    
    // Очистить все иконки
    public void ClearAllIcons()
    {
        foreach (GameObject icon in currentIcons)
        {
            Destroy(icon);
        }
        currentIcons.Clear();
    }
    
    // Получить количество иконок
    public int GetIconCount()
    {
        return currentIcons.Count;
    }
}