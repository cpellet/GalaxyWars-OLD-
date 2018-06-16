using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uDoil;
using System;

public class uCamera : MonoBehaviour
{
    [SerializeField] private float RotationLerpSpeed = 7.0f;
    [SerializeField] private float RotationSpeed = 85;

    [SerializeField] private float MinAngleClamp = 0.2f;
    [SerializeField] private float MaxAngleClamp = 1f;

    [SerializeField] private int MinZoomClamp = 20;
    [SerializeField] private int MaxZoomClamp = 50;
    [SerializeField] private int ZoomSpeed = 35;

    private Transform cameraObject => transform.Find("camera");
    private Transform cameraPivot => transform.Find("camera_pivot");
    private Transform playerObject => transform.Find("active_model");
    
    private float rotation { get; set; }
    private float zoom { get; set; } = 35;
    private float angle { get; set; } = 1;

    public void RotateCamera(bool left_button, bool right_button)
    {
        if (left_button && right_button)
        {
            rotation = 0;
        }
        else if (left_button)
        {
            rotation -= RotationSpeed * Time.deltaTime;
        }

        else if (right_button)
        {
            rotation += RotationSpeed * Time.deltaTime;
        }

        var rotate = Quaternion.Euler(0, rotation, 0);

        cameraPivot.rotation = Quaternion.Lerp(cameraPivot.rotation, rotate, Time.deltaTime * RotationLerpSpeed);
    }

    public void MoveCamera()
    {
        // targetPosition = playerObject.position;
        // cameraPivot.position = Vector3.SmoothDamp(cameraPivot.position, targetPosition, ref velocity, 0.25f);

        cameraObject.position = playerObject.position - cameraPivot.forward * (zoom / 2) + Vector3.up * zoom;
        cameraPivot.position = transform.position + Vector3.up * ((zoom / 2) - angle * 10);

        cameraObject.LookAt(cameraPivot);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawSphere(cameraPivot.position, .4f);
    }
#endif

    public void ZoomCamera(bool left_up, bool left_down)
    {
        if (left_up)
        {
            zoom -= ZoomSpeed * Time.deltaTime;
            Debug.Log($"zoom is now {zoom}");
        }
        else if (left_down)
        {
            zoom += ZoomSpeed * Time.deltaTime;
            Debug.Log($"zoom is now {zoom}");
        }

        zoom = Mathf.Clamp(zoom, MinZoomClamp, MaxZoomClamp);
    }

    /*
        when angle the camera we want the camera to not focus on player.
        could have an invisible gameobject and move it away from player + camera and make camera look at that object instead
        and then depending on the angle the gameobject moves further or closer    
        move up.
     */

    public void AngleCamera(bool dpad_left, bool dpad_right)
    {
        if (dpad_left)
        {
            angle -= Time.deltaTime;
            Debug.Log($"angle is now {angle}");
        }
        else if (dpad_right)
        {
            angle += Time.deltaTime;
            Debug.Log($"angle is now {angle}");
        }

        angle = Mathf.Clamp(angle, MinAngleClamp, MaxAngleClamp);
    }
}