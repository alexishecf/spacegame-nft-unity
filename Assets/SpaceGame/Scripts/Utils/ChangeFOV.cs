using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Utility class to change a camera's FOV
public class ChangeFOV : MonoBehaviour
{

    [SerializeField] Camera targetCamera;

    float targetFOV = 0f;

    public void SmoothChangeFOV(float newValue, float transitionTime)
    {
        if (!targetCamera)
            return;

        if (targetCamera.fieldOfView == newValue)
            return;

        if (targetFOV == newValue)
            return;
            
        targetFOV = newValue;

        StopAllCoroutines();
        StartCoroutine(StepChangeFOV(newValue, transitionTime));
    }

    IEnumerator StepChangeFOV(float newValue, float transitionTime)
    {
        int steps = (int)Mathf.Ceil(transitionTime * 50);
        int step = 0;
        float currentFOV = targetCamera.fieldOfView;

        while (step <= steps)
        {
            targetCamera.fieldOfView = Mathf.Lerp(currentFOV, newValue, (float)step / steps);
            step++;
            yield return new WaitForSeconds(0.02f);
        }

    }
}
