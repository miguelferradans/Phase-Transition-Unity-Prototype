using UnityEngine;
using System.Collections;

public class UsePowerup : MonoBehaviour {

    [Header ("Booleans")]
    public bool m_PoisonActive = false;
    public bool m_ExplActive = false;
    public bool m_CorrosionActive = false;
    public bool _usingPower = false;

    [Header("Explosion")]
    [SerializeField]
    public float m_ExplosionRadius = 5.0f;

    //[SerializeField]
    //Material m_PoisonMaterial = null;

    private ParticleSystem m_particlesPoison = null;
    private ParticleSystem m_particlesExplosion = null;
    private SphereCollider m_colliderExplosion = null;
    private TransformState m_playerState = null;


    void Start()
    {
        m_particlesPoison = this.transform.Find("Poison").GetComponent<ParticleSystem>();
        m_particlesPoison.Stop();

        m_particlesExplosion = this.transform.Find("Explosive").GetComponent<ParticleSystem>();
        m_particlesExplosion.Stop();
        m_colliderExplosion = m_particlesExplosion.gameObject.GetComponent<SphereCollider>();

        m_playerState = transform.GetComponent<TransformState>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_PoisonActive && !m_playerState.m_solid)
            {
                Poison();
            }
            else if (m_ExplActive && !m_playerState.m_solid)
            {
                Explosive();
            }
            else if (m_CorrosionActive && !m_playerState.m_solid)
            {
                Corrosive();
            }
        }
        if (_usingPower && m_playerState.m_solid)
        {
            DeletePowers();
        }
	}
    //------------------------------------------METHODS-----------------------------------------------------

    /// <summary>
    /// 
    /// </summary>
    void Poison()
    {
        if(!_usingPower){
            m_particlesPoison.Play();
            _usingPower = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Explosive()
    {
        if (!_usingPower)
        {
            m_particlesExplosion.Play();
            _usingPower = true;
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, m_ExplosionRadius);
            foreach (Collider coll in objectsInRange)
            {
                if (coll.tag == "BreakableWall" || coll.tag == "Enemy")
                {
                    //Instantiate(m_WallExplosion, col.gameObject.transform.position, col.gameObject.transform.rotation);
                    coll.gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Corrosive()
    {
        if (!_usingPower)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="power">
    /// 
    /// </param>
    void SetPower(string power){
        if (power == "Poison")
            m_PoisonActive = true;
        else if (power == "Explosive")
            m_ExplActive = true;
        else if (power == "Corrosive")
            m_CorrosionActive = true;
    }

    /// <summary>
    /// 
    /// </summary>
    void DeletePowers()
    {
        m_particlesPoison.Stop();
        m_PoisonActive = false;
        m_ExplActive = false;
        m_CorrosionActive = false;
        _usingPower = false;
    }
}
