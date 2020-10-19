using System;
using System.Collections.Generic;
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
            bool fall;

            int platformLength = 2; //How many platforms integrated

            int[] platformX = new int[platformLength];
            int[] platformY = new int[platformLength];
            int[] platformWidth = new int[platformLength];
            int[] platformHeight = new int[platformLength];
            platformX[0] = -500; platformY[0] = 500; platformWidth[0] = 2000; platformHeight[0] = 25; 
            platformX[1] = -200; platformY[1] = 300; platformWidth[1] = 200; platformHeight[1] = 25; 
            int jump = 0;

            Raylib.InitWindow(width,height, "Platformer");
            while(Raylib.WindowShouldClose() !=  true) //Reapeats every Frame
            {
                fall = true;
                if(Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                {
                    if(width/2 - 100 < playerX)
                    {
                        playerX--;
                    }
                    else
                    {
                        worldX++;
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
                        worldX--;
                    }
                }
                
                for(int i = 0; i < platformX.Length; i++)
                {
                    if(playerY+size == platformY[i] && playerX > platformX[i] && playerX + size < platformX[i] + platformWidth[i])
                    {
                        fall = false;
                    }
                    if(Raylib.IsKeyPressed(KeyboardKey.KEY_W) || Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
                    {
                        jump = 40;
                    }
                    
                }
                if(fall == true)
                {
                    playerY++;
                }
                if(jump != 0)
                {
                playerY -= jump;
                    jump-=2;
                }

                //Graphics
                Raylib.BeginDrawing(); //Begin Draw
                Raylib.ClearBackground(Color.BLACK); //Background
                Raylib.DrawRectangle(playerX, playerY, size, size, Color.GREEN); //Player
                for(int i = 0; i < platformX.Length; i++)
                {
                    Raylib.DrawRectangle(worldX + platformX[i], worldY + platformY[i], platformWidth[i], platformHeight[i], Color.WHITE);
                }

                Raylib.EndDrawing(); //End Draw
            }
        }
    }
}
