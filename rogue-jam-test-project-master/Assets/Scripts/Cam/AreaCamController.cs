using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AreaCamController : MonoBehaviour
{
    private CinemachineVirtualCameraBase cinemachineVirtualCameraBase;
    private void Awake()
    {
        GetComponentInChildren<CinemachineConfiner>().m_BoundingShape2D = GetComponent<PolygonCollider2D>();
        cinemachineVirtualCameraBase = GetComponentInChildren<CinemachineVirtualCameraBase>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.name == "Player")
            cinemachineVirtualCameraBase.Priority++;
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.name == "Player")
            cinemachineVirtualCameraBase.Priority--;
    }
}
