using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : Spawner {

    public HashSet<string> spawnedEvents = new HashSet<string>();
    public List<GameObject> events = new List<GameObject>(); 
    public bool isActive = false;
    public bool allEventsSpawned = false;
	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isActive)
        {
            isActive = true;
            StartCoroutine(StartSpawning());
        }
        
	}

    IEnumerator StartSpawning()
    {
        while (isActive)
        {
            
            yield return new WaitForSeconds(rateOfSpawn);
            if (events.Count == spawnedEvents.Count)
            {
                print("Same Amount of events");
                allEventsSpawned = true;
            }
            else
            {
                allEventsSpawned = false;
            }
            if (allEventsSpawned)
                continue;
            
            print("Try to Spawn");
            GameObject eventObj = events[Random.Range(0, events.Count - 1)];
            while (spawnedEvents.Contains(eventObj.GetComponent<Events>().eventName))
            {
                eventObj = events[Random.Range(0, events.Count - 1)];
            }
            
            spawnedEvents.Add(eventObj.GetComponent<Events>().eventName);
            //spawnedEvents.Remove(eventObj.GetComponent<Events>().eventName);
            while (!AttemptToSpawn(eventObj))
            {

            }
            
        }
        print("Done");
    }
    /// <summary>
    /// Checks if spawn position is safe if so then spawns item
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private bool AttemptToSpawn(GameObject obj)
    {
        Vector2 newPos = GetRandomPos();
        if (!CheckForBoundaries(newPos, obj.GetComponent<BoxCollider2D>().size))
        {
            print("Instantiated object");
            SpawnAtPos(newPos,obj);
            return true;
        }
        else
        {
            print("Failed");
            return false;
        }
    }

}
