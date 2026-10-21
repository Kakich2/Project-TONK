using System.Collections.Generic;
using UnityEngine;

public class TrackPathBuilder
{
    public struct Segment
    {
        public enum Type { Line, Arc }
        public Type type;
        public Vector3 start, end;      // для Line
        public Vector3 center;           // для Arc
        public float radius;
        public float startAngle, endAngle;
        public float length;
    }

    public List<Segment> segments = new List<Segment>();

    void BuildFromSprockets(List<SprocketData> sprockets, bool isUpperBranch = true)
    {
        // Для верхней ветви – касательные сверху, для нижней – снизу.
        for (int i = 0; i < sprockets.Count - 1; i++)
        {
            AddTangentBetween(sprockets[i], sprockets[i + 1], isUpperBranch);
        }
        // Добавить дуги на самих катках (между точками касания)
        // ... вычисление начальных и конечных углов дуг
    }

    void AddTangentBetween(SprocketData a, SprocketData b, bool upper)
    {
        Vector3 d = b.wheelTransform.position - a.wheelTransform.position;
        float dist = d.magnitude;
        float r1 = a.radius, r2 = b.radius;
        float angle = Mathf.Asin((r2 - r1) / dist);
        Vector3 perp = Vector3.Cross(d, Vector3.forward).normalized; // предполагаем вид сбоку (ось Z)
        if (!upper) angle = -angle;
        Vector3 dir = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward) * d.normalized;
        Vector3 tangentDir = Vector3.Cross(Vector3.forward, dir).normalized;

        Vector3 contactA = a.wheelTransform.position + tangentDir * r1;
        Vector3 contactB = b.wheelTransform.position + tangentDir * r2;

        segments.Add(new Segment { type = Segment.Type.Line, start = contactA, end = contactB, length = Vector3.Distance(contactA, contactB) });
        // Также добавить дуги для a и b (от предыдущей касательной к этой)

    }
    void GetPointAndTangent(float dist,  Vector3 point,  Vector3 tangent)
    {
        foreach (var seg in segments)
        {
            if (dist <= seg.length)
            {
                float t = dist / seg.length;
                if (seg.type == Segment.Type.Line)
                {
                    point = Vector3.Lerp(seg.start, seg.end, t);
                    tangent = (seg.end - seg.start).normalized;
                }
                else // Arc
                {
                    float angle = Mathf.Lerp(seg.startAngle, seg.endAngle, t);
                    point = seg.center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * seg.radius;
                    tangent = new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle), 0);
                }
                return;
            }
            dist -= seg.length;
        }
        // на случай ошибки – последняя точка
    }
}