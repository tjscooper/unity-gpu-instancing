using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidBody;

    bool _PlayerFacingRight = false;
    bool _PlayerFacingLeft = false;

    // Double tap to suspend thruster (for controlled digging - Half second listener)
    public float doubleTapPressWindow = 0.5f;
    // Times hover has been pressed
    private int _hoverCount = 0;
    // HoverPower
    public float hoverPower = 100f;
    // Thrust Power
    public float thrustPower = 30f;

    // Initializing
    void Start() => rigidBody = GetComponent<Rigidbody>();

    // Called once per frame
    void Update()
    {
        _PlayerFacingRight = rigidBody.transform.eulerAngles.y <= 5;
        _PlayerFacingLeft = rigidBody.transform.eulerAngles.y == 180;

        ProcessInput();
    }

    void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Can thrust while rotating
        {
            if (doubleTapPressWindow > 0 && _hoverCount == 1)
            {
                DoHover();
            }
            else
            {
                DoThrust();            
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * (thrustPower * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (_PlayerFacingRight)
            {
                rigidBody.AddRelativeForce(-Vector3.left);
            }
            else if (_PlayerFacingLeft)
            {
                rigidBody.AddRelativeForce(-Vector3.right);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (_PlayerFacingRight)
            {
                rigidBody.AddRelativeForce(-Vector3.right);
            }
            else if (_PlayerFacingLeft)
            {
                rigidBody.AddRelativeForce(-Vector3.left);
            }
        }

        if (doubleTapPressWindow > 0)
        {
            doubleTapPressWindow -= 1 * Time.deltaTime;
        }
        else
        {
            _hoverCount = 0;
        }
    }

    private void DoHover()
    {
        rigidBody.drag = hoverPower; // Virtually suspended in air
    }

    private void DoThrust()
    {
        doubleTapPressWindow = 0.5f;
        _hoverCount += 1;
        rigidBody.drag = 1;
    }
}


