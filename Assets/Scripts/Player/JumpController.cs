using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {


    public float jumpForce = 10;
    public float wallJumpForce = 10;
    public float groundCheckSize = 1;
    public Transform groundCheck;
    /// <summary>
    /// Starts when player wall jumps and prevents player moving a specified amount of time after
    /// </summary>
    public float wallJumpTimer;
    bool isGrounded;
    public string JumpButton = "Jump_P";
    PlayerNewLevelManager pm;
    Rigidbody2D rb;
    PlayerController playerController;
    Player player;
    	
	// Update is called once per frame
	void Update ()
    {
        InitializeValues();
        //isGrounded = Physics2D.Linecast(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - .1f), 1 << LayerMask.NameToLayer("Ground"));
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckSize, .1f), 0, 1 << LayerMask.NameToLayer("Ground"));
        WallJump();
        Jump();
        
        
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
        if (!pm.initialized_Jump)
        {
            rb = GetComponent<Rigidbody2D>();
            groundCheck = transform.Find("Ground Check");
            pm.initialized_Jump = true;
            playerController = GetComponent<PlayerController>();
            player = GetComponent<Player>();
            
        }
    }
    private void Jump()
    {
        if (Input.GetButtonDown(JumpButton + player.playerNum) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0);
            rb.AddForce(new Vector3(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void WallJump()
    {
        if (playerController.isTouchingWall && !isGrounded)
        {
            if (Input.GetButtonDown(JumpButton + player.playerNum))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0);
                if (playerController.facingRight)
                {
                    //print("RightWall");
                    playerController.facingRight = false;
                    rb.AddForce(new Vector3(-wallJumpForce, jumpForce), ForceMode2D.Impulse);
                }
                else
                {
                    //print("LeftWall");
                    playerController.facingRight = true; 
                    rb.AddForce(new Vector3(wallJumpForce, jumpForce), ForceMode2D.Impulse);
                }
                StartCoroutine(WallJumpTimerStart());
            }
        }
    }
    /// <summary>
    /// Prevents Player from moving for a certain amount of time;
    /// </summary>
    /// <returns></returns>
    IEnumerator WallJumpTimerStart()
    {
        playerController.canMove = false;
        yield return new WaitForSeconds(wallJumpTimer);
        playerController.canMove = true;
    }
}
