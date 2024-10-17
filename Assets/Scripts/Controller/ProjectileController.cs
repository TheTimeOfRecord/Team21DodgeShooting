using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    //LayerMask를 리턴?, GameObject를 리턴?
    [SerializeField] private LayerMask layerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }


}
