using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour 
{
    public Rigidbody bullet;
    public GameObject startPoint;
    public Texture crosshair;
    public GameObject bulletHole;
    public GameObject gunspark;
    public GameObject gunfire;

    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip outOfAmmoSound;
    
    private const float BULLET_FORCE = 15.0f;
    private const float BULLET_DISTANCE_FROM_OBJECT = 0.1f;
    private const float BULLETHOLE_DISTANCE_FROM_OBJECT = 0.1f;
    private const float CROSSHAIR_SIZE = 15.0f;
    private const float GUNFIRE_SHOW_TIME = 0.1f;

    private Rect crosshairRect = new Rect(Screen.width / 2.0f - CROSSHAIR_SIZE / 2.0f, Screen.height / 2.0f - CROSSHAIR_SIZE / 2.0f, CROSSHAIR_SIZE, CROSSHAIR_SIZE);

    private RaycastHit hit;
    private Ray ray;
    private Transform shootDir;
    private Animator anim;
    private PlayerProperties player;

    private float firingTimer;
    private float reloadTimer = 0.0f;
    private bool showGunfire = false;
    private float gunfireTimer;

	void Start() 
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerProperties>();
        shootDir = GameObject.Find("ShootDir").transform;
        gunfire.GetComponent<Renderer>().enabled = false;
        gunspark.GetComponent<Renderer>().enabled = false;
	}
	
	void Update() 
    {
        if (player.hp <= 0.0f)
        {
            return;
        }

        // Reload.
        if (reloadTimer > 0.0f)
        {
            reloadTimer -= Time.deltaTime;

            if (reloadTimer < 0.0f)
            {
                if (player.bullets >= PlayerProperties.CLIP_SIZE)
                {
                    player.bullets -= (PlayerProperties.CLIP_SIZE - player.bulletsInClip);
                    player.bulletsInClip = PlayerProperties.CLIP_SIZE;                    
                }
                else
                {
                    player.bulletsInClip = player.bullets;
                    player.bullets = 0;
                }
            }
        }
        // Shooting.
        else if (Input.GetButton("Fire1") && player.bullets >= 0)
        {
            if (firingTimer < 0.0f)
            {
                firingTimer = PlayerProperties.FIRING_SPEED;
                anim.SetBool("Shooting", true);

                if (player.bulletsInClip > 0)
                {
                    audio.PlayOneShot(shootSound);
                    showGunfire = true;
                    gunfireTimer = 0.0f;

                    --player.bulletsInClip;

                    ray.origin = Camera.main.transform.position;
                    ray.direction = shootDir.position - ray.origin;

                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.DrawLine(ray.origin, hit.point);

                        if (hit.transform.tag == "Enemy")
                        {
                            hit.transform.SendMessage("TakeDamage", player.damage);
                        }
                        else
                        {
                            Rigidbody clone;
                            clone = Instantiate(bullet, hit.point - ray.direction * BULLET_DISTANCE_FROM_OBJECT, Quaternion.identity) as Rigidbody;
                            clone.AddForce(ray.direction * BULLET_FORCE, ForceMode.Impulse);

                            if (hit.transform.tag != "Movable")
                            {
                                Instantiate(bulletHole, hit.point - ray.direction * BULLETHOLE_DISTANCE_FROM_OBJECT, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            }
                        }
                    }
                }
                else
                {
                    if (player.bullets > 0)
                    {
                        reloadTimer = PlayerProperties.RELOAD_TIME;
                        audio.PlayOneShot(reloadSound);
                    }
                    else
                    {
                        audio.PlayOneShot(outOfAmmoSound);
                    }                    
                }
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("Shooting", false);
        }

        firingTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && player.bulletsInClip != PlayerProperties.CLIP_SIZE && player.bullets != 0 && reloadTimer <= 0.0f)
        {
            reloadTimer = PlayerProperties.RELOAD_TIME;
            audio.PlayOneShot(reloadSound);
        }

        // Gunfire.
        if (gunfireTimer < GUNFIRE_SHOW_TIME)
        {
            gunfireTimer += Time.deltaTime;

            if (showGunfire)
            {
                gunfire.GetComponent<Renderer>().enabled = true;
                gunspark.GetComponent<Renderer>().enabled = true;
                showGunfire = false;
            }

            if (gunfireTimer > GUNFIRE_SHOW_TIME)
            {
                gunfire.GetComponent<Renderer>().enabled = false;
                gunspark.GetComponent<Renderer>().enabled = false;                
            }
        }
	}

    void OnGUI()
    {
        GUI.DrawTexture(crosshairRect, crosshair);
    }
}
