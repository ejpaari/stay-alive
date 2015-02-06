using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour 
{
    public Transform objectToFace;

	void Update () 
    {
        transform.forward = objectToFace.position - transform.position;
	}
}
