using UnityEngine;
using System.Collections;

public class BulletHole : MonoBehaviour 
{
    private const float DESTRUCTION_TIME = 30.0f;

	void Start () 
    {
        Destroy(this.gameObject, DESTRUCTION_TIME);
	}
}
