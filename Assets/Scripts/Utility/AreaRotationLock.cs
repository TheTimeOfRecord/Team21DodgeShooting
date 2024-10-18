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
        // 부모의 위치에 맞게 위치는 갱신하지만, 회전은 고정
        transform.position = playerTransform.position + offset;
        transform.rotation = Quaternion.identity; // 회전을 고정
    }
}
