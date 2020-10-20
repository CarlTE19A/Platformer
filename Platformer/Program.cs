using System;
using System.Collections.Generic;
using Raylib_cs;

namespace Platformer
{
    class Program
    {
        static void Main(string[] args)
        {
        //World Rules
            int width = 600;                //Width of the window
            int height = 600;               //Height of the window
            int worldX = 0;                 //Where the world 0 is - X axis
            int worldY = 0;                 //Where the world 0 is - Y axis
            int gameState = 1;              //What part of the game is active       0 = Main Menu   1 = Game

        //Player Rules
            int size = 20;                  //Size of the player squere
            int playerX = width/2-size/2;   //Where the player is on the screen - X axis
            int playerY = height/2-size/2;  //Where the player is on the screen - Y axis
            int playerWorldX;               //Where the player is Worldwise - X axis
            int playerWorldY;               //Where the player is Worldwise - Y axis
            bool fall;                      //If the player should fall
            int jump = 0;                   //Force upwards - Warning should not be tinkered with as the player would start by jumping


        //Normal Platforms
            int platformLength = 2;         //How many platforms are integrated, Flawed system as it needs to be changed as well when adding new platforms

            int[] platformX = new int[platformLength];                                                  //Where each platform is related to world 0 - X axis
            int[] platformY = new int[platformLength];                                                  //Where each platform is related to world 0 - Y axis
            int[] platformWidth = new int[platformLength];                                              //The width of each platform
            int[] platformHeight = new int[platformLength];                                             //The length of each platform
            platformX[0] = -500; platformY[0] = 500; platformWidth[0] = 2000; platformHeight[0] = 25;   //Platform 0
            platformX[1] = -200; platformY[1] = 300; platformWidth[1] = 200; platformHeight[1] = 25;    //Platform 1

            Raylib.InitWindow(width,height, "Platformer");  //Creates Window
            while(Raylib.WindowShouldClose() !=  true)      //Reapeats every Frame, the Game
            {
                if(gameState == 0)  //Main Menu
                {
                    //Graphics
                    Raylib.BeginDrawing();                                              //Begin Draw
                    Raylib.ClearBackground(Color.BLACK);                                //Background
                    
                    Raylib.EndDrawing(); //End Draw
                }
                
                if(gameState == 1)  //Game
                {
                    playerWorldX = playerX - worldX;
                    playerWorldY = playerY - worldY;
                    fall = true;

                    if(Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))       //Go Left
                    {
                        if(width/2 - 100 < playerX)     //Move player within boundary
                        {
                            playerX--;
                        }
                        else                            //Otherwise move world
                        {
                            worldX++;
                        }
                    }
                
                    if(Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))      //Go Right
                    {
                        if(width/2 + 100 > playerX)     //Move player within boundary
                        {
                            playerX++;
                        }
                        else                            //Otherwise move world
                        {
                            worldX--;
                        }
                    }
                
                    for(int i = 0; i < platformLength; i++)                                                 //Platform Logic
                    {
                        //Make not fall if on platform
                        if((playerWorldY + size == platformY[i]) && (playerWorldX + size > platformX[i]) && (playerWorldX < platformX[i] + platformWidth[i]))
                        {
                            fall = false;

                        }
                        //Create the possibility to jump if on platform - Should be changed if diffrent platforms are implemented
                        if(Raylib.IsKeyPressed(KeyboardKey.KEY_W) || Raylib.IsKeyPressed(KeyboardKey.KEY_UP))                                                   
                        {
                            jump = 40;
                        }
                    
                    }
                    if(fall == true) //If nothing stoped it from falling it will fall
                    {
                        playerY++;
                    }
                    if(jump != 0) //Gravity implementation
                    {
                        playerY -= jump; //Affect player
                        jump-=2; //Deceases jumpforce until 0
                    }
                    if(playerY>height) //Fall death
                    {
                        gameState = 2;
                    }

                    //Graphics
                    Raylib.BeginDrawing();                                              //Begin Draw
                    Raylib.ClearBackground(Color.BLACK);                                //Background
                    Raylib.DrawRectangle(playerX, playerY, size, size, Color.GREEN);    //Player
                    for(int i = 0; i < platformX.Length; i++)                           //Platforms loop
                    {
                        Raylib.DrawRectangle(worldX + platformX[i], worldY + platformY[i], platformWidth[i], platformHeight[i], Color.WHITE); //Induvidual Platforms
                    }

                    Raylib.EndDrawing(); //End Draw
                }
                if(gameState == 2)  //Death
                {
                    //Graphics
                    Raylib.BeginDrawing();                                              //Begin Draw
                    Raylib.ClearBackground(Color.BLACK);                                //Background
                    Raylib.DrawText("You Died", width/2-90, height/2-20,40,Color.RED);
                    Raylib.EndDrawing(); //End Draw
                }
            }
        }
    }
}
