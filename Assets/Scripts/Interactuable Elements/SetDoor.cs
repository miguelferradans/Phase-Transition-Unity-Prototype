using UnityEngine;
using System.Collections;

public class SetDoor : MonoBehaviour {

    [SerializeField]
    private GameObject m_door = null;

    private GameObject _camera;

    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Rigidbody playerRigidbody = coll.GetComponent<Rigidbody>();
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
            StartCoroutine(CutsceneCoroutine(coll));
        }
    }

    public IEnumerator CutsceneCoroutine(Collider coll)
    {
        Debug.Log("Corutine");
        _camera.SendMessage("SetCameraPositionTo", m_door.gameObject, SendMessageOptions.RequireReceiver);
        yield return new WaitForSeconds(1);
        m_door.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        _camera.SendMessage("SetCameraPositionTo", coll.gameObject, SendMessageOptions.RequireReceiver);
        gameObject.SetActive(false);
    }
}
