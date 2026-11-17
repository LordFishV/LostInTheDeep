using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _character;
    [SerializeField] private PlayerInteractor _interactor;

    private PlayerInputAction _inputActions;
    private PlayerInputAction.PlayerActions _input;

    private void Awake() {
        _inputActions = new PlayerInputAction();
        _input = _inputActions.Player;
    }

    private void OnEnable() => _inputActions.Enable();
    private void OnDisable() => _inputActions.Disable();
    private void OnDestroy() => _inputActions.Dispose();

    private void Start() => _character.Initialise();

    private void Update() {
        PlayerInputs playerInput = new PlayerInputs {
            Move        = _input.Move.ReadValue<Vector2>(),
            MoveInput   = _input.MoveInput.IsPressed(),
            Sprint      = _input.Sprint.IsPressed(),
            Crouch      = _input.Crouch.WasPressedThisFrame(),
            Interact    = _input.Interact.WasPressedThisFrame()
        };

        _character.CharacterUpdate(playerInput, false);
        _interactor.UpdateInteractor(playerInput);
    }
}
