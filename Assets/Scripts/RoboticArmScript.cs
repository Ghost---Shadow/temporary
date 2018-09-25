using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticArmScript : MonoBehaviour
{

    public float rotationSpeed = 100;
    public float mouseSensitivityFactor = 2;
    public float magnitudeToAngleFactor = 1f;
    public float pincerAngle = 20;

    public RobotJoint baseRotation;
    public RobotJoint arm1;
    public RobotJoint arm2;
    public RobotJoint arm3;
    public RobotJoint axialRotation;
    public RobotJoint pincerLeft;
    public RobotJoint pincerRight;

    private void RotateMotor(RobotJoint joint, float magnitude)
    {
        if (Mathf.Approximately(magnitude, 0f)) return;
        float angle = joint.GetAngle();
        joint.ApplyAngle(angle + magnitude * magnitudeToAngleFactor);
        //Debug.Log(angle + magnitude * magnitudeToAngleFactor);
    }

    private void PincerControl(float magnitude)
    {
        if (Mathf.Approximately(magnitude, 0f))
        {
            pincerLeft.ApplyAngle(0);
            pincerRight.ApplyAngle(0);
            return;
        }

        //float resultantForce = (openingForce - closingForce);
        pincerLeft.ApplyAngle( pincerAngle);
        pincerRight.ApplyAngle(-pincerAngle);
    }

    private void FixedUpdate()
    {
        //float horizontalAxis = Input.GetAxis("Mouse X");
        //RotateMotor(baseRotation, -horizontalAxis * rotationSpeed * mouseSensitivityFactor);

        //float verticalAxis = Input.GetAxis("Vertical");
        //RotateMotor(arm1, verticalAxis * rotationSpeed);

        //float mouseAxis = Input.GetAxis("Mouse Y");
        //RotateMotor(arm2, mouseAxis * rotationSpeed * mouseSensitivityFactor);

        //float arm3Rotation = verticalAxis * rotationSpeed - mouseAxis * rotationSpeed;
        //RotateMotor(arm3, arm3Rotation);

        float mouseScrollAxis = Input.GetAxis("Mouse ScrollWheel");
        //RotateMotor(axialRotation, mouseScrollAxis * rotationSpeed);
        RotateMotor(arm3, mouseScrollAxis * rotationSpeed);

        float closingForce = Input.GetAxis("Fire1");
        PincerControl(closingForce);
    }
}
