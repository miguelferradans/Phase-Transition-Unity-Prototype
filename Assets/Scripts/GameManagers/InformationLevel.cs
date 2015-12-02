/*  InformationLevel.cs
 *      Sets up the initial position of the player and the final door.
 *      It also has the information of the name of this level and the
 *      next one to this.
 *      Also, it restart the level or Load the next one.
 */

using UnityEngine;
using System.Collections;

public class InformationLevel : MonoBehaviour {
    [Header("")]
    [SerializeField]
    private GameObject _player = null;
    [SerializeField]
    private Transform _spawnPointPlayer = null;

    [Header("")]    
    [SerializeField]
    private GameObject _finalDoor = null;
    [SerializeField]
    private Transform _spawnPointFinalDoor = null;

    [Header("")]
    [SerializeField]
    private string _levelName = null;
    [SerializeField]
    private string _nextLevelName = null;


	void Start () {
        _player.transform.position = _spawnPointPlayer.position;
        _finalDoor.transform.position = _spawnPointFinalDoor.position;
	}

    //------------------------------------------METHODS-----------------------------------------------------
    /// <summary>   Restart()
    ///     These method restarts the level we are when its called by another gameObject using a
    ///     specific script.
    /// </summary>
    void Restart()
    {
        Application.LoadLevel(_levelName);
    }

    /// <summary>   NextLevel()
    ///     Thes method loads the next level to this one when its called by another gameObject
    ///     using a specic script.
    /// </summary>
    void NextLevel()
    {
        Application.LoadLevel(_nextLevelName);
    }
	
}
