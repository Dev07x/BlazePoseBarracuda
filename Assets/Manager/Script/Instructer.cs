using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructorPose : MonoBehaviour
{
    public GameObject instructorObject; // Drag your 3D instructor here
    public bool poseReadyForComparison;
    private Animator animator;


    private Transform leftShoulder, leftElbow, leftWrist;
    private Transform rightShoulder, rightElbow, rightWrist;
    private Transform leftHip, leftKnee, leftAnkle;
    private Transform rightHip, rightKnee, rightAnkle;

    void Start()
    {
        if (instructorObject == null)
        {
            Debug.LogError("Instructor object not assigned.");
            return;
        }

        animator = instructorObject.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing on instructor object.");
            return;
        }

        // Auto-assign humanoid bones from Animator
        leftShoulder = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        leftElbow = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        leftWrist = animator.GetBoneTransform(HumanBodyBones.LeftHand);

        rightShoulder = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
        rightElbow = animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
        rightWrist = animator.GetBoneTransform(HumanBodyBones.RightHand);

        leftHip = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
        leftKnee = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        leftAnkle = animator.GetBoneTransform(HumanBodyBones.LeftFoot);

        rightHip = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
        rightKnee = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
        rightAnkle = animator.GetBoneTransform(HumanBodyBones.RightFoot);
    }

    public void PlayAnimation()
    {
        if (animator != null)
        {
            animator.ResetTrigger("start");
            animator.SetTrigger("start");
            StartCoroutine(WaitForPoseToEnd());
        }
    }
    private IEnumerator WaitForPoseToEnd()
    {
        // ✅ Wait 3 seconds (or duration of the animation)
        yield return new WaitForSeconds(3f);
        poseReadyForComparison = true;
    }

    public Dictionary<string, float> GetJointAngles()
    {
        var result = new Dictionary<string, float>();

        if (leftShoulder == null) return result; // Skip if not initialized

        result["LeftElbow"] = Vector3.Angle(
            leftShoulder.position - leftElbow.position,
            leftWrist.position - leftElbow.position
        );

        result["RightElbow"] = Vector3.Angle(
            rightShoulder.position - rightElbow.position,
            rightWrist.position - rightElbow.position
        );

        result["LeftShoulder"] = Vector3.Angle(
            leftHip.position - leftShoulder.position,
            leftElbow.position - leftShoulder.position
        );

        result["RightShoulder"] = Vector3.Angle(
            rightHip.position - rightShoulder.position,
            rightElbow.position - rightShoulder.position
        );

        result["LeftKnee"] = Vector3.Angle(
            leftHip.position - leftKnee.position,
            leftAnkle.position - leftKnee.position
        );

        result["RightKnee"] = Vector3.Angle(
            rightHip.position - rightKnee.position,
            rightAnkle.position - rightKnee.position
        );

        result["LeftHip"] = Vector3.Angle(
            leftShoulder.position - leftHip.position,
            leftKnee.position - leftHip.position
        );

        result["RightHip"] = Vector3.Angle(
            rightShoulder.position - rightHip.position,
            rightKnee.position - rightHip.position
        );

        return result;
    }
}
