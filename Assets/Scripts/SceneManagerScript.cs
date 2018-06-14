using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {

    private static SceneManagerScript _instance;
    private int currentScene;
    
    private void Awake()
    {
        Debug.Log("potato");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


    }


    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public static SceneManagerScript instance
    {
        get; set;
    }

    public int checkScene()
    {
        return currentScene;
    }
}
