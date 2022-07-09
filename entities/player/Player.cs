using Godot;

namespace Cheese.Players
{
    public class Player : Node
    {
        [Export]
        public PlayerNumber PlayerNumber { get; private set; }

        [Export]
        public Color PlayerColor { get; private set; }
    }

    public enum PlayerNumber
    {
        One,
        Two
    }
}