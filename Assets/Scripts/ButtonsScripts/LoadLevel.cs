/*   LoadLevel.cs
 *      Contains the function that loads the level
 */
using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

    [SerializeField]
    private string _level = null;

    void Load()
    {
        Application.LoadLevel(_level);
    }
}
