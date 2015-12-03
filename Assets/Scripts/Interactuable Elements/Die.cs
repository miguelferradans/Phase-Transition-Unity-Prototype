using UnityEngine;
using System.Collections;

public class Die : MonoBehaviour {

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InformationLevel manager = FindObjectOfType<InformationLevel>();
            manager.SendMessage("Restart");
        }
    }
}
