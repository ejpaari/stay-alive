using UnityEngine;
using System.Collections;

public class GoToObject : MonoBehaviour 
{
    public Transform target;

    private NavMeshAgent agent;
    private Vector3 targetLastPosition;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        targetLastPosition = target.position;
    }
	
	void Update () 
    {
        // Target hasn't moved, don't trigger any calculations.
        if (target.position == targetLastPosition)
        {
            return;
        }

        if (agent.enabled)
        {
            agent.SetDestination(target.position);            
        }

        targetLastPosition = target.position;
	}

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }
}
