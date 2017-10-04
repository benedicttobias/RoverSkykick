using System;
using System.Collections.Generic;

namespace RoverSkyKick {

	class Program {

		#region Contants
		// Compass direction
		const string NORTH = "N";
		const string EAST  = "E";
		const string SOUTH = "S";
		const string WEST  = "W";
		const int NORTH_DEGREE = 0;
		const int EAST_DEGREE  = 90;
		const int SOUTH_DEGREE = 180;
		const int WEST_DEGREE  = 270;

		// Command list
		const char COMMAND_LEFT   = 'L';
		const char COMMAND_RIGHT  = 'R';
		const char COMMAND_MOVE   = 'M';
		const int TURN_LEFT_DEGREE  = -90;
		const int TURN_RIGHT_DEGREE = 90;

		// Input order
		const int X_COORDINATE_INDEX = 0;
		const int Y_COORDINATE_INDEX = 1;
		const int DIRECTION_INDEX    = 2;

		// Index failed note
		const int INVALID_INDEX = -1;

		// Max plateau size
		const int MAX_PLATEAU_SIZE = 1000;
		#endregion

		#region Classes
		// Direction of rover in 360 degree based
		class Direction {
			int direction; // Direction of the rover in degree

			public Direction() {
				direction = 0;
			}

			public Direction(int x) {
				direction = x;
				direction = StandardizeDirection(direction);
			}

			public int GetDirection() {
				return direction;
			}

			// Return direction wording
			public string PrintDirectionNotation() {
				string directionString = string.Empty;

				switch (direction) {
					case NORTH_DEGREE:
						directionString = NORTH;
						break;
					case EAST_DEGREE:
						directionString = EAST;
						break;
					case SOUTH_DEGREE:
						directionString = SOUTH;
						break;
					case WEST_DEGREE:
						directionString = WEST;
						break;
				}

				return directionString;
			}

			// Change direction of rover by degree
			public void ChangeDirection(int degree) {
				direction = direction + degree;

				direction = StandardizeDirection(direction);

				Console.WriteLine("Rover facing " + PrintDirectionNotation());
			}

			// Keep the degree always between 0-360 degree
			private int StandardizeDirection(int direction) {
				if (direction >= 360) {
					direction = direction - 360;
				} else if (direction < 0) {
					direction = direction + 360;
				}

				return direction;
			}

			public static bool IsValidDirection(string direction) {
				string[] acceptableDirection = { NORTH, EAST, SOUTH, WEST };
				return Array.Exists<string>(acceptableDirection, f => f.ToString() == direction);
			}
		}

		// Rover object
		class Rover {
			public Coordinate roverCoordinate;
			public Direction roverDirection;

			public Rover(string roverPosition) {
				string[] input = roverPosition.Split(' ');
				roverCoordinate = new Coordinate(Convert.ToInt32(input[X_COORDINATE_INDEX].ToString()), 
												 Convert.ToInt32(input[Y_COORDINATE_INDEX].ToString()));
				roverDirection = CalculateDirection(input[DIRECTION_INDEX].ToString());
			}

			public void SetCoordinate(Coordinate coordinate) {
				roverCoordinate = coordinate;
			}

			public void SetDirection(int degree) {
				roverDirection.ChangeDirection(degree);
			}

			public int GetX() {
				return roverCoordinate.GetX();
			}

			public int GetY() {
				return roverCoordinate.GetY();
			}

			public int GetDirection() {
				return roverDirection.GetDirection();
			}

			private Direction CalculateDirection(string degreeDirection) {
				int degree;

				switch (degreeDirection) {

					case NORTH:
						degree = NORTH_DEGREE;
						break;
					case EAST:
						degree = EAST_DEGREE;
						break;
					case SOUTH:
						degree = SOUTH_DEGREE;
						break;
					case WEST:
						degree = WEST_DEGREE;
						break;
					default:
						degree = NORTH_DEGREE;
						break;
				}

				return new Direction(degree);
			}

			internal void PrintRoverLocationStatus() {
				Console.WriteLine("Rover position: " + roverCoordinate.GetX().ToString() + 
					                             " " + roverCoordinate.GetY().ToString() + 
												 " " + roverDirection.PrintDirectionNotation());
			}
		}

		// Coordinate for grid of plateau
		class Coordinate {
			public int x;
			public int y;
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

			public static void PrintThisCoordinateStatus(Coordinate target) {
				Console.WriteLine("[" + target.GetX().ToString() + ","
									  + target.GetY().ToString() + "]" +
									  " Occupied = " + target.IsOccupied().ToString());
			}
		}

		// Plateau
		class Plateau {
			List<Coordinate> plateau = new List<Coordinate>();
			public int maxXCoordinate = 0;
			public int maxYCoordinate = 0;
			public bool isPlateauBuilt = false;

			public void SetPlateau(int xPlateauSize, int yPlateauSize) {
				if (IsPlateauSizeValid(xPlateauSize, yPlateauSize)) {
					// Build Plateau
					for (int xCoordinate = 0; xCoordinate <= xPlateauSize; xCoordinate++) {
						for (int yCoordinate = 0; yCoordinate <= yPlateauSize; yCoordinate++) {
							plateau.Add(new Coordinate(xCoordinate, yCoordinate));
						}
					}

					// Set max range
					maxXCoordinate = xPlateauSize;
					maxYCoordinate = yPlateauSize;

					isPlateauBuilt = true;
				} else {
					throw new Exception("Invalid Coordinate: X or Y cannot be less than 0");
				}
			}

			private bool IsPlateauSizeValid(int xPlateauSize, int yPlateauSize) {
				bool isValid = false;

				if (xPlateauSize >= 0 && xPlateauSize <= MAX_PLATEAU_SIZE) {
					if (yPlateauSize >= 0 && yPlateauSize <= MAX_PLATEAU_SIZE) {
						isValid = true;
					}
				}

				return isValid;
			}

			public int FindIndexByCoordinate(int x, int y) {
				if (IsCoordinateInRange(x, y)) {
					// Get index of plateau
					int plateauIndex = plateau.FindIndex(f => (f.GetX() == x) &&
															  (f.GetY() == y));

					return plateauIndex;
				} else {
					return INVALID_INDEX;
				}

			}

			private bool IsCoordinateInRange(int x, int y) {
				bool isInRange = false;

				if (maxXCoordinate >= x) {
					if (maxYCoordinate >= y) {
						isInRange = true;
					}
				}

				return isInRange;
			}

			public Coordinate GetCoordiateByIndex(int index) {
				return plateau[index];
			}

			public bool IsOccupied(int x, int y) {
				bool IsOccupied = true;

				// Get index of plateau
				int plateauIndex = FindIndexByCoordinate(x, y);

				// Find that coordinate, if could not find it, that means it is occupied
				if (plateauIndex != INVALID_INDEX) {

					Coordinate target = plateau[plateauIndex];
					IsOccupied = target.IsOccupied();
				}

				return IsOccupied;
			}

			public bool PlaceRover(Rover rover) {
				bool isSuccess = false;

				if (IsOccupied(rover.GetX(), rover.GetY()) == false) {
					// Set new object to occupy current space
					Coordinate roverPlace = new Coordinate(rover.GetX(), rover.GetY(), true);

					int plateauIndex = FindIndexByCoordinate(rover.GetX(), rover.GetY());

					if (plateauIndex != INVALID_INDEX) {
						plateau[plateauIndex] = roverPlace;
					}
					
					Console.WriteLine("Rover placed to coordinate [" + rover.GetX() + "," + rover.GetY() + "]");

					isSuccess = true;
				} else {
					Console.WriteLine("uh oh! Coordiate occupied!");
				}

				return isSuccess;
			}

			public void RemoveRover(Rover rover) {
				if (IsOccupied(rover.GetX(), rover.GetY()) == true) {
					// Set new object to replace current space
					Coordinate roverPlace = new Coordinate(rover.GetX(), rover.GetY(), false);

					int plateauIndex = FindIndexByCoordinate(rover.GetX(), rover.GetY());

					if (plateauIndex != INVALID_INDEX) {
						plateau[plateauIndex] = roverPlace;
					}

					Console.WriteLine("Rover removed from coordinate [" + rover.GetX() + "," + rover.GetY() + "]");
				} else {
					Console.WriteLine("Coordinate is empty anyway...");
				}
			}

			public bool PlaceRover(Coordinate coordinate) {
				bool isSuccess = false;

				if (IsOccupied(coordinate.GetX(), coordinate.GetY()) == false) {
					// Set new object to occupy current space
					Coordinate roverPlace = new Coordinate(coordinate.GetX(), coordinate.GetY(), true);

					int plateauIndex = FindIndexByCoordinate(coordinate.GetX(), coordinate.GetY());

					if (plateauIndex != INVALID_INDEX) {
						plateau[plateauIndex] = roverPlace;
					}

					Console.WriteLine("Rover placed to coordinate [" + coordinate.GetX() + "," + coordinate.GetY() + "]");

					isSuccess = true;
				} else {
					Console.WriteLine("Bummer. Coordiate occupied!");
				}

				return isSuccess;
			}

			public void PrintPlateauMap() {
				plateau.ForEach(delegate (Coordinate coordinate) {
					Console.WriteLine("[" + coordinate.GetX().ToString() + ","
										  + coordinate.GetY().ToString() + "]" +
									  " Occupied = " + coordinate.IsOccupied().ToString());
				});
			}
		}

		// Rover plan (coordinate and its movement plan)
		class RoverPlan {
			public Rover rover;
			public string movementPlan;

			public RoverPlan(Rover roverInput, string movementPlanInput) {
				rover = roverInput;
				movementPlan = movementPlanInput;
			}
		}
		#endregion

		#region Main Program
		static void Main(string[] args) {

			// Plateau
			Plateau plateau = new Plateau();
			int xPlateauSize = 0;
			int yPlateauSize = 0;

			// Rover
			List<RoverPlan> rovers = new List<RoverPlan>();
			int roverCounter = 0;

			// Welcoming user
			printWelcomeMessage();

			// Loop to get a valid Plateau information
			do {
				// Read user input for plateau
				Console.Write("Graph Upper Right coordinate (X Y): ");
				string XYinput = Console.ReadLine();

				// Check user input
				if (ProcessPlateauInput(XYinput, ref xPlateauSize, ref yPlateauSize)) {
					// Build Plateau
					plateau.SetPlateau(xPlateauSize, yPlateauSize);
				} else {
					Console.WriteLine("Inpur Error. Please fix your Plateau size.");
				}
			} while (!plateau.isPlateauBuilt);

			// Loop to get a valid rover amount
			do {
				Console.Write("How many rover(s) do you want to input? ");
			} while (!Int32.TryParse(Console.ReadLine(), out roverCounter));
			Console.WriteLine("Rover amount: " + roverCounter.ToString());

			// Process rovers if any
			if (roverCounter > 0) {
				// Loop until getting the correct coordinate and movement plan
				for (int roverNumber = 1; roverNumber <= roverCounter; roverNumber++) {
					string roverPosition = string.Empty;

					// Loop until get a correct rover position and direction
					do {
						Console.Write("Rover " + roverNumber + " Starting Position (X Y Direction): ");
						roverPosition = Console.ReadLine();
					} while (!ValidateRoverPosition(plateau, roverPosition));

					// Add new rover
					Rover rover = new Rover(roverPosition);

					// Getting movement plan for this rover
					Console.Write("Rover " + roverNumber + " Movement Plan (series of L, R, M): ");
					string roverMovementPlan = Console.ReadLine();
					RoverPlan newRoverPlan = new RoverPlan(rover, roverMovementPlan);

					// Add a rover to rover list
					rovers.Add(newRoverPlan);
				}

				// Process each rover plan
				Console.WriteLine("------------------ Processing Rovers ---------------------");
				for (int rover = 0; rover < rovers.Count; rover++) {
					// Process rover movement plan
					Console.WriteLine("----------------------------------");
					Console.WriteLine("Starting processing rover " + (rover + 1).ToString());
					ProcessRoverMovementPlan(plateau, rovers[rover]);
					Console.WriteLine("Finished processing rover " + (rover + 1).ToString());
				}

				// Print all rovers
				Console.WriteLine("------------------ Rover Status ------------------");
				for (int rover = 0; rover < rovers.Count; rover++) {
					rovers[rover].rover.PrintRoverLocationStatus();
				}
			}

			// End of the program

			Console.WriteLine("----------------------------------------------------------");
			Console.WriteLine("Thank you for using Rover Explorer for SkyKick!");
		}

		#endregion

		#region Methods

		// Print welcome message
		private static void printWelcomeMessage() {
			Console.Clear();
			Console.WriteLine("--------------------------------------------------------------------------------------------");
			Console.WriteLine("  Welcome to Rover Explorer!");
			Console.WriteLine("");
			Console.WriteLine("  This is an assignment from SkyKick for recruiting purposes.");
			Console.WriteLine("  Assignment problem can be found on: SkyKick_-_RoverSpecv1.6.docx file.");
			Console.WriteLine("");
			Console.WriteLine("  Notes for grader(s):");
			Console.WriteLine("    Thank you for the opportunity given for doing this homework! It is challenging!");
			Console.WriteLine("    I learnt a lot of things from this assignment.");
			Console.WriteLine("");
			Console.WriteLine("  Here are my assumptions:");
			Console.WriteLine("    * Plateau is shared among Rovers.");
			Console.WriteLine("    * Rover cannot move to grid that is occupied.");
			Console.WriteLine("    * If Rover's start position is occupied, it will stay and not do anything.");
			Console.WriteLine("    * Any other grid coordinate outside maximum grid is assumed occupied.");
			Console.WriteLine("    * Rover amount -1 means the user exiting the program.");
			Console.WriteLine("    * Grid size is limited to 1000x1000. I care about your RAM. :D");
			Console.WriteLine("    * If movement command is not recognized, it will disregard that and moved on.");
			Console.WriteLine("");
			Console.WriteLine("  Key notes:");
			Console.WriteLine("    * Program designed so it can handle more than two rovers.");
			Console.WriteLine("    * Program designed so it is east to expand if we decided to have more Plateau.");
			Console.WriteLine("    * Program designed so it expandable if we decided to have more specific directions.");
			Console.WriteLine("");
			Console.WriteLine("  Programmer : Benedict Tobias");
			Console.WriteLine("  Email      : benedict.tobias@gmail.com");
			Console.WriteLine("  Date       : 10/4/2017");
			Console.WriteLine("--------------------------------------------------------------------------------------------");
			Console.WriteLine("");
		}

		// Validate rover position and direction
		private static bool ValidateRoverPosition(Plateau plateau, string roverPosition) {
			bool isValid = true;
			bool isXValid = false;
			bool isYvalid = false;

			int roverXPosition;
			int roverYPosition;

			string[] input = roverPosition.Split(' ');

			if (input.Length < 3 || input.Length > 3) {
				Console.WriteLine("Please put three space separated coordinate X and Y and its direction");
				isValid = false;
			} else {
				isXValid = Int32.TryParse(input[X_COORDINATE_INDEX].ToString(), out roverXPosition);
				isYvalid = Int32.TryParse(input[Y_COORDINATE_INDEX].ToString(), out roverYPosition);

				// Validate for input
				if (!isXValid) {
					Console.WriteLine("X coordinate is not a number");
					isValid = false;
				} else {
					if (roverXPosition > plateau.maxXCoordinate) {
						Console.WriteLine("X coordinate is outside the boudary, maximum is " + plateau.maxXCoordinate);
						isValid = false;
					}
				}

				if (!isYvalid) {
					Console.WriteLine("Y coordinate is not a number");
					isValid = false;
				} else {
					if (roverYPosition > plateau.maxYCoordinate) {
						Console.WriteLine("X coordinate is outside the boudary, maximum is " + plateau.maxYCoordinate);
						isValid = false;
					}
				}

				if (!Direction.IsValidDirection(input[2].ToString())) {
					Console.WriteLine("Direction should be only " + NORTH + ", " + EAST + ", " + SOUTH + ", or " + WEST);
					isValid = false;
				}

			}

			return isValid;
		}

		// Processing a rover's plan in the plateau
		private static void ProcessRoverMovementPlan(Plateau plateau, RoverPlan roverPlan) {
			// Locate rover in the plateau
			Coordinate roverPosition = new Coordinate(roverPlan.rover.GetX(), roverPlan.rover.GetY(), true);

			int plateauIndex = plateau.FindIndexByCoordinate(roverPlan.rover.GetX(), roverPlan.rover.GetY());

			Coordinate target = plateau.GetCoordiateByIndex(plateauIndex);

			// Put rover if coordinate is vacant
			if (target.IsOccupied() == false) {
				bool IsSuccess = plateau.PlaceRover(roverPlan.rover);

				if (IsSuccess) {
					Console.WriteLine("Rover placed");
					ProcessPath(ref plateau, ref roverPlan.rover, roverPlan.movementPlan);
				} else {
					Console.WriteLine("Rover cannot be placed here because it is occupied. Cancelling the movement plan.");
				}
			} else {
				Console.WriteLine("Rover cannot be placed here because it is occupied. Cancelling the movement plan.");
			}
		}

		// Process each movement in the plan
		private static void ProcessPath(ref Plateau plateau, ref Rover rover, string roverMovementPlan) {
			foreach (char command in roverMovementPlan) {
				switch (command) {
					case COMMAND_LEFT:
						Console.WriteLine("Received command to turn Left");
						rover.SetDirection(TURN_LEFT_DEGREE);
						break;
					case COMMAND_RIGHT:
						Console.WriteLine("Received command to turn Right");
						rover.SetDirection(TURN_RIGHT_DEGREE);
						break;
					case COMMAND_MOVE:
						Console.WriteLine("Received command to turn move one grid");
						ProcessRoverMove(ref plateau, ref rover);
						break;
					default:
						Console.WriteLine("Command '" + command + "' is not recognized... skipping this one.");
						break;
				}
			}
		}

		// Process user input for Plateau
		static bool ProcessPlateauInput(string XYinput, ref int xPlateauSize, ref int yPlateauSize) {
			bool isValid = true;
			bool isXValid = false;
			bool isYvalid = false;

			string[] input = XYinput.Split(' ');

			if (input.Length < 2 || input.Length > 2) {
				Console.WriteLine("Please put two space separated coordinate X and Y");
				isValid = false;
			} else {
				isXValid = Int32.TryParse(input[X_COORDINATE_INDEX].ToString(), out xPlateauSize);
				isYvalid = Int32.TryParse(input[Y_COORDINATE_INDEX].ToString(), out yPlateauSize);

				if (!isXValid) {
					Console.WriteLine("X coordinate is not a number");
					isValid = false;
				}

				if (!isYvalid) {
					Console.WriteLine("Y coordinate is not a number");
					isValid = false;
				}

				if (xPlateauSize > MAX_PLATEAU_SIZE) {
					Console.WriteLine("Please put X below " + MAX_PLATEAU_SIZE.ToString() + ". Let's love our RAM");
					isValid = false;
				}

				if (yPlateauSize > MAX_PLATEAU_SIZE) {
					Console.WriteLine("Please put Y below " + MAX_PLATEAU_SIZE.ToString() + ". Let's love our RAM");
					isValid = false;
				}
			}

			return isValid;
		}

		// Process movement of the rover
		private static bool ProcessRoverMove(ref Plateau plateau, ref Rover rover) {
			bool isSuccess = false;

			Coordinate roverOldCoordinate = new Coordinate(rover.GetX(), rover.GetY());
			Coordinate roverNewCoordinate;
			Direction roverDirection = new Direction(rover.GetDirection());

			switch (roverDirection.GetDirection()) {
				case NORTH_DEGREE:
					// y + 1
					Console.WriteLine("Rover initiate to move North!");
					roverNewCoordinate = new Coordinate(rover.GetX(), rover.GetY() + 1, true);
					isSuccess = MoveRover(plateau, rover, roverNewCoordinate);
					break;
				case EAST_DEGREE:
					// x + 1
					Console.WriteLine("Rover initiate to move East!");
					roverNewCoordinate = new Coordinate(rover.GetX() + 1, rover.GetY(), true);
					isSuccess = MoveRover(plateau, rover, roverNewCoordinate);
					break;
				case SOUTH_DEGREE:
					// y - 1
					Console.WriteLine("Rover initiate to move South!");
					roverNewCoordinate = new Coordinate(rover.GetX(), rover.GetY() - 1, true);
					isSuccess = MoveRover(plateau, rover, roverNewCoordinate);
					break;
				case WEST_DEGREE:
					// x - 1
					Console.WriteLine("Rover initiate to move West!");
					roverNewCoordinate = new Coordinate(rover.GetX() - 1, rover.GetY(), true);
					isSuccess = MoveRover(plateau, rover, roverNewCoordinate);
					break;
				default:
					break;
			}

			return isSuccess;
		}

		// Move rover one unit into new space
		private static bool MoveRover(Plateau plateau, Rover rover, Coordinate roverNewCoordinate) {
			bool isOperationSuccess = false;

			if (plateau.IsOccupied(roverNewCoordinate.GetX(), roverNewCoordinate.GetY()) == false) {
				plateau.RemoveRover(rover);
				plateau.PlaceRover(roverNewCoordinate);

				// Update rover coordinate
				rover.SetCoordinate(roverNewCoordinate);

				isOperationSuccess = true;
			} else {
				Console.WriteLine("Rover cannot move West since coordinate is occupied!");
			}

			return isOperationSuccess;
		}
		#endregion
	}
}