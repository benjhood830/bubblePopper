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
        int playerSpeed = 10;

        List<Rectangle> greenBubbles = new List<Rectangle>();
        List<Rectangle> redBubbles = new List<Rectangle>();
        List<Rectangle> blueBubbles = new List<Rectangle>();
        List<Rectangle> yellowBubbles = new List<Rectangle>();
        List<Rectangle> playerShots = new List<Rectangle>();

        List<int> bubbleSpeeds = new List<int>();
        //List<string> bubbleColours = new List<string>();
        int bubbleSize = 20;
        int playershotSize = 14;

        int score = 0;

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
                ;
            player.Width = 40;
            player.Height = 10;

            gameTimer.Enabled = true;

            gameState = "running";


        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {

            int randValue = randGen.Next(0, 401);

            if (randValue < 100)
            {
                int x = randGen.Next(0, 80);
                greenBubbles.Add(new Rectangle(x, 0, bubbleSize, bubbleSize));

                bubbleSpeeds.Add(randGen.Next(1, 1));
            }

            int playershotX = player.X;
            int playershotY = player.Y;

            if(spaceDown == true && gameState == "running")
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
                int y = greenBubbles[i].Y + bubbleSpeeds[i];

                //replace the rectangle in the list with updated one using new y 
                greenBubbles[i] = new Rectangle(greenBubbles[i].X, y, bubbleSize, bubbleSize);
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
                for(int i = 0; i < greenBubbles.Count; i++)
                {
                    e.Graphics.DrawEllipse(blackPen, greenBubbles[i]);
                    e.Graphics.FillEllipse(greenBrush, greenBubbles[i]);
                }
                
                }

            }


        }

    }



