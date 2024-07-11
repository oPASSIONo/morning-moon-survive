using UnityEngine;

/// <summary>
/// Base class for all weapons, defining common functionality.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    /// <summary>
    /// Performs the weapon's primary action (e.g., attack).
    /// </summary>
    public abstract void PerformAction();
}