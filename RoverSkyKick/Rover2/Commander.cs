using System;

namespace Rover2
{
    public class Commander
    {
        private Rover Rover { get; }
        
        private char[] Commands { get; }

        public Commander(Rover rover, string commands)
        {
            Rover = rover;
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
            throw new NotImplementedException();
        }

        private void changeDirection(Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
