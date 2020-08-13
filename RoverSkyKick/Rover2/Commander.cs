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
                if (Enum.TryParse<Movement>(command.ToString(), out var movement))
                {
                    executeMovement(movement);
                }
            }
        }

        private void executeMovement(Movement movement)
        {
            switch (movement)
            {
                case Movement.M:
                    MoveForward();
                    break;
                case Movement.L:
                    SpinLeft();
                    break;
                case Movement.R:
                    spinRight();
                    break;
            }
        }

        private void spinRight()
        {
            if (Rover.Direction == Direction.N)
            {
                Rover.Direction = Direction.E;
            } else if (Rover.Direction == Direction.E)
            {
                Rover.Direction = Direction.S;
            } else if (Rover.Direction == Direction.S)
            {
                Rover.Direction = Direction.W;
            } else if (Rover.Direction == Direction.W)
            {
                Rover.Direction = Direction.N;
            }
        }

        private void SpinLeft()
        {
            if (Rover.Direction == Direction.N)
            {
                Rover.Direction = Direction.W;
            } else if (Rover.Direction == Direction.W)
            {
                Rover.Direction = Direction.S;
            } else if (Rover.Direction == Direction.S)
            {
                Rover.Direction = Direction.E;
            } else if (Rover.Direction == Direction.E)
            {
                Rover.Direction = Direction.N;
            }
        }

        private void MoveForward()
        {
            if (Rover.Direction == Direction.N)
            {
                if (Rover.Y + 1 <= Plateau.MaxHeightIndex)
                {
                    Plateau.SetCoordinate(Rover.X, Rover.Y + 1, Rover);
                }
            } else if (Rover.Direction == Direction.E)
            {
                if (Rover.X + 1 <= Plateau.MaxWidthIndex)
                {
                    Plateau.SetCoordinate(Rover.X + 1, Rover.Y, Rover);
                }
            } else if (Rover.Direction == Direction.S)
            {
                if (Rover.Y - 1 >= 0)
                {
                    Plateau.SetCoordinate(Rover.X, Rover.Y - 1, Rover);
                }
            } else if (Rover.Direction == Direction.W)
            {
                if (Rover.X - 1 >= 0)
                {
                    Plateau.SetCoordinate(Rover.X - 1, Rover.Y, Rover);
                }
            }
        }

        public string PrintPosition()
        {
            return $"Rover is in {Rover.X} {Rover.Y} facing {Rover.Direction.ToString()}";
        }
    }
}
