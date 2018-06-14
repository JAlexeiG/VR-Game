using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour {

    private Transform trans;
    private Renderer renderer;

    [SerializeField]
    private GameObject[] positions;

    [SerializeField]
    private int positionNum;

    private Transform currentPosition;
    private NavMeshAgent agent;


    public bool trigger;

    public bool flip;

   
	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
        renderer = GetComponent<Renderer>();
        agent = GetComponent<NavMeshAgent>();
        positions = GameObject.FindGameObjectsWithTag("Position");
        NewPos();
        agent.speed = 20;
        checkTarget();
	}

    private void Update()
    {
        checkTarget();
        if (trigger)
        {
            agent.SetDestination(currentPosition.position);
            trigger = false;
        }

        if(flip)
        {
            EnemySelector.instance.newTarget();
            EnemySelector.instance.killTarget(gameObject);
        }
    }

    private void NewPos()
    {
        int nextPos;
        nextPos = Random.Range(0, positions.Length);

        

        if (positionNum == nextPos)
        {
            NewPos();
        }
        else
        {
            positionNum = nextPos;
            currentPosition = positions[positionNum].transform;
            agent.SetDestination(currentPosition.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentPosition)
        {
            if (other.name == currentPosition.name)
            {
                NewPos();
            }
        }
    }

    public void checkTarget()
    {
        if (EnemySelector.instance.checkTarget() == gameObject)
        {
            gameObject.layer = 8;
            renderer.material.color = Color.red;
        }
        else
        {
            gameObject.layer = 9;
        }
    }

    public void kill()
    {
        flip = true;
    }
}
