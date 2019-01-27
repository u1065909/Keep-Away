using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Events : MonoBehaviour
{
    public string eventName;
    public float duration;
    public abstract void ActivateEvent();
    public abstract void DeactivateEvent();
    public abstract IEnumerator WaitForEventToEnd();
    
}
