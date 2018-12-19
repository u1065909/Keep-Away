using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : ThrowableItem
{
    
    private void Start()
    {
        item.name = "Ball";
        item.isBall = true;
    }
    void Update()
    {
        if (isObtained)
        {
            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
        }
        else
        {
            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = true;
            }

        }
    }

}
