using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Logger;

//public class CameraFollow : MonoBehaviour
//{
//    public Transform target; // Ŀ������� Transform ���
//    public float smoothSpeed = 0.125f; // ƽ���ȣ�����������ƶ����ٶ�
//    public Vector3 offset = new Vector3(0, 0, -10);

//    void LateUpdate()
//    {
//        if (target != null)
//        {
//            //Vector3 offset = Vector3.zero;

//            // ��ȡĿ�������λ��
//            Vector3 targetPosition = target.position;
//            // �����������Ŀ��λ�ã�����һ����ƫ����
//            Vector3 desiredPosition = targetPosition + offset;
//            // ʹ�� Lerp ƽ���ƶ��������Ŀ��λ��
//            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
//            // �����������Ŀ������
//            transform.LookAt(targetPosition);
//        }
//    }
//}

public class CameraFollow : MonoBehaviour
{
    //public Transform target; // Ŀ������� Transform ���
    //public float smoothSpeed = 0.125f; // ƽ���ȣ�����������ƶ����ٶ�

    //void LateUpdate()
    //{
    //    if (target != null)
    //    {
    //        // ��ȡĿ�������λ��
    //        Vector3 targetPosition = target.position;
    //        // ���������Ŀ��λ������Ϊ��Ŀ��������ͬһλ�ã������� Z ���ϱ���ԭ�е�λ��
    //        Vector3 desiredPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
    //        // ʹ�� Lerp ƽ���ƶ��������Ŀ��λ��
    //        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    //    }
    //}

    public Transform target; // Ŀ������� Transform ���
    public float smoothSpeed = 0.125f; // ƽ���ȣ�����������ƶ����ٶ�
    public float minSize = 5f; // ��С���������С
    public float maxSize = 10f; // �����������С

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // ��ȡĿ�������λ��
            Vector3 targetPosition = target.position;
            // ���������Ŀ��λ������Ϊ��Ŀ��������ͬһλ�ã������� Z ���ϱ���ԭ�е�λ��
            Vector3 desiredPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
            // ʹ�� Lerp ƽ���ƶ��������Ŀ��λ��
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // ����Ŀ���scale�������������size
            float targetScale = Mathf.Clamp(target.localScale.x, 0.1f, 20f); // ����Ŀ��scale�ķ�Χ
            //float targetScale = target.localScale.x;
            
            //float newSize = Mathf.Lerp(maxSize, minSize, targetScale*smoothSpeed);
            //LOG("NEWSIZE: "+newSize);
            LOG(targetScale);

            // ʹ�� Lerp ƽ�������������size
            // mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newSize, smoothSpeed * Time.deltaTime);
            //mainCamera.orthographicSize = 10f;

            // �����size����������scale������������smoothSpeed * Time.deltaTime*0.3f*Mathf.Abs(mainCamera.orthographicSize-2*targetScale) �Ĳ���
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetScale*2, smoothSpeed * Time.deltaTime*0.3f*Mathf.Abs(mainCamera.orthographicSize-2*targetScale));
            LOG("SIZE: "+mainCamera.orthographicSize);
            LOG("THEREOTICAL: "+ Mathf.Lerp(mainCamera.orthographicSize, targetScale * 2, smoothSpeed * Time.deltaTime * 0.3f * Mathf.Abs(mainCamera.orthographicSize - targetScale)));
        }
    }

    void LerpScale()
    {
        float targetScale = target.localScale.x;
        float newSize = Mathf.Lerp(minSize, maxSize, targetScale);

        // ʹ�� Lerp ƽ�������������size
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newSize, smoothSpeed * Time.deltaTime);
    }


}

