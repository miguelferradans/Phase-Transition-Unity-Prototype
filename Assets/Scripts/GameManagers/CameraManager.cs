using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    private GameObject _player = null;
    private GameObject[] _trigger = null;
	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        _trigger = GameObject.FindGameObjectsWithTag("Lever");
        transform.parent = _player.transform;
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
	}

    void ChangeCameraTo(GameObject obj)
    {
        transform.parent = obj.transform;
    }

    void SetCameraPositionTo(GameObject obj)
    {
        Rigidbody playerRigidbody = _player.GetComponent<Rigidbody>();
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, transform.position.z);
        if (obj.tag == "Player")
        {
            _player.SendMessage("PlayerMoves", true, SendMessageOptions.RequireReceiver);
            _player.SendMessage("EnergyLeft", true, SendMessageOptions.RequireReceiver);
            playerRigidbody.useGravity = true;
        }
        else
        {
            _player.SendMessage("PlayerMoves", false, SendMessageOptions.RequireReceiver);
            _player.SendMessage("EnergyLeft", false, SendMessageOptions.RequireReceiver);
            
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
            playerRigidbody.useGravity = false;
        }
    }

}
