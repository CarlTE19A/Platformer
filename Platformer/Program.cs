using System;
using Raylib_cs;

namespace Platformer
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 600;
            int height = 600;
            int size = 20;
            int playerX = width/2-size/2;
            int playerY = height/2-size/2;
            int worldX = 0;
            int worldY = 0;
            Raylib.InitWindow(width,height, "Platformer");
            while(Raylib.WindowShouldClose() !=  true) //Reapeats every Frame
            {
                if(Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                {
                    if(width/2 - 100 < playerX)
                    {
                        playerX--;
                    }
                    else
                    {
                        worldX--;
                    }
                }
                if(Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                {
                    if(width/2 + 100 > playerX)
                    {
                        playerX++;
                    }
                    else
                    {
                        worldX++;
                    }
                }
                

                //Graphics
                Raylib.BeginDrawing(); //Begin Draw
                Raylib.ClearBackground(Color.BLACK); //Background
                Raylib.DrawRectangle(playerX, playerY, size, size, Color.GREEN); //Player
                Raylib.DrawRectangle(worldX + 50, worldY + 400,500,25, Color.WHITE);
                

                Raylib.EndDrawing(); //End Draw
            }
        }
    }
}
