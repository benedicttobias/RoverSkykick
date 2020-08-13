using System;

namespace Rover2
{
    public class Plateau
    {
        private int Height { get; set; }

        private int Width { get;set; }

        public int MaxHeightIndex => Height - 1;

        public int MaxWidthIndex => Width - 1;

        private Coordinate[,] Coordinates { get; }

        public Plateau(int height, int width)
        {
            Height = height;
            Width = width;

            Coordinates = new Coordinate[Height, Width];
            initializeJaggedArray();
        }

        private void initializeJaggedArray()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    Coordinates[x, y] = new Coordinate(x, y);
                }
            }
        }

        public Rover SetCoordinate(int x, int y, Rover rover)
        {
            if (Coordinates[x, y]?.Rover != null)
            {
                return rover;
            }

            // take of old coordinate
            Coordinates[rover.X, rover.Y] = new Coordinate(rover.X, rover.Y);

            // Assign new coordinate
            Coordinates[x, y].Rover = rover;
            rover.X = x;
            rover.Y = y;
            return rover;
        }
    }
}
