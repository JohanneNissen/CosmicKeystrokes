using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SparkMovement : MonoBehaviour
{

    public GameObject spark;
    public List<GameObject> MainWP;
    private WaypointManager currentWPmanager;
    private GameObject currentWP;
    private GameObject destination;
    private List<GameObject> pathToDes;
    int current = 0;
    int index = 0;
    bool isMoving = false;
    public float speed = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWP = MainWP[0];
        currentWPmanager = MainWP[0].GetComponent<WaypointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DetermineDestination();
            Debug.Log("is moving: " + isMoving);
        }

        if (isMoving == true)
        {
            moveAlongPath();
        }
    }

    void moveAlongPath()
    {
        if (pathToDes == null || pathToDes.Count == 0)
        {
            Debug.LogError("path invalid");
            return;
        }

        if (index >= pathToDes.Count)
        {
            isMoving = false;
            currentWP = destination;
            currentWPmanager = destination.GetComponent<WaypointManager>();
            current = destination.GetComponent<WaypointManager>().number;
            Debug.Log(currentWP + " and " + currentWPmanager + " and " + isMoving);
            return;
        }

        Vector3 next = pathToDes[index].transform.position;
        spark.transform.position = Vector3.MoveTowards(spark.transform.position, next, speed * Time.deltaTime);
        float distance = Vector3.Distance(spark.transform.position, next);
        if (distance <= 0.05)
        {
            index++;
        }
        Debug.Log("moving active: " + isMoving);
    }
    void DetermineDestination()
    {
        if (currentWPmanager.pathA.GetComponent<PathManager>().isBlocked == false)
        {
            destination = currentWPmanager.nextWPA;
            pathToDes = currentWPmanager.pathA.GetComponent<PathManager>().path;
            isMoving = true;
        }
        else if (currentWPmanager.pathB.GetComponent<PathManager>().isBlocked == false)
        {
            destination = currentWPmanager.nextWPB;
            pathToDes = currentWPmanager.pathB.GetComponent<PathManager>().path;
            isMoving = true;
        }
        else
        {
            Debug.Log("both paths are blocked");
            isMoving = false;
        }
    }
}
