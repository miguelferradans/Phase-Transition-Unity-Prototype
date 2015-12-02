/*   GoNextLevel.cs
 *      This script is used by the Final Door that sets up the next level to
 *      be played when the key enters the trigger by calling the function of
 *      the GameManager that controls the next level to load.
 */


using UnityEngine;
using System.Collections;

public class GoNextLevel : MonoBehaviour {
    [SerializeField]
    private GameObject _gameManager = null;

	void OnTriggerEnter(Collider coll){
        if(coll.gameObject.tag == "Key")
        {
            _gameManager.SendMessage("NextLevel", SendMessageOptions.RequireReceiver);
        }
    }
}
