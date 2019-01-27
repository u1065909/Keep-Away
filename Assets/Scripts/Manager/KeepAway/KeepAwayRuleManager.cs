using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepAwayRuleManager : MonoBehaviour {

    public Dictionary<string, Team> teams;
    public List<Team> scoreBoardTeam;
    //public Transform[] scoreLocations = new Transform[4];
    public GameObject[] textTeams = new GameObject[4];

    float[] playerOrignalSpeed = new float[4];
    RuleManager ruleManager;
    ScoreManager scoreManager;
    GameObject[] players;
    Player playerThatHasPosession;
    float timePerGame;
    // Use this for initialization
    void Start ()
    {
        teams = new Dictionary<string, Team>();
        scoreBoardTeam = new List<Team>();
        ruleManager = GameObject.Find("RuleManager").GetComponent<RuleManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        players = GameObject.FindGameObjectsWithTag("Player");
        GetTeams();
        for (int i = 0; i < players.Length;i++)
        {
            playerOrignalSpeed[i] = players[i].GetComponent<PlayerController>().speed;
        }
        timePerGame = ruleManager.rules.time;
        for(int i = 0; i < scoreBoardTeam.Count; i++)
        {
            textTeams[i].SetActive(true);
            textTeams[i].GetComponent<Text>().text = scoreBoardTeam[i].name + "    "+ scoreBoardTeam[i].score;
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(timePerGame > 0)
        {
            timePerGame -= Time.deltaTime;
            FindPlayerWithBall();
            UpdateScore();
            UpdateSpeed();
        }
        else
        {

        }
	}
    void UpdateScore()
    {
        scoreManager.OrganizeTeamsByScore(scoreBoardTeam);
        for(int i = 0; i < scoreBoardTeam.Count; i++)
        {

            textTeams[i].GetComponent<Text>().text = scoreBoardTeam[i].name + "    " + scoreBoardTeam[i].score;
        }
    }

    void UpdateSpeed()
    {
        for(int i = 0; i < players.Length; i++)
        {
            // Checks if the players obtained item is a ball if so then slows them down, this only affects the player if they are holding an item that is a ball
            if(players[i].GetComponent<Player>().obtainedItem!= null && players[i].GetComponent<Player>().obtainedItem.GetComponent<ThrowableItem>().item.isBall)
            {
                players[i].GetComponent<PlayerController>().speed = playerOrignalSpeed[i]*.75f;
            }
            else
            {
                players[i].GetComponent<PlayerController>().speed = playerOrignalSpeed[i];
            }
        }
    }
    void GetTeams()
    {
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

    /// <summary>
    /// Gives Team points based on what team the player is on
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
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
