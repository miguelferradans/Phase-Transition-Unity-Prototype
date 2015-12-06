using UnityEngine;
using System.Collections;

public class FanRotation : MonoBehaviour
{    
        void Update()
        {
            transform.Rotate(new Vector3(0, 1, 0), 500 * Time.deltaTime);
        }   
}