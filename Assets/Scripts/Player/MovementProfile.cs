using System.Runtime.Serialization;
using System.Security.Cryptography;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementProfile", menuName = "Scriptable Objects/MovementProfile")]
public class MovementProfile : ScriptableObject
{
    public float speed = 10f;
    [Header("Forced Movement")]
    public bool isForcedMovement = false;
    public float forcedSpeed = 2f;
    public float minForcedMultiplier = 0.5f;
    public float maxForcedMultiplier = 1.5f;
    public float fluctuationInterval = 0.5f;
    [Header("Jerk")]
    public float jerkChance = 0.2f;
    public float jerkStrength = 0.5f;
    [Header("Input Lock")]
    public float inputLockChance = 0.2f;
    public float inputLockDuration = 0.3f;
}
