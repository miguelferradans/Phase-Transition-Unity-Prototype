using UnityEngine;
using System.Collections;

public class MoveCollider : MonoBehaviour {

    private Vector3 m_EnterScale = Vector3.one;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_EnterScale = other.transform.localScale;
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
            other.transform.localScale = m_EnterScale;
            
        }
    }
}
