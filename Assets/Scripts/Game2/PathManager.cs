using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public List<GameObject> path;
    public bool isBlocked;
    public int ObsTotal;
    public int ObsOpen;

    private void Update()
    {
        if (ObsOpen == ObsTotal)
        {
            isBlocked = false;
        }
        if (ObsOpen != ObsTotal)
        {
            isBlocked = true;
        }
    }
}
