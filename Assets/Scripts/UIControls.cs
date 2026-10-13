using System;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    [SerializeField]
    string command;

    public string GetCommand()
    {
        return $"Press E to {command}";
    }
}
