using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LockRotationCanvas : MonoBehaviour
{
    [SerializeField] private Camera cm;
     void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        //transform.LookAt(transform.position + cm.transform.position);
    }
}
