using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK2D : MonoBehaviour
{

  [Header("Joints")]
  public Transform Turret;
  public Transform Joint0;
  public Transform Joint1;
  public Transform Hand;

  [Header("Target")]
  public Transform Target;

  // public Transform debug;
  // public Transform debug2;

  private float length0;
  private float length1;

  void Start()
  {
    length0 = Vector3.Distance(Joint0.position, Joint1.position);
    length1 = Vector3.Distance(Joint1.position, Hand.position);
  }

  private void updateLookDirection()
  {
    Vector3 lookDirection = (Target.position - Turret.position);
    float angle = Mathf.Atan2(lookDirection.x, lookDirection.z);
    Vector3 eulerAngles = Turret.rotation.eulerAngles;
    Turret.rotation = Quaternion.Euler(eulerAngles.x, angle * Mathf.Rad2Deg, eulerAngles.z);
  }

  void Update()
  {
    updateLookDirection();

    float jointAngle0;
    float jointAngle1;

    float length2 = Vector3.Distance(Joint0.position, Target.position);

    // Angle from Joint0 and Target
    Vector3 diff = Target.position - Joint0.position;
    Vector3 baseVec = Joint0.position + new Vector3(0, diff.y, 0);
    float baseLen = Vector3.Distance(baseVec, Target.position);
    float atan = Mathf.Atan2(diff.y, baseLen) * Mathf.Rad2Deg;

    // Is the target reachable?
    // If not, we stretch as far as possible
    if (length0 + length1 < length2)
    {
      jointAngle0 = atan;
      jointAngle1 = 0f;
    }
    else
    {
      float cosAngle0 = ((length2 * length2) + (length0 * length0) - (length1 * length1)) / (2 * length2 * length0);
      float angle0 = Mathf.Acos(cosAngle0) * Mathf.Rad2Deg;

      float cosAngle1 = ((length1 * length1) + (length0 * length0) - (length2 * length2)) / (2 * length1 * length0);
      float angle1 = Mathf.Acos(cosAngle1) * Mathf.Rad2Deg;

      jointAngle0 = atan + angle0;
      jointAngle1 = angle1 - 180;
    }

    // Vector3 arm1 = new Vector3(0f, Mathf.Sin(jointAngle0 * Mathf.Deg2Rad),
    //  Mathf.Cos(jointAngle0 * Mathf.Deg2Rad)) * length0;
    // Vector3 arm2 = new Vector3(0f, Mathf.Sin((jointAngle1 + jointAngle0) * Mathf.Deg2Rad),
    //  Mathf.Cos((jointAngle0 + jointAngle1) * Mathf.Deg2Rad)) * length1;
    // debug.position = Joint0.position + arm1 + arm2;
    // debug2.position = Joint0.position + arm1;

    Joint0.localRotation = Quaternion.AngleAxis(-jointAngle0, Vector3.right);
    Joint1.localRotation = Quaternion.AngleAxis(-jointAngle1, Vector3.right);
    Hand.localRotation = Quaternion.AngleAxis(jointAngle0 + jointAngle1, Vector3.right);
  }

}
