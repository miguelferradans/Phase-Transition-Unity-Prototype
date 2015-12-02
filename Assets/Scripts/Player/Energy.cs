using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Energy : MonoBehaviour {
    [Header("EnergyFields")]
    [SerializeField][Range(10.0f, 100.0f)]
    private float _maxValue = 50.0f;
    [SerializeField][Range(1.0f, 50.0f)]
    private float _energyUsed = 10.0f;
    [SerializeField]
    private Slider _energyBar;

	// Use this for initialization
	void Start () 
    {
        _energyBar.maxValue = _maxValue;
        _energyBar.minValue = 0;
        _energyBar.value = _energyBar.maxValue; // se inicializa al valor máximo
	}


    //------------------------------------------METHODS-----------------------------------------------------
    /// <summary>   ConsumeEnergy()
    ///     This method is used to decrease the energy left. When done if theirs
    ///     no energy left you won`t be able to transform
    /// </summary>
    void ConsumeEnergy()
    {
        _energyBar.value -= _energyUsed;
        if (_energyBar.value == 0)
            SendMessage("EnergyLeft", false, SendMessageOptions.RequireReceiver);
    }

    /// <summary>   RegenEnergy(float amount)
    ///     Rengenerates certain amount of the energy
    /// </summary>
    /// <param name="amount">
    ///     It's the amount of energy to regenerate
    /// </param>
    void RegenEnergy(float amount)
    {
        _energyBar.value += amount;
        if(_energyBar.value >= _energyUsed)
            SendMessage("EnergyLeft", true, SendMessageOptions.RequireReceiver);
    }
    /// <summary>   RegenMaxEnergy()
    ///     Regenerates all the energy to the max.
    /// </summary>
    void RegenMaxEnergy()
    {
        _energyBar.value = _energyBar.maxValue;
        SendMessage("EnergyLeft", true, SendMessageOptions.RequireReceiver);
    }
}
