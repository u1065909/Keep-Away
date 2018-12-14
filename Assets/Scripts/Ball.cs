using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    Rigidbody2D rb;
    float originalGravity;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void Free()
    {
        rb.gravityScale = originalGravity;
        GetComponent<CircleCollider2D>().enabled = true;
    }
    public void GotOwner()
    {
        print("Hey");
        rb.velocity = new Vector3(0, 0, 0);
        rb.gravityScale = 0;
        GetComponent<CircleCollider2D>().enabled = false;
    }
    public void FollowPlayer(Transform target)
    {
        StartCoroutine(Follow(target));
    }
    IEnumerator Follow(Transform target)
    {
        while (target.gameObject.GetComponent<Player>().hasBall)
        {
            transform.position = new Vector3(target.position.x, target.position.y + 1.5f);
            yield return new WaitForEndOfFrame();
        }
    }

}
