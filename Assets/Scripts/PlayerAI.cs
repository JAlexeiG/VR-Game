using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour {

    private Transform trans;
    private NavMeshAgent agent;
    private PlayerMovement playerAIController;

    [SerializeField]
    private Transform positions;

    public bool start;
    // Use this for initialization
    void Start () {
        trans = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        playerAIController = FindObjectOfType<PlayerMovement>();
        agent.speed = 20;
    }
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            agent.SetDestination(positions.position);
            Debug.Log(positions.position);
        }
    }

    public void setPosition(Transform position)
    {
        positions = position;
        start = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Door " + playerAIController.getPosNum())
        {
            Debug.Log(other.name);
            Debug.Log("Player has reached dest");
            playerAIController.endMovement();
            Destroy(gameObject);
        }
    }
}
