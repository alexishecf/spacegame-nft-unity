using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidModel : EntityModel
{
    [Header("Humanoid bones")]
    [SerializeField] Transform head;
    [SerializeField] Transform spine, pelvis, leftArm, rightArm, leftHand, rightHand, leftLeg, rightLeg;

    protected override void OnCameraViewChange(CameraViewType newViewType)
    {
        Vector3 localScale = newViewType == CameraViewType.FPS ? Vector3.one : Vector3.zero;
        head.localScale = localScale;
    }


}
