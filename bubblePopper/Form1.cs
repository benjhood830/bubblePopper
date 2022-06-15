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
        List<Rectangle> redBubbles = new List<Rectangle>();
        List<Rectangle> blueBubbles = new List<Rectangle>();
        List<Rectangle> yellowBubbles = new List<Rectangle>();
        List<Rectangle> playerShots = new List<Rectangle>();
        List<string> ballColours = new List<string>();

        //List<int> bubbleSpeeds = new List<int>();
        //List<string> bubbleColours = new List<string>();
        int bubbleSize = 20;
        int playershotSize = 14;
        int randredBall;
        int randblueBall;
        int randyellowBall;
        int randColor;


        int score = 0;
        int timer;
        int bubbleSpeeds = 1;
        int shotSpeed = 2;
        float finalBubbleSpeed;


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

            if (spaceDown == true && timer % 20 == 0 && gameState == "running")
            {

            }


            if (timer % 20 == 1)
            {
                int randgreenBall = randGen.Next(3, 8);
                int xGreen = 0;

                for (int i = 0; i < randgreenBall; i++)
                {
                    greenBubbles.Add(new Rectangle(xGreen, -30, bubbleSize, bubbleSize));
                    xGreen = xGreen + bubbleSize;
                }

                if (randgreenBall == 3)
                {
                    randredBall = 5;
                }
                else if (randgreenBall == 4)
                {
                    randredBall = 4;
                }
                else if (randgreenBall == 5)
                {
                    randredBall = 3;
                }
                else if (randgreenBall == 6)
                {
                    randredBall = 2;
                }
                else if (randgreenBall == 7)
                {
                    randredBall = 1;
                }
                else if (randgreenBall == 8)
                {
                    randredBall = 0;
                }

                int xRed = 140;

                for (int i = 0; i < randredBall; i++)
                {
                    redBubbles.Add(new Rectangle(xRed, -30, bubbleSize, bubbleSize));
                    xRed = xRed - bubbleSize;
                }

                randredBall = randGen.Next(3, 5);

                xRed = 160;

                for (int i = 0; i < randredBall; i++)
                {
                    redBubbles.Add(new Rectangle(xRed, -30, bubbleSize, bubbleSize));
                    xRed = xRed + bubbleSize;
                }

                //randblueBall = randGen.Next(3, 6);
                if (randredBall == 3)
                {
                    randblueBall = 5;
                }
                else if (randredBall == 4)
                {
                    randblueBall = 4;
                }
                else if (randredBall == 5)
                {
                    randblueBall = 3;
                }
                //else if (randredBall == 6)
                //{
                //    randblueBall = 2;
                //}
                //else if (randredBall == 7)
                //{
                //    randblueBall = 1;
                //}
                //else if (randredBall == 8)
                //{
                //    randblueBall = 0;
                //}
                int xBlue = 300;

                for (int i = 0; i < randblueBall; i++)
                {
                    blueBubbles.Add(new Rectangle(xBlue, -30, bubbleSize, bubbleSize));
                    xBlue = xBlue - bubbleSize;
                }

                randyellowBall = randGen.Next(3, 6);

                int xYellow = 380;

                for (int i = 0; i < randyellowBall; i++)
                {
                    yellowBubbles.Add(new Rectangle(xYellow, -30, bubbleSize, bubbleSize));
                    xYellow = xYellow - bubbleSize;
                }


                randblueBall = randGen.Next(1, 4);

                xBlue = 320;

                for (int i = 0; i < randblueBall; i++)
                {
                    blueBubbles.Add(new Rectangle(xBlue, -30, bubbleSize, bubbleSize));
                    xBlue = xBlue + bubbleSize;
                }



                if (randblueBall == 3)
                {
                    randyellowBall = 4;
                }
                else if (randblueBall == 4)
                {
                    randyellowBall = 3;
                }
                else if (randblueBall == 5)
                {
                    randyellowBall = 2;
                }
                else if (randblueBall == 6)
                {
                    randyellowBall = 1;
                }
                else if (randblueBall == 7)
                {
                    randyellowBall = 0;
                }
                else if (randblueBall == 8)
                {
                    randyellowBall = 0;
                }


                int previousColor;
                if (timer % 3 == 0)
                {
                    randColor = randGen.Next(1, 5);
                    previousColor = randColor;

                    //if (randColor == previousColor)
                    //{
                    //    randColor = randGen.Next(1, 5);

                    //}

                    if (randColor == 1)
                    {
                        brush1.Color = Color.Blue;
                    }
                    else if (randColor == 2)
                    {
                        brush1.Color = Color.Green;
                    }
                    else if (randColor == 3)
                    {
                        brush1.Color = Color.Red;
                    }
                    else if (randColor == 4)
                    {
                        brush1.Color = Color.Yellow;
                    }

                    if (randColor == 1)
                    {
                        brush2.Color = Color.Green;
                    }
                    else if (randColor == 2)
                    {
                        brush2.Color = Color.Red;
                    }
                    else if (randColor == 3)
                    {
                        brush2.Color = Color.Yellow;
                    }
                    else if (randColor == 4)
                    {
                        brush2.Color = Color.Blue;
                    }

                    if (randColor == 1)
                    {
                        brush3.Color = Color.Red;
                    }
                    else if (randColor == 2)
                    {
                        brush3.Color = Color.Yellow;
                    }
                    else if (randColor == 3)
                    {
                        brush3.Color = Color.Blue;
                    }
                    else if (randColor == 4)
                    {
                        brush3.Color = Color.Green;
                    }

                    if (randColor == 1)
                    {
                        brush4.Color = Color.Yellow;
                    }
                    else if (randColor == 2)
                    {
                        brush4.Color = Color.Blue;
                    }
                    else if (randColor == 3)
                    {
                        brush4.Color = Color.Green;
                    }
                    else if (randColor == 4)
                    {
                        brush4.Color = Color.Red;
                    }


                }

            }


            int playershotX = player.X;
            int playershotY = player.Y;

            if (spaceDown == true && gameState == "running")
            {
                playerShots.Add(new Rectangle(playershotX, playershotY, playershotSize, playershotSize));

            }





            //for (int i = 0; i < greenBubbles.Count(); i++)
            //{
            //    while(greenBubbles.IntersectWith(greenBubbles))

            //}


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
            for (int i = 0; i < redBubbles.Count(); i++)
            {
                //find the new postion of y based on speed 
                int y = redBubbles[i].Y + bubbleSpeeds;

                //replace the rectangle in the list with updated one using new y 
                redBubbles[i] = new Rectangle(redBubbles[i].X, y, bubbleSize, bubbleSize);
            }
            for (int i = 0; i < blueBubbles.Count(); i++)
            {
                //find the new postion of y based on speed 
                int y = blueBubbles[i].Y + bubbleSpeeds;

                //replace the rectangle in the list with updated one using new y 
                blueBubbles[i] = new Rectangle(blueBubbles[i].X, y, bubbleSize, bubbleSize);
            }
            for (int i = 0; i < yellowBubbles.Count(); i++)
            {
                //find the new postion of y based on speed 
                int y = yellowBubbles[i].Y + bubbleSpeeds;

                //replace the rectangle in the list with updated one using new y 
                yellowBubbles[i] = new Rectangle(yellowBubbles[i].X, y, bubbleSize, bubbleSize);
            }
            
            for(int i = 0; i < greenBubbles.Count(); i++)
            {
                if (greenBubbles[i].Y > this.Height - bubbleSize)
                {
                    greenBubbles.RemoveAt(i);
                }
            }
            for (int i = 0; i < blueBubbles.Count(); i++)
            {
                if (blueBubbles[i].Y > this.Height - bubbleSize)
                {
                    blueBubbles.RemoveAt(i);
                }
            }
            for (int i = 0; i < yellowBubbles.Count(); i++)
            {
                if (yellowBubbles[i].Y > this.Height - bubbleSize)
                {
                    yellowBubbles.RemoveAt(i);
                }
            }
            for (int i = 0; i < redBubbles.Count(); i++)
            {
                if (redBubbles[i].Y > this.Height - bubbleSize)
                {
                    redBubbles.RemoveAt(i);
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
                e.Graphics.FillRectangle(whiteBrush, player);
                e.Graphics.DrawRectangle(blackPen, player);
                for (int i = 0; i < playerShots.Count; i++)
                {
                    e.Graphics.FillRectangle(purpleBrush, playerShots[i]);
                    e.Graphics.DrawEllipse(blackPen, playerShots[i]);
                }
                for (int i = 0; i < greenBubbles.Count; i++)
                {
                    e.Graphics.DrawEllipse(blackPen, greenBubbles[i]);
                    e.Graphics.FillEllipse(brush1, greenBubbles[i]);
                }
                for (int i = 0; i < redBubbles.Count; i++)
                {
                    e.Graphics.DrawEllipse(blackPen, redBubbles[i]);
                    e.Graphics.FillEllipse(brush2, redBubbles[i]);
                }
                for (int i = 0; i < blueBubbles.Count; i++)
                {
                    e.Graphics.DrawEllipse(blackPen, blueBubbles[i]);
                    e.Graphics.FillEllipse(brush3, blueBubbles[i]);
                }
                for (int i = 0; i < yellowBubbles.Count; i++)
                {
                    e.Graphics.DrawEllipse(blackPen, yellowBubbles[i]);
                    e.Graphics.FillEllipse(brush4, yellowBubbles[i]);
                }
                //for(int i = 0; i < randyellowBall; i++)
                //{

                //}
            }

        }


    }

}



