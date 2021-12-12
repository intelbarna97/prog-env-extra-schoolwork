using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Xml.Linq;

namespace basketapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static Random random = new Random();
        public static List<int> canvasNum = new List<int>();
        public static int result;
        public static int score = 0;
        public static int round = 0;
        public static string activeUser = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            Canvas1.Children.Clear();
            Canvas2.Children.Clear();
            Canvas3.Children.Clear();
            Canvas4.Children.Clear();
            Canvas5.Children.Clear();
            canvasNum.Clear();
            score = 0;
            Start_Button.Visibility = Visibility.Hidden;
            Settings_Button.Visibility = Visibility.Hidden;
            Exit_Button.Visibility = Visibility.Hidden;
            Math_label.Visibility = Visibility.Visible;
            drawPoints();
            mathLine();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            roundEnd();
        }

        private void mathLine()
        {
            int num1 = random.Next(1, 4);
            int num2 = random.Next(1, 3);
            if (((num1 + num2) < 5) && num1!=num2)
            {
                switch (random.Next(1, 4))
                {
                    case 1:
                        result = num1 + num2;
                        Math_label.Content = num1 + " + " + num2;
                        break;
                    case 2:
                        if (num1 > num2)
                        {
                            result = num1 - num2;
                            Math_label.Content = num1 + " - " + num2;
                        }
                        else
                        {
                            result = num2 - num1;
                            Math_label.Content = num2 + " - " + num1;
                        }
                        break;
                    case 3:
                        result = num1 * num2;
                        Math_label.Content = num1 + " * " + num2;
                        break;
                }
            }
            else if((num1==num2) && ((num1 + num2) < 5))
            {
                switch (random.Next(1, 3))
                {
                    case 1:
                        result = num1 + num2;
                        Math_label.Content = num1 + " + " + num2;
                        break;
                    case 2:
                            result = num1 * num2;
                            Math_label.Content = num1 + " * " + num2;
                        break;
                        
                }
            }
            else
            {
                switch (random.Next(1, 3))
                {
                    case 1:
                        result = num1 + num2;
                        Math_label.Content = num1 + " + " + num2;
                        break;
                    case 2:
                        if (num1 > num2)
                        {
                            result = num1 - num2;
                            Math_label.Content = num1 + " - " + num2;
                        }
                        else
                        {
                            result = num2 - num1;
                            Math_label.Content = num2 + " - " + num1;
                        }
                        break;
                }
            }
        }

        private void drawPoints()
        {
            canvasNum.Clear();

            List<int> num = new List<int>();
            for(int i=1;i<6;i++)
            {
                num.Add(i);
            }
            int chosen = random.Next(0, num.Count);
            pointDrawer(Canvas1, num[chosen]);
            num.RemoveAt(chosen);
            chosen = random.Next(0, num.Count);
            pointDrawer(Canvas2, num[chosen]);
            num.RemoveAt(chosen);
            chosen = random.Next(0, num.Count);
            pointDrawer(Canvas3, num[chosen]);
            num.RemoveAt(chosen);
            chosen = random.Next(0, num.Count);
            pointDrawer(Canvas4, num[chosen]);
            num.RemoveAt(chosen);
            chosen = random.Next(0, num.Count);
            pointDrawer(Canvas5, num[chosen]);
            num.RemoveAt(chosen);
        }

        private void pointDrawer(Canvas canvas, int rndnum)
        {
            canvas.Children.Clear();
            
            canvasNum.Add(rndnum);
            for (int i = 0; i < rndnum; i++)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Width = 99;
                rectangle.Height = 99;
                rectangle.Fill = new SolidColorBrush(Colors.Transparent);
                rectangle.Stroke = new SolidColorBrush(Colors.Red);
                rectangle.StrokeThickness = 1;
                canvas.Children.Add(rectangle);

                canvas.Children.Add(new Rectangle()
                {
                    Width = 15,
                    Height = 15,
                    Fill = new SolidColorBrush(Colors.Green),
                    Stroke = new SolidColorBrush(Colors.Transparent),
                    StrokeThickness = 5,
                    Margin = new Thickness(2+(i*15)+(i*4),35,0,0),
                    VerticalAlignment = VerticalAlignment.Center,
                }
                );

            }

        }

        private void Canvas1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvasClicker(0);
        }

        private void Canvas2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvasClicker(1);
        }

        private void Canvas3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvasClicker(2);

        }

        private void Canvas4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvasClicker(3);
        }

        private void Canvas5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvasClicker(4);
        }

        private void canvasClicker(int num)
        {
            if (canvasNum != null)
                if (comparer(result, canvasNum[num]))
                {
                    if (activeUser == null)
                    {
                        MessageBox.Show("Good job!");
                    }
                    else
                    {
                        MessageBox.Show("Good job " + activeUser + "!");
                    }

                    score++;

                    if(round<9)
                    {
                        drawPoints();
                        mathLine();
                        round++;
                    }
                    else
                    {
                        roundEnd();
                    }
                }
                else
                {
                    MessageBox.Show("Bad choice you potato!");
                    if(round < 10)
                    {
                        drawPoints();
                        mathLine();
                        round++;
                    }
                    else
                    {
                        roundEnd();
                    }
                }
        }

        private void roundEnd()
        {
            if(activeUser!=null)
            {
                MessageBox.Show("Round ended, " + activeUser + " scored: " + score + " points out of "+(round+1)+". Congrats!");
                scoreSerializer(activeUser);
            }
            else
            {
                MessageBox.Show("Round ended, you scored: " + score + " points out of "+(round+1)+". Congrats!");
            }
            Canvas1.Children.Clear();
            Canvas2.Children.Clear();
            Canvas3.Children.Clear();
            Canvas4.Children.Clear();
            Canvas5.Children.Clear();
            Start_Button.Visibility = Visibility.Visible;
            Settings_Button.Visibility = Visibility.Visible;
            Exit_Button.Visibility = Visibility.Visible;
            Math_label.Visibility = Visibility.Hidden;
            round = 0;
            score = 0;

        }

        public void scoreSerializer(string username)
        {
            XAttribute name = new XAttribute("username", username);
            XAttribute xscore = new XAttribute("score", score);
            XElement user = new XElement("user");
            user.ReplaceAttributes(name, xscore);
            XDocument document = XDocument.Load("entity/scores.xml");
            document.Root.Add(user);
            document.Save("entity/scores.xml");
        }

        private bool comparer(int num1, int num2)
        {
            return num1 == num2;
        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            User userWindow = new User(this);
            userWindow.Show();
            this.Hide();
        }                
    }
}
