using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityModel : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] Animator animator;
    public Animator Animator { get { return animator; } }
    protected enum CameraViewType
    {
        FPS,
        TPS,
        Other
    }

    // Add logic to hide/show parts of the model that should not be seen from certain perspectives
    // (e.g. humanoid head when in FPS view)
    protected abstract void OnCameraViewChange(CameraViewType newViewType);

}
