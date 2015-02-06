using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public GUISkin mySkin;
    public AudioClip hurt1;
    public AudioClip hurt2;

    private Animator anim;
    private PlayerProperties player;
    private float startHp;
    private float lastHp;
    private Rect hudPos = new Rect(0.0f, Screen.height - Screen.height * 0.1f, Screen.width, Screen.height * 0.07f);

	void Start () 
    {
        player = GetComponent<PlayerProperties>();
        anim = GetComponent<Animator>();

        startHp = player.hp;
        Screen.showCursor = false;
	}
	
	void Update () 
    {
        if (player.hp <= 0.0f)
        {
            return;
        }
        else if (player.hp <= startHp)
        {
            player.hp += player.hpRegen * Time.deltaTime;
        }

        // Player took damage since last update, play hurt audio.
        if (player.hp < lastHp)
        {
            if (!audio.isPlaying)
            {
                if (audio.clip == hurt1)
                {
                    audio.clip = hurt2;
                }
                else
                {
                    audio.clip = hurt1;
                }

                audio.Play();
            }
        }

        lastHp = player.hp;
	}

    void OnGUI()
    {
        GUI.skin = mySkin;

        GUI.Label(hudPos, "HP: " + player.hp.ToString("#") + " / " + startHp.ToString("#") 
            + "     Bullets: " + player.bulletsInClip + " / " + player.bullets
            + "     Cash: " + player.cash);
    }

    void TakeDamage(float damage)
    {
        if (player.hp > 0.0f)
        {
            player.hp -= damage;
            anim.SetFloat("HP", player.hp);
        }
    }

    void AddCash(int cash)
    {
        player.cash += cash;
    }

    void AddBullets(int bullets)
    {
        player.bullets += bullets;
    }
}
