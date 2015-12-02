﻿using UnityEngine;
using System.Collections;

public class TransformState : MonoBehaviour {

    // Update is called once per frame

    MeshRenderer m_render = null;
    ParticleSystem m_particlesGas = null;
    ParticleSystem m_particlesWater = null;

    [Header("Materials")]
    [SerializeField]
    private Material m_solidMat = null;
    [SerializeField]
    private Material m_waterMat = null;
    [SerializeField]
    private Material m_gasMat = null;

    [HideInInspector]
    public bool m_water = false;
    [HideInInspector]
    public bool m_solid = true;
    [HideInInspector]
    public bool m_gas = false;

    private bool m_canTransform = true;

    void Start()
    {
        m_render = GetComponent<MeshRenderer>();
        m_particlesGas = this.transform.Find("Gas").GetComponent<ParticleSystem>();
        m_particlesGas.enableEmission = false;
        m_particlesWater = this.transform.Find("Water").GetComponent<ParticleSystem>();
        m_particlesWater.enableEmission = false;
        m_render.enabled = true;
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.DownArrow))//Water
        {

            if (m_solid && m_canTransform) //Solid --> Water
            {
                Water();
            }
            else if (m_gas)//Gas --> Solid
            {
                SendMessage("resetGasVelocity", SendMessageOptions.RequireReceiver);
                Solid();
            }
            else if (m_water && m_canTransform)//Water --> Gas
            {
                Gas();
            }
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))//Water
        {
            if (m_solid && m_canTransform)//Solid --> Gas
            {
                Gas();
            }
            else if (m_water)//Water --> Solid
            {
                Solid();
            }
            else if (m_gas && m_canTransform)//Gas --> Water
            {
                SendMessage("resetGasVelocity", SendMessageOptions.RequireReceiver);
                Water();
            }
        }
        else if (Input.GetKeyDown(KeyCode.R)) //"R" key = recharge energy
            SendMessage("RegenMaxEnergy", SendMessageOptions.RequireReceiver);
    }

    //------------------------------------------METHODS-----------------------------------------------------
    /// <summary>   Solid()
    ///     It changes the properties of the gameObject to those of a solid.
    ///     Also, it consumes part of the energy used in the transformation to another state.
    /// </summary>
    void Solid()
    {
        this.gameObject.layer = 10;//Solid
        m_water = false;
        m_solid = true;
        m_gas = false;
        m_render.material = m_solidMat;
        m_render.enabled = true;
        m_particlesGas.enableEmission = false;
        m_particlesWater.enableEmission = false;
        //EnergyTransformation();
    }

    /// <summary>   Water()
    ///     It changes the properties of the gameObject to those of a water.
    ///     Also, it consumes part of the energy used in the transformation to another state.
    /// </summary>
    void Water()
    {
        this.gameObject.layer = 8;//Water
        m_water = true;
        m_solid = false;
        m_gas = false;
        m_render.material = m_waterMat;
        m_render.enabled = false;
        m_particlesGas.enableEmission = false;
        m_particlesWater.enableEmission = true;
        EnergyTransformation();
    }

    /// <summary>   Gas()
    ///     It changes the properties of the gameObject to those of a gas.
    ///     Also, it consumes part of the energy used in the transformation to another state.
    /// </summary>
    void Gas()
    {
        this.gameObject.layer = 9;//Gas
        m_gas = true;
        m_solid = false;
        m_water = false;
        m_render.material = m_gasMat;
        m_render.enabled = false;
        m_particlesGas.enableEmission = true;
        m_particlesWater.enableEmission = false;
        EnergyTransformation();
    }

    /// <summary>   EnergyTransformation()
    ///     Send a Message to consume the energy used by a transformation.
    /// </summary>
    void EnergyTransformation()
    {
        SendMessage("ConsumeEnergy", SendMessageOptions.RequireReceiver);
    }

    /// <summary>
    ///     This method is used to set if there is any energy left to transform 
    /// </summary>
    /// <param name="value">
    ///     It's a bool that sets if theres energy left.
    ///         No --> false
    ///         Yes --> true
    /// </param>
    void EnergyLeft(bool value)
    {
        m_canTransform = value;
    }
}