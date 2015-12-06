using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour {
    [Header("Patrolling Options")]
    [SerializeField]
    private float m_patrolSpeed = 2.5f; // Movement speed of the enemy
    [SerializeField]
    private float m_patrolPause = 5.0f; // Pause time after arriving to a waypoint
    [SerializeField]
    private bool m_loop = true; // Boolean to control if the patrolling loops or stops after completing every waypoint
    [SerializeField]
    private float m_turnSpeed = 6.0f; // Turn speed when going from one waypoint to another

    private CharacterController m_enemyController;
    private Transform[] m_waypoints;
    private int m_currentWaypoint = 0;
    private float m_currentTime;



	// Use this for initialization
	void Start () {
        m_enemyController = GetComponent<CharacterController>();
        getWPTransforms();
	}

    //  Method to initialize the array of waypoints
    void getWPTransforms(){
        GameObject waypoints = GameObject.Find("Waypoints");
        m_waypoints = new Transform[waypoints.transform.childCount];

        for (int i = 0; i < waypoints.transform.childCount; i++)
            m_waypoints[i] = waypoints.transform.GetChild(i).transform;

    }
	
	// Update is called once per frame
	void Update () {
        if (m_currentWaypoint < m_waypoints.Length) // If there are waypoints available, then patrol
        {
            patrol();
        }
        else if (m_loop) // If the enemy loops between waypoints, then set the current waypoint to the first in the Array
            m_currentWaypoint = 0;
                 
	}

    void patrol()
    {
        Vector3 targetWaypoint = m_waypoints[m_currentWaypoint].position; // Position of current waypoint
        targetWaypoint.y = transform.position.y;  // Keep waypoint height to the same height as the Enemy, for consistency
        Vector3 moveDirection = targetWaypoint - transform.position; // Calculate the movement direction of the enemy
 
        if(moveDirection.magnitude < 0.5){ // If the movement direction vector magnitude is less than 0.5, it means the enemy has arrived at the current waypoint
            if (m_currentTime == 0)
                m_currentTime = Time.time; // Pause over the Waypoint
            if ((Time.time - m_currentTime) >= m_patrolPause){ // If construction to control the pause time for the enemy, when enough time has elapsed, then move to the next waypoint and reset the time counter
                m_currentWaypoint++;
                m_currentTime = 0;
            }
        }else{  // Calculate the model rotation and execute the movement with the attached CharacterController
            var rotation = Quaternion.LookRotation(targetWaypoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * m_turnSpeed);
            m_enemyController.Move(moveDirection.normalized * m_patrolSpeed * Time.deltaTime);
       }    
    }
}
