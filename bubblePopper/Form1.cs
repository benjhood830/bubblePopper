using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Media;

namespace bubblePopper
{
    public partial class bubblePopper : Form
    {
        Rectangle player = new Rectangle(280, 540, 40, 10);

        List<Rectangle> colorBubbles = new List<Rectangle>();
        List<Rectangle> playerShots = new List<Rectangle>();
        List<string> bubbleColours = new List<string>();
        List<string> shotColours = new List<string>();

        SoundPlayer ding = new SoundPlayer(Properties.Resources.Ding);

        int bubbleSize = 40;
        int playershotSize = 25;
        int randColor;
        int bubbleSpawn = 40;
        int timer = 0;
        int bubbleSpeeds = 1;
        int shotSpeed = 10;
        int shotTimer = 0;
        int playerSpeed = 10;

        bool ADown = false;
        bool DDown = false;
        bool spaceDown = false;
        string gameState = "waiting";

        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);

        //create random gen
        Random randGen = new Random();

        public bubblePopper()
        {
            InitializeComponent();
        }

        private void GameInitialize()
        {
            //reset Labels
            titleLabel.Text = "";
            subtitleLabel.Text = "";

            //spawn player in correct location
            player.X = this.ClientSize.Width / 2 - player.Width / 2;
            player.Y = this.ClientSize.Height - player.Height;

            //declare new player size
            player.Width = 40;
            player.Height = 10;
            //enable game timer
            gameTimer.Enabled = true;
            //start running gamestate
            gameState = "running";
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //start timer for modulus comands and shot timer for shot cooldown
            timer++;
            shotTimer++;

            //have the shot come out of where the player is currently located
            int playershotX = player.X;
            int playershotY = player.Y;

            //if space is down and cooldown is ready
            if (spaceDown == true && gameState == "running" && shotTimer > 10)
            {
                //add to payershots list 
                playerShots.Add(new Rectangle(player.X, player.Y, playershotSize, playershotSize));
                //random generator to pick colour
                randColor = randGen.Next(1, 4);

                //select colour
                if (randColor == 1)
                {
                    shotColours.Add("yellow");
                }
                else if (randColor == 2)
                {
                    shotColours.Add("red");
                }
                else if (randColor == 3)
                {
                    shotColours.Add("blue");
                }
                //reset shot timer
                shotTimer = 0;
            }

            //how often balls spawn
            if (timer % bubbleSpawn == 1)
            {
                int randgreenBall = 10;
                int xGreen = 0;

                for (int i = 0; i < randgreenBall; i++)
                {
                    colorBubbles.Add(new Rectangle(xGreen, -80, bubbleSize, bubbleSize));
                    xGreen = xGreen + bubbleSize;
                }
            }
            //if timer is divisible by 40 then add new bubbles
            if (timer % 40 == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    randColor = randGen.Next(1, 4);

                    if (randColor == 1)
                    {
                        bubbleColours.Add("yellow");
                    }
                    else if (randColor == 2)
                    {
                        bubbleColours.Add("red");
                    }
                    else if (randColor == 3)
                    {
                        bubbleColours.Add("blue");
                    }
                }
            }

            for (int i = 0; i < playerShots.Count(); i++)
            {
                //find the new postion of y based on speed 
                int y = playerShots[i].Y - shotSpeed;

                //replace the rectangle in the list with updated one using new y 
                playerShots[i] = new Rectangle(playerShots[i].X, y, playershotSize, playershotSize);
            }
            //if bubbles reach bottom of screen end game
            for (int i = 0; i < colorBubbles.Count(); i++)
            {
                if (colorBubbles[i].Y == 600 - bubbleSize)
                {
                    Application.Exit();
                }
            }

            for (int i = 0; i < colorBubbles.Count(); i++)
            {
                for (int a = 0; a < playerShots.Count; a++)
                {
                    //if bubble and shots intersect
                    if (colorBubbles[i].IntersectsWith(playerShots[a]))
                    {
                        int bubbleY = colorBubbles[i].Y;

                        playerShots.RemoveAt(a);
                        shotColours.RemoveAt(a);
                        //play sound
                        ding.Play();

                        for (int x = 0; x < colorBubbles.Count(); x++)
                        {
                            if (colorBubbles[x].Y == bubbleY)
                            {   //remove bubbles
                                colorBubbles.RemoveAt(x);
                                bubbleColours.RemoveAt(x);

                            }
                        }
                    }
                }
            }

            gameTimer.Enabled = true;

            //move player 
            if (ADown == true && player.X > 0 + 4)
            {
                player.X -= playerSpeed;
            }

            if (DDown == true && player.X < this.ClientSize.Width - player.Width - 4)
            {
                player.X += playerSpeed;
            }

            // move balls 
            for (int i = 0; i < colorBubbles.Count(); i++)
            {
                //find the new postion of y based on speed 
                int y = colorBubbles[i].Y + bubbleSpeeds;

                //replace the rectangle in the list with updated one using new y 
                colorBubbles[i] = new Rectangle(colorBubbles[i].X, y, bubbleSize, bubbleSize);
            }

            Refresh();
        }

        private void bubblePopper_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    ADown = false;
                    break;
                case Keys.D:
                    DDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
            }
        }

        private void bubblePopper_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    ADown = true;
                    break;
                case Keys.D:
                    DDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;

                    if (gameState == "waiting" || gameState == "over")
                    {
                        //if space is pressed, start game
                        GameInitialize();
                    }
                    break;
                case Keys.Escape:
                    if (gameState == "waiting" || gameState == "over")
                    {
                        //if esc is pressed exit program
                        Application.Exit();
                    }
                    break;
            }
        }
        private void bubblePopper_Paint(object sender, PaintEventArgs e)
        {
            if (gameState == "running")
            {
                for (int i = 1; i < shotColours.Count(); i++)
                {
                    if (shotColours[i] == "red")
                    {
                        e.Graphics.FillRectangle(redBrush, player);
                    }
                    else if (shotColours[i] == "blue")
                    {
                        e.Graphics.FillRectangle(blueBrush, player);
                    }
                    else if (shotColours[i] == "yellow")
                    {
                        e.Graphics.FillRectangle(yellowBrush, player);
                    }
                }

                for (int i = 0; i < bubbleColours.Count(); i++)
                {
                    if (bubbleColours[i] == "red")
                    {
                        e.Graphics.FillEllipse(redBrush, colorBubbles[i]);
                    }
                    else if (bubbleColours[i] == "blue")
                    {
                        e.Graphics.FillEllipse(blueBrush, colorBubbles[i]);
                    }
                    else if (bubbleColours[i] == "yellow")
                    {
                        e.Graphics.FillEllipse(yellowBrush, colorBubbles[i]);
                    }
                }

                for (int i = 0; i < shotColours.Count(); i++)
                {
                    if (shotColours[i] == "red")
                    {
                        e.Graphics.FillEllipse(redBrush, playerShots[i]);
                    }
                    else if (shotColours[i] == "blue")
                    {
                        e.Graphics.FillEllipse(blueBrush, playerShots[i]);
                    }
                    else if (shotColours[i] == "yellow")
                    {
                        e.Graphics.FillEllipse(yellowBrush, playerShots[i]);
                    }
                }
            }
        }
    }
}