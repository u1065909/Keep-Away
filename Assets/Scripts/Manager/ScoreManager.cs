using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {


    public static ScoreManager instance;
	// Use this for initialization
	void Start ()
    {
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
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// From Least To Greatest
    /// </summary>
    /// <param name="teams"></param>
    public void OrganizeTeamsByScore(List<Team> teams)
    {
        float highestScore = teams[0].score;
        for(int i = 0; i < teams.Count; i++)
        {
            for(int j = 0; j < teams.Count; j++)
            {
                if(teams[i].score > teams[j].score)
                {
                    Team temp = teams[j];
                    teams[j] = teams[i];
                    teams[i] = temp;
                }
            }
        }
    }

    public void GivePoints(Team team, float amount)
    {
        team.score += amount;
    }

    public void LosePoints(Team team, float amount)
    {
        team.score -= amount;
    }
    public void ResetPoints(Team team)
    {
        team.score = 0;
    }
}
