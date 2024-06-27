using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    
    public void SetCameraAngle(float angle)
    {
        cameraPivot.eulerAngles = new Vector3(0f,angle,0f);
    }
}
