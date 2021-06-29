using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int cameraDragSpeed = 50;
    public float minFOV;
    public float maxFOV;
    public float sensitivity;
    public float FOV;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float speed = cameraDragSpeed * Time.deltaTime;
            Camera.main.transform.position -=
                new Vector3(Input.GetAxis("Mouse X") * speed, 0, Input.GetAxis("Mouse Y") * speed);
        }

        FOV = Camera.main.fieldOfView;
        FOV += (Input.GetAxis("Mouse ScrollWheel") * sensitivity) * -1;
        FOV = Mathf.Clamp(FOV, minFOV, maxFOV);
        Camera.main.fieldOfView = FOV;
    }
}