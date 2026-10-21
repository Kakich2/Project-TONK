using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedScript  : MonoBehaviour
{
    [SerializeField] private TMP_Text uiText; // Перетащи Text сюда в инспекторе
	[SerializeField] private int Speed;

    
    void Update()
    {
        uiText.text = Speed + " км/ч";
    }
    
}

