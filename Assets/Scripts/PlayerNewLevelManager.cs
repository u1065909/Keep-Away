using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Allows Players to Reinitialize their start values upon loading a new scene
/// </summary>
public class PlayerNewLevelManager : MonoBehaviour {

    public bool initialized_Move;
    public bool initialized_Jump;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += InitializePlayerValues;
        print("Level Loaded");
    }
    void InitializePlayerValues(Scene scene, LoadSceneMode mode)
    {
        initialized_Move = false;
        initialized_Jump = false; 
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= InitializePlayerValues;
    }
}
