using System;

namespace Rover2
{
    public class Commander
    {
        private Rover Rover { get; }
        private Plateau Plateau { get; }
        private char[] Commands { get; }

        public Commander(Rover rover, Plateau plateau, string commands)
        {
            Rover = rover;
            Plateau = plateau;
            Commands = commands.ToCharArray();
        }

        public void ExecuteCommand()
        {
            foreach (var command in Commands)
            {
                if (Enum.TryParse<Direction>(command.ToString(), out var direction))
                {
                    changeDirection(direction);
                } else if (Enum.TryParse<Movement>(command.ToString(), out var movement))
                {
                    executeMovement(movement);
                }
            }
        }

        private void executeMovement(Movement movement)
        {
            switch (movement)
            {
                case Movement.MoveForward:
                    moveForward();
                    break;
            }
        }

        private void moveForward()
        {
            if (Rover.Direction == Direction.North)
            {
                if (Rover.Y + 1 <= Plateau.MaxHeightIndex)
                {
                    Rover.Y++;
                }
            }

            if (Rover.Direction == Direction.East)
            {
                if (Rover.X + 1 <= Plateau.MaxWidthIndex)
                {
                    Rover.X++;
                }
            }

            if (Rover.Direction == Direction.South)
            {
                if (Rover.Y - 1 >= 0)
                {
                    Rover.Y--;
                }
            }

            if (Rover.Direction == Direction.West)
            {
                if (Rover.X - 1 >= 0)
                {
                    Rover.X--;
                }
            }
        }

        private void changeDirection(Direction direction)
        {
            if (Rover.Direction == Direction.North)
            {
                if ()
            }
        }
    }
}
