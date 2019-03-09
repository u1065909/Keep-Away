using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravity : Events
{
    [Range(0.1f, 1)]
    public float gravityScale;
    private float originalScale;
    // Use this for initialization
    void Start()
    {
        
        eventSpawner = FindObjectOfType<EventSpawner>().GetComponent<EventSpawner>();
        eventName = "Low Gravity";
        players = GameObject.FindGameObjectsWithTag("Player");
        originalScale = players[0].GetComponent<Rigidbody2D>().gravityScale;
    }
    public override void ActivateEvent()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<Rigidbody2D>().gravityScale *= gravityScale;
            player.GetComponent<Player>().currentEvent = eventName;
        }
    }

    public override void DeactivateEvent()
    {
        eventSpawner.spawnedEvents.Remove(eventName);
        foreach (GameObject player in players)
        {
            player.GetComponent<Rigidbody2D>().gravityScale /= gravityScale;
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


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(WaitForEventToEnd());
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

        }
    }
}
