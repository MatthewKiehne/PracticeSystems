using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary {

    /// <summary>
    /// keys
    /// </summary>
    public object[] dictionaryKeys;

    /// <summary>
    /// y component
    /// </summary>
    public object[] dictionaryValues;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="keys"></param>
    /// <param name="values"></param>
    public SerializableDictionary(object[] keys, object[] values) {
        dictionaryKeys = keys;
        dictionaryValues = values;
    }

    /// <summary>
    /// Returns a string representation of the object
    /// </summary>
    /// <returns></returns>
    public override string ToString() {

        string result = "[";

        for (int i = 0; i < this.dictionaryKeys.Length - 1; i++) {
            result += "{" + dictionaryKeys[i].ToString() + " " +
                dictionaryValues[i].ToString() + "},";
        }

        result += "{" + dictionaryKeys[this.dictionaryValues.Length - 2].ToString() + " " +
                dictionaryValues[this.dictionaryValues.Length - 2].ToString() + "}";

        result += "]";
        return result;
    }

    /// <summary>
    /// Automatic conversion from SerializableDictionary to Dictionary
    /// </summary>
    /// <param name="ser"></param>
    /// <returns></returns>
    public static implicit operator Dictionary<object,object>(SerializableDictionary ser) {
        Dictionary<object, object> result = new Dictionary<object, object>();
        for (int i = 0; i < ser.dictionaryKeys.Length; i++) {
            result.Add(ser.dictionaryKeys[i], ser.dictionaryValues[i]);
        }
        return result;
    }

    /// <summary>
    /// Automatic conversion from Dictionary to SerializableDictionary
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public static implicit operator SerializableDictionary(Dictionary<object,object> dictionary) {
        return new SerializableDictionary(
            (object[])new List<object>(dictionary.Keys).ToArray(),
            (object[])new List<object>(dictionary.Values).ToArray());
    }
}