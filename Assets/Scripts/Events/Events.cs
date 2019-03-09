using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Events : MonoBehaviour
{
    public string eventName;
    public float duration;
    /// <summary>
    /// Players in the game
    /// </summary>
    [HideInInspector]
    public GameObject[] players;
    /// <summary>
    /// Spawner that the event came from
    /// </summary>
    [HideInInspector]
    public EventSpawner eventSpawner;
    public abstract void ActivateEvent();
    public abstract void DeactivateEvent();
    public abstract IEnumerator WaitForEventToEnd();
    public abstract void OnTriggerEnter2D(Collider2D collision);
    

}
