using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float shakeIntensity = 0.2f;
    public float shakeDuration = 0.5f;
    public float shakeDecay = 0.02f;
    public bool shakeEnabled = false;


    /// <summary>
    /// shake the camera with specific shake intensity and duration
    /// </summary>
    /// <returns></returns>

    public IEnumerator ShakeCamera()
    {
        Vector3 originalPosition = transform.position;
        float shakeTimer = 0f;

        while (shakeTimer < shakeDuration)
        {
            shakeTimer += Time.deltaTime;
            float shakeAmount = shakeIntensity * (1f - shakeTimer / shakeDuration);
            transform.position = originalPosition + Random.insideUnitSphere * shakeAmount;
            yield return null;
        }

        transform.position = originalPosition;
    }

    public void Shake()
    {
        if (shakeEnabled)
        {
            StartCoroutine(ShakeCamera());
        }

    }

}
