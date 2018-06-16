using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utility;

public class uMovement : MonoBehaviour
{
    private CharacterController characterController => GetComponent<CharacterController>();
    private Transform cameraPivot => transform.Find("camera_pivot");
    private Transform active_model => transform.Find("active_model");
    private float gravity = -1;
    private float lastForce;
    private Vector3 moveDirection;
    private Quaternion lookDirection;

    private void Update()
    {
        if (characterController.isGrounded)
        {
            gravity = -0.5f;
        }
        else
        {
            Mathf.Clamp(gravity -= Time.deltaTime, -40, -0.5f);
        }

        moveDirection.y = gravity;
        Rotate();
        ApplyMovement();
    }

    public void Move(Vector2 direction)
    {
        if (characterController.isGrounded && (lastForce + 0.1f < Time.time) && direction.sqrMagnitude > 0.1f)
        {
            moveDirection.x = direction.x;
            moveDirection.z = direction.y;
        }
        else
        {
            moveDirection.x = 0;
            moveDirection.z = 0;
        }
    }

    public void Rotate()
    {
        lookDirection = Quaternion.LookRotation(cameraPivot.transform.forward);

        float step = 360 * Time.deltaTime;

        active_model.rotation = Quaternion.RotateTowards(active_model.rotation, lookDirection, step);
    }

    private void ApplyMovement()
    {
        Vector3 newmove = cameraPivot.TransformDirection(moveDirection) * Time.deltaTime * 10;
        characterController.Move(newmove);
    }

    public void ApplyForce(Vector3 origin, float force, float radius)
    {
        float multiplier = (radius - Vector3.Distance(origin, transform.position)) / 10;
        if (multiplier <= 0.1f)
        {
            return;
        }
        lastForce = Time.time;
        moveDirection = ((transform.position - origin) * force) * multiplier;
    }
}
