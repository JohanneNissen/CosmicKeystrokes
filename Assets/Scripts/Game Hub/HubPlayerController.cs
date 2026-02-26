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
    [SerializeField] private LayerMask clickableLayers;

    private NavMeshAgent agent;
    private Animator animator;
    private float lookRotationSpeed = 8f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
}
