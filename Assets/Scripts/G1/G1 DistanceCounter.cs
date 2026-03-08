using TMPro;
using UnityEngine;

public class G1DistanceCounter : MonoBehaviour
{
    public TMP_Text text;          
    public float countSpeed = 1.1f;
    public bool gameRunning = true;

    public float currentValue = 0f;

    void Update()
    {
        if (gameRunning == true)
        {
            currentValue += countSpeed * Time.deltaTime;
            text.text = Mathf.FloorToInt(currentValue).ToString();
        }
    }


}
