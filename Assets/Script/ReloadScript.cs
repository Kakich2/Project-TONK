using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleTextUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text uiText; // Перетащи Text сюда в инспекторе
	[SerializeField] private Image CircleImage;
	[SerializeField] private float Reload;
	private float ReloadToShow;
    
    
    void Update()
    {
		ReloadToShow = Reload - Reload * CircleImage.fillAmount;
        // Просто обновляем текст каждый кадр
        uiText.text = ReloadToShow.ToString("F2");
        
        // Или для отдельных значений:
        // uiText.text = score.ToString();
    }
    
}