using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ObstacleManager : MonoBehaviour
{
    public bool typeBlock;

    public GameObject obstacle;
    public GameObject connectedPath;
    public GroupColor group;

    public Vector3 posClose;
    public Vector3 posOpen;
    public bool closed;
    private bool IsMoving;

    public int speed = 100;

    public enum GroupColor
    {
        Blue,
        Green,
        Yellow
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (obstacle.transform.position == posClose)
        {
            closed = true;
            connectedPath.GetComponent<PathManager>().isBlocked = true;
        } 
        else if (obstacle.transform.position == posOpen)
        {
            closed= false;
            connectedPath.GetComponent<PathManager>().isBlocked = false;
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (IsMoving == true)
        {
            if (closed == false)
            {
                obstacle.transform.position = Vector3.MoveTowards(obstacle.transform.position, posClose, Time.deltaTime * speed);
            }
            else if (closed == true)
            {
                obstacle.transform.position = Vector3.MoveTowards(obstacle.transform.position, posOpen, Time.deltaTime * speed);
            }
        }

        if (IsMoving == true && (obstacle.transform.position == posClose || obstacle.transform.position == posOpen))
        {
            IsMoving= false;
            if (obstacle.transform.position == posOpen)
            {
                closed = false;
                connectedPath.GetComponent<PathManager>().isBlocked = false;
            }
            if (obstacle.transform.position == posClose)
            {
                closed = true;
                connectedPath.GetComponent<PathManager>().isBlocked = true;
            }
        }
    }

    public void ObsMoveStart()
    {
        IsMoving = true;
    }
}
