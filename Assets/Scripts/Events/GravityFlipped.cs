using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlipped : Events {

    GameObject[] players;
	// Use this for initialization
	void Start ()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
	}

    public override void ActivateEvent()
    {

        foreach(GameObject player in players)
        {
            JumpController jump = player.GetComponent<JumpController>();
            player.GetComponent<Rigidbody2D>().gravityScale = -1 * player.GetComponent<Rigidbody2D>().gravityScale;
            jump.jumpForce = jump.jumpForce * -1;
            jump.groundCheck.localPosition = new Vector3(jump.groundCheck.localPosition.x, -1 * jump.groundCheck.localPosition.y, jump.groundCheck.localPosition.z);
        }
    }

    public override void DeactivateEvent()
    {
        foreach (GameObject player in players)
        {
            JumpController jump = player.GetComponent<JumpController>();
            player.GetComponent<Rigidbody2D>().gravityScale = -1 * player.GetComponent<Rigidbody2D>().gravityScale;
            jump.jumpForce = jump.jumpForce * -1;
            jump.groundCheck.localPosition = new Vector3(jump.groundCheck.localPosition.x, -1 * jump.groundCheck.localPosition.y, jump.groundCheck.localPosition.z);
        }
    }

    public override IEnumerator WaitForEventToEnd()
    {
        ActivateEvent();
        yield return new WaitForSeconds(duration);
        DeactivateEvent();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(WaitForEventToEnd());
        }
    }
}
