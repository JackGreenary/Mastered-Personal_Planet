using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPreferencesController
{
    public static void StoreValue(string key, string type, string value)
    {
        switch (type)
        {
            case "int":
                PlayerPrefs.SetInt(key, Int32.Parse(value));
                break;
            case "string":
                PlayerPrefs.SetString(key, value);
                break;
            default:
                break;
        }
    }

    public static string GetValue(string key)
    {
        return PlayerPrefs.GetString(key);
    }
}
