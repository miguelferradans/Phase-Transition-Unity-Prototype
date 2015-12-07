using UnityEngine;
using System.Collections;

public class SetPowerUp : MonoBehaviour
{
    [SerializeField]
    public bool m_PoisonPowerup = false;
    [SerializeField]
    public bool m_ExplosionPowerup = false;
    [SerializeField]
    public bool m_CorrosionPowerup = false;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("setPoison", m_PoisonPowerup, SendMessageOptions.RequireReceiver);
            coll.gameObject.SendMessage("setExplosion", m_ExplosionPowerup, SendMessageOptions.RequireReceiver);
            coll.gameObject.SendMessage("setCorrosion", m_CorrosionPowerup, SendMessageOptions.RequireReceiver);
            this.gameObject.SetActive(false);
        }
    }
}
