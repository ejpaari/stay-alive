using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    private const float DESTRUCTION_TIME = 0.5f;

	void Start() 
    {
        Destroy(this.gameObject, DESTRUCTION_TIME);
	}
}
