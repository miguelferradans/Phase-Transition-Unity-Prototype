using UnityEngine;
using System.Collections;

public class RechargeEnergy : MonoBehaviour {

    [SerializeField]
    private GameObject m_tarjet = null;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            m_tarjet.SendMessage("RegenMaxEnergy", SendMessageOptions.DontRequireReceiver);
            Debug.Log("Energy Recovered!"); 
        }
    }
}
