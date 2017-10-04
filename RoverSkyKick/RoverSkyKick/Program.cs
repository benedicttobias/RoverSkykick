using System;
using System.Collections.Generic;
using System.Linq;

namespace RoverSkyKick
{
	class Direction {
		int direction;

		public Direction() {
			direction = 0;
		}

		public Direction(int x) {
			direction = x;

			direction = standardizeDirection(direction);
		}

		public int GetDirection() {
			return direction;
		}

		public void changeDirection(int degree) {
			direction = direction + degree;

			direction = standardizeDirection(direction);
		}

		private int standardizeDirection(int direction) {
			if (direction >= 360) {
				direction = direction - 360;
			} else if (direction < 0) {
				direction = direction + 360;
			}

			return direction;
		}
	}

	class Rover {
		int x;
		int y;
		Direction direction;

		public Rover(string roverPosition) {
			string[] input = roverPosition.Split(' ');
			x = Convert.ToInt32(input[0].ToString());
			y = Convert.ToInt32(input[1].ToString());
			direction = calculateDirection(input[2].ToString());
		}

		public int getX() {
			return x;
		}

		public int getY() {
			return y;
		}

		public int getDirection() {
			return direction.GetDirection();
		}

		private Direction calculateDirection(string degreeDirection) {
			int degree;

			switch (degreeDirection) {

				case "N":
					degree = 0;
					break;
				case "E":
					degree = 90;
					break;
				case "S":
					degree = 180;
					break;
				case "W":
					degree = 270;
					break;
				default:
					degree = 0;
					break;
			}

			return new Direction(degree);
		}
	}

	class Coordinate {
		int x;
		int y;
		bool occupied = false;

		public Coordinate(int xCoordinate, int yCoordinate, bool isOccupied = false) {
			x = xCoordinate;
			y = yCoordinate;
			occupied = isOccupied;
		}

		public int GetX() {
			return x;
		}

		public int GetY() {
			return y;
		}

		public bool IsOccupied() {
			return occupied;
		}
	}

	class Program
    {
		public static List<Coordinate> plateau = new List<Coordinate>();

		static void Main(string[] args)
        {
			
			int xPlateauSize = 0;
			int yPlateauSize = 0;

			Console.WriteLine("Welcome to Rover");

			// Get Plateau
			Console.Write("Graph Upper right coordinate: ");
			string XYinput = Console.ReadLine();
			ProcessPlateauInput(XYinput, ref xPlateauSize, ref yPlateauSize);

			// Input for Plateau size
			Console.Write("Your X input was: ");
			Console.WriteLine(xPlateauSize.ToString());
			Console.Write("Your Y input was: ");
			Console.WriteLine(yPlateauSize.ToString());

			// Build Plateau
			for (int xCoordinate = 0; xCoordinate <= xPlateauSize; xCoordinate++) {
				for (int yCoordinate = 0; yCoordinate <= yPlateauSize; yCoordinate++) {
					plateau.Add(new Coordinate(xCoordinate, yCoordinate));
				}
			}


			PrintPlateauMap();

			// Get rover 1
			Console.Write("Rover 1 Starting Position: ");
			string roverPosition = Console.ReadLine();

			// Add new rover
			Rover rover1 = new Rover(roverPosition);

			Console.Write("Rover 1 Movement Plan: ");
			string roverMovementPlan = Console.ReadLine();

			// Process rover movement plan
			ProcessRoverMovementPlan(plateau, rover1, roverMovementPlan);
		}

		private static void ProcessRoverMovementPlan(List<Coordinate> plateau, Rover rover, string roverMovementPlan) {
			// Locate rover in the plateau
			Coordinate roverPosition = new Coordinate(rover.getX(), rover.getY(), true);

			int plateauIndex = plateau.FindIndex(f => (f.GetX() == rover.getX()) &&
													  (f.GetY() == rover.getY()))];

			Coordinate target = plateau[plateauIndex];

			Console.WriteLine("[" + target.GetX().ToString() + ","
							      + target.GetY().ToString() + "]" +
								  " Occupied = " + target.IsOccupied().ToString());
		}

		static void ProcessPlateauInput(string XYinput, ref int xPlateauSize, ref int yPlateauSize) {
			string[] input = XYinput.Split(' ');
			xPlateauSize = Convert.ToInt32(input[0].ToString());
			yPlateauSize = Convert.ToInt32(input[1].ToString());
		}

		#region methods

		public static void PrintPlateauMap() {
			plateau.ForEach(delegate (Coordinate coordinate) {
				Console.WriteLine("[" + coordinate.GetX().ToString() + "," 
								      + coordinate.GetY().ToString() + "]" +
								  " Occupied = " + coordinate.IsOccupied().ToString());
			});
		}

		#endregion
	}
}