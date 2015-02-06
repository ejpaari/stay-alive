using UnityEngine;
using System.Collections;

public class EnemyProperties : MonoBehaviour 
{
    public const float DEFAULT_HP = 100.0f;
    public const float DEFAULT_REGEN = 0.5f;
    public const float DEFAULT_DPS = 15.0f;    
    public const float DEFAULT_SPEED = 3.0f;

    public const float DEAD_TIME = 1.5f;
    public const float DAMAGE_DISTANCE = 1.0f;

    public const float HP_INCREASE = 10.0f;
    public const float REGEN_INCREASE = 1.0f;
    public const float DPS_INCREASE = 10.0f;
    public const float SPEED_INCREASE = 0.25f;

    public float hp;
    public float hpRegen;
    public float dps;
    public float speed;

    public bool isAlive()
    {
        return hp > 0.0f;
    }
}
