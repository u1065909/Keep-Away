using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {

    public int playerNum = 1;
    public Vector3 dir;
    public float deadSpotTimer;
    public float IgnoreBallPlayerCollisionTime = .5f;
    public BoxCollider2D hitBox;
    public Transform arrow;
    PlayerNewLevelManager pm;
    Player player;
    public Ball ball;
    bool canChangeDir = true;
    float timeCount = 0;
    

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        InitializeValues();

        Aim();
        Fire();
	}
    private void Aim()
    {
        if (canChangeDir&& player.hasThrowableItem)
        {
            float horz = Input.GetAxisRaw("Horizontal_Right_Stick_P" + playerNum);
            float vert = Input.GetAxisRaw("Vertical_Right_Stick_P" + playerNum);
            Vector3 tempDir = new Vector3(horz, vert);
            if (horz != 0 || vert != 0)
            {
                dir = tempDir.normalized;
                StartCoroutine(WaitForDeadSpot());
                
                /*
                Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward,dir);
                arrow.rotation = Quaternion.Slerp(arrow.rotation, lookRotation, timeCount);
                timeCount = timeCount + Time.deltaTime;
                */
                arrow.rotation = Quaternion.LookRotation(Vector3.forward,dir );
            }
        }
    }

    /// <summary>
    /// Gives Player time to keep direction before it changes 
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForDeadSpot()
    {
        canChangeDir = false;
        yield return new WaitForSeconds(deadSpotTimer);
        canChangeDir = true;
    }

    private void Fire()
    {
        if (Input.GetButton("Fire_P" + playerNum) && player.hasThrowableItem)
        {
            arrow.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetButtonUp("Fire_P" + playerNum) && player.hasThrowableItem)
        {
            player.hasThrowableItem = false;
            player.obtainedItem.GetComponent<Ball>().Free();
            player.obtainedItem.GetComponent<Rigidbody2D>().AddForce(dir * player.throwPower);
            player.obtainedItem = null;
            StartCoroutine(WaitForNoIgnore());
            arrow.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    IEnumerator WaitForNoIgnore()
    {
        hitBox.enabled = false;
        Physics2D.IgnoreLayerCollision(ball.gameObject.layer, gameObject.layer);
        yield return new WaitForSeconds(IgnoreBallPlayerCollisionTime);
        Physics2D.IgnoreLayerCollision(ball.gameObject.layer, gameObject.layer, false);
        hitBox.enabled = true;
    }
    private void InitializeValues()
    {
        if (pm == null)
        {
            pm = GetComponent<PlayerNewLevelManager>();
        }
        if (!pm.initialized_Aim)
        {
            pm.initialized_Aim = true;
            player = GetComponent<Player>();

        }
    }
}
