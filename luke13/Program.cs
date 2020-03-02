using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        var roomsRaw = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText("MAZE.TXT"));

        // Swap axis (not really needed, but it keeps the rest of the code simpler)
        var rooms = new Room[500, 500];
        for (int x = 0; x <= 499; x++)
        {
            for (int y = 0; y <= 499; y++)
            {
                rooms[x, y] = roomsRaw[y, x];
            }
        }

        var heatMap = new int[500, 500];

        var robotPosition = (X: 0, Y: 0);
        var visitedRoomsInOrder = new Stack<Room>();
        var iterationsCounter = 0;

        var image = CreateMazeImage(rooms);

        while (!(robotPosition.X == 499 && robotPosition.Y == 499))
        {
            iterationsCounter++;
            var currentRoom = rooms[robotPosition.X, robotPosition.Y];
            currentRoom.AlreadyVisited = true;
            heatMap[currentRoom.X, currentRoom.Y]++;

            var positionBeforeMove = (X: robotPosition.X, Y: robotPosition.Y);

            // Current setup for robot Arthur (SEWN), rearrange for Isaax (ESWN)
            if (!currentRoom.Syd && !rooms[robotPosition.X, robotPosition.Y + 1].AlreadyVisited)
            {
                robotPosition.Y++;
                visitedRoomsInOrder.Push(currentRoom);
            }
            else if (!currentRoom.Aust && !rooms[robotPosition.X + 1, robotPosition.Y].AlreadyVisited)
            {
                robotPosition.X++;
                visitedRoomsInOrder.Push(currentRoom);
            }
            else if (!currentRoom.Vest && !rooms[robotPosition.X - 1, robotPosition.Y].AlreadyVisited)
            {
                robotPosition.X--;
                visitedRoomsInOrder.Push(currentRoom);
            }
            else if (!currentRoom.Nord && !rooms[robotPosition.X, robotPosition.Y - 1].AlreadyVisited)
            {
                robotPosition.Y--;
                visitedRoomsInOrder.Push(currentRoom);
            }
            else // All rooms in all directions have been visited before, go back one step
            {
                // Console.WriteLine("STUCK, needs to go back");
                var previousRoom = visitedRoomsInOrder.Pop();
                robotPosition.X = previousRoom.X;
                robotPosition.Y = previousRoom.Y;
            }

            var positionAfterMove = (X: robotPosition.X, Y: robotPosition.Y);
            DrawLineBetween(image, positionBeforeMove, positionAfterMove);
            //image.Save($"step{iterationsCounter.ToString("000000")}.png");
        }

        Console.WriteLine($"Number of visited rooms: {rooms.Cast<Room>().Count(v => v.AlreadyVisited)}");

        // Save image to file
        image.Save("result_arthur.png");
        image.Dispose();
    }

    private static void DrawLineBetween(Image<Rgba32> image, (int X, int Y) positionBeforeMove, (int X, int Y) positionAfterMove)
    {
        var startPositionInImage = (X: (positionBeforeMove.X * 3) + 1, Y: (positionBeforeMove.Y * 3) + 1);
        var endPositionInImage = (X: (positionAfterMove.X * 3) + 1, Y: (positionAfterMove.Y * 3) + 1);

        var color = Rgba32.LightGoldenrodYellow;
        if (startPositionInImage.X < endPositionInImage.X)
        {
            for (int i = startPositionInImage.X; i < endPositionInImage.X; i++)
            {
                image[i, startPositionInImage.Y] = color;
            }
        }
        else if (startPositionInImage.X > endPositionInImage.X)
        {
            for (int i = startPositionInImage.X; i > endPositionInImage.X; i--)
            {
                image[i, startPositionInImage.Y] = color;
            }
        }
        else if (startPositionInImage.Y < endPositionInImage.Y)
        {
            for (int i = startPositionInImage.Y; i < endPositionInImage.Y; i++)
            {
                image[startPositionInImage.X, i] = color;
            }
        }
        else if (startPositionInImage.Y > endPositionInImage.Y)
        {
            for (int i = startPositionInImage.Y; i > endPositionInImage.Y; i--)
            {
                image[startPositionInImage.X, i] = color;
            }
        }
    }

    public static Image<Rgba32> CreateMazeImage(Room[,] rooms)
    {
        var image = new Image<Rgba32>(500 * 3, 500 * 3);

        for (int x = 0; x < 500; x++)
        {
            for (int y = 0; y < 500; y++)
            {
                var room = rooms[x, y];
                var imageX = 3 * x;
                var imageY = 3 * y;
                var backgroundColor = Rgba32.Black;

                // Paint background
                for (int a = 0; a < 3; a++)
                {
                    for (int b = 0; b < 3; b++)
                    {
                        image[imageX + a, imageY + b] = backgroundColor;
                    }
                }

                var mazeColor = Rgba32.DimGray;
                if (room.Nord)
                {
                    image[imageX, imageY] = mazeColor;
                    image[imageX + 1, imageY] = mazeColor;
                    image[imageX + 2, imageY] = mazeColor;
                }
                if (room.Aust)
                {
                    image[imageX + 2, imageY] = mazeColor;
                    image[imageX + 2, imageY + 1] = mazeColor;
                    image[imageX + 2, imageY + 2] = mazeColor;
                }
                if (room.Vest)
                {
                    image[imageX, imageY] = mazeColor;
                    image[imageX, imageY + 1] = mazeColor;
                    image[imageX, imageY + 2] = mazeColor;
                }
                if (room.Syd)
                {
                    image[imageX, imageY + 2] = mazeColor;
                    image[imageX + 1, imageY + 2] = mazeColor;
                    image[imageX + 2, imageY + 2] = mazeColor;
                }
            }
        }

        return image;
    }
}

class Room
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool Nord { get; set; }
    public bool Vest { get; set; }
    public bool Syd { get; set; }
    public bool Aust { get; set; }
    [JsonIgnore]
    public bool AlreadyVisited { get; set; }

    public override string ToString()
    {
        return $"Room ({X.ToString("000")},{Y.ToString("000")})\nVisited = {AlreadyVisited}\nNord = {Nord}, Vest = {Vest}, Syd = {Syd}, Aust = {Aust}";
    }
}