using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetFollow : MonoSingleton<CameraTargetFollow>
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] float rotationSpeed = 100.0f;
    [SerializeField] float mouseSensitivity = 100.0f;

    private float currentYaw = 0.0f;

    void LateUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float mouseInput = 0.0f;

        if (Input.GetMouseButton(1))
        {
            mouseInput = Input.GetAxis("Mouse X");
        }

        float yaw = (horizontalInput * rotationSpeed + mouseInput * mouseSensitivity) * Time.deltaTime;
        currentYaw += yaw;

        Vector3 desiredPosition = target.position + Quaternion.Euler(0, currentYaw, 0) * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition * ScrollUpDown.Instance.GetScrollValue();

        transform.LookAt(target);
    }
}
