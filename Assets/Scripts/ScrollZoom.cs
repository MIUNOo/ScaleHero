using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static Logger;



public static class Logger
{
    public static void LOG<T>(T message)
    {
        Debug.LogAssertion(message);
    }
}


public class ScrollZoom : MonoBehaviour
{
    public float scaleSpeed = 1f;
    public float minScale = 0.1f;
    public float maxScale = 10f;
    private float previousScroll;

    public int lerpThreshold = 2;
    //public float lerpDuration = 0.5f;

    private int zoomInCount = 0;
    private int zoomOutCount = 0;
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   //////////// TODO: Zoom In out Burst in Slow mo
    {
        // 获取滚轮滚动值
        //float scrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");
        //float scrollWheel = Input.mouseScrollDelta.y;

        //LOG(scrollWheel);
        
        
        ////if (previousScroll == scrollWheel && scrollWheel!=0)
        ////{
        ////    scaleSpeed += 1f;
        ////}
        ////else
        ////{
        ////    scaleSpeed = 1f;
        ////}

        //LOG("Scale Speed: "+scaleSpeed);

        //previousScroll = scrollWheel;

        //// 调整物体的缩放
        //ScaleObject(scrollWheel);


        float scrollDelta = Input.mouseScrollDelta.y;

        //if (zoomInCount>lerpThreshold)
        //{
        //    LerpToMaxScale();
        //}



        if (scrollDelta > 0)
        {
            previousScroll = scrollDelta;
            elapsedTime = 0;
            //StopAllCoroutines();

            zoomOutCount = 0;
            StopAllCoroutines();
            
            AdjustZoom(1, ref zoomInCount, zoomOutCount, LerpToMaxScale);
            //LOG("ZoomInCount: " + zoomInCount);
        }
        else if (scrollDelta < 0)
        {
            previousScroll = scrollDelta;
            elapsedTime = 0;
            //StopAllCoroutines();

            zoomInCount = 0;
            StopAllCoroutines();
            
            AdjustZoom(-1, ref zoomOutCount, zoomInCount, LerpToMinScale);
            //LOG("ZoomOutCount: " + zoomOutCount);
        }
        //else
        //{
        //    elapsedTime += Time.deltaTime;
        //    if (elapsedTime>3f)
        //    {
        //        previousScroll = scrollDelta;
        //    }

        //}

    }



    void AdjustZoom(int direction, ref int currentCount, int otherCount, System.Action lerpAction)
    {
        Vector3 currentScale = transform.localScale;
        Vector3 newScale = CalculateNewScale(currentScale, direction);
        transform.localScale = newScale;

        IncrementAndCheckThreshold(ref currentCount, otherCount, lerpThreshold, lerpAction);
    }

    Vector3 CalculateNewScale(Vector3 currentScale, int direction)
    {
        float newScaleX = Mathf.Clamp(currentScale.x + direction * scaleSpeed, minScale, maxScale);
        float newScaleY = Mathf.Clamp(currentScale.y + direction * scaleSpeed, minScale, maxScale);
        return new Vector3(newScaleX, newScaleY, currentScale.z);
    }

    void IncrementAndCheckThreshold(ref int currentCount, int otherCount, int threshold, System.Action lerpAction)
    {
        currentCount++;
        if (currentCount >= threshold && otherCount == 0)
        {
            lerpAction.Invoke();
            currentCount = 0;
        }
    }

    void LerpToMaxScale()
    {
        StartCoroutine(LerpScale(Vector3.one * maxScale));
    }

    void LerpToMinScale()
    {
        StartCoroutine(LerpScale(Vector3.one * minScale));
    }

    IEnumerator LerpScale(Vector3 targetScale)
    {
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;
        float targetDistance = Mathf.Abs(transform.localScale.x - targetScale.x);
        float lerpDuration = targetDistance * scaleSpeed;

        while (elapsedTime < lerpDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / lerpDuration);
            //LOG(transform.localScale);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 缩放完成后调整为最接近的偶数
        transform.localScale = new Vector3(RoundToNearestEven(targetScale.x), RoundToNearestEven(targetScale.y), targetScale.z);
    }

    float RoundToNearestEven(float value)
    {
        return Mathf.Round(value / 2f) * 2f;
    }


}
