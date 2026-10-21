using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
	[SerializeField] private Sprite sprite;
    // Start is called before the first frame update
	void Start() {
			ChangeModule();
	}
	void ChangeModule() 
	{
			GetComponent<Image>().sprite = sprite;
	}
}
