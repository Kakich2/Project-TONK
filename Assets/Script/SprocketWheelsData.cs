using UnityEngine;

[System.Serializable]
class SprocketData
{
    public Transform wheelTransform; // ссылка на объект колеса
    public float radius;             // радиус катка
    // порядок следования задаётся либо вручную, либо вычисляется по углу
}
