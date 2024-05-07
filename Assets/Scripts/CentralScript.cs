using UnityEngine;
//MY 1 Directive :)

public class CentralScript : MonoBehaviour
{
    public Camera mainCamera; //THE MAIN CAM
    public Transform ballSpawnPoint; // Point where new bowling balls are spawned ( NEEDS FIX )
    public GameObject bowlingBallPrefab; // Prefab for the bowling ball.
    public GameObject scoreboard; // UI element to display scores.
    public Transform cameraTarget; // Focus point for cinematic camera transitions.
    public Collider alleyCollider;  // Defines the physical boundaries of the bowling alley.

    // Internal state variables !!!
    private GameObject currentBall; // THE CURRENT BALL BEING USED!!
    private bool isDragging = false; // Is the ball currently being dragged?
    private bool isThrown = false; // Has the ball been thrown?
    private Vector3 offset; // Used to calculate the correct position of the ball under the cursor.
    private int score = 0; // Current score ( NOT WORKING YET )
    private float lateralSpeed = 2.0f; // Speed modifier for lateral movement of the ball. moveing left to right

    void Update()
    {
        // Here i am checking for mouse the input to grab, move, or throw the ball.
        if (Input.GetMouseButtonDown(0) && currentBall == null)
        {
            Grab(); // Attempt to grab a ball.
        }
        if (isDragging)
        {
            MoveBallWithMouse(); // Continue moving the ball with the mouse if it is being dragged
        }
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Throw(); // Release the ball if it is being thrown
        }

        if (isThrown)
        {
            ControlBall(); // if the ball has been thrown i will then gain controll of the ball only move left and right
        }
        SteadyCAM(); // Manage CAM movements
    }

    // raycasting from the CAM to detect mouse clicks on the ball COLLIDER
    void Grab()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("BowlingBall"))
            {
                currentBall = hit.collider.gameObject;
                Rigidbody rb = currentBall.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true; // Disable physics while moving the ball.
                    offset = currentBall.transform.position - hit.point;
                    offset.z = 0;
                    isDragging = true;
                }
            }
        }
    }

    // updates the position of the ball based on mouse movement.
    void MoveBallWithMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f)) // Extend the raycast distance to 100 units.
        {
            Vector3 newPosition = hit.point + offset;
            newPosition.z = currentBall.transform.position.z; // Maintain the z position of the ball.
            currentBall.transform.position = newPosition;
        }
    }

    // to throw the ball down the alley using a forward force well adding force to the rb aka(rigidbody)
    void Throw()
    {
        if (currentBall != null)
        {
            Rigidbody rb = currentBall.GetComponent<Rigidbody>(); //getting the rb componenet of the current bowling ball that is being held with the mouse
            if (rb != null)
            {
                rb.isKinematic = false; // Re-enable physics.
                rb.AddForce(mainCamera.transform.forward * 1500); // Apply a forward force to relative to the CAM POS
                isDragging = false;
                isThrown = true; // Mark da ball as thrown
            }
        }
    }

    // Allows lateral movement of the ball after it has been thrown, using the A and D keys for left and right movement.
    void ControlBall()
    {
        if (currentBall != null)
        {
            Rigidbody rb = currentBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float moveHorizontal = Input.GetAxis("Horizontal") * lateralSpeed * Time.deltaTime;
                Vector3 newPosition = rb.position + Vector3.right * moveHorizontal;

                // Clamping the position to keep the ball within the alley boundaries
                newPosition.x = Mathf.Clamp(newPosition.x, alleyCollider.bounds.min.x, alleyCollider.bounds.max.x);

                rb.MovePosition(newPosition); // mvoes the rigidbody to new position
            }
        }
    }

    void SteadyCAM() //THIS WILL ZOOM IN AND OUT CHANGING THE CAM POS
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            mainCamera.transform.position += mainCamera.transform.forward * Time.deltaTime * 10; // Move CAM position forward smoothly using the time.deltatime 
        }
        if (Input.GetKey(KeyCode.DownArrow)) 
        {
            mainCamera.transform.position -= mainCamera.transform.forward * Time.deltaTime * 10; // Move CAM position backward smoothly using the time.deltatime
        }
    }
}
