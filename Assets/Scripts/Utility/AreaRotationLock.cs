using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaRotationLock : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 offset;

    private void Start()
    {
        playerTransform = transform.parent;
        offset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        // �θ��� ��ġ�� �°� ��ġ�� ����������, ȸ���� ����
        transform.position = playerTransform.position + offset;
        transform.rotation = Quaternion.identity; // ȸ���� ����
    }
}
