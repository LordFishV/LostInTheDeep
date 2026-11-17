// Door.cs
public class Door : RepairableObject<DoorSO>
{
    private bool _isOpen;

    protected override void Awake()
    {
        base.Awake();
        _isOpen = _data.StartsOpen;
    }

    public override void Interact(PlayerInteractor interactor)
    {
        if (!EnsureRepaired(interactor)) return;

        _isOpen = !_isOpen;
        // animate here, using _data.OpenDuration
    }
}