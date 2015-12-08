using UnityEngine;
using System.Collections;

public class SetPowerUp : MonoBehaviour
{
    [System.Serializable]
    public enum powerUp
    {
        Poison, Explosive, Corrosive
    }

    public powerUp m_power;

    [SerializeField]
    private bool oneUse = false;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.SendMessage("SetPower", m_power.ToString(), SendMessageOptions.RequireReceiver);
            if(oneUse)
                this.gameObject.SetActive(false);
        }
    }
}
