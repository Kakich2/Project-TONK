using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTankController : MonoBehaviour
{
	public WheelCollider[] LeftMovementWheels;
    public WheelCollider[] RightMovementWheels;
    public Rigidbody PhysicalBody; //Ссылка на физическое тело чтобы считать скорость
	
	public float MotorTorque;
    public float TurnTorque;
	public float BrakeTorque;
	
	public float MaxSpeedForwards;
	public float MaxSpeedBackwards;
	public float MaxTurnSpeed; 
	
	public float CurrentSpeed;

    // Update is called once per frame
    void Update()
    {
		float MovementAxis = Input.GetAxis("Vertical");   // W/S
        	float TurnAxis = Input.GetAxis("Horizontal"); // A/D
		CurrentSpeed = PhysicalBody.linearVelocity.magnitude;
		
		for (int i = 0; i < LeftMovementWheels.Length; i++) { //Движение колёс вперёд-назад
			WheelCollider Wheel = LeftMovementWheels[i];
			if (CurrentSpeed <= MaxSpeedForwards) { 
			    Wheel.motorTorque = MotorTorque * MovementAxis;
			}
			else {
			    Wheel.motorTorque = 0;
			}
		}

        for (int i = 0; i < RightMovementWheels.Length; i++)
        {
            WheelCollider Wheel = RightMovementWheels[i];
            if (CurrentSpeed <= MaxSpeedForwards)
            {
                Wheel.motorTorque = MotorTorque * MovementAxis;
            }
            else
            {
                Wheel.motorTorque = 0;
            }
        }

        for (int i = 0; i < LeftMovementWheels.Length; i++) //Поворот вправо-влево
        { 
            WheelCollider Wheel = LeftMovementWheels[i];
            PhysicalBody.AddRelativeTorque(0, TurnAxis * TurnTorque * 0.5f, 0, ForceMode.Force);
            if (Mathf.Abs(MovementAxis) == 0)
            {
                Wheel.motorTorque += MotorTorque * TurnAxis;
            }
            else
            {
                if (MovementAxis > 0)
                {
                    Wheel.motorTorque += MotorTorque * Mathf.Clamp(TurnAxis, -1, 0);
                }
                else
                {
                    Wheel.motorTorque += MotorTorque * -Mathf.Clamp(TurnAxis, -1, 0);
                }
            }

        }

        for (int i = 0; i < RightMovementWheels.Length; i++)
        {
            WheelCollider Wheel = RightMovementWheels[i];
            PhysicalBody.AddRelativeTorque(0, TurnAxis * TurnTorque * 0.5f, 0, ForceMode.Force);
            if (Mathf.Abs(MovementAxis) == 0)
            {
                Wheel.motorTorque += MotorTorque * -TurnAxis;
            }
            else
            {
                if (MovementAxis > 0)
                {
                    Wheel.motorTorque += MotorTorque * -Mathf.Clamp(TurnAxis, 0, 1);
                }
                else
                {
                    Wheel.motorTorque += MotorTorque * Mathf.Clamp(TurnAxis, 0, 1);
                }
            }


        }
    }
}
