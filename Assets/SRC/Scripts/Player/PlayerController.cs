using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    float ver;
    float hor;
    public Joystick joystick;
    private Vector3 pos;

    void Update()
    {
        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        //this.transform.rotation = Quaternion.LookRotation(direction);

        //transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        ver = joystick.Vertical;
        hor = joystick.Horizontal;
        VectorMove();
        //CamBasedMove();
    }

    void VectorMove()
    {
        pos = transform.position;
        pos.x += moveSpeed * hor * Time.deltaTime;
        pos.z += moveSpeed * ver * Time.deltaTime;
        transform.position = pos;
    }


    void CamBasedMove()
    {
        Vector3 CameraForward = Camera.main.transform.forward;
        Vector3 CameraRight = Camera.main.transform.right;

        CameraForward.y = 0;
        CameraForward = CameraForward.normalized;
        CameraRight.y = 0;
        CameraRight = CameraRight.normalized;

        // Rotate the Input Vectors 
        Vector3 forwardRelativeMovementVector = ver * CameraForward;
        Vector3 rightRelativeMovementVector = hor * CameraRight;
        // Create Camera-Relative Movement Vector 
        Vector3 cameraRelativeMovement = forwardRelativeMovementVector + rightRelativeMovementVector;
        // Move in world space 
        this.transform.Translate(cameraRelativeMovement * moveSpeed * Time.deltaTime, Space.World);
    }
}
