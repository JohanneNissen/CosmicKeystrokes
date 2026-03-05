using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    public TextAsset wordJSON;
    List<Word> allWords;

    [Serializable]
    public class Word
    {
        public string word;
        public float complexity;
    }

    [Serializable]
    public class WordList
    {
        public List<Word> words;
    }

    void Awake()
    {
        WordList wordlist = JsonUtility.FromJson<WordList>(wordJSON.text);
        allWords = wordlist.words;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public string GenerateWord(int Cmin, int Cmax, List<string> usedWords)
    {
        List<Word> candidates = new List<Word>();

        foreach (var word in allWords)
        {
            if (word.complexity < Cmin)
            {
                continue;
            }
            if (word.complexity > Cmax)
            {
                break;
            }
            if (!usedWords.Contains(word.word))
            {
                candidates.Add(word);
            }
        }
        if (candidates.Count == 0)
        {
            Debug.Log("no unused words within this complexity range");
            return null;
        }

        Word selected = candidates[UnityEngine.Random.Range(0, candidates.Count)];
        usedWords.Add(selected.word);

        return selected.word;
    }
}
