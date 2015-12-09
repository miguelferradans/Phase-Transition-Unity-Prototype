using UnityEngine;
using System.Collections;

public class SetPlatform : MonoBehaviour
{

    [SerializeField]
    private GameObject m_platform = null;

    private GameObject _camera;

    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            StartCoroutine(CutsceneCoroutine(coll));
        }
    }

    public IEnumerator CutsceneCoroutine(Collider coll)
    {
        Debug.Log("Corutine");
        _camera.SendMessage("SetCameraPositionTo", m_platform.gameObject, SendMessageOptions.RequireReceiver);
        _camera.SendMessage("ChangeCameraTo", m_platform.gameObject, SendMessageOptions.RequireReceiver);
        yield return new WaitForSeconds(1);
        m_platform.SendMessage("PlatformMoves", true);
        yield return new WaitForSeconds(2);
        _camera.SendMessage("SetCameraPositionTo", coll.gameObject, SendMessageOptions.RequireReceiver);
        _camera.SendMessage("ChangeCameraTo", coll.gameObject, SendMessageOptions.RequireReceiver);
        gameObject.SetActive(false);
    }
}
