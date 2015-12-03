using UnityEngine;
using System.Collections;

public class SetPlatform : MonoBehaviour
{

    [SerializeField]
    private GameObject m_platform = null;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            m_platform.SendMessage("PlatformMoves", true);
            gameObject.SetActive(false);
        }
    }
}
