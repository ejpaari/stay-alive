using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour 
{
    public AudioClip stepSound1;
    public AudioClip stepSound2;

    private Movement move;

    private const float FOOTSTEP_TIME = 0.3f;

    private float footstepTimer = 0.0f;

	void Start () 
    {
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        audio.clip = stepSound1;
	}
	
	void Update () 
    {
        if (move.isMoving())
        {
            if (footstepTimer < 0.0f)
            {
                if (audio.clip == stepSound1)
                {
                    audio.clip = stepSound2;
                    audio.Play();
                }
                else
                {
                    audio.clip = stepSound1;
                    audio.Play();
                }

                footstepTimer = FOOTSTEP_TIME;
            }
            else
            {
                footstepTimer -= Time.deltaTime;
            }
        }
	}
}
