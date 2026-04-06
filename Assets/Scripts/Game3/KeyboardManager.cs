using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    private Dictionary<string, GameObject> keyDict;

    private void Awake()
    {
        keyDict = new Dictionary<string, GameObject>();

        foreach (Transform child in transform)
        {
            if (child.name == "Outline") continue;

            string keyName = child.name.ToUpper();
            keyDict[keyName] = child.gameObject;
        }
    }
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void HighlightKey(string letter)
    {
        letter = letter.ToUpper();

        if (keyDict.TryGetValue(letter, out GameObject key))
        {
            key.SetActive(true);
        }
        else
        {
            Debug.Log("Key not found " +  letter);
        }
    }

    public void ResetKeys()
    {
        foreach (var key in keyDict.Values)
        {
            key.SetActive(false);
        }
    }
}
