using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlipped : Events {

    EventSpawner eventSpawner;
    GameObject[] players;
	// Use this for initialization
	void Start ()
    {
        eventSpawner = FindObjectOfType<EventSpawner>().GetComponent<EventSpawner>();
        eventName = "Gravity Flipped";
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
            player.GetComponent<Player>().currentEvent = eventName;
        }
    }

    public override void DeactivateEvent()
    {
        print(eventName);
        eventSpawner.spawnedEvents.Remove(eventName);
        foreach (GameObject player in players)
        {
            JumpController jump = player.GetComponent<JumpController>();
            player.GetComponent<Rigidbody2D>().gravityScale = -1 * player.GetComponent<Rigidbody2D>().gravityScale;
            jump.jumpForce = jump.jumpForce * -1;
            jump.groundCheck.localPosition = new Vector3(jump.groundCheck.localPosition.x, -1 * jump.groundCheck.localPosition.y, jump.groundCheck.localPosition.z);
            player.GetComponent<Player>().currentEvent = "";
            DestroyObject(gameObject);
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
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

        }
    }
}
