using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour 
{
    public Enemy enemy;
    public int enemyCount;

    private const float SPAWN_TIME = 2.0f;

    private ArrayList spawnPoints = new ArrayList();
    private int spawnNdx = 0;
    private Transform spawnPoint;
    private int enemiesSpawned = 0;
    private float spawnTimer = 0.0f;
    private bool spawnPointFound = false;
    private RaycastHit hitInfo;
    private GameObject player;

	void Start ()
    {
        // Get all child objects and add them to array for random positioning.
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }
        
        player = GameObject.FindGameObjectWithTag("Player");
        enemy.GetComponent<GoToObject>().Target = player.transform;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (enemiesSpawned < enemyCount && spawnTimer > SPAWN_TIME)
        {
            ++enemiesSpawned;

            // Find a spawning that is not visible for the player.
            while (!spawnPointFound)
            {
                spawnNdx = (int)(Random.value * (spawnPoints.Count - 1));
                spawnPoint = spawnPoints[spawnNdx] as Transform;

                if (Physics.Raycast(spawnPoint.position, player.transform.position, out hitInfo))
                {
                    if (hitInfo.collider.tag == "Building")
                    {                        
                        spawnPointFound = true;
                    }
                }
            }            

            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            spawnTimer = 0.0f;
            spawnPointFound = false;
        }
    }
}
