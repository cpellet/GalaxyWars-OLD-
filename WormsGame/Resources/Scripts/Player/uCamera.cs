using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uDoil;
using System;

public class uCamera : MonoBehaviour
{
    private Transform cameraObject => transform.Find("camera");
    private Transform cameraPivot => transform.Find("camera_pivot");
    private Transform playerObject => transform.Find("active_model");
    private float speed { get; set; } = 1;
    private bool isLocalPlayer { get; set; } = true;
    private Vector3 velocity;
    // private Vector3 targetPosition;
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
            rotation -= 85 * Time.deltaTime;
        }

        else if (right_button)
        {
            rotation += 85 * Time.deltaTime;
        }

        var rotate = Quaternion.Euler(cameraPivot.eulerAngles.x, rotation, cameraPivot.eulerAngles.z);

        cameraPivot.rotation = Quaternion.Lerp(cameraPivot.rotation, rotate, Time.deltaTime * 7);
    }

    public void MoveCamera()
    {
        cameraObject.position = cameraPivot.position - cameraPivot.forward * (zoom / 2) + Vector3.up * (zoom * angle);

        // targetPosition = playerObject.position;

        // cameraPivot.position = Vector3.SmoothDamp(cameraPivot.position, targetPosition, ref velocity, 0.25f);

        cameraPivot.position = transform.position + Vector3.up * (10 - (angle * 10));

        cameraObject.LookAt(cameraPivot);
    }

    public void ZoomCamera(bool left_up, bool left_down)
    {
        if (left_up)
        {
            zoom -= 35 * Time.deltaTime;
            Debug.Log($"zoom is now {zoom}");
        }
        else if (left_down)
        {
            zoom += 35 * Time.deltaTime;
            Debug.Log($"zoom is now {zoom}");
        }

        zoom = Mathf.Clamp(zoom, 20, 50);
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

        angle = Mathf.Clamp(angle, 0.2f, 1f);
    }
}