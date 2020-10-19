using System;
using Raylib_cs;

namespace Platformer
{
    class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(600,600, "Platformer");
            int size = 20;
            int playerY = 300-size/2;
            int worldX = 0;
            int worldY = 0;
            while(Raylib.WindowShouldClose() !=  true) //Reapeats every Frame
            {
                if(Raylib.IsKeyDown(KeyboardKey.KEY_A) ||Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                {
                    worldX--;   
                }
                if(Raylib.IsKeyDown(KeyboardKey.KEY_D) ||Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                {
                    worldX++;   
                }

                //Graphics
                Raylib.BeginDrawing(); //Begin Draw
                Raylib.ClearBackground(Color.BLACK); //Background
                Raylib.DrawRectangle(300-size,playerY,size,size,Color.GREEN); //Player
                Raylib.DrawRectangle(worldX + 50, worldY + 400,500,25, Color.WHITE);
                

                Raylib.EndDrawing(); //End Draw
            }
        }
    }
}
