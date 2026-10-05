using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine; 

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public float Health;
    public float Speed;
}