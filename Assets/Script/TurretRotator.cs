using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHehe : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 120f; 
	[SerializeField] private CameraFollow classreference;
	private float rotationY;
	
	void Start() {
		Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	void LateUpdate()
	{

		rotationY = classreference.rotationY;
		Quaternion targetRotation = Quaternion.Euler(0, rotationY, 0);
        // Плавно поворачиваем объект
	
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
                                          rotationSpeed * Time.deltaTime); 			
     		
	}
}