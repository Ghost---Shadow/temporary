using UnityEngine;

public class RobotJoint : MonoBehaviour
{
    public Vector3 Axis;
    public Vector3 StartOffset;

    public float MinAngle;
    public float MaxAngle;

    void Awake()
    {
        StartOffset = transform.localPosition;
    }

    public void ApplyAngle(float angle){
        this.transform.localRotation = Quaternion.AngleAxis(angle, Axis);
    }

    public float GetAngle(){
        float degrees = Vector3.Dot(Axis, this.transform.localEulerAngles);
        return degrees;
    }
}