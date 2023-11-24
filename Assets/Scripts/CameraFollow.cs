using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Logger;

//public class CameraFollow : MonoBehaviour
//{
//    public Transform target; // 目标物体的 Transform 组件
//    public float smoothSpeed = 0.125f; // 平滑度，控制摄像机移动的速度
//    public Vector3 offset = new Vector3(0, 0, -10);

//    void LateUpdate()
//    {
//        if (target != null)
//        {
//            //Vector3 offset = Vector3.zero;

//            // 获取目标物体的位置
//            Vector3 targetPosition = target.position;
//            // 计算摄像机的目标位置，加上一定的偏移量
//            Vector3 desiredPosition = targetPosition + offset;
//            // 使用 Lerp 平滑移动摄像机到目标位置
//            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
//            // 将摄像机朝向目标物体
//            transform.LookAt(targetPosition);
//        }
//    }
//}

public class CameraFollow : MonoBehaviour
{
    //public Transform target; // 目标物体的 Transform 组件
    //public float smoothSpeed = 0.125f; // 平滑度，控制摄像机移动的速度

    //void LateUpdate()
    //{
    //    if (target != null)
    //    {
    //        // 获取目标物体的位置
    //        Vector3 targetPosition = target.position;
    //        // 将摄像机的目标位置设置为与目标物体在同一位置，但是在 Z 轴上保持原有的位置
    //        Vector3 desiredPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
    //        // 使用 Lerp 平滑移动摄像机到目标位置
    //        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    //    }
    //}

    public Transform target; // 目标物体的 Transform 组件
    public float smoothSpeed = 0.125f; // 平滑度，控制摄像机移动的速度
    public float minSize = 5f; // 最小的摄像机大小
    public float maxSize = 10f; // 最大的摄像机大小

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // 获取目标物体的位置
            Vector3 targetPosition = target.position;
            // 将摄像机的目标位置设置为与目标物体在同一位置，但是在 Z 轴上保持原有的位置
            Vector3 desiredPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
            // 使用 Lerp 平滑移动摄像机到目标位置
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // 根据目标的scale来调整摄像机的size
            float targetScale = Mathf.Clamp(target.localScale.x, 0.1f, 20f); // 限制目标scale的范围
            //float targetScale = target.localScale.x;
            
            //float newSize = Mathf.Lerp(maxSize, minSize, targetScale*smoothSpeed);
            //LOG("NEWSIZE: "+newSize);
            LOG(targetScale);

            // 使用 Lerp 平滑调整摄像机的size
            // mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newSize, smoothSpeed * Time.deltaTime);
            //mainCamera.orthographicSize = 10f;

            // 将相机size调整到物体scale的两倍，以其smoothSpeed * Time.deltaTime*0.3f*Mathf.Abs(mainCamera.orthographicSize-2*targetScale) 的步数
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetScale*2, smoothSpeed * Time.deltaTime*0.3f*Mathf.Abs(mainCamera.orthographicSize-2*targetScale));
            LOG("SIZE: "+mainCamera.orthographicSize);
            LOG("THEREOTICAL: "+ Mathf.Lerp(mainCamera.orthographicSize, targetScale * 2, smoothSpeed * Time.deltaTime * 0.3f * Mathf.Abs(mainCamera.orthographicSize - targetScale)));
        }
    }

    void LerpScale()
    {
        float targetScale = target.localScale.x;
        float newSize = Mathf.Lerp(minSize, maxSize, targetScale);

        // 使用 Lerp 平滑调整摄像机的size
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newSize, smoothSpeed * Time.deltaTime);
    }


}

