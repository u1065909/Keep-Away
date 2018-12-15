using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    Player player;
    PlayerNewLevelManager pm;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        InitializeValues();
	}

    private void InitializeValues()
    {
        if (pm == null)
        {
            pm = GetComponent<PlayerNewLevelManager>();
        }
        if (!pm.initialized_PlayerInteraction)
        {
            player = GetComponent<Player>();
            pm.initialized_PlayerInteraction = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball" && !player.hasThrowableItem)
        {
            GrabBall(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Item" && !player.hasThrowableItem)
        {
            GrabItem(collision.gameObject);
        }
    }

    private void GrabItem(GameObject item)
    {
        player.hasThrowableItem = true;
    }
    private void GrabBall(GameObject ball)
    {
        player.hasThrowableItem = true;
        ball.GetComponent<Ball>().GotOwner();
        ball.GetComponent<Ball>().FollowPlayer(transform);
    }
}
