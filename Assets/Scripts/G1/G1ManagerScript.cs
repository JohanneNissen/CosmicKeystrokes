using System.Collections;
using UnityEngine;

public class G1ManagerScript : MonoBehaviour
{
    public AudioSource source;
    public HashTest ord;
    public AudioClip næsteOrd;


    private void Awake()
    {
        ord = gameObject.GetComponent<HashTest>();
        source = gameObject.GetComponent<AudioSource>();
    }

    /*private void Start()
    {
        StartCoroutine(PlayNextWord());
    }*/


    /*IEnumerator PlayNextWord()
    {
        source.clip = næsteOrd;
        source.Play();
        yield return new WaitForSeconds(.5f);

        source.clip = ord.GetAudio(GenerateWord());

        source.Play();
    }*/

   

   /* void ReplayCurrentWord()
    {


    }/*
    

    /*string GenerateWord()
    {

    }*/


}
