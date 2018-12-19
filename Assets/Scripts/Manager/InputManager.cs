using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    string jumpButton = "Jump_P";
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        string[] names = Input.GetJoystickNames();

        foreach(string t in names)
        {
            if(t =="Wireless Controller")
            {
                print("hey");
            }
        }
	}
}
