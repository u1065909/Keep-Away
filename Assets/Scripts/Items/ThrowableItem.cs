using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowableItem : MonoBehaviour
{
    public Item item;
    public bool isObtained;
    public float offsetFromPlayer;
    Rigidbody2D rb;
    float originalGravity;
    GameObject owner;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Free()
    {
        rb.gravityScale = originalGravity;
        isObtained = false;

    }
    public void GotOwner()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            originalGravity = rb.gravityScale;
        }
        isObtained = true;
        rb.velocity = new Vector3(0, 0, 0);
        rb.gravityScale = 0;
    }
    public void FollowPlayer(Transform target)
    {
        StartCoroutine(Follow(target));
        owner = target.gameObject;
    }
    IEnumerator Follow(Transform target)
    {
        while (isObtained)
        {
            if(target.GetComponent<Player>().currentEvent == "GravityFlipped")
            {
                offsetFromPlayer = -1.1f;
            }
            else
            {
                offsetFromPlayer = 1.1f;
            }
            transform.position = new Vector3(target.position.x, target.position.y + offsetFromPlayer);
            yield return new WaitForEndOfFrame();
        }
    }
}
