using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicroscopeUIScript : MonoBehaviour
{

  public int panSpeed = 1000;

  public bool malaria = false;

  public GameObject blueCircle;
  public GameObject yellowCircle;
  public GameObject counterUI;
  public GameObject reportUI;
  public GameObject hemocytometer;
  public Transform myRect;

  private float aliveCells = 0; // TODO: int
  private float deadCells = 0; // TODO: int
  private bool reportShowing = false;

  //private RectTransform myRect;
  private void Start()
  {
    //myRect = this.GetComponent<RectTransform>();
  }

  private void updatePan(float dx, float dy)
  {
    Vector3 p = myRect.transform.position;
    p.x += dx * Time.deltaTime;
    p.y += dy * Time.deltaTime;
    myRect.transform.position = p;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.D))
    {
      updatePan(-panSpeed, 0);
    }
    else if (Input.GetKey(KeyCode.A))
    {
      updatePan(panSpeed, 0);
    }
    else if (Input.GetKey(KeyCode.W))
    {
      updatePan(0, -panSpeed);
    }
    else if (Input.GetKey(KeyCode.S))
    {
      updatePan(0, panSpeed);
    }

    if (Input.GetKeyUp(KeyCode.Mouse0))
    {
      Vector3 mousePos = Input.mousePosition;
      GameObject temp = Instantiate(yellowCircle) as GameObject;
      temp.transform.position = mousePos;
      temp.transform.SetParent(myRect.transform);
      aliveCells += 1;
    }
    if (Input.GetKeyUp(KeyCode.Mouse1))
    {
      Vector3 mousePos = Input.mousePosition;
      GameObject temp = Instantiate(blueCircle) as GameObject;
      temp.transform.position = mousePos;
      temp.transform.SetParent(myRect.transform);
      deadCells += 1;
    }

    if (malaria)
    {
      counterUI.GetComponent<Text>().text = "Infected: " + aliveCells; //TODO: Performance
    }
    else
    {
      counterUI.GetComponent<Text>().text = "Alive: " + aliveCells + "\nDead:" + deadCells; //TODO: Performance
    }

    if (Input.GetKeyUp(KeyCode.R))
    {
      reportShowing = !reportShowing;
      reportUI.SetActive(reportShowing);
      hemocytometer.SetActive(!reportShowing);
      reportUI.GetComponent<Text>().text = "RBC count: " + (aliveCells / 4 * 50000) + "\nViability: " + (aliveCells / (aliveCells + deadCells) * 100.0f) + "%"; // TODO: Performance
    }
  }
}
