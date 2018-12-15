﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {

    public int playerNum = 1;
    public Vector3 dir;
    public float deadSpotTimer;
    public float IgnoreBallPlayerCollisionTime = .5f;
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
        if (canChangeDir&& player.hasBall)
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
        if (Input.GetButton("Fire_P" + playerNum) && player.hasBall)
        {
            arrow.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetButtonUp("Fire_P" + playerNum) && player.hasBall)
        {
            player.hasBall = false;
            ball.GetComponent<Ball>().Free();
            ball.GetComponent<Rigidbody2D>().AddForce(dir * player.throwPower);
            StartCoroutine(WaitForNoIgnore());
            arrow.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    IEnumerator WaitForNoIgnore()
    {
        Physics2D.IgnoreLayerCollision(ball.gameObject.layer, gameObject.layer);
        yield return new WaitForSeconds(IgnoreBallPlayerCollisionTime);
        Physics2D.IgnoreLayerCollision(ball.gameObject.layer, gameObject.layer, false);
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
