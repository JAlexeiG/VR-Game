using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    [SerializeField]
    private Transform trans;

    [SerializeField]
    private GameObject mainCam;
    [SerializeField]
    private GameObject scope;
    [SerializeField]
    private GameObject binoc;
    [SerializeField]
    private GameObject radMenu;
    [SerializeField]
    private Transform radPos;
    [SerializeField]
    private Text debugLog;
    
    private float touchTimer;
    

    [SerializeField]
    private int itemOn;


    float h;
    float v;
    // Use this for initialization
    void Start ()
    {
        radMenu.transform.SetParent(null);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalSpeed = 2.0F;
        float verticalSpeed = 2.0F;

        v += horizontalSpeed * Input.GetAxis("Mouse X");
        h -= verticalSpeed * Input.GetAxis("Mouse Y");
        mainCam.transform.eulerAngles = new Vector3(h, v, 0.0f);

    }

    void OnGUI()
    {
#if UNITY_EDITOR

        for (int i = 0; i < 3; i++)
        {
            if (Input.GetMouseButton(i) && touchTimer <= 1.5f)
            {
                Debug.Log("Mouse down");
                touchTimer += Time.deltaTime;

                radMenu.transform.position = radPos.position;
                radMenu.transform.rotation = radPos.rotation;

                RaycastHit hit;
                if (Physics.Raycast(trans.position, trans.forward, out hit))
                {
                    Transform hitTrans = hit.transform;
                    if (hitTrans.gameObject.layer == 8 /*&& itemOn == 1*/)
                    {
                        hitTrans.GetComponent<AIAgent>().kill();
                    }

                    else if (hitTrans.gameObject.layer == 9)
                    {
                        SceneManagerScript.instance.LoadLevel(0);
                    }

                    else if (hitTrans.tag == "Button")
                    {
                        hitTrans.GetComponent<Button>().onClick.Invoke();
                    }

                }

            }
            else if (Input.GetMouseButtonUp(i))
            {
                Debug.Log("Mouse up");
                RaycastHit hit;
                if (Physics.Raycast(trans.position, trans.forward, out hit))
                {
                    Transform hitTrans = hit.transform;

                    if (hitTrans.tag == "Rad")
                    {
                        if (hitTrans.name == "Item 1")
                        {
                            itemSwitch(1);
                            debugLog.text = "Item 1";
                        }
                        else if (hitTrans.name == "Item 2")
                        {
                            itemSwitch(2);
                            debugLog.text = "Item 2";
                        }
                        else if (hitTrans.name == "Item 3")
                        {
                            GetComponent<PlayerMovement>().startMove();
                        }
                        else
                        {
                            debugLog.text = "Radmenu off";
                        }
                    }
                }
                touchTimer = 0;
                radMenu.SetActive(false);
            }
            else if (touchTimer >= 1.5f && Input.GetMouseButton(i))
            {
                Debug.Log("Mouse held");
                touchTimer += Time.deltaTime;
                debugLog.text = SceneManagerScript.instance.checkScene().ToString();
                if (SceneManagerScript.instance.checkScene() == 1)
                {
                    if (!GetComponent<PlayerMovement>().isMoving())
                    {
                        radMenu.SetActive(true);
                    }
                }
                if (touchTimer < 1.25f)
                {
                    itemSwitch(0);
                }
            }
        
        }
#endif

#if UNITY_IOS || UNITY_ANDROID
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && touchTimer < 1.5f)
            {

                touchTimer += Time.deltaTime;
                
                radMenu.transform.position = radPos.position;
                radMenu.transform.rotation = radPos.rotation;

                RaycastHit hit;
                if (Physics.Raycast(trans.position, trans.forward, out hit))
                {
                    Transform hitTrans = hit.transform;
                    if (hitTrans.gameObject.layer == 8 /*&& itemOn == 1*/)
                    {
                        hitTrans.GetComponent<AIAgent>().kill();
                    }

                    else if (hitTrans.gameObject.layer == 9)
                    {
                        SceneManagerScript.instance.LoadLevel(0);
                    }

                    else if (hitTrans.tag == "Button")
                    {
                        hitTrans.GetComponent<Button>().onClick.Invoke();
                    }
                    
                }
                
            }

            if (touch.phase == TouchPhase.Ended)
            {
                RaycastHit hit;
                if (Physics.Raycast(trans.position, trans.forward, out hit))
                {
                    Transform hitTrans = hit.transform;

                    if (hitTrans.tag == "Rad")
                    {
                        if (hitTrans.name == "Item 1")
                        {
                            itemSwitch(1);
                            debugLog.text = "Item 1";
                        }
                        else if (hitTrans.name == "Item 2")
                        {
                            itemSwitch(2);
                            debugLog.text = "Item 2";
                        }
                        else if (hitTrans.name == "Item 3")
                        {
                            GetComponent<PlayerMovement>().startMove();
                        }
                        else
                        {
                            debugLog.text = "Radmenu off";
                        }
                    }
                }
                touchTimer = 0;
                radMenu.SetActive(false);
            }

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                touchTimer += Time.deltaTime;
                debugLog.text = SceneManagerScript.instance.checkScene().ToString();
                if (SceneManagerScript.instance.checkScene() == 1)
                {
                    if (!GetComponent<PlayerMovement>().isMoving())
                    {
                        radMenu.SetActive(true);
                    }
                }
                if (touchTimer < 1.25f)
                {
                    itemSwitch(0);
                }
            }
        }
#endif
    }
    
    public void itemSwitch(int itemNum)
    {
        if (itemNum == 0)
        {
            binoc.SetActive(false);
            scope.SetActive(false);
        }
        if (itemNum == 1)
        {
            scope.SetActive(true);
            binoc.SetActive(false);
        }
        if (itemNum == 2)
        {
            scope.SetActive(false);
            binoc.SetActive(true);
        }
    }
}
