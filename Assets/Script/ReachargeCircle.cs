using UnityEngine;
using UnityEngine.UI;

public class SimpleRechargeCircle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image circleImage;
    
    [Header("Colors")]
    [SerializeField] private Color readyColor = Color.green;     // Полностью заряжено
    [SerializeField] private Color chargingColor = Color.red;    // В процессе
    [SerializeField] private Color emptyColor = Color.black;     // Разряжено
    private Color colorlerp;
    [Header("Settings")]
    [SerializeField] private float rechargeTime = 3f;
    [SerializeField] private float currentCharge = 1f; // 0-1
    [SerializeField] private bool isCharging = false;
    
    void Start()
    {
        // Начинаем полностью заряженным
        SetCharge(0f);
    }
    
    void Update()
    {
        // Тест: нажатие ПКМ запускает перезарядку
        if (Input.GetMouseButtonDown(0) && !isCharging)
        {
            StartRecharge();
        }
    }
    
    public void StartRecharge()
    {
        if (isCharging || currentCharge >= 0.99f) return;
        
        // Запускаем перезарядку
        isCharging = true;
        StartCoroutine(RechargeCoroutine());
    }
    
    System.Collections.IEnumerator RechargeCoroutine()
    {
        float startCharge = currentCharge;
        float elapsed = 0f;
        
        while (elapsed < rechargeTime)
        {
            elapsed += Time.deltaTime;
            
            // Рассчитываем прогресс от startCharge до 1
            float progress = elapsed / rechargeTime;
            currentCharge = Mathf.Lerp(startCharge, 1f, progress);
            
            // Обновляем визуал
            UpdateVisual();
            
            yield return null;
        }
        
        // Завершили зарядку
        currentCharge = 1f;
        isCharging = false;
        UpdateVisual();
        
        // Моргнем зеленым при полной зарядке
        StartCoroutine(FlashReady());
    }
    
    void UpdateVisual()
    {
        // 1. Обновляем заполнение круга
        circleImage.fillAmount = currentCharge;
		colorlerp = Color.Lerp(chargingColor, readyColor, currentCharge);
        
        // 2. Определяем цвет
        if (currentCharge >= 0.999f) // Полностью заряжено
        {
            circleImage.color = readyColor;
        }
        else if (isCharging) // В процессе зарядки
        {
            // Красная часть - то, что уже прошло
            circleImage.color = colorlerp;
        }
        else // Разряжено или не заряжается
        {
            // Черная часть - то, что еще осталось
            circleImage.color = emptyColor;
        }
    }
    
    System.Collections.IEnumerator FlashReady()
    {
        // Моргаем зеленым 4 раза
        for (int i = 0; i < 3; i++)
        {
            circleImage.color = Color.white; // Белый
            yield return new WaitForSeconds(0.05f);
            
            circleImage.color = readyColor; // Зеленый
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    public void SetCharge(float charge)
    {
        currentCharge = Mathf.Clamp01(charge);
        isCharging = false;
        UpdateVisual();
    }
    
    public bool IsReady => currentCharge >= 0.99f && !isCharging;
    public bool IsCharging => isCharging;
}