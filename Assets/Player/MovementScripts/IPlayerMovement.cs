public interface IPlayerMovement
{
    bool IsDodging {  get; }

    void OnJumpPerformed();

    void OnDodgePerformed();
}
