using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerrb;
    private bool isPlayerActive = true;
    [SerializeField] private float playerVelocity;
    [SerializeField] private bool isGrounded;
    private Vector2 initPos;

    [SerializeField] private float jumpForce;
    [SerializeField] private float bulletProjectileAngle; //The angle of the gun that determines the angle at which the portal bullet will be launched from the gun

    private float fallMultiplier = 2.5f;

    public bool levelCompleted;

    // Start is called before the first frame update
    void Start()
    {
        levelCompleted = false;
        initPos = transform.position;
        isGrounded = true;
        ComponentReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelCompleted && isPlayerActive)
        {
            GravityCheck();
            Jump();
        }
    }
    void FixedUpdate()
    {
        if (!levelCompleted && isPlayerActive)
        {
            MoveFwd();
        }
        else
        {
            playerrb.velocity = Vector2.zero;
        }
    }
    private void ComponentReferences()
    {
        playerrb = gameObject.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    ///  Handles the player's jumping mechanism
    /// </summary>
    private void MoveFwd()
    {
        playerrb.velocity = new Vector2(Input.GetAxis("Horizontal") * playerVelocity, playerrb.velocity.y);
    }

    /// <summary>
    /// Handles the player's jumping mechanism
    /// </summary>
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerrb.velocity = Vector2.up * jumpForce;
        }
    }

    /// <summary>
    /// This function determines when the player is on the ground
    /// </summary>
    public void GravityCheck()
    {
        if (playerrb.velocity.y != 0)
        {
            isGrounded = false;
        }
        else if (playerrb.velocity.y == 0 || gameObject.transform.parent != null)
        {
            isGrounded = true;
        }
        if (playerrb.velocity.y < 0)
        {
            playerrb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "orange" && Vector2.Distance(collision.transform.position, transform.position) > 0.2f)
        {
            StartCoroutine(JumpPortal("blue"));
        }

        if (collision.tag == "blue" && Vector2.Distance(collision.transform.position, transform.position) > 0.2f)
        {
            StartCoroutine(JumpPortal("orange"));
        }

        //Determines that the level has been completed once the player reaches the victory area
        if (collision.tag == "victory")
        {
            levelCompleted = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

    }

    /// <summary>
    /// A coroutine that enables the player to jump between the two portals he places in the level
    /// </summary>
    /// <param name="exitPortal"></param>
    /// <returns></returns>
    public IEnumerator JumpPortal(string exitPortal)
    {
        GameObject outPortal = GameObject.FindGameObjectWithTag(exitPortal);
        if (outPortal != null)
        {
            outPortal.GetComponent<BoxCollider2D>().enabled = false;
            if (outPortal != null)
            {
                transform.position = new Vector2(outPortal.transform.position.x, outPortal.transform.position.y);
                Quaternion newRotation = Quaternion.Euler(-outPortal.transform.rotation.x, transform.rotation.y, transform.rotation.z);
                transform.rotation = newRotation;
            }
            yield return new WaitForSeconds(0.4f);
            outPortal.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void OnEnable()
    {
        GameEvents.current.onCameraShiftStarted += TurnPlayerActive;
    }
    public void OnDisable()
    {
        GameEvents.current.onCameraShiftStarted -= TurnPlayerActive;

    }
    void TurnPlayerActive()
    {
        isPlayerActive = false;
    }
}
