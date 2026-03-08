using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class G1ManagerScript : MonoBehaviour
{
    
    public AudioSource source;
    public HashTest ord;
    public AudioClip næsteOrd;

    public ScrollUV starfieldFront;
    public G1Timer timer;
    public G1DistanceCounter DistanceCounter;
    public ParticleSystem flameEffect;
    private ParticleSystem.MainModule flameMain;



    private void Awake()
    {
        ord = gameObject.GetComponent<HashTest>();
        source = gameObject.GetComponent<AudioSource>();

        if (flameEffect != null)
            flameMain = flameEffect.main;
    }

    /*private void Start()
    {
        //StartCoroutine(PlayNextWord());
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        { 
         IncreaseSpeed();
        
        }



    }


    void IncreaseSpeed()
    {
        DistanceCounter.countSpeed = DistanceCounter.countSpeed + 11f;
        starfieldFront.parralax = starfieldFront.parralax - 0.2f;
        if (starfieldFront.parralax <= 2f)
        {
            starfieldFront.parralax = 2f;
        }

        if (starfieldFront.parralax >= 12f)
        {
            starfieldFront.parralax = 8;
        }

    }

    void DecreaseSpeed()
    {
        DistanceCounter.countSpeed = DistanceCounter.countSpeed - 11f;
        starfieldFront.parralax = starfieldFront.parralax + 0.2f;

        if (starfieldFront.parralax >= 12f)
        {
            starfieldFront.parralax = 12f;
        }


    }


    /*IEnumerator PlayNextWord()
    {
        source.clip = n�steOrd;
        source.Play();
        yield return new WaitForSeconds(.5f);

        source.clip = ord.GetAudio(GenerateWord());

        source.Play();
    }*/

   

    /*void ReplayCurrentWord()
    {


    }*/
    

    /*string GenerateWord()
    {

    }*/


}
