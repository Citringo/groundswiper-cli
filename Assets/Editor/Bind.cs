using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bind : MonoBehaviour {
    [MenuItem("Edit/Bind")]
    static void Attach()
    {
        var panels = FindObjectsOfType<Panel>();
        foreach (var p in panels)
            p.KeyInfo = (ConsoleKey)Enum.Parse(typeof(ConsoleKey), p.gameObject.name, true);
    }
}
