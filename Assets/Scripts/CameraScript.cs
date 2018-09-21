using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject cam1ViewRoot;
    public GameObject cam2ViewRoot;

    private bool tVal = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            cam1ViewRoot.SetActive(tVal);
            cam2ViewRoot.SetActive(!tVal);
            tVal = !tVal;
        }
    }
}
