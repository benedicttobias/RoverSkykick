namespace Rover2
{
    public class Rover
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Direction Direction { get; set; }
        public Rover(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
    }
}
