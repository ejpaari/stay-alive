using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour 
{
    public PlayerProperties player;
    public EnemyProperties enemy;

    public int lastLevel;

    private const string FILENAME = "savegame.dat";

    private int currentLevel = 0;

    void Awake()
    {
        if (System.IO.File.Exists(FILENAME))
        {
            Load();
        }
        else
        {
            Reset();
            Load();
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.LoadLevel("StartScreen");
        }
    }

    public bool Load()
    {
        System.IO.StreamReader fileReader;

        try
        {
            fileReader = new System.IO.StreamReader(FILENAME);

            currentLevel = System.Convert.ToInt32(fileReader.ReadLine());
            player.hp = System.Convert.ToSingle(fileReader.ReadLine());
            player.hpRegen = System.Convert.ToSingle(fileReader.ReadLine());
            player.damage = System.Convert.ToSingle(fileReader.ReadLine());
            player.speed = System.Convert.ToSingle(fileReader.ReadLine());
            player.cash = System.Convert.ToInt32(fileReader.ReadLine());
            enemy.hp = System.Convert.ToSingle(fileReader.ReadLine());
            enemy.hpRegen = System.Convert.ToSingle(fileReader.ReadLine());
            enemy.dps = System.Convert.ToSingle(fileReader.ReadLine());
            enemy.speed = System.Convert.ToSingle(fileReader.ReadLine());            
        }
        catch (System.IO.IOException e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        fileReader.Close();

        return true;
    }

    public bool Save()
    {
        try
        {
            System.IO.StreamWriter fileWriter;
            fileWriter = new System.IO.StreamWriter(FILENAME);

            fileWriter.WriteLine(++currentLevel);
            fileWriter.WriteLine(player.hp);
            fileWriter.WriteLine(player.hpRegen);
            fileWriter.WriteLine(player.damage);
            fileWriter.WriteLine(player.speed);
            fileWriter.WriteLine(player.cash);
            fileWriter.WriteLine(enemy.hp + EnemyProperties.HP_INCREASE);
            fileWriter.WriteLine(enemy.hpRegen + EnemyProperties.REGEN_INCREASE);
            fileWriter.WriteLine(enemy.dps + EnemyProperties.DPS_INCREASE);
            fileWriter.WriteLine(enemy.speed + EnemyProperties.SPEED_INCREASE);
            fileWriter.Close();
        }
        catch (System.IO.IOException e)
        {
            Debug.Log(e.ToString());
            return false;
        }
        
        return true;
    }

    public bool Reset()
    {
        try
        {
            System.IO.StreamWriter fileWriter;
            fileWriter = new System.IO.StreamWriter(FILENAME);
            
            fileWriter.WriteLine(0);
            fileWriter.WriteLine(PlayerProperties.DEFAULT_HP);
            fileWriter.WriteLine(PlayerProperties.DEFAULT_REGEN);
            fileWriter.WriteLine(PlayerProperties.DEFAULT_DAMAGE);
            fileWriter.WriteLine(PlayerProperties.DEFAULT_SPEED);
            fileWriter.WriteLine(0);
            fileWriter.WriteLine(EnemyProperties.DEFAULT_HP);
            fileWriter.WriteLine(EnemyProperties.DEFAULT_REGEN);
            fileWriter.WriteLine(EnemyProperties.DEFAULT_DPS);
            fileWriter.WriteLine(EnemyProperties.DEFAULT_SPEED);
            fileWriter.Close();
        }
        catch (System.IO.IOException e)
        {
            Debug.Log(e.ToString());
            Application.Quit();
        }

        return true;
    }

    public bool isLastLevel()
    {
        return currentLevel == lastLevel;
    }
}
