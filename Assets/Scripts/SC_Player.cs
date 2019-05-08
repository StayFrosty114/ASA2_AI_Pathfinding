using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_Player : MonoBehaviour
{
    //User-Controlled Variables
    public float moveSpeed = 0.02f;
    public float rotateSpeed = 1.5f;
    public KeyCode jumpKey;

    //Input Variables
    private float mouseXAxis;
    private float mouseYAxis;
    private bool jump;
    private float jumpHeight = 4.0f;

    //Component Variables
    Rigidbody rigBody;
    public GameObject player;
    
    //Reference Variables
    public GameObject myCameraPivot;

    // UI Variables
    public GameObject deathPopUp;
    public GameObject winPopUp;

    void Start()
    {
        // Capture a reference to the rigidbody component.
        rigBody = GetComponent<Rigidbody>();

        // Hiding UI At start.
        deathPopUp.SetActive(false);
        winPopUp.SetActive(false);
    }

    void FixedUpdate()
    {
        #region Player Movement
        // Capture the mouse axis values
        mouseXAxis = Input.GetAxis("Mouse X");
        mouseYAxis = Input.GetAxis("Mouse Y");

        // Capture keyboard input
        if (Input.GetKey(KeyCode.W))
            rigBody.MovePosition(transform.position += (transform.forward * moveSpeed));

        if (Input.GetKey(KeyCode.A))
            rigBody.MovePosition(transform.position -= (transform.right * moveSpeed));

        if (Input.GetKey(KeyCode.S))
            rigBody.MovePosition(transform.position -= (transform.forward * moveSpeed));

        if (Input.GetKey(KeyCode.D))
            rigBody.MovePosition(transform.position += (transform.right * moveSpeed));

        // Rotate Player
        transform.rotation = (Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseXAxis * rotateSpeed, transform.rotation.eulerAngles.z));

    // Rotate Camera
        // User is wishing to look up
        if(mouseYAxis > 0)
        {
            if (myCameraPivot.transform.rotation.eulerAngles.x + mouseYAxis * rotateSpeed < 60 || myCameraPivot.transform.rotation.eulerAngles.x > 300)
                myCameraPivot.transform.rotation = (Quaternion.Euler(myCameraPivot.transform.rotation.eulerAngles.x + mouseYAxis * rotateSpeed, myCameraPivot.transform.rotation.eulerAngles.y, myCameraPivot.transform.rotation.eulerAngles.z));
        }
        
        // User is wishing to look down
        else if (mouseYAxis < 0)
        {
            if (myCameraPivot.transform.rotation.eulerAngles.x + mouseYAxis * rotateSpeed > 300 || myCameraPivot.transform.rotation.eulerAngles.x < 60)
                myCameraPivot.transform.rotation = (Quaternion.Euler(myCameraPivot.transform.rotation.eulerAngles.x + mouseYAxis * rotateSpeed, myCameraPivot.transform.rotation.eulerAngles.y, myCameraPivot.transform.rotation.eulerAngles.z));
        }
        
        //Jump
        if (jump == true)
        {
            if (Input.GetKeyDown(jumpKey))
            {
                rigBody.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
                jump = false;
            }
        }
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Allowing jump after player has collided with something.
        jump = true;

        // Kills player on collision with an Agent.
        if (collision.gameObject.name.Contains("Agent"))
        {
            Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player reaches the goal.
        if (other.name.Contains("Win_Trigger"))
        {
            Win();
        }
    }

    private void Death()
    {
        Destroy(player);
        deathPopUp.SetActive(true);
    }

    private void Win()
    {
        Destroy(player);
        winPopUp.SetActive(true);
    }
}