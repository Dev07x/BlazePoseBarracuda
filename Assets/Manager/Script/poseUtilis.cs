using System.Collections.Generic;
using UnityEngine;

public static class PoseUtils
{
    public enum Lm
    {
        LeftShoulder = 11, LeftElbow = 13, LeftWrist = 15,
        RightShoulder = 12, RightElbow = 14, RightWrist = 16,
        LeftHip = 23, LeftKnee = 25, LeftAnkle = 27,
        RightHip = 24, RightKnee = 26, RightAnkle = 28
    }

    private static float Angle(Vector3[] lm, Lm a, Lm center, Lm b)
    {
        Vector3 v1 = lm[(int)a] - lm[(int)center];
        Vector3 v2 = lm[(int)b] - lm[(int)center];
        return Vector3.Angle(v1, v2);
    }

    public static Dictionary<string, float> GetJointAngles(Vector3[] lm)
    {
        return new Dictionary<string, float>
        {
            { "LeftElbow",     Angle(lm, Lm.LeftShoulder, Lm.LeftElbow, Lm.LeftWrist) },
            { "RightElbow",    Angle(lm, Lm.RightShoulder, Lm.RightElbow, Lm.RightWrist) },
            { "LeftShoulder",  Angle(lm, Lm.LeftHip, Lm.LeftShoulder, Lm.LeftElbow) },
            { "RightShoulder", Angle(lm, Lm.RightHip, Lm.RightShoulder, Lm.RightElbow) },
            { "LeftKnee",      Angle(lm, Lm.LeftHip, Lm.LeftKnee, Lm.LeftAnkle) },
            { "RightKnee",     Angle(lm, Lm.RightHip, Lm.RightKnee, Lm.RightAnkle) },
            { "LeftHip",       Angle(lm, Lm.LeftShoulder, Lm.LeftHip, Lm.LeftKnee) },
            { "RightHip",      Angle(lm, Lm.RightShoulder, Lm.RightHip, Lm.RightKnee) },
        };
    }
}
