using UnityEngine;
using UnityEngine.UI;

public class HealthbarAmountScript  : MonoBehaviour
{
    [SerializeField] private Image Healthbar;
	[SerializeField] private float MaxHealth;
	[SerializeField] private float CurrentHealth;
    [SerializeField] private Color FullHealthColor = Color.green;     
    [SerializeField] private Color NoHealthColor = Color.red;      
	 private Color colorlerp;
    void Update()
    {
		colorlerp = Color.Lerp(NoHealthColor, FullHealthColor, CurrentHealth / MaxHealth);
		
		Healthbar.color = colorlerp;
		Healthbar.fillAmount = CurrentHealth / MaxHealth;
    }
    
    
}