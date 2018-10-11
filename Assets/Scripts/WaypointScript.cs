using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{

  [System.Serializable]
  public class Waypoint
  {
    public Transform[] transforms;

    public bool[] isGrabbed;
    public KeyCode key;
  }

  [SerializeField]
  public Waypoint[] waypoints;
  public GameObject target;

  public float stepSize = 1f;

  private Waypoint currentWaypoint;
  public GrabberScript grabber;

  IEnumerator WaypointCoroutine()
  {
    for (int i = 0; i < currentWaypoint.transforms.Length; i += 1)
    {
      Vector3 lastPosition = i == 0 ? target.transform.position : currentWaypoint.transforms[i - 1].position;
      Vector3 nextPosition = currentWaypoint.transforms[i].position;

      float distance = Vector3.Distance(lastPosition, nextPosition);
      grabber.isGrabbed = currentWaypoint.isGrabbed[i];

      for (float t = 0f; t <= 1; t += stepSize / distance * Time.deltaTime)
      {
        target.transform.position = t * nextPosition + (1 - t) * lastPosition;
        yield return null;
      }
    }
		grabber.isOveridden = false;
  }

  void Update()
  {
    foreach (Waypoint waypoint in waypoints)
    {
      if (Input.GetKeyUp(waypoint.key))
      {
        currentWaypoint = waypoint;
				grabber.isOveridden = true;
        StartCoroutine("WaypointCoroutine");
      }
    }
  }
}
