using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITester : MonoBehaviour {

    public RaycastHit2D[] hit2D;
    
	// Update is called once per frame
	void Update () {
        hit2D = Physics2D.RaycastAll(transform.position, transform.forward);
        for (int i = 0; i != hit2D.Length; i++)
        {
                Debug.Log("Potato");
            if (hit2D[i])
            {
                Debug.Log(hit2D[i].transform.name);
            }
        }
	}
}
