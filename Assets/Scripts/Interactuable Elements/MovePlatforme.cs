using UnityEngine;
using System.Collections;

public class MovePlatforme : MonoBehaviour {

    [SerializeField]
    private Transform _finalTransform;
    [SerializeField]
    private Transform _initialTransform;

    [SerializeField][Range(1.0f, 10.0f)]
    private float _moveSpeed = 5.0f;

    public bool _moves = false;

    private Vector3 _direction;
    private Vector3 _directionAux;
    private Vector3 _directionPlatform;

    void Awake()
    {
        _direction = _finalTransform.position - _initialTransform.position;
        _direction.Normalize();
        _directionAux = _direction;
    }

    void Update()
    {
        if (_moves)
        {
            if (_direction == _directionAux)
            {
                
                _directionPlatform = _finalTransform.position - transform.position;
                _directionPlatform.Normalize();
            }
            else if (_direction == _directionAux * -1)
            {
                
                _directionPlatform = _initialTransform.position - transform.position;
                _directionPlatform.Normalize();
                
            }

            if (_directionPlatform == _direction || _directionPlatform == Vector3.zero)
            {
                this.transform.position += _direction * _moveSpeed * Time.deltaTime;
            }
            else
            {
                _direction *= -1;
            }
            
        }
    }

    //------------------------------------------METHODS-----------------------------------------------------
    /// <summary>   PlatformMoves(bool var)
    ///     This method is used to initialize the movement of the platform or to stop it.
    ///     For it to be used it has to be called by another script.
    /// </summary>
    /// <param name="var">
    ///     var sets the movement of the platform.
    ///     Play --> true
    ///     Stop --> false
    /// </param>
    void PlatformMoves(bool var)
    {
        _moves = var;
    }
}
