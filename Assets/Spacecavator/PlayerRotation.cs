using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    GameObject player; // Arm section attached to Spacecavator body 

    // Rotate Speed
    public float rotateSpeed = 240.0f;
    private float startingRotation = 0.0f;

    // Double tap to rotate player (Half second listener)
    public float doubleTapRotateLeft = 0.5f;
    public float doubleTapRotateRight = 0.5f;
    // Times rotate right or left has been pressed
    private int _rotateLeftCount = 0;
    private int _rotateRightCount = 0;

    // Current state of rotation
    private bool _isRotatingLeft = false;
    private bool _isRotatingRight = false;

    // Initializing
    void Start() => player = GameObject.FindWithTag("Player");

    // Called once per frame
    void Update()
    {
        if (!_isRotatingLeft && !_isRotatingRight)
        {
            ProcessInput();
        }
        else if (_isRotatingLeft)
        {
            float currentY = player.transform.eulerAngles.y;
            if (currentY >= 180)
            {
                player.transform.eulerAngles = new Vector3(
                    player.transform.eulerAngles.x,
                    180, // Ensure exact rotation to 180 Y
                    player.transform.eulerAngles.z);
                _isRotatingLeft = false;
            }
            else
            {
                player.transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
            }
        }
        else if (_isRotatingRight)
        {
            float currentY = player.transform.eulerAngles.y;
            if (currentY <= 5)
            {
                player.transform.eulerAngles = new Vector3(
                    player.transform.eulerAngles.x,
                    0, // Ensure exact rotation to 0 Y
                    player.transform.eulerAngles.z);
                _isRotatingRight = false;
            }
            else
            {
                player.transform.Rotate(Vector3.down * (rotateSpeed * Time.deltaTime));
            }
            
        }
    }

    void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (doubleTapRotateLeft > 0 && _rotateLeftCount == 1)
            {
                DoRotateLeft();
            }
            else
            {
                doubleTapRotateLeft = 0.5f;
                _rotateLeftCount += 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (doubleTapRotateRight > 0 && _rotateRightCount == 1)
            {
                DoRotateRight();
            }
            else
            {
                doubleTapRotateRight = 0.5f;
                _rotateRightCount += 1;
            }
        }

        if (doubleTapRotateLeft > 0)
        {
            doubleTapRotateLeft -= 1 * Time.deltaTime;
        }
        else
        {
            _rotateLeftCount = 0;
        }

        if (doubleTapRotateRight > 0)
        {
            doubleTapRotateRight -= 1 * Time.deltaTime;
        }
        else
        {
            _rotateRightCount = 0;
        }
    }

    private void DoRotateLeft()
    {
        _isRotatingLeft = true;
    }

    private void DoRotateRight()
    {
        _isRotatingRight = true;
    }
}


