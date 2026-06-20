using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadgame3 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("enter");
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(3);
        }


    }
}
