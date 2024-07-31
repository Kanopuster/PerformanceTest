using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity = 0.5f;
    public float speed = 1f;
    public GameObject mover;

    private Vector3 deltaMove;

    void Update()
    {
        HandleMouseInput();
        HandleMovement();
    }

    private void HandleMouseInput()
    {
        // Check if the right mouse button is held down
        if (Input.GetMouseButton(1)) 
        {
            turn.x += Input.GetAxis("Mouse X") * sensitivity;
            turn.y += Input.GetAxis("Mouse Y") * sensitivity;

            // Rotate the camera around the y-axis and x-axis
            mover.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        }
    }

    private void HandleMovement()
    {
        deltaMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;
        mover.transform.Translate(deltaMove, Space.Self);
    }
}