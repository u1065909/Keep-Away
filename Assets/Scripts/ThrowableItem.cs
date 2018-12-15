using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowableItem : MonoBehaviour
{
    
    Rigidbody2D rb;
    float originalGravity;
    public bool isObtained;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
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
        isObtained = true;
        rb.velocity = new Vector3(0, 0, 0);
        rb.gravityScale = 0;
    }
    public void FollowPlayer(Transform target)
    {
        StartCoroutine(Follow(target));
    }
    IEnumerator Follow(Transform target)
    {
        while (target.gameObject.GetComponent<Player>().hasThrowableItem)
        {
            transform.position = new Vector3(target.position.x, target.position.y + 1.1f);
            yield return new WaitForEndOfFrame();
        }
    }
}
