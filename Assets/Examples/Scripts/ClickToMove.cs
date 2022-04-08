using UnityEngine;
using UnityEngine.AI;

// Use physics raycast hit from mouse click to set agent destination
[RequireComponent(typeof(NavMeshAgent))]
public class ClickToMove : MonoBehaviour
{
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();

    [SerializeField] bool _isRandomPosition = false;
    [SerializeField] float _randomDistanceRadius = 10f;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(_isRandomPosition)
        {
            RandomMove();
        }

        ClickMove();
        
        ChangeAreaSpeed();
    }

    void ChangeAreaSpeed()
    {
        NavMeshHit navHit;
        m_Agent.SamplePathPosition(-1, 0.0f, out navHit);

        int GrassMask = 1 << NavMesh.GetAreaFromName("Tall Grass");

        if (navHit.mask == GrassMask)
        {
            m_Agent.speed = 3f;
        }
        else
        {
            m_Agent.speed = 30f;
        }
    }
    
    void RandomMove()
    {
        if (m_Agent.remainingDistance < 0.1f)
        {
            Vector3 randomOffest = new Vector3(Random.Range(-_randomDistanceRadius, _randomDistanceRadius),
                                                0,
                                                Random.Range(-_randomDistanceRadius, _randomDistanceRadius));
            Vector3 newPosition = m_Agent.transform.position + randomOffest;

            m_Agent.destination =newPosition;
        }
    }    

    void ClickMove()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
            {
                m_Agent.destination = m_HitInfo.point;
            }
        }
    }
}
