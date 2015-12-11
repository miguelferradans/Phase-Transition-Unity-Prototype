using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UsePowerup : MonoBehaviour {

    [Header ("Power Information")]
    public string m_ActivePower;
    public bool _usingPower = false;
    public bool _oneTimePower = false;

    [Header("Explosion")]
    [SerializeField]
    public float m_ExplosionRadius = 5.0f;

    //[SerializeField]
    //Material m_PoisonMaterial = null;

    private ParticleSystem m_particlesPoison = null;
    private ParticleSystem m_particlesExplosion = null;
    private SphereCollider m_colliderExplosion = null;
    private TransformState m_playerState = null;

    [SerializeField]
    public Image m_powerUpImage;



    void Start()
    {
        m_particlesPoison = this.transform.Find("Poison").GetComponent<ParticleSystem>();
        m_particlesPoison.Stop();

        m_particlesExplosion = this.transform.Find("Explosive").GetComponent<ParticleSystem>();
        m_particlesExplosion.Stop();
        m_colliderExplosion = m_particlesExplosion.gameObject.GetComponent<SphereCollider>();
        m_powerUpImage.enabled = false;
        m_playerState = transform.GetComponent<TransformState>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!m_playerState.m_solid)
            {
                switch (m_ActivePower)
                {
                    case "Poison":
                        Poison();
                        break;
                    case "Explosive":
                        Explosive();
                        break;
                    case "StateTimeExtender":
                        TimeExtender();
                        break;
                }
            }
        }
        if (_usingPower && (m_playerState.m_solid || _oneTimePower))
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
            _oneTimePower = true;
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

    void TimeExtender()
    {
        if (!_usingPower)
        {
            SendMessage("RecoverTime", SendMessageOptions.RequireReceiver);
            _usingPower = true;
            _oneTimePower = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="power">
    /// 
    /// </param>
    void SetPower(string power){
        m_ActivePower = power;
        m_powerUpImage.enabled = true;
        switch (m_ActivePower)
        {
            case "Poison":
                m_powerUpImage.color = Color.green;
                break;
            case "Explosive":
                m_powerUpImage.color = Color.red;
                break;
            case "StateTimeExtender":
                m_powerUpImage.color = Color.blue;
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void DeletePowers()
    {
        m_particlesPoison.Stop();
        m_ActivePower = "";
        _usingPower = false;
        _oneTimePower = false;
        m_powerUpImage.enabled = false;
    }
}
