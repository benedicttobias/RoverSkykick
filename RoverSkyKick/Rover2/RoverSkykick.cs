using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Rover2
{
    public class RoverSkykick
    {
        private Plateau _plateau;
        private Rover _rover1;
        private Rover _rover2;

        public RoverSkykick(string[] args)
        {
            var plateauSize = args[0].Split(' ');
            var rover1Position = args[1].Split(' ');
            var rover1Commands = args[2];
            var rover2Position = args[3].Split(' ');
            var rover2Commands = args[4];

            _plateau = new Plateau(
                int.Parse(plateauSize[0]), 
                int.Parse(plateauSize[1]));

            Enum.TryParse<Direction>(rover1Position[2], out var roverOneDirection);
            _rover1 = new Rover(int.Parse(rover1Position[0]), int.Parse(rover1Position[1]), roverOneDirection);

            Enum.TryParse<Direction>(rover2Position[2], out var roverTwoDirection);
            _rover2 = new Rover(int.Parse(rover2Position[0]), int.Parse(rover2Position[1]), roverTwoDirection);

            var commandRoverOne = new Commander(_rover1, _plateau, rover1Commands);
            commandRoverOne.ExecuteCommand();
            var position = commandRoverOne.PrintPosition();

            var commandRoverTwo = new Commander(_rover2, _plateau, rover2Commands);
            commandRoverTwo.ExecuteCommand();
            position = commandRoverTwo.PrintPosition();
        }
    }
}