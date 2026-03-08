using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class HubPlayerController : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference clickMoveAction;

    [Header("Movement")]
    [SerializeField] private ParticleSystem clickEffect;
    [SerializeField] private ParticleSystem flameEffect;
    [SerializeField] private LayerMask clickableLayers;
    private ParticleSystem.MainModule flameMain;

    private NavMeshAgent agent;
    private Animator animator;
    private float lookRotationSpeed = 8f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (flameEffect != null)
            flameMain = flameEffect.main;

    }

    private void OnEnable()
    {
        if (clickMoveAction != null)
            clickMoveAction.action.performed += OnClickMove;

        clickMoveAction?.action.Enable();
    }

    private void OnDisable()
    {
        if (clickMoveAction != null)
            clickMoveAction.action.performed -= OnClickMove;

        clickMoveAction?.action.Disable();
    }

    private void Update()
    {
        FaceTarget();
        CheckIfReachedDestination();
    }

    private void OnClickMove(InputAction.CallbackContext context)
    {
        Vector2 screenPos = Mouse.current.position.ReadValue(); // new Input System mouse position
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, clickableLayers))
        {
            agent.destination = hit.point;

            if (clickEffect != null)
            {
                Instantiate(clickEffect, hit.point + Vector3.up * 0.1f, clickEffect.transform.rotation);
            }

            if (flameEffect != null)
            {
                flameMain.startLifetime = 1.3f;
                flameEffect.Play();
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;

        if (direction.magnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }
    }

    private void CheckIfReachedDestination()
    {
        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance &&
            (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
        {
            if (flameEffect != null)
            {
                flameMain.startLifetime = 0f;
                flameEffect.Stop();
            }
        }
    }
}
