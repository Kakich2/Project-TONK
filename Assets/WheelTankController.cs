using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTankController : MonoBehaviour
{
	public WheelCollider[] TorqueWheels;
	public WheelCollider[] RotatingWheels;
	public WheelCollider[] SecondaryRotatingWheels;
	public WheelCollider[] ReverseRotatingWheels;
	public Rigidbody PhysicalBody; //Ссылка на физическое тело чтобы считать скорость
	
	public float MotorTorque;
	public float BrakeTorque;
	
	public float MaxSpeedForwards;
	public float MaxSpeedBackwards;
	public float MaxTurnSpeed; 
	
	public float MaxTurnAngle;
	public float MaxSecondaryTurnAngle;
	public float MaxReverseTurnAngle;
	
	public float MaxSpeedBeforeTurnSecondary;
	public float MaxSpeedBeforeTurnReverse;
	
	public float CurrentSpeed;

    // Update is called once per frame
    void Update()
    {
		float MovementAxis = Input.GetAxis("Vertical");   // W/S
        	float TurnAxis = Input.GetAxis("Horizontal"); // A/D
		CurrentSpeed = PhysicalBody.linearVelocity.magnitude;
		
		for (int i = 0; i < TorqueWheels.Length; i++) {
			WheelCollider Wheel = TorqueWheels[i];
			if (CurrentSpeed <= MaxSpeedForwards) { 
			    Wheel.motorTorque = MotorTorque * MovementAxis;
			}
			else {
			    Wheel.motorTorque = 0;
			}
			if (Mathf.Abs(MovementAxis) < 0.1) {
			    Wheel.motorTorque = Wheel.motorTorque / 2;	
			}
		}
		
		for (int i = 0; i < RotatingWheels.Length; i++) {
			WheelCollider Wheel = RotatingWheels[i];
			Wheel.steerAngle = MaxTurnAngle * TurnAxis;
		} 
		
		for (int i = 0; i < SecondaryRotatingWheels.Length; i++) {
			WheelCollider Wheel = SecondaryRotatingWheels[i];
			Wheel.steerAngle = MaxSecondaryTurnAngle * TurnAxis;
		} 
		
		for (int i = 0; i < ReverseRotatingWheels.Length; i++) {
			WheelCollider Wheel = ReverseRotatingWheels[i];
			if (CurrentSpeed <= MaxSpeedBeforeTurnReverse) {
				Wheel.steerAngle = -MaxReverseTurnAngle * TurnAxis;
			}
			else {
				Wheel.steerAngle = 0;
			}
		} 
    }
}
