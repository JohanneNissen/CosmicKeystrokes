using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardManager : MonoBehaviour
{
    private Dictionary<string, GameObject> keyDict;
    public Color highlightColor;
    public Color baseColor;
    private string currentHighlightedKey = "";

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

    public Transform GetKeyTransform(string letter)
    {
        letter = letter.ToUpper();
        if (keyDict.TryGetValue(letter, out GameObject key))
        {
            Transform keyTransform = key.transform;
            return keyTransform;
        }
        else
        {
            return null;
        }
    }

    public void HighlightKey(string letter)
    {
        letter = letter.ToUpper();
        currentHighlightedKey = letter;

        if (keyDict.TryGetValue(letter, out GameObject key))
        {
            key.GetComponent<Image>().color = highlightColor;
        }
        else
        {
            Debug.Log("Key not found " +  letter);
        }
    }

    public void ResetKeys()
    {
        currentHighlightedKey = "";
        foreach (var key in keyDict.Values)
        {
            key.GetComponent<Image>().color = baseColor;
        }
    }

    public IEnumerator PulseKey(string letter)
    {
        if (keyDict.TryGetValue(letter.ToUpper(), out GameObject key))
        {
            Image img = key.GetComponent<Image>();

            img.color = Color.yellow;
            yield return new WaitForSeconds(0.1f);

            if (letter == currentHighlightedKey)
            {
                img.color = highlightColor;
            }
            else
            {
                img.color = baseColor;
            }
        }
    }

}
