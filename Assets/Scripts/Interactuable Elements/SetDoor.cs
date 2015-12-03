using UnityEngine;
using System.Collections;

public class SetDoor : MonoBehaviour {

    [SerializeField]
    private GameObject m_door = null;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            m_door.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
