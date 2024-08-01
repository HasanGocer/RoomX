using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");

            float mouseY = Input.GetAxis("Mouse Y");

            // Nesneyi döndür
            transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, mouseY * rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
