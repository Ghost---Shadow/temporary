﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberScript : MonoBehaviour
{

  private Transform grabbedTransform = null;
  private bool isGrabbed = false;

  // Update is called once per frame
  void Update()
  {
    isGrabbed = Input.GetAxis("Fire1") > 0;

    if (grabbedTransform != null)
    {
      //Debug.Log("isGrabbed "+ isGrabbed.ToString());
      if (isGrabbed)
      {
        grabbedTransform.parent = this.transform;
      }
      else
      {
        grabbedTransform.parent = null;
      }
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    //Debug.Log("Enterred");
    grabbedTransform = other.transform;
  }

  private void OnTriggerExit(Collider other)
  {
    //Debug.Log("Exited");
    if (grabbedTransform != null && !isGrabbed)
    {
      grabbedTransform.parent = null;
      grabbedTransform = null;
    }
  }
}