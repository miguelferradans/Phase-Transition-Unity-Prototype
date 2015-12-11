using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Timer : MonoBehaviour {

    private Text _text;
    private float _tSeconds;
    private int _tMinutos;
    private string _seconds;
    string _fileName = "MyFile.txt";
    StreamWriter _pFile = null;
    private string _minutes = "";
    void Start()
    {
        _text = GetComponent<Text>();
        _tSeconds = 0.0f;
        _tMinutos = 0;

    }
	
	// Update is called once per frame
	void Update () {
        _tSeconds += Time.deltaTime;
        if (_tSeconds>=60.0f)
        {
            _tMinutos++;
            _tSeconds -= 60.0f;
            _pFile = new StreamWriter(_fileName, true);
            _pFile.WriteLine(_tMinutos + ":" + _tSeconds);
            _pFile.Close();
        }
        if (_tSeconds < 10.0f)
        {
            _seconds = "0" + ((int)_tSeconds).ToString();
        }
        else
        {
            _seconds = ((int)_tSeconds).ToString();
        }
        if (_tMinutos < 10.0f)
        {
            _minutes = "0" + _tMinutos.ToString();
        }
        else
        {
            _minutes = _tMinutos.ToString();
        }
//        _minutes = _tMinutos.ToString();
        _text.text = _minutes + ":" + _seconds;
	}

}
