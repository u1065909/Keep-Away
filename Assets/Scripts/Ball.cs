using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : ThrowableItem
{

    void Update()
    {
        if (isObtained)
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }

}
