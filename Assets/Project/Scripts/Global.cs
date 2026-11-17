using UnityEngine; // Only used for Unity spacific variables (Vectors)

public interface IInteractable {
    string PromptText { get; }
    void Interact(PlayerInteractor interactor);
    public void OnMouseHoverEnter();
    public void OnMouseHoverExit();
}

public struct PlayerInputs {
    public Vector2 Move;
    public bool MoveInput;
    public bool Sprint;
    public bool Crouch;
    public bool Interact;
}

public enum Stance {
    Idle, Walk, Sprint, Crouch
}