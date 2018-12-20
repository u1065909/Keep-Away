using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    public float attackBoxOffset;
    public GameObject attackBox;
    public CapsuleCollider2D capsuleAttackBox;
    public Player player;
    public PlayerController playerController;
    PlayerNewLevelManager pm;
    ContactFilter2D contact;
    Collider2D[] playersHit = new Collider2D[4];

    // Use this for initialization
    void Start ()
    {
    }
	void InitializeValues()
    {
        if (pm == null)
        {
            pm = GetComponent<PlayerNewLevelManager>();
        }
        if (!pm.initialized_Attack)
        {
            contact.SetLayerMask(1<<LayerMask.NameToLayer("Player"));
            pm.initialized_Attack = true;

        }
    }
	// Update is called once per frame
	void Update ()
    {
        InitializeValues();
        if (playerController.facingRight)
        {
            attackBox.transform.localPosition = new Vector3(0 + attackBoxOffset, 0, 0);
        }
        else
        {
            attackBox.transform.localPosition = new Vector3(0 - attackBoxOffset, 0, 0);
        }
		if(Input.GetButtonDown("Interact_P" + player.playerNum))
        {
            for(int i = 0; i < playersHit.Length; i++)
            {
                playersHit[i] = null;
            }
            if(Physics2D.OverlapCapsule(attackBox.transform.position, capsuleAttackBox.size, capsuleAttackBox.direction, 0, contact, playersHit) > 0)
            {
                foreach (Collider2D playerContact in playersHit)
                {
                    if (playerContact == null)
                        continue;
                    Player playerHit = playerContact.gameObject.GetComponent<Player>();
                    if (playerHit.playerNum != player.playerNum)
                    {
                        playerHit.GotHit(0);
                        print(playerContact.gameObject.GetComponent<Player>().playerNum);
                        print(player.playerNum);
                        print("Take DMG");
                    }
                }
            }
            
            
        }
        
	}

}
