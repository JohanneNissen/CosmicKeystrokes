using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Key
{
    public string name;
    public int hand;
    public int finger;
    public int row;
    public float basepenalty;
    public int fingerpenalty;
    public int rowpenalty;
}

[Serializable]
public class Keyboard
{
    public Key[] keys;
}
public class WordComplexityAlgorithm
{
    Dictionary<string, Key> keysDic;

    public void KeyDataStore(string json)
    {
        Keyboard keyData = JsonUtility.FromJson<Keyboard>(json);

        keysDic = new Dictionary<string, Key>();

        foreach(Key key in keyData.keys)
        {
            keysDic[key.name] = key;
        }
        
    }

    public int getHand(string letter)
    {
        if (!keysDic.TryGetValue(letter, out Key key))
        {
            Debug.LogWarning($"Key {letter} not found");
            return -1;
        }

        return key.hand;
    }

    public int getFinger(string letter)
    {
        if (!keysDic.TryGetValue(letter, out Key key))
        {
            Debug.LogWarning($"Key {letter} not found");
            return -1;
        }

        return key.finger;
    }

    public int getRow(string letter)
    {
        if (!keysDic.TryGetValue(letter, out Key key))
        {
            Debug.LogWarning($"Key {letter} not found");
            return -1;
        }

        return key.row;
    }

    public float getBasePenalty(string letter)
    {
        if (!keysDic.TryGetValue(letter, out Key key))
        {
            Debug.LogWarning($"Key {letter} not found");
            return -1;
        }

        return key.basepenalty;
    }

    public int getFingerPenalty(string letter)
    {
        if (!keysDic.TryGetValue(letter, out Key key))
        {
            Debug.LogWarning($"Key {letter} not found");
            return -1;
        }

        return key.fingerpenalty;
    }

    public int getRowPenalty(string letter)
    {
        if (!keysDic.TryGetValue(letter, out Key key))
        {
            Debug.LogWarning($"Key {letter} not found");
            return -1;
        }

        return key.rowpenalty;
    }

    public float calculateComplexity(string word, string json)
    {
        /*for (int i = 0; i + 3 <= word.Length; i++)
        {
            string triad = word.Substring(i, 3);
            Debug.Log(triad);
        }*/
        return 1;
    }
}