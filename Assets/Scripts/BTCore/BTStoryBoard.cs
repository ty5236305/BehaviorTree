using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BTStoryBoard : MonoBehaviour
{

    private Dictionary<string, object> data = new Dictionary<string, object>();

    public T GetData<T>(string key)
    {
        return (T)data[key];
    }

    public void SetData<T>(string key, T value)
    {
        data[key] = value;
    }
  
}




