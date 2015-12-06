using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour {
    [Header("Patrolling Options")]
    [SerializeField]
    private float m_patrolSpeed = 5.0f;
    [SerializeField]
    private float m_patrolPause = 3.0f;

    private CharacterController m_enemyController;
    private Transform[] m_waypoints;


	// Use this for initialization
	void Start () {
        m_enemyController = GetComponent<CharacterController>();
        getWPTransforms();
	}

    void getWPTransforms(){
        GameObject waypoints = GameObject.Find("Waypoints");

        for (int i = 0; i < waypoints.transform.childCount; i++)
            m_waypoints[i] = waypoints.transform.GetChild(i).transform;
     
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
