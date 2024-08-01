using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public Vector2 turn;
    public float mouseSensitivity = 0.5f;
    public float touchSensitivity = 0.1f; // Обычно сенсорные экраны требуют меньшей чувствительности
    public float speed = 1f;
    public GameObject mover;

    private Vector3 deltaMove;
    private Touch initialTouch;
    private bool isTouching = false;

    void Update()
    {
        HandleTouchInput();
        HandleMouseInput();
        HandleMovement();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    initialTouch = touch;
                    isTouching = true;
                    break;

                case TouchPhase.Moved:
                    if (isTouching)
                    {
                        turn.x += touch.deltaPosition.x * touchSensitivity;
                        turn.y += touch.deltaPosition.y * touchSensitivity;

                        // Rotate the camera around the y-axis and x-axis
                        mover.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
                        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
                    }
                    break;

                case TouchPhase.Ended:
                    isTouching = false;
                    break;
            }
        }
    }

    private void HandleMouseInput()
    {
        // Check if the right mouse button is held down
        if (Input.GetMouseButton(1))
        {
            turn.x += Input.GetAxis("Mouse X") * mouseSensitivity;
            turn.y += Input.GetAxis("Mouse Y") * mouseSensitivity;

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
