using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    Player player;
    PlayerNewLevelManager pm;
    GameObject[] players;
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
            players = GameObject.FindGameObjectsWithTag("Player");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item" && !player.hasThrowableItem)
        {
            GrabItem(collision.gameObject);
            player.obtainedItem = collision.gameObject;
        }
    }

    
    private void GrabItem(GameObject item)
    {
        if (item.GetComponent<ThrowableItem>().item.isBall)
        {
            foreach(GameObject p in players)
            {
                Player otherPlayer = p.GetComponent<Player>();
                if(otherPlayer.playerNum != player.playerNum)
                {
                    otherPlayer.hasBall = false;
                }
            }
            player.hasBall = true;
        }
        player.hasThrowableItem = true;
        item.GetComponent<ThrowableItem>().GotOwner();
        item.GetComponent<ThrowableItem>().FollowPlayer(transform);
    }
}
