using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lab1_Game
{
    public abstract class Animal
    {
        protected Canvas mainCanvas;
        protected Image image;
        protected float speed;
        protected double width;
        protected double height;
        protected Vector2 position;
        public Animal()
        {
            
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public double Width
        {
            get { return width; }
            set { width = value; }
        }
        public double Height
        {
            get { return height; }
            set { height = value; }
        }
        public void SetPostion(Vector2 position)
        {
            this.position = position;
        }
        public virtual void ChangePosition(float X = 0, float Y = 0)
        {
            this.position.X += X;
            this.position.Y += Y;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public abstract void Update();
        public abstract void Move();
        public abstract bool CheckPlayer(Animal player);
        
    }
    public class Tiger : Animal
    {
        private float speedX;
        private float speedY;
        private bool isPlayerClose=false;
        private Vector2 playerPosition;
        private float chasingDistance = 128;
        private float killingDistance = 64;
        private float distanceToPlyaer;
        static private Random random = new Random();
        public Tiger() { }
        public Tiger(Canvas canvas, Image image, Vector2 position) 
        {
            this.mainCanvas = canvas;
            this.image = image;
            speed=1.5f;
            this.position=position;
            speedX = 1.5f;
            speedY = 1.5f;
            random.Next(100);
        }
        public override void Update() 
        {
            if (!isPlayerClose)
            {
                int direction = random.Next(100);
                // Randomly choose the amount to move

                if (direction == 0)
                {
                    speedX = -speedX;
                }
                if (direction == 99)
                {
                    speedY = -speedY;
                }
                position.X += speedX;
                position.Y += speedY;
               
            }
            else
            {
                Vector2 directionToPlayer = playerPosition - position;

                float magnitude = (float)Math.Sqrt(directionToPlayer.X * directionToPlayer.X + directionToPlayer.Y * directionToPlayer.Y);

                if (magnitude > 0)
                {
                    directionToPlayer.X /= magnitude;
                    directionToPlayer.Y /= magnitude;
                }

                position.X += directionToPlayer.X * speed * 1.5f;
                position.Y += directionToPlayer.Y * speed * 1.5f;

            }
            ClampPosition();
            Move();
            
            
        }
        public override void Move() 
        {
            Canvas.SetLeft(image, position.X);
            Collide("X");

            Canvas.SetTop(image, position.Y);
        }
        private void Collide(string dir)
        {

        }
        public void ClampPosition()
        {
            // Ensure the Tiger stays within the window boundaries
            if (position.X < 0)
                position.X = 0;
            if (position.X > (800 - image.Width))
                position.X = (float)(800 - image.Width);

            if (position.Y < 0)
                position.Y = 0;
            if (position.Y > (600 - image.Height))
                position.Y = (float)(600 - image.Height);
            
        }
        public override bool CheckPlayer(Animal player)
        {
            playerPosition = player.GetPosition();
            distanceToPlyaer = (float)Math.Sqrt(Math.Pow((playerPosition.X - position.X), 2) + Math.Pow((playerPosition.Y - position.Y), 2));
            if (distanceToPlyaer < chasingDistance)
            {
                isPlayerClose = true;
            }
            else
            {
                isPlayerClose = false;
            }
            if (distanceToPlyaer<=killingDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class Deer : Animal
    {
        public Deer(Canvas canvas,Image image, Vector2 position)
        {
            mainCanvas = canvas;
            this.image = image;
            speed = 3.2f;
            this.position = position;
            width = image.Width;
            height = image.Height;
        }
        public override void Update()
        {
            Move();
        }
        public override void Move()
        {
            Canvas.SetLeft(image, position.X);
            Canvas.SetTop(image, position.Y);
        }
        

        public override void ChangePosition(float X = 0, float Y = 0)
        {
            this.position.X += X;
            this.position.Y += Y;
        }
        public override bool CheckPlayer(Animal player)
        {
            throw new NotImplementedException();
        }
    }
    public abstract class Food
    {
        static private Random random = new Random();
        protected int score;
        protected int parobability=500;
        protected Image image;
        protected Vector2 position;
        protected bool isEaten=true;

        public Food(Image image)
        {
            this.image = image;
            SetPostion(new Vector2(-100, -100));
            UpdatePosition();
        }
        public void SetPostion(Vector2 position)
        {
            this.position = position;
        }
        public virtual void ChangePosition(float X = 0, float Y = 0)
        {
            this.position.X += X;
            this.position.Y += Y;
        }
        public virtual void UpdatePosition()
        {
            Canvas.SetLeft(image, position.X);
            Canvas.SetTop(image, position.Y);
        }
        public bool IsEaten()
        {
            return isEaten;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public virtual void Update()
        {
            if (isEaten)
            {
                int value = random.Next(parobability);
                if (value == 0)
                {
                    position.X = random.Next(750);
                    position.Y = random.Next(550);
                    UpdatePosition();
                    isEaten= false;
                }
            }
        }
        public virtual int CollidePlayer(Animal player)
        {
            var playerX = player.GetPosition().X;
            var playerY = player.GetPosition().Y;
            Rect playerRect = new Rect(playerX, playerY, player.Width, player.Height);
            Rect foodRect = new Rect(position.X, position.Y, image.Width, image.Height);
            if (playerRect.IntersectsWith(foodRect))
            {
                isEaten = true;
                SetPostion(new Vector2(-100,-100));
                UpdatePosition();
                return score;
            }
            return 0;
        }
    }
    public class Cabbage : Food
    {
        public Cabbage(Image image):base(image)
        {
            score = 300;
        }
    }
    public class Berry : Food
    {
        public Berry(Image image) : base(image)
        {
            parobability = 2000;
            score = 500;
        }

    }
    public abstract class AnimalFactory
    {
        public abstract Animal CreateAnimal(Canvas canvas, Image image, Vector2 position);
    }
    public class TigerFactory : AnimalFactory
    {
        public override Animal CreateAnimal(Canvas canvas, Image image, Vector2 position)
        {
            return new Tiger(canvas, image, position);
        }
    }
    public class DeerFactory : AnimalFactory
    {
        public override Animal CreateAnimal(Canvas canvas, Image image, Vector2 position)
        {
            return new Deer(canvas, image, position);
        }
    }
    public abstract class FoodFactory
    {
        public abstract Food CreateFood(Image image);
    }
    public class CabbageFactory: FoodFactory
    {
        public override Food CreateFood(Image image)
        { 
            return new Cabbage(image); 
        }
    }
    public class BerryFactory : FoodFactory
    {
        public override Food CreateFood(Image image)
        {
            return new Berry(image);
        }
    }

    public partial class MainWindow : Window
    {
        private Random random = new Random();
        private DataBase db = new DataBase();
        private string playerName;
        private PauseWindow pauseWindow;
        private EndGameWindow endGameWindow;
        private DispatcherTimer gameTimer = new DispatcherTimer();
        private DispatcherTimer dbTimer = new DispatcherTimer();
        private bool leftKeyPressed, rightKeyPressed, upKeyPressed, downKeyPressed;
        private AnimalFactory tigerFactory = new TigerFactory();
        private AnimalFactory deerFactory = new DeerFactory();
        private FoodFactory cabbageFactory = new CabbageFactory();
        private FoodFactory berryFactory = new BerryFactory();
        private List<Animal> tigers = new List<Animal>();
        private List<Food> cabbages = new List<Food>();
        private List<Food> berries = new List<Food>();
        private Animal player;
        private int score=0;
        private int cabbageCount = 3;
        private bool isEscape = false;
        public MainWindow(string Name)
        {
            playerName = Name;
            InitializeComponent();
            pauseWindow = new PauseWindow(this);
            endGameWindow = new EndGameWindow(this);
            SetUp();
        }
        private void SetUp()
        {
            ScoreLabel.Content= "Score: " + score.ToString();
            
            db.InsertPlayer(playerName, score);
            foreach (var image in MyCanvas.Children.OfType<Image>())
            {
                if ((string)image.Tag=="Tiger")
                {
                    tigers.Add(tigerFactory.CreateAnimal(MyCanvas,image, new Vector2((float)Canvas.GetLeft(image), (float)Canvas.GetTop(image))));
                }
                else if ((string)image.Tag == "Cabbage")
                {
                    cabbages.Add(cabbageFactory.CreateFood(image));
                }
                else if ((string)image.Tag == "Berry")
                {
                    berries.Add(berryFactory.CreateFood(image));
                }
            }
            player = deerFactory.CreateAnimal(MyCanvas,Animal, new Vector2((float)Canvas.GetLeft(Animal), (float)Canvas.GetTop(Animal)));
            MyCanvas.Focus();
            
            gameTimer.Interval = TimeSpan.FromMilliseconds(16);
            gameTimer.Tick += GameTick;
            gameTimer.Start();

            dbTimer.Interval = TimeSpan.FromSeconds(1);
            dbTimer.Tick += UpdatePlayers;
            dbTimer.Start();
        }
        private void GameTick(object sender, EventArgs e)
        {   
            if (leftKeyPressed)
            {
                player.ChangePosition(-player.Speed);
            }
            if (rightKeyPressed)
            {
                player.ChangePosition(player.Speed);
            }
            if (upKeyPressed)
            {
                player.ChangePosition(0, -player.Speed);
            }
            if (downKeyPressed)
            {
                player.ChangePosition(0, player.Speed);
            }
            player.Update();
            foreach (var tiger in tigers)
            {
                if (tiger.CheckPlayer(player))
                {
                    EndGame();
                    return;
                }
                tiger.Update();
                
            }
            foreach (var cabbage in cabbages)
            {
                cabbage.Update();
                score += cabbage.CollidePlayer(player);
            }
            foreach (var berry in berries)
            {
                berry.Update();
                score += berry.CollidePlayer(player);
            }
            score += 1;
            ScoreLabel.Content = "Score: " + (score/10).ToString();
        }
        private async void UpdatePlayers(object sender, EventArgs e)
        {
            await db.EditPlayer(playerName, score/10);
            var players = await db.GetPlayers();
            players = players.OrderByDescending(player => player["score"]).ToList();

            LeaderBoardText.Text = "Leaderboard:\n";
            foreach (var item in players)
            {
                LeaderBoardText.Text += $"{item["name"]}: {item["score"]}\n";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            gameTimer.Stop();
            db.DeletePlayer(playerName);
            dbTimer.Stop();
            pauseWindow.Close();
            endGameWindow.Close();
        }

        private void EndGame()
        {
            gameTimer.Stop();
            score /= 10;
            ScoreLabel.Content = "Score: " + score.ToString();
            endGameWindow.PutScore(score);
            db.DeletePlayer(playerName);
            dbTimer.Stop();
            endGameWindow.Show();
            endGameWindow.RestartBtn.Focus();
            /* MessageBox.Show("Game Over! The tiger caught you.\nYour score: "+score.ToString());
             MessageBoxResult result = MessageBox.Show("Do you want to restart the game?", "Game Over", MessageBoxButton.YesNo);

             if (result == MessageBoxResult.Yes)
             {
                 MainWindow mainWindow = new MainWindow();
                 mainWindow.Show();
                 Close();
             }
             else
             {
                 Close();
             }*/
        }
        private void KeyBoardDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                leftKeyPressed = true;
            }
            if (e.Key == Key.D)
            {
                rightKeyPressed = true;
            }
            if (e.Key == Key.W)
            {
                upKeyPressed = true;
            }
            if (e.Key == Key.S)
            {
                downKeyPressed = true;
            }
            if (e.Key == Key.Escape)
            {
                if (!isEscape)
                {
                    gameTimer.Stop();
                    pauseWindow.Show();
                    e.Handled = true;
                    pauseWindow.ContinueBtn.Focus();
                }
                else
                {
                    pauseWindow.Hide();
                    gameTimer.Start();
                }
                isEscape =!isEscape;
            }
        }
        public void InsertPlayer()
        {
            db.InsertPlayer(playerName, score);
        }
        private void KeyBoardUp(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.A)
            {
                leftKeyPressed = false;
            }
            if (e.Key == Key.D)
            {
                rightKeyPressed = false;
            }
            if (e.Key == Key.W)
            {
                upKeyPressed = false;
            }
            if (e.Key == Key.S)
            {
                downKeyPressed = false;
            }
        }
        public void ResumeGame() 
        {
            isEscape = false;
            MyCanvas.Focus();
            gameTimer.Start();
        }
        public void RestartGame()
        {
            gameTimer.Stop();
            db.DeletePlayer(playerName);
            dbTimer.Stop();
            MainWindow mainWindow = new MainWindow(playerName);
            mainWindow.Show();
            Close();
            mainWindow.InsertPlayer();
        }
        public void Exit()
        {
            gameTimer.Stop();
            db.DeletePlayer(playerName);
            dbTimer.Stop();
            Close();
        }
    }
}
