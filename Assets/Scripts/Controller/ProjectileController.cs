using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    //LayerMask�� ����?, GameObject�� ����?
    [SerializeField] private LayerMask layerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }


}
