using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    GameObject player;
    GameObject arm_0; // Arm section attached to Spacecavator body 
    GameObject arm_1; // Arm section "elbow"
    GameObject arm_2; // Arm section attached to bucket
    GameObject bucket; // Bucket

    // Rotation smoothness
    public float tiltSpeed = 120.0f;
    public float xLimitUp_Arm_0 = -0.09873292f;
    public float xLimitDown_Arm_0 = 0.50f;

    bool _PlayerFacingRight = false;
    bool _PlayerFacingLeft = false;

    // Initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        arm_0 = GameObject.FindWithTag("Arm_0");
        arm_1 = GameObject.FindWithTag("Arm_1");
        arm_2 = GameObject.FindWithTag("Arm_2");
        bucket = GameObject.FindWithTag("Bucket");
    }

    // Called once per frame
    void Update()
    {
        _PlayerFacingRight = player.transform.eulerAngles.y <= 5;
        _PlayerFacingLeft = player.transform.eulerAngles.y == 180;

        ProcessInput();
    }

    void ProcessInput()
    {
        float _arm0CurrentX = arm_0.transform.rotation.normalized.x;
        float _arm0CurrentY = arm_0.transform.rotation.normalized.y;
        // arm 0 Right Facing (Keyboard Z to activate + Up/Down Arrows)
        if (Input.GetKey(KeyCode.Z)
            && Input.GetKey(KeyCode.UpArrow)
            && _arm0CurrentX >= xLimitUp_Arm_0
            && _PlayerFacingRight)
        {
            arm_0.transform.Rotate(Vector3.down * (tiltSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.Z)
            && Input.GetKey(KeyCode.DownArrow)
            && _arm0CurrentX <= xLimitDown_Arm_0
            && _PlayerFacingRight)
        {
            arm_0.transform.Rotate(Vector3.up * (tiltSpeed * Time.deltaTime));
        }

        // arm 0 Left Facing (Keyboard Z to activate + Up/Down Arrows)
        if (Input.GetKey(KeyCode.Z)
            && Input.GetKey(KeyCode.UpArrow)
            && _arm0CurrentY >= xLimitUp_Arm_0
            && _PlayerFacingLeft)
        {
            arm_0.transform.Rotate(Vector3.down * (tiltSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.Z)
            && Input.GetKey(KeyCode.DownArrow)
            && _arm0CurrentY <= xLimitDown_Arm_0
            && _PlayerFacingLeft)
        {
            arm_0.transform.Rotate(Vector3.up * (tiltSpeed * Time.deltaTime));
        }


        // arm 1 (Keyboard X to activate + Up/Down Arrows)
        float _arm1CurrentX = arm_1.transform.rotation.normalized.x;
        if (Input.GetKey(KeyCode.X)
            && Input.GetKey(KeyCode.UpArrow))
        {
            arm_1.transform.Rotate(Vector3.down * (tiltSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.X)
            && Input.GetKey(KeyCode.DownArrow))
        {
            arm_1.transform.Rotate(Vector3.up * (tiltSpeed * Time.deltaTime));
        }

        // arm 2 (Keyboard C to activate + Up/Down Arrows)
        float _arm2CurrentX = arm_2.transform.rotation.normalized.x;
        if (Input.GetKey(KeyCode.C)
            && Input.GetKey(KeyCode.UpArrow))
        {
            arm_2.transform.Rotate(Vector3.down * (tiltSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.C)
            && Input.GetKey(KeyCode.DownArrow))
        {
            arm_2.transform.Rotate(Vector3.up * (tiltSpeed * Time.deltaTime));
        }

        // bucket (Keyboard V to activate + Up/Down Arrows)
        float _bucketCurrentX = bucket.transform.rotation.normalized.x;
        if (Input.GetKey(KeyCode.V)
            && Input.GetKey(KeyCode.UpArrow))
        {
            bucket.transform.Rotate(Vector3.down * (tiltSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.V)
            && Input.GetKey(KeyCode.DownArrow))
        {
            bucket.transform.Rotate(Vector3.up * (tiltSpeed * Time.deltaTime));
        }
    }
}
