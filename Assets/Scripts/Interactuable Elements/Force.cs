using UnityEngine;
using System.Collections;

public class Force : MonoBehaviour {
    [SerializeField]
    private Vector3 _velocity;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.SendMessage("setGasVelocity", _velocity, SendMessageOptions.RequireReceiver);
            Debug.Log(coll.tag);
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.SendMessage("setGasVelocity", _velocity, SendMessageOptions.RequireReceiver);
            Debug.Log(coll.tag);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.SendMessage("resetGasVelocity", SendMessageOptions.RequireReceiver);
        }
    }
}
