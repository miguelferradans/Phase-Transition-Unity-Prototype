using UnityEngine;
using System.Collections;

public class EnemyIA : MonoBehaviour {

    private Vector3 _direction;

    [SerializeField]
    private float _moveSpeed = 3.0f;
    [SerializeField]
    private float _turnSpeed = 2.0f;
    [SerializeField]
    private float _timePaused = 3.0f;

    [Header("IA Options")]
    [SerializeField]
    private bool _basicIA = true;

    void Start()
    {
        //Moving first right because i want
        _direction.Set(1.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        if (_basicIA)
            BasicIA();
    }

    //------------------------------------------METHODS-----------------------------------------------------

    /// <summary>   BasicIA()
    ///     The enemy moves left or right first. When he finds an obstacle he turns around.
    /// </summary>
    void BasicIA()
    {
        //Check collision to where the enemy is moving with Raycast
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(transform.position, _direction); Debug.DrawRay(transform.position, _direction);
        Ray ray2 = new Ray(transform.position + (_direction * 0.2f), new Vector3(0.0f, -1.0f, 0.0f)); Debug.DrawRay(transform.position + (_direction * 0.2f), new Vector3(0.0f, -1.0f, 0.0f));
        if (!Physics.Raycast(ray2, out hit, transform.localScale.y))
        {
            Rotate();
        }
        else if (Physics.Raycast(ray, out hit, transform.localScale.x))
        {
            if (hit.transform.tag == "Player" || hit.transform.tag == "Lever")//Si el objeto con el que choca es distinto del jugador
            {
                if (hit.transform.tag == "Player")
                    Kill(hit.transform.gameObject);
                else
                    Move();
            }
            else
            {
                Rotate();
            }
        }
        else
        {
            Move();
        }
    }

    /// <summary>   Rotate()
    ///     This method is used to rotate the enemy when it collides with an object, to look
    ///     to the other direction.
    /// </summary>
    void Rotate()
    {
        var rotation = Quaternion.LookRotation(_direction*-1);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _turnSpeed);
        if (rotation == transform.rotation)
        {
            _direction *= -1;
        }
    }

    void Move()
    {
        //Movement
        this.transform.position += _direction * _moveSpeed * Time.deltaTime;
    }

    void Kill(GameObject obj)
    {
        //obj.SetActive(false);
    }
}
