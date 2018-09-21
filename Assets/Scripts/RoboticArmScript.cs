using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticArmScript : MonoBehaviour
{

  public float rotationSpeed = 100;
  public float mouseSensitivityFactor = 2;

  public HingeJoint baseRotation;
  public HingeJoint arm1;
  public HingeJoint arm2;
  public HingeJoint arm3;
  public HingeJoint axialRotation;
  public HingeJoint pincerLeft;
  public HingeJoint pincerRight;

  private void RotateMotor(HingeJoint joint, float speed)
  {
    JointMotor motor = joint.motor;
    motor.targetVelocity = speed;
    joint.motor = motor;
  }

  private void PincerControl(float openingForce, float closingForce)
  {
    JointMotor leftMotor = pincerLeft.motor;
    JointMotor rightMotor = pincerRight.motor;

    float resultantForce = (openingForce - closingForce) * rotationSpeed;
    leftMotor.targetVelocity = -resultantForce;
    rightMotor.targetVelocity = resultantForce;

    pincerLeft.motor = leftMotor;
    pincerRight.motor = rightMotor;
  }

  private void FixedUpdate()
  {
    float horizontalAxis = Input.GetAxis("Mouse X");
    RotateMotor(baseRotation, -horizontalAxis * rotationSpeed * mouseSensitivityFactor);

    float verticalAxis = Input.GetAxis("Vertical");
    RotateMotor(arm1, verticalAxis * rotationSpeed);

    float mouseAxis = Input.GetAxis("Mouse Y");
    RotateMotor(arm2, mouseAxis * rotationSpeed * mouseSensitivityFactor);

    float arm3Rotation = verticalAxis * rotationSpeed - mouseAxis * rotationSpeed;
    RotateMotor(arm3, arm3Rotation);

    float mouseScrollAxis = Input.GetAxis("Mouse ScrollWheel");
    RotateMotor(axialRotation, mouseScrollAxis * rotationSpeed);

    float closingForce = Input.GetAxis("Fire1");
    PincerControl(.5f, closingForce);
  }
}
