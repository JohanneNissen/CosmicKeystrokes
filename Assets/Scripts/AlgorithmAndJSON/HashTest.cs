using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HashTest : MonoBehaviour
{
    [System.Serializable]
    public class wordAudioPair
    {
        public string word;
        public AudioClip audio;
    }

    public List<wordAudioPair> pairlist;
    private Dictionary<string, AudioClip> audiodictionary;

    private void Awake()
    {
        audiodictionary = new Dictionary<string, AudioClip>();
        foreach (var pair in pairlist)
        {
            audiodictionary[pair.word.ToLower()] = pair.audio;
        }
    }
    
    public AudioClip GetAudio(string word)
    {
        word = word.ToLower();
        return audiodictionary.TryGetValue(word, out var audio) ? audio : null;
    }
}
