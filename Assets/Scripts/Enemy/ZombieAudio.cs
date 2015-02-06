using UnityEngine;
using System.Collections;

public class ZombieAudio : MonoBehaviour 
{
    public AudioClip[] sounds;

    private EnemyProperties enemy;
    private float interval = 2.0f;
    private float timer = 0.0f;

    void Start()
    {
        enemy = GetComponent<EnemyProperties>();
    }
	
	void Update () 
    {
        timer += Time.deltaTime;

        if (timer > interval && enemy.isAlive())
        {
            timer = 0.0f;
            interval = Random.value * 10.0f + 3.0f;
            audio.clip = sounds[(int)(Random.value * (sounds.Length - 1))];
            audio.Play();
        }
	}
}
