using UnityEngine;
using UnityEngine.SceneManagement;

public class Loaderintro : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(5);
        }


    }
}
