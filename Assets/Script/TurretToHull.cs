using UnityEngine;

public class AttachToObject : MonoBehaviour
{
    [Header("Настройки прикрепления")]
    [SerializeField] private Transform target;           // Целевой объект для прикрепления
    [SerializeField] private Vector3 positionOffset;     // Смещение позиции
    [SerializeField] private bool useLocalOffset = false; // Использовать локальное смещение
    
    [Header("Обновление")]
    [SerializeField] private UpdateMode updateMode = UpdateMode.LateUpdate;
    
    private enum UpdateMode
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

    void Update()
    {
        if (updateMode == UpdateMode.Update)
        {
            FollowTarget();
        }
    }

    void FixedUpdate()
    {
        if (updateMode == UpdateMode.FixedUpdate)
        {
            FollowTarget();
        }
    }

    void LateUpdate()
    {
        if (updateMode == UpdateMode.LateUpdate)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        if (target == null)
            return;

        if (useLocalOffset)
        {
            // Используем локальное смещение (относительно поворота цели)
            transform.position = target.position + target.rotation * positionOffset;
        }
        else
        {
            // Используем глобальное смещение (не зависит от поворота цели)
            transform.position = target.position + positionOffset;
        }
    }

    // Методы для изменения параметров в runtime
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetOffset(Vector3 newOffset)
    {
        positionOffset = newOffset;
    }

    public void SetUseLocalOffset(bool useLocal)
    {
        useLocalOffset = useLocal;
    }

    // Автоматически установить смещение на основе текущей позиции
    public void CalculateOffsetFromCurrentPosition()
    {
        if (target != null)
        {
            if (useLocalOffset)
            {
                // Для локального смещения нужно учитывать поворот цели
                positionOffset = Quaternion.Inverse(target.rotation) * (transform.position - target.position);
            }
            else
            {
                positionOffset = transform.position - target.position;
            }
        }
    }

    // Рисуем гизмо в редакторе для визуализации смещения
    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.yellow;
            
            Vector3 targetPosition;
            if (useLocalOffset && Application.isPlaying)
            {
                targetPosition = target.position + target.rotation * positionOffset;
            }
            else
            {
                targetPosition = target.position + positionOffset;
            }
            
            Gizmos.DrawLine(target.position, targetPosition);
            Gizmos.DrawWireSphere(targetPosition, 0.1f);
            
            // Показываем текущую позицию объекта
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.15f);
        }
    }
    #endif
}