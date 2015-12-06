﻿using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    private GameObject _player = null;
    private GameObject[] _trigger = null;
	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        _trigger = GameObject.FindGameObjectsWithTag("Lever");
        transform.parent = _player.transform;
	}

    void ChangeCameraTo(GameObject obj)
    {
        transform.parent = obj.transform;
    }

    void SetCameraPositionTo(GameObject obj)
    {
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, transform.position.z);
        if (obj.tag == "Player")
        {
            _player.SendMessage("PlayerMoves", true, SendMessageOptions.RequireReceiver);
            Debug.Log("Player can move");
        }
        else
        {
            _player.SendMessage("PlayerMoves", false, SendMessageOptions.RequireReceiver);
        }
    }

}