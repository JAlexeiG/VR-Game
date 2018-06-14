using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    Transform[] playerPositionsTrans;
    [SerializeField]
    Vector3[] playerPositions = { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero};

    [SerializeField]
    Transform[] movementDestTrans;
    [SerializeField]
    Vector3[] movementDest = { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero,};

    GameObject player;
    Transform playerTrans;

    [SerializeField]
    GameObject playerMover;
    [SerializeField]
    GameObject playerCam;
    
    private int playerPosNum;

    public bool move;
    private bool moving;
    private GameObject newMovement;

    private bool playing;

    private void Start()
    {
        playing = false;
        if (SceneManagerScript.instance.checkScene() == 1)
        {
            playing = true;
            for (int i = 0; i < playerPositionsTrans.Length; i++)
            {
                playerPositions[i] = playerPositionsTrans[i].position;
            }
            for (int i = 0; i < movementDestTrans.Length; i++)
            {
                movementDest[i] = movementDestTrans[i].position;
            }
            player = FindObjectOfType<PlayerScript>().gameObject;
            playerTrans = player.transform;
            playerPosNum = 0;
        }
    }
    private void Update()
    {
        if(!playing && SceneManagerScript.instance.checkScene() == 1)
        {
            for (int i = 0; i < playerPositionsTrans.Length; i++)
            {
                playerPositions[i] = playerPositionsTrans[i].position;
            }
            for (int i = 0; i < movementDestTrans.Length; i++)
            {
                movementDest[i] = movementDestTrans[i].position;
            }
            player = FindObjectOfType<PlayerScript>().gameObject;
            playerTrans = player.transform;
            playerPosNum = 0;
        }
        if (SceneManagerScript.instance.checkScene() == 1)
        {
            playing = true;
        }
        if (playing)
        {
            if (move)
            {
                move = false;
                moving = true;
                startMovement();
            }
            else if (moving)
            {
                playerCam.transform.position = newMovement.transform.position;
            }
            else
            {
                playerCam.transform.position = player.transform.position;
            }
        }
    }

    public void startMove()
    {
        move = true;
    }

    public void startMovement()
    {
        newMovement = Instantiate(playerMover, movementDestTrans[playerPosNum].position, movementDestTrans[playerPosNum].rotation);
        newMovement.GetComponent<NavMeshAgent>().enabled = true;
        Debug.Log(newMovement.transform.position);
        playerPosNum++;
        if (playerPosNum == playerPositionsTrans.Length)
        {
            playerPosNum = 0;
        }
        Debug.Log(playerPosNum);
        newMovement.GetComponent<PlayerAI>().setPosition(movementDestTrans[playerPosNum]);
        playerCam.transform.parent = newMovement.transform;
        movePlayer();
    }

    public void movePlayer()
    {
        player.transform.position = playerPositions[playerPosNum];
        
    }

    public void endMovement()
    {
        playerCam.transform.parent = player.transform;
        moving = false;
    }

    public int getPosNum()
    {
        return playerPosNum;
    }

    public bool isMoving()
    {
        return moving;
    }
}
