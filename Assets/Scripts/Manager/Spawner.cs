using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Boundaries boundaries;
    public float rateOfSpawn;
    
	// Use this for initialization
	void Start ()
    {
		
	}

    /// <summary>
    /// Returns true if spawn point is touching any walls or floors
    /// </summary>
    /// <param name="spawnPoint"></param>
    /// <returns></returns>
    public bool CheckForBoundaries(Vector2 spawnPoint,Vector2 size)
    {
        return Physics2D.OverlapBox(spawnPoint, size, 0, 1 << LayerMask.NameToLayer("Ground"))|| Physics2D.OverlapBox(spawnPoint, size, 0, 1 << LayerMask.NameToLayer("Wall"));
    }
    public Vector2 GetRandomPos()
    {
        return new Vector2(Random.Range(boundaries.left, boundaries.right), Random.Range(boundaries.down, boundaries.up)); 
    }
    public GameObject SpawnAtPos(Vector2 spawnPoint,GameObject obj)
    {
        return Instantiate(obj, spawnPoint, Quaternion.identity);
    }

    
    
        
    

}
