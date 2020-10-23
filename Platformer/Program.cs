using System;
using System.IO;
using System.Collections.Generic;
using Raylib_cs;

namespace Platformer
{
    class Program
    {
        static void Main(string[] args)
        {
        //World Rules
            bool open = true;
            int width = 600;                        //Width of the window
            int height = 600;                       //Height of the window
            int worldX = 0;                         //Where the world 0 is - X axis
            int worldY = 0;                         //Where the world 0 is - Y axis
            int gameState = 0;                      //What part of the game is active       0 = Main Menu   1 = Game   2 = Death
            int gravity = 0;                        //The force of gravity
            int cayTimeOri = 10;                    //How long the cayote Time should be
            int cayTime = 0;                        //Current Cayote Time

        //Player Rules
            int size = 20;                  //Size of the player squere
            int playerX = width/2-size/2;   //Where the player is on the screen - X axis
            int playerY = height/2-size/2;  //Where the player is on the screen - Y axis
            int playerWorldX;               //Where the player is Worldwise - X axis
            int playerWorldY;               //Where the player is Worldwise - Y axis
            bool fall;                      //If the player should fall
            int jump = 0;                   //Force upwards - Warning should not be tinkered with as the player would start by jumping
            int speed = 16;                 //How fast the player is
            int coins = 0;

        //Normal Platforms
            int platformLength = 2;         //How many platforms are integrated, Flawed system as it needs to be changed as well when adding new platforms

            int[] platformX = new int[platformLength];                                                  //Where each platform is related to world 0 - X axis
            int[] platformY = new int[platformLength];                                                  //Where each platform is related to world 0 - Y axis
            int[] platformWidth = new int[platformLength];                                              //The width of each platform
            int[] platformHeight = new int[platformLength];                                             //The length of each platform
            platformX[0] = -500; platformY[0] = 500; platformWidth[0] = 2000; platformHeight[0] = 25;   //Platform 0
            platformX[1] = -200; platformY[1] = 301; platformWidth[1] = 200; platformHeight[1] = 25;    //Platform 1
            //Should possibley be changed to a class format
        
        //Coin
            int coinLength = 1;                         //How many coins are integrated, Flawed system as it needs to be changed as well when adding new coins
            int coinSize = 10;                          //The size of coins
            int[] coinX = new int[coinLength];          //Where each coin is related to world 0 - X axis
            int[] coinY = new int[coinLength];          //Where each coin is related to world 0 - Y axis
            bool[] coinHidden = new bool[coinLength];     //Hide coins picked up
            coinX[0] = 100; coinY[0] = 480;             //Coin 0
            for(int i = 0; i < coinHidden.Length; i++)
            {coinHidden[i] = false; }
        
        //File Creation
            bool fileCreation = false;
            string fileName = "";
            int fileCreationStep = 0;

        //Map creation
            int mapCreationStep = 0;

        //checkPoint creation
            string checkPointString = "";
            Color checkPointColor = new Color(0,255,0,130);
            int checkPointSize = 30;
            List<int> buildingcheckPointX = new List<int>();
            List<int> buildingcheckPointY = new List<int>();
        //Platform creation
            string platformString = "";
            Color platformColor = new Color(255,255,255,130);
            List<int> buildingPlatformX = new List<int>();
            List<int> buildingPlatformY = new List<int>();
            List<int> buildingPlatformWidth = new List<int>();
            List<int> buildingPlatformHigth = new List<int>();
            int currentPlatformX = 0;
            int currentPlatformY = 0;
            int currentPlatformW = 0;
            int currentPlatformH = 0;
        //Coin creation
            string coinString = "";
            Color coinColor = new Color(0,255,0,130);
            List<int> buildingCoinX = new List<int>();
            List<int> buildingCoinY = new List<int>();
        //
            Raylib.InitWindow(width,height, "Platformer");  //Creates Window
            Raylib.SetTargetFPS(60);                        //Target Framerate          
            Raylib.SetExitKey(0);
            while(!Raylib.WindowShouldClose() && open == true)      //Reapeats every Frame, The Game
            {
                if(gameState == 0)      //Main Menu
                {
                    menu();
                }
                
                else if(gameState == 1) //Game
                {
                   game(); 
                }
                
                else if(gameState == 2) //Death
                {
                    death();
                }
                
                else if(gameState == 3) //Create Map File
                {
                    fileCreate();
                }
                
                else if(gameState == 4) //Make Level
                {
                    mapCreate();
                }
                
                else if(gameState == 5) //Choose Map Not Implemented
                {

                }

                else
                {
                    gameState = 0;
                }
                
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE) && gameState != 3 && gameState != 4)
                {
                    gameState = 0;
                }

            }

            void menu()         //Gamestate 0
            {
                int mouseX = Raylib.GetMouseX();
                int mouseY = Raylib.GetMouseY();
                Color but1ColBack = new Color(30, 30, 30, 255);
                Color but2ColBack = new Color(30, 30, 30, 255);
                Color but3ColBack = new Color(30, 30, 30, 255);
                Color but1ColFront = new Color(255, 255, 255, 255);
                Color but2ColFront = new Color(255, 255, 255, 255);
                Color but3ColFront = new Color(255, 255, 255, 255);
                if((mouseX > 20 && mouseX < 320) && (mouseY > 200 && mouseY < 270))
                {
                    but1ColBack = new Color(255, 255, 255, 255);
                    but1ColFront = new Color(10, 10, 10, 255);
                }
                if((mouseX > 20 && mouseX < 320) && (mouseY > 270 && mouseY < 320))
                {
                    but2ColBack = new Color(255, 255, 255, 255);
                    but2ColFront = new Color(10, 10, 10, 255);
                }
                if((mouseX > 20 && mouseX < 320) && (mouseY > 340 && mouseY < 390))
                {
                    but3ColBack = new Color(255, 255, 255, 255);
                    but3ColFront = new Color(10, 10, 10, 255);
                }
                if((mouseX > 20 && mouseX < 320) && (mouseY > 200 && mouseY < 270) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) == true)
                {
                    gameState = 1;
                }
                if((mouseX > 20 && mouseX < 320) && (mouseY > 270 && mouseY < 320) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) == true)
                {
                    gameState = 3;
                }
                if((mouseX > 20 && mouseX < 320) && (mouseY > 340 && mouseY < 390) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) == true)
                {
                    gameState = 0;
                }
                //Graphics
                Raylib.BeginDrawing();                                              //Begin Draw
                Raylib.ClearBackground(Color.BLACK);                                //Background
                Raylib.DrawText("A Small Platformer", 20, 20, 60, Color.WHITE);
                Raylib.DrawRectangle(20, 200, 300, 50, but1ColBack);
                Raylib.DrawText("Play Game", 30, 210, 30, but1ColFront);
                Raylib.DrawRectangle(20, 270, 300, 50, but2ColBack);
                Raylib.DrawText("Make Level", 30, 280, 30, but2ColFront);
                Raylib.DrawRectangle(20, 340, 300, 50, but3ColBack);
                Raylib.DrawText("LOREM IPSUM", 30, 350, 30, but3ColFront);
                Raylib.EndDrawing();                                                //End Draw
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE) && gameState == 0)
                {
                    open = false;
                }
            }


            //GAME
            void game()         //Gamestate 1
            {
                playerWorldX = playerX - worldX;
                playerWorldY = playerY - worldY;
                fall = true;
               
                movement(); //Player Movement
            
                logic(); //Platform and Gravity Logic + Jump

                //Graphics
                Raylib.BeginDrawing();                                              //Begin Draw
                Raylib.ClearBackground(Color.BLACK);                                //Background
                Raylib.DrawRectangle(playerX, playerY, size, size, Color.GREEN);    //Player
                for(int i = 0; i < platformX.Length; i++)                           //Platforms loop
                {
                    Raylib.DrawRectangle(worldX + platformX[i], worldY + platformY[i], platformWidth[i], platformHeight[i], Color.WHITE); //Induvidual Platforms
                }

                for(int i = 0; i < coinX.Length; i++)                           //Platforms loop
                {
                    if(coinHidden[i] == false)
                    {
                        Raylib.DrawRectangle(worldX + coinX[i], worldY + coinY[i], coinSize, coinSize, Color.YELLOW); //Induvidual Platforms
                    }
                }
                Raylib.DrawFPS(10,10);
                Raylib.DrawText("Coins: " + coins, 10, 40, 10, Color.YELLOW);
                Raylib.EndDrawing(); //End Draw
            }

            void movement()         //Prt of GS 1
            {
                if(Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))       //Go Left
                {
                    if(width/2 - 100 < playerX)     //Move player within boundary
                    {
                        playerX-=speed;
                        }
                        else                            //Otherwise move world
                        {
                            worldX+=speed;
                        }
                    }
                
                    if(Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))      //Go Right
                    {
                        if(width/2 + 100 > playerX)     //Move player within boundary
                        {
                            playerX+=speed;
                        }
                        else                            //Otherwise move world
                        {
                            worldX-=speed;
                        }
                    }
            }

            void logic()            //Prt of GS 1
            {
                for(int i = 0; i < platformLength; i++)                                                 //Platform Logic
                    {
                        //Make not fall if on platform
                        if((playerWorldY + size - gravity <= platformY[i]) && (playerWorldY + size >= platformY[i]) && (playerWorldX + size > platformX[i]) && (playerWorldX < platformX[i] + platformWidth[i]))
                        {
                            if(playerWorldY +size -gravity <= platformY[i])
                            {
                                playerY = platformY[i] - size;
                            }
                            fall = false;
                        }
                    
                    }
                    //Create the possibility to jump if cayote Time hasent gone to 0
                    if((Raylib.IsKeyPressed(KeyboardKey.KEY_W) || Raylib.IsKeyPressed(KeyboardKey.KEY_UP)) && cayTime > 0)                                                   
                    {
                        jump = 40;
                        cayTime = 0;                    
                    }
                    if(fall == true) //If nothing stoped it from falling it will fall
                    {
                        playerY += gravity;
                        gravity++;
                        if(cayTime >0)
                        {
                            cayTime--;
                        }
                    }
                    if(fall == false) //If the player isnt falling
                    {
                        gravity = 0;
                        cayTime = cayTimeOri;
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

                    //Coins
                    for(int i = 0; i < coinLength; i++)
                    {
                        if(((playerWorldX < coinX[i] && playerWorldX + size > coinX[i]) || (playerWorldX < coinX[i]+coinSize && playerWorldX + size > coinX[i]+coinSize)) && ((playerWorldY < coinY[i] && playerWorldY + size > coinY[i]) || (playerWorldY < coinY[i]+coinSize && playerWorldY + size > coinY[i]+coinSize)) && coinHidden[i] == false)
                        {
                            coins++;
                            coinHidden[i] = true;
                        }
                    }
            }


            void death()            //Gamestate 2
            {
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_R))
                {
                    worldX = 0;
                    worldY = 0;
                    playerX = 300;
                    playerY = 300; 
                    gameState = 1;
                }
                //Graphics
                Raylib.BeginDrawing();                                              //Begin Draw
                Raylib.ClearBackground(Color.BLACK);                                //Background
                Raylib.DrawText("You Died", width/2-90, height/2-20,40,Color.RED);  //Death Text
                Raylib.DrawText("Press [R] to restart", width/2-210, height/2+60,40,Color.RED);
                Raylib.EndDrawing();                                                //End Draw                
            }

            void fileCreate()       //Gamestate 3
            {
                if(fileCreationStep == 0)
                {
                    fileCreation = true;
                    keyBoard();
                    fileCreation = false;
                    if(Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        fileCreationStep = 1;
                    }
                    //Graphics
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.BLACK);
                    Raylib.DrawText("Make a new File", 10, 10, 20, Color.WHITE);
                    Raylib.DrawText(fileName, 20, 110, 20, Color.WHITE);
                    Raylib.EndDrawing();
                }
                if(fileCreationStep == 1)
                {
                    Directory.CreateDirectory(@"maps\" + fileName);
                    File.Create(@"maps\" + fileName + @"\platform.txt").Close();
                    System.Console.WriteLine("File Created: " + fileName + @"\platform.txt");
                    File.Create(@"maps\" + fileName + @"\checkPoint.txt").Close();
                    System.Console.WriteLine("File Created: " + fileName + @"\checkPoint.txt");
                    File.Create(@"maps\" + fileName + @"\coin.txt").Close();
                    System.Console.WriteLine("File Created: " + fileName + @"\coin.txt");
                    
                    fileCreationStep = 2;
                }
                if(fileCreationStep == 2)
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.BLACK);
                    Raylib.DrawText("New File Created", 10, 10, 20, Color.WHITE);
                    Raylib.EndDrawing();
                    gameState = 4;
                }

                
            }

            void mapCreate()        //Gamestate 4
            {

                    if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                    {
                        if(mapCreationStep == 5)    //checkPoints
                        {
                            coinString += (Raylib.GetMouseX() - worldX).ToString();
                            coinString += ", " + (Raylib.GetMouseY() - worldY).ToString();
                            buildingCoinX.Add(Raylib.GetMouseX() - worldX - coinSize/2);
                            buildingCoinY.Add(Raylib.GetMouseY() - worldY - coinSize/2);
                            coinString += Environment.NewLine;
                        }
                        if(mapCreationStep == 2)    //X, Y for platform
                        {
                            platformString += (Raylib.GetMouseX() + worldX).ToString();
                            platformString += ", " + (Raylib.GetMouseY() + worldY).ToString();
                            buildingPlatformX.Add(Raylib.GetMouseX() - worldX);
                            buildingPlatformY.Add(Raylib.GetMouseY() - worldY);
                            currentPlatformX = Raylib.GetMouseX() - worldX;
                            currentPlatformY = Raylib.GetMouseY() - worldY;
                            mapCreationStep = -1;
                        }
                        if(mapCreationStep == 3)    //Width and Heigth for platforms
                        {
                            currentPlatformW = currentPlatformX - (Raylib.GetMouseX() - worldX);
                            currentPlatformH = currentPlatformY - (Raylib.GetMouseY() - worldY);
                            if(currentPlatformW < 0)
                            {
                                currentPlatformW = currentPlatformW*-1;
                            }
                            if(currentPlatformH < 0)
                            {
                                currentPlatformH = currentPlatformH*-1;
                            }
                            platformString += ", " + (currentPlatformW).ToString();
                            platformString += ", " + (currentPlatformH).ToString();
                            buildingPlatformWidth.Add(currentPlatformW);
                            buildingPlatformHigth.Add(currentPlatformH);
                            platformString += Environment.NewLine;
                            mapCreationStep = 2;
                        }
                        if(mapCreationStep == 1)    //checkPoints
                        {
                            checkPointString += Environment.NewLine;
                            checkPointString += (Raylib.GetMouseX() - worldX).ToString();
                            checkPointString += ", " + (Raylib.GetMouseY() - worldY).ToString();
                            buildingcheckPointX.Add(Raylib.GetMouseX() - worldX);
                            buildingcheckPointY.Add(Raylib.GetMouseY() - worldY);
                        }
                        if(mapCreationStep == 0)    //Initial spawn
                        {
                            System.Console.WriteLine("ACTIVE FILE = " + fileName);
                            checkPointString = (Raylib.GetMouseX() - worldX).ToString();
                            checkPointString += ", " + (Raylib.GetMouseY() - worldY).ToString();
                            buildingcheckPointX.Add(Raylib.GetMouseX() - worldX - checkPointSize/2);
                            buildingcheckPointY.Add(Raylib.GetMouseY() - worldY - checkPointSize/2);
                            mapCreationStep = 1;
                        }
                        if(mapCreationStep == -1)
                        {
                            mapCreationStep = 3;
                        }
 
                    }

                    if(Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                    {
                        worldY+=speed;
                    }
                    if(Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                    {
                        worldX+=speed;
                    }
                    if(Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
                    {
                        worldY-=speed;
                    }
                    if(Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                    {
                        worldX-=speed;
                    }

                    if(Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        if(mapCreationStep == 5)
                        {
                        File.WriteAllText(@"maps\" + fileName + @"\coin.txt", coinString);
                        gameState = 0;
                        }
                        if(mapCreationStep == 2)
                        {
                        File.WriteAllText(@"maps\" + fileName + @"\platform.txt", platformString);
                        mapCreationStep = 5;
                        }
                        if(mapCreationStep == 1)
                        {
                        File.WriteAllText(@"maps\" + fileName + @"\checkPoint.txt", checkPointString);
                        mapCreationStep = 2;
                        }
                    }

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.BLACK);
                    if(mapCreationStep == 0)
                    {
                        Raylib.DrawText("Choose a place for player spawn", 10, 10, 20, Color.WHITE);
                    }
                    if(mapCreationStep == 1)
                    {
                        Raylib.DrawText("Choose a place for the next checkPoint", 10, 10, 20, Color.WHITE);
                    }
                    if(mapCreationStep == 2)
                    {
                        Raylib.DrawText("Choose a place for the next platform", 10, 10, 20, Color.WHITE);
                    }
                    if(mapCreationStep == 3)
                    {
                        Raylib.DrawText("Choose a place for the other end of the platform", 10, 10, 20, Color.WHITE);
                    }
                    if(mapCreationStep == 5)
                    {
                        Raylib.DrawText("Choose a place to place coins", 10, 10, 20, Color.WHITE);
                    }
                    for(int i = 0; i < buildingcheckPointX.Count; i++)
                    {
                        Raylib.DrawRectangle(buildingcheckPointX[i] + worldX, buildingcheckPointY[i] + worldY, checkPointSize, checkPointSize, checkPointColor);
                    }
                    for(int i = 0; i < buildingPlatformHigth.Count; i++)
                    {
                        Raylib.DrawRectangle(buildingPlatformX[i] + worldX, buildingPlatformY[i] + worldY,buildingPlatformWidth[i], buildingPlatformHigth[i], platformColor);
                    }
                    for(int i = 0; i < buildingCoinX.Count; i++)
                    {
                        Raylib.DrawRectangle(buildingCoinX[i] + worldX, buildingCoinY[i] + worldY, coinSize, coinSize, Color.YELLOW);
                    }
                    
                    Raylib.EndDrawing();
            }
            
            void keyBoard()     //When the Player should write text
            {

                if(Raylib.IsKeyPressed(KeyboardKey.KEY_A))
                {
                    if(fileCreation == true)
                    {
                        fileName += "a";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_B))
                {
                    if(fileCreation == true)
                    {
                        fileName += "b";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_C))
                {
                    if(fileCreation == true)
                    {
                        fileName += "c";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_D))
                {
                    if(fileCreation == true)
                    {
                        fileName += "d";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_E))
                {
                    if(fileCreation == true)
                    {
                        fileName += "e";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_F))
                {
                    if(fileCreation == true)
                    {
                        fileName += "f";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_G))
                {
                    if(fileCreation == true)
                    {
                        fileName += "g";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_H))
                {
                    if(fileCreation == true)
                    {
                        fileName += "h";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_I))
                {
                    if(fileCreation == true)
                    {
                        fileName += "i";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_J))
                {
                    if(fileCreation == true)
                    {
                        fileName += "j";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_K))
                {
                    if(fileCreation == true)
                    {
                        fileName += "k";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_L))
                {
                    if(fileCreation == true)
                    {
                        fileName += "l";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_M))
                {
                    if(fileCreation == true)
                    {
                        fileName += "m";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_N))
                {
                    if(fileCreation == true)
                    {
                        fileName += "n";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_O))
                {
                    if(fileCreation == true)
                    {
                        fileName += "o";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_P))
                {
                    if(fileCreation == true)
                    {
                        fileName += "p";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_Q))
                {
                    if(fileCreation == true)
                    {
                        fileName += "q";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_R))
                {
                    if(fileCreation == true)
                    {
                        fileName += "r";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_S))
                {
                    if(fileCreation == true)
                    {
                        fileName += "s";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_T))
                {
                    if(fileCreation == true)
                    {
                        fileName += "t";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_U))
                {
                    if(fileCreation == true)
                    {
                        fileName += "u";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_V))
                {
                    if(fileCreation == true)
                    {
                        fileName += "v";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_W))
                {
                    if(fileCreation == true)
                    {
                        fileName += "w";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_X))
                {
                    if(fileCreation == true)
                    {
                        fileName += "x";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_Y))
                {
                    if(fileCreation == true)
                    {
                        fileName += "y";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_Z))
                {
                    if(fileCreation == true)
                    {
                        fileName += "z";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_ONE))
                {
                    if(fileCreation == true)
                    {
                        fileName += "1";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_TWO))
                {
                    if(fileCreation == true)
                    {
                        fileName += "2";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_THREE))
                {
                    if(fileCreation == true)
                    {
                        fileName += "3";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_FOUR))
                {
                    if(fileCreation == true)
                    {
                        fileName += "4";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_FIVE))
                {
                    if(fileCreation == true)
                    {
                        fileName += "5";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_SIX))
                {
                    if(fileCreation == true)
                    {
                        fileName += "6";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_SEVEN))
                {
                    if(fileCreation == true)
                    {
                        fileName += "7";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_EIGHT))
                {
                    if(fileCreation == true)
                    {
                        fileName += "8";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_NINE))
                {
                    if(fileCreation == true)
                    {
                        fileName += "9";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_ZERO))
                {
                    if(fileCreation == true)
                    {
                        fileName += "0";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    if(fileCreation == true)
                    {
                        fileName += " ";
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
                {
                    if(fileCreation == true)
                    {
                        try{
                            fileName = fileName.Remove(fileName.Length -1, 1);
                        }
                        catch{}
                    }
                }
            }
        }
    }
}