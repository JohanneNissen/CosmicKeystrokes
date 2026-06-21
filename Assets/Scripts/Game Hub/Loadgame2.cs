using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadgame2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("enter");
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(5);
        }


    }
}
