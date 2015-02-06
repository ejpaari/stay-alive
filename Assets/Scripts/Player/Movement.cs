using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
    public GameObject playerCamera;
    public GameObject rotationPoint;
    public GameObject rotationTester;

    private const float SENSITIVITY = 0.2f;
    private const float ROT_UPPER_LIMIT = 320.0f;
    private const float ROT_LOWER_LIMIT = 50.0f;
    private const float MAX_CAMERA_DISTANCE = 8.0f;
    private const float MIN_CAMERA_DISTANCE = 0.1f;
    private const float JUMP_THRESHOLD = 0.1f;

    private Vector3 dirToCamera;

    private float t = 0.5f;
    private float movementSpeed;
    private Vector3 translation;

    private Animator anim;
    private AnimatorStateInfo animState;
    private RaycastHit hit;
    private PlayerProperties player;
    
	void Start() 
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerProperties>();
	}

    void FixedUpdate() 
    {
        if (player.hp <= 0.0f)
        {
            return;
        }

        // Mouse scroll camera movement.
        dirToCamera = playerCamera.transform.position - rotationPoint.transform.position;
        dirToCamera.Normalize();

        t -= Input.GetAxis("Mouse ScrollWheel");
        
        if (t < 0.0f)
        {
            t = 0.0f;
        }
        else if (t > 1.0f)
        {
            t = 1.0f;
        }

        float temp = t;

        Physics.Raycast(rotationPoint.transform.position, dirToCamera, out hit);

        // If camera collided with a wall behind the camera, move the camera forwards.
        if (hit.distance <= t * MAX_CAMERA_DISTANCE)
        {
            temp = hit.distance / MAX_CAMERA_DISTANCE;

            playerCamera.transform.position = Vector3.Lerp(rotationPoint.transform.position + dirToCamera * MIN_CAMERA_DISTANCE,
                                                           rotationPoint.transform.position + dirToCamera * MAX_CAMERA_DISTANCE, temp);
        }
        // Smoothly transform the camera according to user's input.
        else
        {
            Vector3 newPos = Vector3.Lerp(rotationPoint.transform.position + dirToCamera * MIN_CAMERA_DISTANCE,
                                          rotationPoint.transform.position + dirToCamera * MAX_CAMERA_DISTANCE, temp);

            Vector3 camTranslation = newPos - playerCamera.transform.position;
            playerCamera.transform.Translate(camTranslation * Time.deltaTime, Space.World);
        }

        // Run.
        if (Input.GetKey("left shift"))
        {
            movementSpeed = player.speed * PlayerProperties.RUN_SPEED_MULTIPLIER;
        }
        else
        {
            movementSpeed = player.speed;
        }

        // Movement.
        float movement = Input.GetAxis("Vertical");
        float strafe = Input.GetAxis("Horizontal");
        translation = new Vector3(strafe, 0, movement);
        translation.Normalize();
        translation *= movementSpeed * Time.deltaTime;
        transform.Translate(translation);
        anim.SetFloat("Speed", translation.magnitude);
            
        // Rotation.
        float horizontal = SENSITIVITY * Input.GetAxis("Mouse X");
        float vertical = SENSITIVITY * Input.GetAxis("Mouse Y");

        transform.Rotate(transform.up, horizontal);

        // Try to do the rotation and if it is inside the limits, then actually do it.
        rotationTester.transform.RotateAround(transform.position, transform.right, -vertical);

        if (Mathf.Abs(rotationTester.transform.eulerAngles.z) < 0.1f)
        {
            if (rotationTester.transform.eulerAngles.x > ROT_UPPER_LIMIT || rotationTester.transform.eulerAngles.x < ROT_LOWER_LIMIT)
            {
                playerCamera.transform.RotateAround(rotationPoint.transform.position, transform.right, -vertical);
            }
        }       

        rotationTester.transform.eulerAngles = playerCamera.transform.eulerAngles;
        rotationTester.transform.position = playerCamera.transform.position;
	}

    public bool isMoving()
    {
        return translation.sqrMagnitude > 0.0f;
    }
}
