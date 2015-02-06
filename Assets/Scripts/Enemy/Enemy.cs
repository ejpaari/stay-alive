using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public AudioClip deathSound;

    private EnemyProperties enemy;
    private Animator anim;
    private NavMeshAgent navAgent;
    private GameObject player;

    private const float PLAYER_NEAR_DISTANCE = 15.0f;
    private const int NUM_SKIP_FRAMES = 10;

    private int frameCounter = 0;
    private bool checkDistanceEveryFrame = false;
    private float deadTimer = 0.0f;
    private float startHp;
    
	void Start()
    {
        enemy = GetComponent<EnemyProperties>();
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.speed = (0.5f + Random.value) * enemy.speed;
        startHp = enemy.hp;

        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update()
    {
        // Check hit points.
        if (enemy.hp <= 0.0f)
        {
			audio.clip = deathSound;
			audio.Play();

            if (GetComponent<NavMeshAgent>().enabled)
            {
                GetComponent<NavMeshAgent>().Stop();
                GetComponent<NavMeshAgent>().enabled = false;
                player.SendMessage("AddCash", 50);
            }

            deadTimer += Time.deltaTime;

            if (deadTimer > EnemyProperties.DEAD_TIME && !GetComponent<Rigidbody>().freezeRotation)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
        else if (enemy.hp <= startHp)
        {
            enemy.hp += enemy.hpRegen * Time.deltaTime;
        }

        // Small optimization: if enemy is far it is not necessary to check distance every frame.
        ++frameCounter;

        if (!checkDistanceEveryFrame && frameCounter >= NUM_SKIP_FRAMES)
        {
            if ((transform.position - player.transform.position).sqrMagnitude < PLAYER_NEAR_DISTANCE * PLAYER_NEAR_DISTANCE)
            {
                checkDistanceEveryFrame = true;
            }
            else
            {
                frameCounter = 0;
            }
        }

        if (checkDistanceEveryFrame)
        {
            // Apply damage to player if enemy is alive and close enough. Comparing squares is faster than calculating square root.
            if ((transform.position - player.transform.position).sqrMagnitude < EnemyProperties.DAMAGE_DISTANCE * EnemyProperties.DAMAGE_DISTANCE && enemy.hp > 0.0f)
            {
                player.SendMessage("TakeDamage", Time.deltaTime * enemy.dps);
            }
        }
    }

    void TakeDamage(float damage)
    {
        enemy.hp -= damage;
        anim.SetFloat("HP", enemy.hp);
    }
}
