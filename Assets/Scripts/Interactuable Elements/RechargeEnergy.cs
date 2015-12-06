using UnityEngine;
using System.Collections;

public class RechargeEnergy : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("RegenMaxEnergy", SendMessageOptions.DontRequireReceiver);
        }
    }
}
