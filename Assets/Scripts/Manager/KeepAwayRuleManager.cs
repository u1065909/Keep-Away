using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAwayRuleManager : MonoBehaviour {

    ScoreManager scoreManager;
    RuleManager ruleManager;
    Dictionary<string, Team> teams;
    List<Team> scoreBoardTeam;
    GameObject[] players;
    Player playerThatHasPosession;
    float timePerGame;
    // Use this for initialization
    void Start ()
    {
        teams = new Dictionary<string, Team>();
        scoreBoardTeam = new List<Team>();
        GetTeams();
        ruleManager = GameObject.Find("RuleManager").GetComponent<RuleManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        timePerGame = ruleManager.rules.time;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(timePerGame > 0)
        {
            timePerGame -= Time.deltaTime;
            FindPlayerWithBall();
        }
        else
        {

        }
	}

    void GetTeams()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject t in players)
        {
            if (t.GetComponent<Team>() == null)
                continue;
            Team team = t.GetComponent<Team>();
            if (!teams.ContainsKey(team.teamName))
            {
                /*
                //Initialize player that has posession even though they don't have the ball
                if (playerThatHasPosession == null)
                    playerThatHasPosession = t.GetComponent<Player>();
                */
                teams.Add(team.teamName, team);
                scoreBoardTeam.Add(team);
            }
        }
    }
    void FindPlayerWithBall()
    {
        if (playerThatHasPosession != null && playerThatHasPosession.hasBall)
            return;
        foreach (GameObject t in players)
        {
            
            if (t.GetComponent<Team>() == null)
                continue;
            
            Player player = t.GetComponent<Player>();
            //print(player);
            if (player.obtainedItem == null)
                continue;
            //print("In here");
            if (player.obtainedItem.GetComponent<ThrowableItem>().item.isBall)
            {
                
                playerThatHasPosession = player;
                StartCoroutine(GivePlayerPoints(playerThatHasPosession));
            }
        }
    }

    IEnumerator GivePlayerPoints(Player player)
    {
        while (playerThatHasPosession.hasBall)
        {

            Team team;
            teams.TryGetValue(playerThatHasPosession.team.teamName, out team);
            scoreManager.GivePoints(team, ruleManager.rules.scorePerPosession);
            yield return new WaitForSeconds(ruleManager.rules.timePerScore);

        }
        
    }

    void LosePoints(string teamName, float amount)
    {
        Team team;
        teams.TryGetValue(teamName, out team);
        scoreManager.LosePoints(team, amount);
    }
}
