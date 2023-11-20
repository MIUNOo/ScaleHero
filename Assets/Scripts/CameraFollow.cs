using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Transform target; // 目标物体的 Transform 组件
    public float smoothSpeed = 0.125f; // 平滑度，控制摄像机移动的速度

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
        }
    }
}

