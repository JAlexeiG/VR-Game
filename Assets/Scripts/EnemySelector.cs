using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MonoBehaviour {

    private static EnemySelector _instance;

    [SerializeField]
    private List<GameObject> agent_list;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private int enemySpawnNumber;
    [SerializeField]
    private Transform[] spawnPoint_list;
    [SerializeField]
    GameObject enemyPref;

    [SerializeField]
    private bool newLevel;
    [SerializeField]
    private int levelNum;
    private int levelNumMarker;

    [SerializeField]
    private float spawnTime;
    private float timer;

    private float gameTimer;


    private void Start()
    {
        agent_list.Capacity = 1;
        levelNum = 1;
        foreach(Transform spawn in spawnPoint_list)
        {
            GameObject Enemy = Instantiate(enemyPref, spawn.position, spawn.rotation);
            agent_list.Capacity++;
            agent_list.Add(Enemy);
            newTarget();
        }
    }
    private void Update()
    {
        gameTimer += Time.deltaTime;
        if (newLevel)
        {
            timer += Time.deltaTime;
            if(levelNumMarker == 0)
            {
                levelNum++;
                newLevel = false;
                newTarget();
            }
            if(timer > spawnTime)
            {
                levelNumMarker--;
                timer = 0;
                foreach (Transform spawn in spawnPoint_list)
                {
                    GameObject Enemy = Instantiate(enemyPref, spawn.position, spawn.rotation);
                    agent_list.Add(Enemy);
                }
            }
        }
        else
        {
            levelNumMarker = levelNum;
        }
    }
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void newTarget()
    {
        target = agent_list[Random.Range(0, agent_list.Count)];
    }
    
    public GameObject checkTarget()
    {
        return target;
    }

    public void killTarget(GameObject target)
    {
        HighScoreManager highscoreSet = FindObjectOfType<HighScoreManager>();
        highscoreSet.setScore(1);
        highscoreSet.setTime(gameTimer);


        Debug.Log("HighScore: " + highscoreSet.getScore()[0].ToString() + " Your Score: " + highscoreSet.getScore()[1].ToString());
        Debug.Log("Your Time: " + highscoreSet.getTime()[0].ToString() + " Lowest Time: " + highscoreSet.getTime()[1].ToString());///////////

        gameTimer = 0;
        agent_list.Remove(target);
        Destroy(target);
        agent_list.Capacity--;
        newLevel = true;
    }
    
    public static EnemySelector instance
    { get; set; }
}
