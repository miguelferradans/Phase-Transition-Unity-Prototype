using UnityEngine;
using System.Collections;

public class CollisionEnd : MonoBehaviour {

    private GameObject m_timer;

	// Use this for initialization
	void Start () {
        m_timer = GameObject.FindGameObjectWithTag("Timer");
	}
	
	// Update is called once per frame
	void OnTriggerEnter() {
        m_timer.SendMessage("stopTimer", SendMessageOptions.RequireReceiver);
	}
}
