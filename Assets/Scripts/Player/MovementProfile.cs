using System.Runtime.Serialization;
using System.Security.Cryptography;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementProfile", menuName = "Scriptable Objects/MovementProfile")]
public class MovementProfile : ScriptableObject
{
    public float speed = 10.0f;

    public bool isForcedMovement = false;
    public float forcedSpeed = 5f;
}
