using System;
using UnityEngine; // Only used for Unity spacific variables (Vectors)

public interface IInteractable {
    string PromptText { get; }
    /// <summary>The main call for player interaction</summary>
    /// <param name="interactor">The interactor affecting the object</param>
    void Interact(PlayerInteractor interactor);
    public void OnMouseHoverEnter(); // Here for future implementation
    public void OnMouseHoverExit(); // Here for future implementation
}

public interface IChargeable {
    /// <summary>[Read Only] The amount of charge left in the Dive Gear</summary>
    float Charge { get; }
    /// <summary>[Read Only] The max charge the dive gear can hold</summary>
    float MaxCharge { get; }
    /// <summary>Adds charge and returns how much was actually accepted.</summary>
    float AddCharge(float amount);
}

public struct PlayerInputs {
    /// <summary>Used to save a Vector2 for the players input direction</summary>
    public Vector2 Move;
    /// <summary>Used to save the movement keys pressed (Default WASD & Arrow Keys)</summary>
    public bool MoveInput;
    /// <summary>Used to save the Sprint key pressed (Default LShift)</summary>
    public bool Sprint;
    /// <summary>Used to save the Crouch key pressed (Default C Key)</summary>
    public bool Crouch;
    /// <summary>Used to save the Interaction key pressed (Default E Key)</summary>
    public bool Interact;
}

/// <summary>Allows the ability to enable or dissable custom values</summary>
/// <typeparam name="T">The data type of the value</typeparam>
[Serializable]
public struct Override<T>
{
    public bool Enabled;
    public T Value;
    /// <summary>If enabled is set to true use T value, otherwise use default value from Scriptable Object</summary>
    /// <param name="defaultValue">The default value from the Scriptable Object</param>
    public T Resolve(T defaultValue) => Enabled ? Value : defaultValue;
}

public enum Stance {
    Idle, Walk, Sprint, Crouch
}