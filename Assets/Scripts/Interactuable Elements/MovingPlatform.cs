using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    [SerializeField]
    private GameObject m_platform = null;

    [SerializeField]
    private Transform m_destiny = null;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            m_platform.transform.position = m_destiny.transform.position;
            gameObject.SetActive(false);
        }
    }
}
