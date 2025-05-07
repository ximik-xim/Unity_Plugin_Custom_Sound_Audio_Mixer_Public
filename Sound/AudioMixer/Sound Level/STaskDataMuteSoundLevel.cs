using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class STaskDataMuteSoundLevel 
{
    private string _text;

    public STaskDataMuteSoundLevel(string text)
    {
        _text = text;   
    }

    public string GetInfo()
    {
        return _text;
    }
}
