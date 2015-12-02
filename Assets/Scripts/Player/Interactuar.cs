using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TransformState))]
public class Interactuar : MonoBehaviour {

    [SerializeField]
    private bool m_onTop = false;

    private GameObject m_object = null;
    private GameObject m_objectCarried = null;

    private TransformState m_transforState = null;

    void Start()
    {
        m_transforState = GetComponent<TransformState>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Hand")
        {
            m_onTop = true;
            m_object = coll.gameObject.transform.parent.gameObject;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Hand")
        {
            m_onTop = false;
            m_object = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E) && m_transforState.m_solid)
        {
            if (m_objectCarried != null)
            {
                SendMessage("DropObject", SendMessageOptions.RequireReceiver);
                m_objectCarried.GetComponent<Rigidbody>().useGravity = true;
                m_objectCarried = null;
            }
            else if (m_onTop)
            {
                m_objectCarried = m_object;
                SendMessage("SetCarriedObject", m_objectCarried, SendMessageOptions.RequireReceiver);
                m_objectCarried.GetComponent<Rigidbody>().useGravity = false;
            }
        }

        // If we leave solid state and we are carrying an object, drop it
        if ((m_transforState.m_solid != true) && (m_objectCarried != null))
        {
            SendMessage("DropObject", SendMessageOptions.RequireReceiver);
            m_objectCarried.GetComponent<Rigidbody>().useGravity = true;
            m_objectCarried = null;
        }
	}
}
