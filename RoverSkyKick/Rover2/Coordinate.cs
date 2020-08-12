namespace Rover2
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Rover Rover { get; set; }
        public bool IsOccupied => Rover != null;

        public Coordinate(int xCoordinate, int yCoordinate)
        {
            X = xCoordinate;
            Y = yCoordinate;
            Rover = null;
        }
    }
}