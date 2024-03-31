using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotationCanvas : MonoBehaviour
{
    void LateUpdate()
    {
        //transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
