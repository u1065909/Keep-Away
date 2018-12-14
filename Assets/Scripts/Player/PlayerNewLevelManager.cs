using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Allows Players to Reinitialize their start values upon loading a new scene
/// </summary>
public class PlayerNewLevelManager : MonoBehaviour {

    [HideInInspector()]
    public bool initialized_Move, initialized_Jump, initialized_Aim, initialized_Player, initialized_PlayerInteraction;

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
        initialized_Aim = false;
        initialized_Player = false;
        initialized_PlayerInteraction = false;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= InitializePlayerValues;
    }
}
