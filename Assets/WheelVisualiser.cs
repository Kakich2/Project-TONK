using UnityEngine;

public class WheelVisual : MonoBehaviour
{
    [SerializeField] private WheelCollider _wheelCollider; // Ссылка на коллайдер
    public bool isFlipped;

    void Update()
    {
        if (_wheelCollider == null) return;

        // Получаем позицию и вращение коллайдера в мире
        _wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion quat);

        // Применяем их к модели колеса
        transform.position = pos;
        transform.rotation = quat;
	if (isFlipped) {
	  transform.rotation *= Quaternion.Euler(0, 180, 0);
	}
    }
}