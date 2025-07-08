using System.Collections.Generic;
using UnityEngine;

public class PoseComparer : MonoBehaviour
{
    [Range(0, 180)]
    public float maxAcceptableAngle = 45f;

    public float Compare(Dictionary<string, float> instructor, Dictionary<string, float> player)
    {
        float total = 0f;
        int count = 0;

        foreach (var kv in instructor)
        {
            if (!player.TryGetValue(kv.Key, out float pAngle)) continue;

            float diff = Mathf.Abs(kv.Value - pAngle);
            diff = Mathf.Min(diff, maxAcceptableAngle);
            total += 1f - (diff / maxAcceptableAngle);
            count++;
        }

        return (count > 0) ? (total / count) : 0f;
    }
}
