using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

    public float calculateComplexity(string word)
    {
        float strokepenalty = 0;
        float keypenalty = 0;

        for (int k = 0; k < word.Length; k++)
        {
            float penalty = getBasePenalty(word[k].ToString()) + getFingerPenalty(word[k].ToString()) + getRowPenalty(word[k].ToString());
            keypenalty += penalty;
        }

        for (int i = 0; i + 3 <= word.Length; i++)
        {
            string triad = word.Substring(i, 3);
            Debug.Log(triad);
            string key1 = triad.Substring(0, 1);
            string key2 = triad.Substring(1, 1);
            string key3 = triad.Substring(2, 1);

            int[] Phand = new int[3] { getHand(key1), getHand(key2), getHand(key3)};
            int[] Pfinger = new int[3] {getFinger(key1), getFinger(key2), getFinger(key3)};
            int[] Prow = new int[3] {getRowPenalty(key1), getRowPenalty(key2), getRowPenalty(key3)};

            string Chand = $"{Phand[0]}{Phand[1]}{Phand[2]}";
            Debug.Log(Chand);
            switch (Chand)
            {
                case "112":
                case "122":
                case "211":
                case "221":
                    strokepenalty += 0;
                    break;
                case "121":
                case "212":
                    strokepenalty += 1;
                    break;
                case "111":
                case "222":
                    strokepenalty += 2;
                    break;
                default:
                    Debug.Log("Unhandled combination");
                    break;
            }
        }
        
        Debug.Log("result: " + strokepenalty);
        return strokepenalty;
    }
}