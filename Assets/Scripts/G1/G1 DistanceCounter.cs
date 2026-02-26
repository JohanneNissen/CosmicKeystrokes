using TMPro;
using UnityEngine;

public class G1DistanceCounter : MonoBehaviour
{
    public TMP_Text text;          
    public float countSpeed = 1f;   


    private float currentValue = 0f;

    void Update()
    {
        currentValue += countSpeed * Time.deltaTime;
        text.text = Mathf.FloorToInt(currentValue).ToString();
        
    }


}
