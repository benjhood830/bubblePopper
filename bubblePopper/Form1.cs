using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bubblePopper
{
    public partial class bubblePopper : Form
    {
        Rectangle player = new Rectangle(280, 540, 40, 10);
        Rectangle shot = new Rectangle(280, 530, 20, 20);
        int playerSpeed = 10;

        List<Rectangle> greenBubbles = new List<Rectangle>();
        List<Rectangle> playerShots = new List<Rectangle>();
        List<string> bubbleColours = new List<string>();
        List<string> shotColours = new List<string>();

        //List<int> bubbleSpeeds = new List<int>();
        //List<string> bubbleColours = new List<string>();
        int bubbleSize = 40;
        int playershotSize = 25;
        int randColor;
        int bubbleSpawn = 40;

        int score = 0;
        int timer = 0;
        int bubbleSpeeds = 1;
        int shotSpeed = 10;
        int shotTimer = 0;

        bool ADown = false;
        bool DDown = false;
        bool spaceDown = false;
        bool escDown = false;

        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush purpleBrush = new SolidBrush(Color.Purple);
        SolidBrush brush1 = new SolidBrush(Color.Chartreuse);
        SolidBrush brush2 = new SolidBrush(Color.Chartreuse);
        SolidBrush brush3 = new SolidBrush(Color.Chartreuse);
        SolidBrush brush4 = new SolidBrush(Color.Chartreuse);

        Pen blackPen = new Pen(Color.Black, 3);

        string gameState = "waiting";

        Random randGen = new Random();
        int randValue = 0;

        public bubblePopper()
        {
            InitializeComponent();
        }

        private void GameInitialize()
        {
            titleLabel.Text = "";
            subtitleLabel.Text = "";

            player.X = this.ClientSize.Width / 2 - player.Width / 2;
            player.Y = this.ClientSize.Height - player.Height;

            player.Width = 40;
            player.Height = 10;

            gameTimer.Enabled = true;

            gameState = "running";
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            timer++;
            shotTimer++;

            //if (timer % 1000 == 0)
            //{
            //    bubbleSpeeds++; ;
            //    bubbleSpawn--;
            //}

            int playershotX = player.X;
            int playershotY = player.Y;

            if (spaceDown == true && gameState == "running" && shotTimer > 10)
            {
                playerShots.Add(new Rectangle(player.X, player.Y, playershotSize, playershotSize));

                randColor = randGen.Next(1, 4);

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
                shotTimer = 0;
            }

            if (timer % bubbleSpawn == 1)
            {
                int randgreenBall = 10;
                int xGreen = 0;

                for (int i = 0; i < randgreenBall; i++)
                {
                    greenBubbles.Add(new Rectangle(xGreen, -80, bubbleSize, bubbleSize));
                    xGreen = xGreen + bubbleSize;
                }
            }

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

            for (int i = 0; i < greenBubbles.Count(); i++)
            {
                for (int a = 0; a < playerShots.Count; a++)
                {
                    if (greenBubbles[i].IntersectsWith(playerShots[a]))
                    {
                        int bubbleY = greenBubbles[i].Y;

                        playerShots.RemoveAt(a);
                        shotColours.RemoveAt(a);

                        for (int x = 0; x < greenBubbles.Count(); x++)
                        {
                            if (greenBubbles[x].Y == bubbleY)
                            {
                                greenBubbles.RemoveAt(x);
                                bubbleColours.RemoveAt(x);
                                
                            }
                        }
                       

                    }
                }
            }

            

            gameTimer.Enabled = true;

            if (gameState == "waiting")
            {
                // player.Size = new Size(0, 0);
            }
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
            for (int i = 0; i < greenBubbles.Count(); i++)
            {
                //find the new postion of y based on speed 
                int y = greenBubbles[i].Y + bubbleSpeeds;

                //replace the rectangle in the list with updated one using new y 
                greenBubbles[i] = new Rectangle(greenBubbles[i].X, y, bubbleSize, bubbleSize);
            }

            for (int i = 0; i < greenBubbles.Count(); i++)
            {
                if (greenBubbles[i].Y > this.Height - bubbleSize)
                {
                    //greenBubbles.RemoveAt(i);
                    //bubbleColours.RemoveAt(i);
                }
            }

            for (int i = 0; i < playerShots.Count(); i++)
            {
                if (playerShots[i].Y < this.Height - playershotSize)
                {
                    //playerShots.RemoveAt(i);
                    //bubbleColours.RemoveAt(i);
                }
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
                case Keys.Escape:
                    escDown = false;
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
                        //gameTimer.Enabled = true;
                        GameInitialize();
                    }
                    break;

                case Keys.Escape:

                    if (gameState == "waiting" || gameState == "over")

                    {
                        Application.Exit();
                    }
                    break;
            }
        }

        private void bubblePopper_Paint(object sender, PaintEventArgs e)
        {
            if (gameState == "running")
            {

                //e.Graphics.FillRectangle(whiteBrush, player);
                //e.Graphics.DrawRectangle(blackPen, player);

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
                    if (bubbleColours[i] == "red" )
                    {
                        e.Graphics.FillEllipse(redBrush, greenBubbles[i]);
                    }
                    else if (bubbleColours[i] == "blue")
                    {
                        e.Graphics.FillEllipse(blueBrush, greenBubbles[i]);
                    }
                    else if (bubbleColours[i] == "yellow")
                    {
                        e.Graphics.FillEllipse(yellowBrush, greenBubbles[i]);
                    }
                }

                for(int i = 0; i < shotColours.Count(); i++)
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
                //if(randColor == 1)
                //{
                //    e.Graphics.FillEllipse(yellowBrush, player);
                //}
                //else if(randColor == 2)
                //{
                //    e.Graphics.FillEllipse(redBrush, player);
                //}
                //else if (randColor == 3)
                //{
                //    e.Graphics.FillEllipse(blueBrush, player);
                //}
            }
        }
    }
}



