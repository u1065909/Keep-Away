using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10;
    public float wallCheckOffset;
    public bool isTouchingWall { get; private set; }
    
    [HideInInspector()]
    public bool facingRight = true;
    [HideInInspector()]
    public bool canMove = true;
    public Transform wallCheck;

    float originalWallPos;
    PlayerNewLevelManager pm;
    Rigidbody2D rb;
    JumpController jumpController;
    Player player;
	// Update is called once per frame
	void Update ()
    {
        InitializeValues();
        CheckForWall();
        if (canMove)
        {
            Move();
        }
    }
    /// <summary>
    /// Checks if Player is facing wall if so don't move in that direction
    /// </summary>
    private void CheckForWall()
    {
        if (facingRight)
        {
            wallCheck.localPosition = new Vector3(originalWallPos + wallCheckOffset, wallCheck.localPosition.y);
            isTouchingWall = Physics2D.Linecast(wallCheck.position, new Vector3(wallCheck.position.x + .1f, wallCheck.position.y), 1 << LayerMask.NameToLayer("Wall"));
        }
        else
        {
            wallCheck.localPosition = new Vector3(originalWallPos - wallCheckOffset, wallCheck.localPosition.y);
            isTouchingWall = Physics2D.Linecast(wallCheck.position, new Vector3(wallCheck.position.x - .1f, wallCheck.position.y), 1 << LayerMask.NameToLayer("Wall"));
        }
    }
    /// <summary>
    /// Start function but instead is called after a new scene loads
    /// </summary>
    private void InitializeValues()
    {
        if (pm == null)
        {
            pm = GetComponent<PlayerNewLevelManager>();
        }
        if (!pm.initialized_Move)
        {
            wallCheck = transform.Find("Wall Check");
            originalWallPos = wallCheck.localPosition.x;
            pm.initialized_Move = true;
            rb = GetComponent<Rigidbody2D>();
            jumpController = GetComponent<JumpController>();
            player = GetComponent<Player>();
        }
    }

    /// <summary>
    /// Moves Player
    /// </summary>
    private void Move()
    {
        float move = Input.GetAxis("Horizontal_Left_Stick_P" + player.playerNum);
        if (move > 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y);
            if (!isTouchingWall)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0);
                facingRight = true;
            }
            else if (!facingRight)
            {
                facingRight = true;
                transform.position += new Vector3(speed * Time.deltaTime, 0);
            }
        }
        if (move < 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y);
            if (!isTouchingWall)
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0);
                facingRight = false;
            }
            else if (facingRight)
            {
                facingRight = false;
                transform.position -= new Vector3(speed * Time.deltaTime, 0);
            }
        }
    }
    private void FixedUpdate()
    {
        
    }
}
