using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool hasThrowableItem = false;
    public bool hasBall = false;
    public bool canMove = true;
    public bool isHitable = true;
    public GameObject obtainedItem;
    public int playerHealth = 5;
    public int playerNum = 1;
    public int timesHitTilStunned = 5;
    public Team team;
    public float throwPower;
    public float timeToRecover, timeToRecoverMovement;
    public float timeKnockedOut;
    int timesHit = 0;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    IEnumerator RecoverMovement(float timeToRecoverMovement)
    {
        canMove = false;
        yield return new WaitForSeconds(timeToRecoverMovement);
        canMove = true;
    }
    IEnumerator Recover(float timeToRecover,float timeToRecoverMovement)
    {
        StartCoroutine(RecoverMovement(timeToRecoverMovement));
        isHitable = false;
        yield return new WaitForSeconds(timeToRecover);
        isHitable = true;
    }
    /// <summary>
    /// If timeStunned = 0 then reverts to default values found in Script "Player"
    /// </summary>
    /// <param name="timeStunned"></param>
    public void GotHit(float timeStunned)
    {
        hasBall = false;
        hasThrowableItem = false;
        timesHit++;
        if (timeStunned != 0)
        {
            StartCoroutine(Recover(timeStunned, timeStunned));
            timesHit = 0;
            timesHit++;
            if(timesHit == timesHitTilStunned)
                timesHit = 0;
            return;
        }
        if(timesHit == timesHitTilStunned)
        {
            StartCoroutine(Recover(timeKnockedOut, timeKnockedOut));
            timesHit = 0;
        }
        else
        {
            StartCoroutine(Recover(timeToRecover, timeToRecoverMovement));
        }
    }
    
}
