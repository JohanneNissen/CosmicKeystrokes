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

    public bool KeyAllDiff (string key1, string key2, string key3)
    {
        return key1 != key2 && key2 != key3 && key1 != key3;
    }

    public bool FingerAllDiff (int f1, int f2, int f3)
    {
        return f1 != f2 && f2 != f3 && f1 != f3;
    }

    public bool Monotonic (int f1, int f2, int f3)
    {
        return (f1 > f2 && f2 > f3) || (f1 < f2 && f2 < f3);
    }

    public bool KeyTwoSame (string key1, string key2, string key3)
    {
        return (key1 == key2 && key1 != key3) ||
               (key1 == key3 && key1 != key2) ||
               (key2 == key3 && key2 != key1);
    }

    public bool FingerTwoSame (int  f1, int f2, int f3)
    {
        return (f1 == f2 && f1 != f3) ||
               (f1 == f3 && f1 != f2) ||
               (f2 == f3 && f2 != f1);
    }

    public bool FingerAllSame(int f1, int f2, int f3)
    {
        return f1 == f2 && f1 == f3;
    }

    public double calculateComplexity(string word)
    {
        //penalties
        float strokepenalty = 0;
        float rowpenalty = 0;
        float handpenalty = 0;
        float keypenalty = 0;

        for (int k = 0; k < word.Length; k++)
        {
            float penalty = getBasePenalty(word[k].ToString()) + getFingerPenalty(word[k].ToString()) + getRowPenalty(word[k].ToString());
            keypenalty += penalty;
        }

        for (int i = 0; i + 3 <= word.Length; i++)
        {
            //Split word into triad and individual letters
            string triad = word.Substring(i, 3);
            Debug.Log(triad);
            string key1 = triad.Substring(0, 1);
            string key2 = triad.Substring(1, 1);
            string key3 = triad.Substring(2, 1);

            //Save data for each key
            int[] Phand = new int[3] { getHand(key1), getHand(key2), getHand(key3)};
            int[] Pfinger = new int[3] {getFinger(key1), getFinger(key2), getFinger(key3)};
            int[] Prow = new int[3] {getRow(key1), getRow(key2), getRow(key3)};

            string Chand = $"{Phand[0]}{Phand[1]}{Phand[2]}";
            string Cfinger = $"{Pfinger[0]}{Pfinger[1]}{Pfinger[2]}";
            string Crow = $"{Prow[0]}{Prow[1]}{Prow[2]}";

            //Bool logic
            bool AllDiffKeys = KeyAllDiff(key1, key2, key3);
            bool AllDiffFingers = FingerAllDiff(Pfinger[0], Pfinger[1], Pfinger[2]);
            bool MonotonicF = Monotonic(Pfinger[0], Pfinger[1], Pfinger[2]);
            bool TwoSameKey = KeyTwoSame(key1, key2, key3);
            bool TwoSameFinger = FingerTwoSame(Pfinger[0], Pfinger[1], Pfinger[2]);
            bool AllSameFinger = FingerAllSame(Pfinger[0], Pfinger[1], Pfinger[2]);

            //Logic for hand penalty
            switch (Chand)
            {
                case "112":
                case "122":
                case "211":
                case "221":
                    handpenalty += 0;
                    break;
                case "121":
                case "212":
                    handpenalty += 1;
                    break;
                case "111":
                case "222":
                    handpenalty += 2;
                    break;
                default:
                    Debug.Log("Unhandled combination of hand: " + Chand);
                    break;
            }

            //Logic for row penalty
            switch (Crow)
            {
                case "111":
                case "222":
                case "333":
                    rowpenalty += 0;
                    break;
                case "112":
                case "113":
                case "223":
                case "233":
                case "122":
                case "133":
                    rowpenalty += 1;
                    break;
                case "221":
                case "211":
                case "311":
                case "322":
                case "331":
                case "332":
                    rowpenalty += 2;
                    break;
                case "121":
                case "212":
                case "232":
                case "323":
                    rowpenalty += 3;
                    break;
                case "123":
                    rowpenalty += 4;
                    break;
                case "132":
                case "213":
                    rowpenalty += 5;
                    break;
                case "321":
                    rowpenalty += 6;
                    break;
                case "131":
                case "231":
                case "312":
                case "313":
                    rowpenalty += 7;
                    break;
                default:
                    Debug.Log("Unhandled combination of row: " + Crow);
                    break;
            }

            //Logic for finger stroke penalty
            //Penalty of 0:
            if (AllDiffKeys && AllDiffFingers && MonotonicF)
            {
                strokepenalty += 0;
                Debug.Log("SP 0: " + triad);
            }
            //Penalty of 1:
            else if (TwoSameKey && TwoSameFinger && MonotonicF)
            {
                strokepenalty += 1;
                Debug.Log("SP 1: " + triad);
            }
            //Penalty of 3:
            else if (AllDiffKeys && AllDiffFingers && !MonotonicF)
            {
                strokepenalty += 3;
                Debug.Log("SP 3: " + triad);
            }
            //Penalty of 4:
            else if (TwoSameFinger && AllDiffKeys && !MonotonicF)
            {
                strokepenalty += 4;
                Debug.Log("SP 4: " + triad);
            }
            //Penalty of 5:
            else if (AllSameFinger && TwoSameKey)
            {
                strokepenalty += 5;
                Debug.Log("SP 5: " + triad);
            }
            //Penalty of 6:
            else if (TwoSameFinger && AllDiffKeys && MonotonicF)
            {
                strokepenalty += 6;
                Debug.Log("SP 6: " + triad);
            }
            //Penalty of 7:
            else if (AllSameFinger && AllDiffKeys)
            {
                strokepenalty += 7;
                Debug.Log("SP 7: " + triad);
            }
            //Penalty of 2:
            else if (AllDiffFingers && AllDiffKeys)
            {
                strokepenalty += 2;
                Debug.Log("SP 2: " + triad);
            }
            else
            {
                Debug.Log("Triad does not fit stroke options: " + triad);
            }
            Debug.Log("Triad: " + triad + " cfinger: " + Cfinger);
        }

        Debug.Log("result: SP: " + strokepenalty + " KP: " + keypenalty + " RP: " + rowpenalty + " HP: " + handpenalty);
        return (strokepenalty * 0.3) + (rowpenalty * 0.3) + keypenalty + handpenalty;
    }
}