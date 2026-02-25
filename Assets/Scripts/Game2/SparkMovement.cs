using System.Collections.Generic;
using UnityEngine;

public class SparkMovement : MonoBehaviour
{
    public GameObject spark;
    public List<GameObject> waypoints;
    public float speed = 100f;

    int index = 0;
    bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isMoving = true;
        }

        if (isMoving == true && index <= waypoints.Count)
        {
            Debug.Log("got into if");
            Vector3 destination = waypoints[index].transform.position;
            spark.transform.position = Vector3.MoveTowards(spark.transform.position, destination, speed * Time.deltaTime);
            float distance = Vector3.Distance(spark.transform.position, destination);

            if (distance <= 0.05)
            {
                index++;
                Debug.Log("Made index go up");

                if (index >= waypoints.Count)
                {
                    isMoving = false;
                }
            }
        }
    }
}
