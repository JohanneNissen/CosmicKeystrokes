using UnityEngine;
using UnityEngine.SceneManagement;

public class SparkController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float gridSize = 1f;           // Size of one grid step (usually 1)
    public float moveDuration = 0.15f;    // Time to animate the full slide (0 = instant)

    [Header("Input")]
    public bool enableArrowKeys = true;   // Toggle arrow key input

    private bool isMoving = false;

    public AudioClip[] clips;

    public AudioSource source;

    public ParticleSystem clickEffect;

    public IntroController introcontroller;

    private void Update()
    {
        /*
        if (!enableArrowKeys || isMoving) return;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            MoveRight();

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            MoveLeft();

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            MoveUp();

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            MoveDown();
        */
    }

  
    public void MoveRight()
    {
        if (isMoving) return;

        source.clip = clips[0];
        source.pitch = Random.Range(0.6f, 1.4f);
        source.Play();

        MoveInDirection(Vector3.right);
    }

    public void MoveLeft()
    {
        if (isMoving) return;

        source.clip = clips[0];
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();

        MoveInDirection(Vector3.left);
    }

    public void MoveUp()
    {
        if (isMoving) return;

        source.clip = clips[0];
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();

        MoveInDirection(Vector3.forward);
    }

    public void MoveDown()
    {
        if (isMoving) return;

        source.clip = clips[0];
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();

        MoveInDirection(Vector3.back);
    }

    private void MoveInDirection(Vector3 direction)
    {
        direction.Normalize();
        StartCoroutine(SlideCoroutine(direction));
    }

    private System.Collections.IEnumerator SlideCoroutine(Vector3 dir)
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        Vector3 currentPos = startPos;
        Vector3 nextPos = currentPos + dir * gridSize;

        Vector3 finalTarget = currentPos;

        while (true)
        {
            // Check at mid-height to reliably detect objects at Y=0
            Vector3 checkPosition = new Vector3(nextPos.x, 0.5f, nextPos.z);

            Collider[] hits = Physics.OverlapBox(
                checkPosition,
                new Vector3(0.45f, 1.0f, 0.45f),
                Quaternion.identity
            );

            bool blockedByStud = false;
            bool landingOnJumper = false;
            bool landingOnGoal = false;
            Vector3 targetObjectPosition = Vector3.zero;

            foreach (Collider col in hits)
            {
                if (col.gameObject == gameObject) continue;

                string tag = col.tag;

                if (tag == "Stud")
                {
                    blockedByStud = true;
                    break;
                }
                else if (tag == "Jumper")
                {
                    landingOnJumper = true;
                    targetObjectPosition = col.transform.position;
                    break;
                }
                else if (tag == "Goal")
                {
                    landingOnGoal = true;
                    targetObjectPosition = col.transform.position;
                    break;
                }
            }

            if (blockedByStud)
            {
                finalTarget = currentPos;
                break;
            }

            // Move to next position
            currentPos = nextPos;
            finalTarget = currentPos;

            // Stop on Jumper or Goal
            if (landingOnJumper || landingOnGoal)
            {

                source.clip = clips[1];
                source.pitch = 1;
                source.Play();
                finalTarget = new Vector3(targetObjectPosition.x, transform.position.y, targetObjectPosition.z);
                if (clickEffect != null)
                {
                    Instantiate(clickEffect, finalTarget, clickEffect.transform.rotation);
                }

                if (landingOnGoal)
                {
                    introcontroller.gameRunning = false;
                    introcontroller.goalReached = true;

                }



                break;
            }

            nextPos = currentPos + dir * gridSize;
        }

        // Animate the movement
        Vector3 startMovementPos = transform.position;
        float elapsed = 0f;

        if (moveDuration > 0f)
        {
            while (elapsed < moveDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / moveDuration);
                transform.position = Vector3.Lerp(startMovementPos, finalTarget, t);
                yield return null;
            }
        }

        // Snap to final position
        transform.position = finalTarget;
        isMoving = false;

       
    }

    
}