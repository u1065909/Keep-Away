using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Let's Player Customize Rules
/// </summary>
public class RuleManager : MonoBehaviour {

    public Rules rules;
    public static RuleManager instance;
    // Use this for initialization
    private void Awake()
    {
        SetDefaultRules();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start ()
    {
        
    }

    private void SetDefaultRules()
    {
        rules.name = "Default";
        rules.maxScore = 1000;
        // 5 mins
        rules.time = 300;
        rules.timePerScore = .1f;
        rules.scorePerPosession = 1;
        rules.timesHitTilStunned = 5;
        rules.playerGravity = 3;
        rules.ballBounciness = 1;
        rules.playerKnockBack = 0;
        rules.timeKnockedOut = 3;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
