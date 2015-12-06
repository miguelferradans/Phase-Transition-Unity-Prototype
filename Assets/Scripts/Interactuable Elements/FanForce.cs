using UnityEngine;
using System.Collections;

public class FanForce : MonoBehaviour
{

    private Rigidbody m_rg = null;
    private TransformState m_transform = null;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            m_transform = coll.GetComponent<TransformState>();
            m_rg = coll.GetComponent<Rigidbody>();
            if (m_transform.m_gas) 
            {
                Debug.Log("ha entrado en gas!");
                m_rg.AddForce(new Vector3(10, 0, 0), ForceMode.Force);
            }

        }
    }
}
