using UnityEngine;
using UnityEngine.AI;

// Use physics raycast hit from mouse click to set agent destination
[RequireComponent(typeof(NavMeshAgent))]
public class ClickToMove : MonoBehaviour
{
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();

    [SerializeField] private bool _isRandomPosition = false;
    [SerializeField] private float _randomDistanceRadius = 10f;

    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _grassSpeed = 1.5f;

    [SerializeField] private float _animationStop = 0.01f;
    [SerializeField] private Animator _anim;


    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
           
    }

    void Update()
    {
        if(_isRandomPosition)
        {
            RandomMove();
        }

        ClickMove();

        ChangeAreaSpeed();

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {


        if(m_Agent.velocity.magnitude < _animationStop)
        {
            _anim.SetBool("isWalking", false);
        }
        else
        {
            _anim.SetBool("isWalking", true);
        }
    }

    void ChangeAreaSpeed()
    {
        NavMeshHit navHit;
        
        m_Agent.SamplePathPosition(-1, 0.0f, out navHit);

        int GrassMask = 1 << NavMesh.GetAreaFromName("Tall Grass");

        if (navHit.mask == GrassMask)
        {
            m_Agent.speed = _grassSpeed;
        }
        else
        {
            m_Agent.speed = _speed;
        }


    }
    
    void RandomMove()
    {
        //&& m_Agent.hasPath == true

        if (m_Agent.remainingDistance < 0.1f 
            &&  m_Agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-_randomDistanceRadius, _randomDistanceRadius),
                                               0,
                                               Random.Range(-_randomDistanceRadius, _randomDistanceRadius));
            Vector3 newPosition = m_Agent.transform.position + randomOffset;

            m_Agent.SetDestination(newPosition);
        }
    }

    void ClickMove()
    {
        if (Input.GetMouseButtonDown(0)
            && !Input.GetKey(KeyCode.LeftShift))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                m_Agent.destination = m_HitInfo.point;
        }
    }
}
