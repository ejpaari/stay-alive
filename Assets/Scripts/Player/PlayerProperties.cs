using UnityEngine;
using System.Collections;

public class PlayerProperties : MonoBehaviour 
{
    // Start values.
    public const float DEFAULT_SPEED = 3.0f;
    public const float DEFAULT_HP = 100.0f;
    public const float DEFAULT_REGEN = 1.0f;
    public const float DEFAULT_DAMAGE = 10.0f;
    
    public const float HP_INCREASE = 10.0f;
    public const float HP_REGEN_INCREASE = 1.0f;
    public const float SPEED_INCREASE = 0.15f;
    public const float RUN_SPEED_MULTIPLIER = 1.0f;
    public const float FIRING_SPEED = 0.1f;
    public const float DAMAGE_INCREASE = 2.0f;
    public const float RELOAD_TIME = 1.5f;
    public const int CLIP_SIZE = 30;
    public const int BULLETS_AT_START = 120;
    public const int STAT_PRICE = 100;

    public float hp;
    public float hpRegen;
    public float speed;
    public float damage;
    public int bullets;
    public int bulletsInClip;
    public int cash;

    void Start()
    {
        bulletsInClip = CLIP_SIZE;
        bullets = BULLETS_AT_START;
    }

    public bool isAlive()
    {
        return hp > 0.0f;
    }
}
