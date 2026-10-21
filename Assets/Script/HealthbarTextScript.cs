using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthbarTextScript  : MonoBehaviour
{
    [SerializeField] private TMP_Text uiText; // Перетащи Text сюда в инспекторе
	[SerializeField] private int MaxHealth;
	[SerializeField] private int CurrentHealth;

    
    void Update()
    {
        uiText.text = CurrentHealth + " / " + MaxHealth;
    }
    
    void ChangeText(int MaxHealth, int CurrentHealth)
    {
        uiText.text = CurrentHealth + " / " + MaxHealth;
    }
    
}

