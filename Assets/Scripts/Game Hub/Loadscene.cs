using UnityEngine;

public class Loadscene : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the base!");

            
        }
    }

}
