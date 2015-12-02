/*   InputManager.cs
 *      This scrpit contains the input of the game that the user has acces to.
 */
using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))//Restart the level
        {
            SendMessage("Restart", SendMessageOptions.RequireReceiver);
        }
	}


           

}
