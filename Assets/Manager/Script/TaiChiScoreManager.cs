using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaiChiScoreManager : MonoBehaviour
{
    [Header("References")]
    public PoseVisuallizer3D playerPose;
    public InstructorPose instructorPose;
    public PoseComparer comparer;
    public TextMeshProUGUI scoreText;

    [Header("Settings")]
    public float delayAfterAnimation = 3f;

    private bool scoreShown = false;

    public void StartTaiChiSequence()
    {
        Debug.Log("▶ TaiChiSequence started.");
        scoreShown = false;
        scoreText.text = "";

        instructorPose.PlayAnimation();
        StartCoroutine(WaitAndScore());
    }

    private IEnumerator WaitAndScore()
    {
        Debug.Log($"⏳ Waiting {delayAfterAnimation} seconds...");
        yield return new WaitForSeconds(delayAfterAnimation);

        Vector3[] playerLandmarks = playerPose.GetLandmarks3D();
        if (playerLandmarks == null || playerLandmarks.Length < 33)
        {
            Debug.LogWarning("❌ Player pose not ready or incomplete.");
            yield break;
        }

        var playerAngles = PoseUtils.GetJointAngles(playerLandmarks);
        var instructorAngles = instructorPose.GetJointAngles();

        Debug.Log("📊 Player Angles:");
        foreach (var kv in playerAngles)
            Debug.Log($"{kv.Key}: {kv.Value}");

        Debug.Log("📊 Instructor Angles:");
        foreach (var kv in instructorAngles)
            Debug.Log($"{kv.Key}: {kv.Value}");

        float similarity = comparer.Compare(instructorAngles, playerAngles);
        int percent = Mathf.RoundToInt(similarity * 100f);

        scoreText.text = $"Final Match: {percent}%";
        Debug.Log($"✅ Score shown: {percent}%");
        scoreShown = true;
    }
}
