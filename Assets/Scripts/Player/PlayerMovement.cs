using UnityEngine;
using System.Collections;
[RequireComponent(typeof(TransformState))]
public class PlayerMovement : MonoBehaviour {
    [Header("Solid")]
    [SerializeField]
    private float m_velocitySolid = 5.0f;
    [SerializeField]
    private float m_maxVelocitySolid = 10.0f;

    [Header("Water")]
    [SerializeField]
    private float m_velocityWater = 1.0f;
    [SerializeField]
    private float m_maxVelocityWater = 3.0f;

    [Header("Gas")]
    [SerializeField]
    private float m_velocityGas = 0.0f;
    [SerializeField]
    private float m_maxVelocityGas = 3.0f;
    [SerializeField]
    private float m_maxRightVelocityGas = 3.0f;
    [SerializeField]
    private float m_maxLeftVelocityGas = -3.0f;
    [SerializeField]
    private float m_StepVelocityGas = 0.1f;
    [SerializeField]
    private float m_UpwardsGravityModifier = 1.05f;

    [Header("Jump")]
    [SerializeField]
    private float m_jumpForce = 250.0f;

    [Header("Booleans")]
    [SerializeField]
    private bool m_moveInAir = true;
    //[HideInInspector]
    public bool m_grounded = false;
    //[HideInInspector]
    public bool m_jumping = false;
    //[HideInInspector]
    public bool m_collidingRight = false;
    //[HideInInspector]
    public bool m_collidingLeft = false;
    [SerializeField]
    private bool _canMove = true;

    private Rigidbody m_rg = null;

    private TransformState m_transform = null;

    private GameObject m_carriedObject = null;

    void Start()
    {
        m_transform = GetComponent<TransformState>();
        m_rg = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Floor")
        {
            //If the collider is at the bot
            float distance = transform.position.y - coll.transform.position.y;
            if (distance >= 0)
            {
                m_grounded = true;
                m_jumping = false;
            }
        }

        if(coll.gameObject.tag == "Wall")
        {
            float distance = transform.position.x - coll.transform.position.x;
            //If the collider is at the right
            if (distance <= 0)
                m_collidingRight = true;
            else if( distance >= 0)
                m_collidingLeft = true;

        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Floor")
            m_grounded = false;
        if(coll.gameObject.tag == "Wall")
        {
            if (m_collidingLeft)
                m_collidingLeft = false;
            if (m_collidingRight)
                m_collidingRight = false;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_canMove)
        {
            if (m_transform.m_solid)
                SolidMovement();
            else if (m_transform.m_water)
                WaterMovement();
            else if (m_transform.m_gas)
                GasMovement();
            MoveCarriedObject();
        }
	}


    void SolidMovement()
    {
        ControlSolidMovement();
        if (Input.GetKey(KeyCode.Space) && m_grounded)//Jump
        {
            m_rg.AddForce(new Vector3(0.0f, m_jumpForce, 0.0f));
            m_jumping = true;
        }

        if (Input.GetKey(KeyCode.RightArrow) && !m_collidingRight && MoveInAir())//Right
        {
            m_rg.AddForce(new Vector3(m_velocitySolid, 0.0f, 0.0f));
            //transform.position += new Vector3(m_velocitySolid * Time.deltaTime, 0.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !m_collidingLeft && MoveInAir())//Left
        {
            m_rg.AddForce(new Vector3(-m_velocitySolid, 0.0f, 0.0f));
            //transform.position += new Vector3(-m_velocitySolid * Time.deltaTime, 0.0f, 0.0f);
        }
    }
    void ControlSolidMovement()
    {
        float currentVelocity = m_rg.velocity.x;
        if (currentVelocity > m_maxVelocitySolid)
            m_rg.velocity = new Vector3(m_maxVelocitySolid, m_rg.velocity.y, m_rg.velocity.z);
        else if(currentVelocity < -m_maxVelocitySolid)
            m_rg.velocity = new Vector3(-m_maxVelocitySolid, m_rg.velocity.y, m_rg.velocity.z);
    }

    void WaterMovement()
    {
        ControlWaterMovement();
        if (Input.GetKey(KeyCode.RightArrow) && !m_collidingRight && MoveInAir())//Right
        {
            m_rg.AddForce(new Vector3(m_velocityWater, 0.0f, 0.0f));
            //transform.position += new Vector3(m_velocityWater * Time.deltaTime, 0.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !m_collidingLeft && MoveInAir())//Left
        {
            m_rg.AddForce(new Vector3(-m_velocityWater, 0.0f, 0.0f));
            //transform.position += new Vector3(-m_velocityWater * Time.deltaTime, 0.0f, 0.0f);
        }
    }
    void ControlWaterMovement()
    { 
            float currentVelocity = m_rg.velocity.x;
            if (currentVelocity > m_maxVelocityWater)
                m_rg.velocity = new Vector3(m_maxVelocityWater, m_rg.velocity.y, m_rg.velocity.z);
            else if (currentVelocity < -m_maxVelocityWater)
                m_rg.velocity = new Vector3(-m_maxVelocityWater, m_rg.velocity.y, m_rg.velocity.z);
    }

    void GasMovement()
    {
        ControlGasMovement();
        m_rg.AddForce(Physics.gravity*-m_rg.mass*m_UpwardsGravityModifier);

        if (m_velocityGas != 0)
            m_rg.AddRelativeForce(new Vector3(m_velocityGas, 0.0f, 0.0f));

        if (Input.GetKeyDown(KeyCode.RightArrow) && !m_collidingRight && MoveInAir() && (m_velocityGas <= m_maxRightVelocityGas))//Right
        {
            m_velocityGas += m_StepVelocityGas;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !m_collidingLeft && MoveInAir() && (m_velocityGas >= m_maxLeftVelocityGas))//Left
        {
            m_velocityGas -= m_StepVelocityGas;
        }
    }
    void ControlGasMovement()
    {
        float currentVelocity = m_rg.velocity.x;
        if (currentVelocity > m_maxVelocityGas)
            m_rg.velocity = new Vector3(m_maxVelocityGas, m_rg.velocity.y, m_rg.velocity.z);
        else if (currentVelocity < -m_maxVelocityGas)
            m_rg.velocity = new Vector3(-m_maxVelocityGas, m_rg.velocity.y, m_rg.velocity.z);
    }

    void resetGasVelocity()
    {
        m_velocityGas = 0.0f;
    }
    void setGasVelocity(float vel)
    {
        m_velocityGas = vel;
    }

    bool MoveInAir()
    {
        if (m_moveInAir)
            return true;
        else
        {
            if (m_jumping)
                return false;
            else
                return true;
        }
    }
    void PlayerMoves(bool var)
    {
        _canMove = var;
    }

    void SetCarriedObject(GameObject obj)
    {
        m_carriedObject = obj;
    }
    void DropObject()
    {
        m_carriedObject = null;
    }
    void MoveCarriedObject()
    {
        if (m_carriedObject != null)
        {
            m_carriedObject.transform.position =
                this.GetComponent<Transform>().position + new Vector3(0, 1.1f, 0);
        }
    }
}
