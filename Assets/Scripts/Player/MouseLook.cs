using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/MouseLook")]
public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXandY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXandY;

    [SerializeField] private float sensitivityX = 2f, sensitivityY = 2f;

    [SerializeField] private float minX = -360f, maxX = 360f;
    [SerializeField] private float minY = -70f, maxY = 70f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private Quaternion originalRotation; 

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        MouseLookingLogic();
    }

    void MouseLookingLogic()
    {
        if (axes == RotationAxes.MouseXandY)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX = ClampAngle(rotationX, minX, maxX);
            rotationY = ClampAngle(rotationY, minY, maxY);

            var xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            var yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }

        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minX, maxX);
            var xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }

        else if (axes == RotationAxes.MouseY)
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minY, maxY);
            var yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }
    }

    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
