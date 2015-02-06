using UnityEngine;
using System.Collections;

public class SurviveTime : MonoBehaviour 
{
    public const float SURVIVE_TIME = 90.0f;
    
    public GUISkin left;
    public GUISkin centered;
    
    private Rect timerPos = new Rect(0.0f, 0.0f, Screen.width, Screen.height * 0.07f);
    private Rect buttonPos = new Rect(Screen.width / 2.0f - 150.0f, Screen.height / 2.0f, 300.0f, 75.0f);
    private Rect textPos = new Rect(0.0f, Screen.height / 2.0f - Screen.height * 0.1f, Screen.width, Screen.height * 0.07f);

    private PlayerProperties player;
    private StatsScreenUI statsScreen;
    private GameData data;

    private bool levelCompleted = false;
    private float startHp;
    private float surviveTimer;

	void Start()
    {        
        statsScreen = GameObject.Find("StatsScreen").GetComponent<StatsScreenUI>();
        player = GameObject.Find("Player").GetComponent<PlayerProperties>();
        data = GameObject.Find("GameController").GetComponent<GameData>();

        startHp = player.hp;
        Time.timeScale = 1.0f;
        surviveTimer = SURVIVE_TIME;
    }
	
	void Update() 
    {
        if (surviveTimer > 0.0f && !levelCompleted && player.isAlive())
        {
            surviveTimer -= Time.deltaTime;
        }
        else if (surviveTimer <= 0.0f && !levelCompleted && player.isAlive())
        {
            surviveTimer = 0.0f;
            levelCompleted = true;
            Time.timeScale = 0.0f;
            Screen.showCursor = true;
        }
	}

    void OnGUI()
    {
        if (statsScreen.StatsScreenVisible)
        {
            return;
        }

        GUI.skin = left;
        GUI.Label(timerPos, "Time to survive: " + surviveTimer.ToString("#.##"));

        GUI.skin = centered;

        if (levelCompleted)
        {
            if (data.isLastLevel())
            {
                GUI.Label(textPos, "Gongrats! Game Completed!");

                if (GUI.Button(buttonPos, "Reset"))
                {
                    data.Reset();
                    Application.LoadLevel("StartScreen");
                }
            }
            else
            {
                GUI.Label(textPos, "Level Completed!");

                if (GUI.Button(buttonPos, "Continue"))
                {
                    player.hp = startHp;
                    statsScreen.StatsScreenVisible = true;
                }
            }            
        }
        else if (!player.isAlive())
        {
            GUI.Label(textPos, "Game Over");

            if (GUI.Button(buttonPos, "Restart"))
            {
                Application.LoadLevel("Main");
            }
        }
    }
}
