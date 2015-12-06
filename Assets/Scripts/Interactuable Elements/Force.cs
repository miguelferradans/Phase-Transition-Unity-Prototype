using UnityEngine;
using System.Collections;

public class Force : MonoBehaviour {
    [SerializeField]
    private float _velocity = 5.0f;
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.SendMessage("setGasVelocity", _velocity, SendMessageOptions.RequireReceiver);
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
