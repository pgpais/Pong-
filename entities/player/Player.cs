using System.Collections.Generic;
using Godot;

namespace Pong.Entities
{
    public class Player : Node
    {
        public static List<Player> Players = new List<Player>();
        [Export]
        public PlayerNumber PlayerNumber { get; private set; }

        [Export]
        public Color PlayerColor { get; private set; }

        public Player()
        {
            Players.Add(this);
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            Players.Remove(this);
        }
    }

    public enum PlayerNumber
    {
        One,
        Two
    }
}