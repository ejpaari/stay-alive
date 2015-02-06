using UnityEngine;
using System.Collections;

public class StatsScreenUI : MonoBehaviour 
{
    public GUISkin centered;
    public GUISkin left;
    public Texture2D background;
    public GameData gameData;
    public PlayerProperties player;

    private const float BTN_WIDTH_OFFSET = 300.0f;
    private const float LABEL_WIDTH_OFFSET = 100.0f;
    private const float BUTTON_WIDTH = 300.0f;
    private const float BUTTON_HEIGHT = 75.0f;
    
    private static float halfWidth = Screen.width / 2.0f;
    private static float halfHeight = Screen.height / 2.0f;

    private bool statsScreenVisible = false;

    private Rect completedLabel =     new Rect(0.0f, 0.0f, Screen.width, Screen.height * 0.2f);
    private Rect cashLabel =          new Rect(0.0f, 0.0f, Screen.width, Screen.height * 0.4f);

    private Rect hpBtn =        new Rect(halfWidth - BTN_WIDTH_OFFSET, halfHeight - 200.0f,   BUTTON_WIDTH, BUTTON_HEIGHT);
    private Rect hpRegenBtn =   new Rect(halfWidth - BTN_WIDTH_OFFSET, halfHeight - 100.0f,   BUTTON_WIDTH, BUTTON_HEIGHT);
    private Rect damageBtn =    new Rect(halfWidth - BTN_WIDTH_OFFSET, halfHeight,            BUTTON_WIDTH, BUTTON_HEIGHT);
    private Rect speedBtn =     new Rect(halfWidth - BTN_WIDTH_OFFSET, halfHeight + 100.0f,   BUTTON_WIDTH, BUTTON_HEIGHT);

    private Rect hpLabel =      new Rect(halfWidth + LABEL_WIDTH_OFFSET, halfHeight - 200.0f,   BUTTON_WIDTH, BUTTON_HEIGHT);
    private Rect hpRegenLabel = new Rect(halfWidth + LABEL_WIDTH_OFFSET, halfHeight - 100.0f,   BUTTON_WIDTH, BUTTON_HEIGHT);
    private Rect damageLabel =  new Rect(halfWidth + LABEL_WIDTH_OFFSET, halfHeight,            BUTTON_WIDTH, BUTTON_HEIGHT);
    private Rect speedLabel =   new Rect(halfWidth + LABEL_WIDTH_OFFSET, halfHeight + 100.0f,   BUTTON_WIDTH, BUTTON_HEIGHT);

    private Rect continueBtn = new Rect(halfWidth - BUTTON_WIDTH / 2.0f, Screen.height - 100.0f, BUTTON_WIDTH, BUTTON_HEIGHT);

    void OnGUI()
    {
        if (statsScreenVisible)
        {
            GUI.skin = centered;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);

            GUI.Label(completedLabel, "Level completed!\nUse money for better stats since the enemies get more powerful.");

            if (player.cash >= PlayerProperties.STAT_PRICE)
            {
                GUI.Label(cashLabel, "Cash: " + player.cash.ToString() + "$ (stats upgrade " + PlayerProperties.STAT_PRICE + "$)");
            }
            else
            {
                GUI.Label(cashLabel, "Not enough cash to buy stats (stats updgrade " + PlayerProperties.STAT_PRICE + ").");
            }           

            if (GUI.Button(hpBtn, "More hitpoints"))
            {
                if (player.cash >= PlayerProperties.STAT_PRICE)
                {
                    player.hp += PlayerProperties.HP_INCREASE;
                    player.cash -= PlayerProperties.STAT_PRICE;
                }
            }
            if (GUI.Button(damageBtn, "More damage"))
            {
                if (player.cash >= PlayerProperties.STAT_PRICE)
                {
                    player.damage += PlayerProperties.DAMAGE_INCREASE;
                    player.cash -= PlayerProperties.STAT_PRICE;
                }
            }
            if (GUI.Button(hpRegenBtn, "Faster recovery"))
            {
                if (player.cash >= PlayerProperties.STAT_PRICE)
                {
                    player.hpRegen += PlayerProperties.HP_REGEN_INCREASE;
                    player.cash -= PlayerProperties.STAT_PRICE;
                }
            }            
            if (GUI.Button(speedBtn, "Faster movement"))
            {
                if (player.cash >= PlayerProperties.STAT_PRICE)
                {
                    player.speed += PlayerProperties.SPEED_INCREASE;
                    player.cash -= PlayerProperties.STAT_PRICE;
                }
            }

            if (GUI.Button(continueBtn, "Continue"))
            {
                gameData.Save();
                Application.LoadLevel("Main");
            }

            GUI.skin = left;
            GUI.Label(hpLabel, "HP: " + player.hp.ToString("#"));
            GUI.Label(damageLabel, "Damage: " + player.damage.ToString());
            GUI.Label(hpRegenLabel, "Recovery speed: " + player.hpRegen.ToString());
            GUI.Label(speedLabel, "Movement speed: " + ((player.speed / PlayerProperties.DEFAULT_SPEED) * 100.0f).ToString("#") + "%");
        }
    }

    public bool StatsScreenVisible
    {
        get { return statsScreenVisible; }
        set { statsScreenVisible = value; }        
    }
}
