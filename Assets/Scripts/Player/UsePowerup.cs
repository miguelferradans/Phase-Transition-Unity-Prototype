using UnityEngine;
using System.Collections;

public class UsePowerup : MonoBehaviour {

    [Header ("Booleans")]
    [SerializeField]
    private bool m_PoisonActive = false;
    [SerializeField]
    private bool m_ExplActive = false;
    [SerializeField]
    private bool m_CorrosionActive = false;

    [Header("Explosion")]
    [SerializeField]
    public ParticleSystem m_Explosion = null;
    [SerializeField]
    public float m_ExplosionRadius = 5.0f;
    [SerializeField]
    public GameObject m_WallExplosion = null;

    [SerializeField]
    Material m_PoisonMaterial = null;


    [HideInInspector]
    public bool m_water = false;
    [HideInInspector]
    public bool m_solid = true;
    [HideInInspector]
    public bool m_gas = false;
    
	
	// Update is called once per frame
	void Update () {
        // TODO Add check if we are "poisonous" or "corrosive" if we enter a trigger being it
	    if (Input.GetKeyDown(KeyCode.Q))
        {
            // If we are in water or gas
            if ((m_water == true) || (m_gas == true))
            { 
                if (m_PoisonActive == true)
                {
                    usePUPoison();
                    m_PoisonActive = false;
                }
                else if (m_ExplActive == true)
                {
                    usePUExplosion();
                    m_ExplActive = false;
                }
                else if (m_CorrosionActive == true)
                {
                    usePUCorrosion();
                    m_CorrosionActive = false;
                }
            }
        }
	}

    void usePUPoison()
    {
        // TODO This should send a message to the states script and indicate that the next time we are Water/Gas we will be poisonous
        Debug.Log("Poisooooon");
        //Renderer rend = this.GetComponent<Renderer>();
        //rend.material = m_PoisonMaterial;
    }

    void usePUExplosion()
    {
        Debug.Log("Booom");
        m_Explosion.transform.position = this.transform.position;
        m_Explosion.gameObject.SetActive(true);
        AreaDestroy(m_Explosion.transform.position, m_ExplosionRadius);
    }

    void usePUCorrosion()
    {
        Debug.Log("Corrosion");
    }

    void AreaDestroy(Vector3 location, float radius)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in objectsInRange)
        {
            if (col.tag == "BreakableWall")
            {
                Instantiate(m_WallExplosion, col.gameObject.transform.position, col.gameObject.transform.rotation);
                col.gameObject.SetActive(false);
            }
        }
    }

    void setPoison(bool bStatus)
    {
        m_PoisonActive = bStatus;
    }

    void setExplosion(bool bStatus)
    {
        m_ExplActive = bStatus;
    }

    void setCorrosion(bool bStatus)
    {
        m_CorrosionActive = bStatus;
    }

    void setSolid()
    {
        m_solid = true;
        m_water = false;
        m_gas = false;
        // If any of the powerUps is active, in-active it
        m_PoisonActive = false;
        m_ExplActive = false;
        m_CorrosionActive = false;
    }

    void setWater()
    {
        m_water = true;
        m_solid = false;
        m_gas = false;
    }

    void setGas()
    {
        m_gas = true;
        m_solid = false;
        m_water = false;
    }
}
