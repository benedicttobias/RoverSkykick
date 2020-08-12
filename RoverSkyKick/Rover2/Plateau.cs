using System;

namespace Rover2
{
    public class Plateau
    {
        private int Height { get; set; }

        private int Width { get;set; }

        public int MaxHeightIndex => Height - 1;

        public int MaxWidthIndex => Width - 1;

        private Coordinate[,] Coordinate { get; }

        public Plateau(int height, int width)
        {
            Height = height;
            Width = width;

            Coordinate = new Coordinate[Height, Width];
        }

        public void SetCoordinate(int x, int y, Rover rover)
        {
            Coordinate[x, y].Rover = rover;
        }

        public void ClearRover(int x, int y)
        {
            Coordinate[x, y].Rover = null;
        }
    }
}
