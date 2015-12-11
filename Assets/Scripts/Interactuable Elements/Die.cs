using UnityEngine;
using System.Collections;

public class Die : MonoBehaviour {

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            UsePowerup playerPowers = coll.gameObject.GetComponent<UsePowerup>();
            if (!playerPowers._usingPower)
            {
                InformationLevel manager = FindObjectOfType<InformationLevel>();
                manager.SendMessage("Restart");
            }
            else if (playerPowers.m_ActivePower == "Poison" || playerPowers.m_ActivePower == "Explosive")
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
