using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Transform target; // Ŀ������� Transform ���
    public float smoothSpeed = 0.125f; // ƽ���ȣ�����������ƶ����ٶ�

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
        }
    }
}

