using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TimeCounter : MonoBehaviour
{
    [Header("TimeFields")]
    [SerializeField]
    [Range(0.0f, 5.0f)]
    private float _totalTime = 5f;
    [SerializeField]
    [Range(0.0f, 5.0f)]
    public float _timeRemaining;
    [SerializeField]
    private Slider _timeBar;

    // Use this for initialization
    void Start()
    {
        _timeBar.maxValue = _totalTime;
        _timeBar.minValue = 0;
        _timeBar.value = _timeRemaining;
    }

    //------------------------------------------METHODS-----------------------------------------------------
    /// <summary>   ConsumeTime()
    ///     
    /// </summary>
    void ConsumeTime()
    {
        _timeBar.value -= Time.deltaTime;
        if (_timeBar.value == 0)
            SendMessage("TimeLeft", false, SendMessageOptions.RequireReceiver);
    }
    /// <summary>   RecoverTime()
    ///     
    /// </summary>
    /// 
    void RecoverTime()
    {
        _timeBar.value = _totalTime;
    }
}

