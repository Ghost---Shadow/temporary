﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {

    public float speed = 0.1f;
    public float rotationSpeed = 5f;
    public Transform actual;

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool QPressed = Input.GetKey(KeyCode.Q);
        bool EPressed = Input.GetKey(KeyCode.E);

        Vector3 myPos = this.transform.position;

        myPos += transform.up * vertical * speed;
        myPos += - transform.forward * horizontal * speed;

        float rotateDirection = QPressed ? -1 : (EPressed ? 1 : 0);

        this.transform.position = myPos;

        // Rotate around the body
        this.transform.RotateAround(this.transform.parent.position, Vector3.up, rotateDirection * rotationSpeed);
    }
}
