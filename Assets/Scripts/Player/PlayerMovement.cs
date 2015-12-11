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
    private Vector3 m_velocityGas;
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
    public bool m_grounded = false;
    public bool _jumped = false;
    public bool m_collidingRight = false;
    public bool m_collidingLeft = false;
    [SerializeField]
    private bool _canMove = true;

    private Rigidbody m_rg = null;

    private TransformState m_transform = null;

    private GameObject m_carriedObject = null;

    [SerializeField]
    private float _offSet = 0.05f;

    //---------------------------------------------START----------------------------------------------------
    void Start()
    {
        m_transform = GetComponent<TransformState>();
        m_rg = GetComponent<Rigidbody>();
    }

    //---------------------------------------------UPDATE---------------------------------------------------
    void Update()
    {
        CollisionPlayerManager();
    }

    //------------------------------------------FIXED_UPDATE------------------------------------------------
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

    //------------------------------------------METHODS-----------------------------------------------------

    /// <summary>
    /// 
    /// </summary>
    void SolidMovement()
    {
        ControlSolidMovement();
        if (Input.GetKey(KeyCode.Space) && m_grounded && !_jumped)//Jump
        {
            m_rg.AddForce(new Vector3(0.0f, m_jumpForce, 0.0f));
            _jumped = true;
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
    /// <summary>
    /// 
    /// </summary>
    void ControlSolidMovement()
    {
        float currentVelocity = m_rg.velocity.x;
        if (currentVelocity > m_maxVelocitySolid)
            m_rg.velocity = new Vector3(m_maxVelocitySolid, m_rg.velocity.y, m_rg.velocity.z);
        else if(currentVelocity < -m_maxVelocitySolid)
            m_rg.velocity = new Vector3(-m_maxVelocitySolid, m_rg.velocity.y, m_rg.velocity.z);
    }

    /// <summary>
    /// 
    /// </summary>
    void WaterMovement()
    {
        ControlWaterMovement();
        if (Input.GetKey(KeyCode.RightArrow) && MoveInAir())//Right
        {
            m_rg.AddForce(new Vector3(m_velocityWater, 0.0f, 0.0f));
            //transform.position += new Vector3(m_velocityWater * Time.deltaTime, 0.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && MoveInAir())//Left
        {
            m_rg.AddForce(new Vector3(-m_velocityWater, 0.0f, 0.0f));
            //transform.position += new Vector3(-m_velocityWater * Time.deltaTime, 0.0f, 0.0f);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void ControlWaterMovement()
    { 
            float currentVelocity = m_rg.velocity.x;
            if (currentVelocity > m_maxVelocityWater)
                m_rg.velocity = new Vector3(m_maxVelocityWater, m_rg.velocity.y, m_rg.velocity.z);
            else if (currentVelocity < -m_maxVelocityWater)
                m_rg.velocity = new Vector3(-m_maxVelocityWater, m_rg.velocity.y, m_rg.velocity.z);
    }

    /// <summary>
    /// 
    /// </summary>
    void GasMovement()
    {
        ControlGasMovement();
        m_rg.AddForce(Physics.gravity*-m_rg.mass*m_UpwardsGravityModifier);

        if (m_velocityGas != Vector3.zero)
            m_rg.AddRelativeForce(m_velocityGas);

        if (Input.GetKeyDown(KeyCode.RightArrow) && !m_collidingRight && MoveInAir() && (m_velocityGas.x <= m_maxRightVelocityGas))//Right
        {
            m_velocityGas.x += m_StepVelocityGas;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !m_collidingLeft && MoveInAir() && (m_velocityGas.x >= m_maxLeftVelocityGas))//Left
        {
            m_velocityGas.x -= m_StepVelocityGas;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void ControlGasMovement()
    {
        float currentVelocity = m_rg.velocity.x;
        if (currentVelocity > m_maxVelocityGas)
            m_rg.velocity = new Vector3(m_maxVelocityGas, m_rg.velocity.y, m_rg.velocity.z);
        else if (currentVelocity < -m_maxVelocityGas)
            m_rg.velocity = new Vector3(-m_maxVelocityGas, m_rg.velocity.y, m_rg.velocity.z);
    }

    /// <summary>
    /// 
    /// </summary>
    void resetGasVelocity()
    {
        Debug.Log("Reiniciando velocity gas");
        m_velocityGas = Vector3.zero;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vel">
    /// 
    /// </param>
    void setGasVelocity(Vector3 vel)
    {
        m_velocityGas = vel;
    }

    /// <summary>   MoveInAir()
    ///     So this method is used if you want or not to move while in the air.
    /// </summary>
    /// <returns>
    ///     It returns true if you can move, false if not.
    /// </returns>
    bool MoveInAir()
    {
        if (m_moveInAir)
            return true;
        else
        {
            if (!m_grounded)
                return false;
            else
                return true;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="var">
    /// 
    /// </param>
    void PlayerMoves(bool var)
    {
        _canMove = var;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj">
    /// 
    /// </param>
    void SetCarriedObject(GameObject obj)
    {
        m_carriedObject = obj;
    }
    /// <summary>
    /// 
    /// </summary>
    void DropObject()
    {
        m_carriedObject = null;
    }
    /// <summary>
    /// 
    /// </summary>
    void MoveCarriedObject()
    {
        if (m_carriedObject != null)
        {
            m_carriedObject.transform.position =
                this.GetComponent<Transform>().position + new Vector3(0, 1.1f, 0);
        }
    }

    /// <summary>   CollisionPlayerManager()
    ///     This method is used to know if the player is colliding left, right and down.
    ///     If he is colliding down he can jump. If colliding left or right he can't move
    ///     in that direction.
    ///     To achive this, we are going to use Raycast
    /// </summary>
    /// <mejora>
    ///     Se pueden añadir mas ray cast, ya que en las cuestas hay un fallo porque el rayo no llega a tocarla.
    ///     Se podrían hacer varios rayos separados entre si una cierta distancia hacia abajo. Se consigue que abarque
    ///     más área.
    /// </mejora>

    void CollisionPlayerManager()
    {
        RaycastHit hitLeft = new RaycastHit();
        RaycastHit hitRight = new RaycastHit();
        Ray rayDown = new Ray(transform.position, new Vector3(0.0f, -1.0f, 0.0f)); Debug.DrawRay(transform.position, new Vector3(0.0f, -1.0f, 0.0f));
        Ray rayLeft = new Ray(transform.position, new Vector3(-1.0f, 0.0f, 0.0f)); Debug.DrawRay(transform.position, new Vector3(-1.0f, 0.0f, 0.0f));
        Ray rayRight = new Ray(transform.position, new Vector3(1.0f, 0.0f, 0.0f)); Debug.DrawRay(transform.position, new Vector3(1.0f, 0.0f, 0.0f));
        if (Physics.Raycast(rayDown, _offSet + transform.localScale.y / 2)) //Raycast down
        {
            if (!_jumped)
                m_grounded = true;
        }
        else
        {
            m_grounded = false;
            _jumped = false;
        }
        if(Physics.Raycast(rayLeft, out hitLeft, (transform.localScale.x / 2) + _offSet)) //Raycast Left
        {
            if (hitLeft.transform.tag != "Trigger")
                m_collidingLeft = true;
        }
        else
        {
            m_collidingLeft = false;
        }
        if (Physics.Raycast(rayRight, out hitRight,  (transform.localScale.x / 2) + _offSet)) //Raycast Right
        {
            if (hitRight.transform.tag != "Trigger")
                m_collidingRight = true;
        }
        else
        {
            m_collidingRight = false;
        }

    }
}
