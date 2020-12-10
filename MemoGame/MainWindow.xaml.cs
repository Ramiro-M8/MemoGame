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

namespace MemoGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer(); 
        int tenthsOfSecondsElapsed; 
        int matchesFound;
        //MAIN
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                //timeTextBlock.Text = timeTextBlock.Text + " - Volver a Jugar?";
                timeTextBlock.Text = " Tu Tiempo - "+ timeTextBlock.Text;
            }
        }
        //METODO DE INICIAR JUEGO
        private void SetUpGame()
        {
            //LIST: es una coleccion que almacena varios valores en un orden
            List<string> animalEmoji = new List<string>()
            {
                "🐐","🐐",
                "🐎","🐎",
                "🦌","🦌",
                "🐪","🐪",
                "🦙","🦙",
                "🐘","🐘",
                "🐇","🐇",
                "🦘","🦘",

            };

            Random random = new Random();

            foreach(TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
            {
                if(textblock.Name != "timeTextBlock") {  //este "IF" es necesario por que sino genera un bug
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textblock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
                }
                //El Bug se genera por que antes trabajaba con 16 TextBlocks, al añadir uno adicional,
                //rompe el programa por que hice una lista de 16 Emojis y no 17
                timer.Start(); 
                tenthsOfSecondsElapsed = 0;
                matchesFound = 0;
            }
        }

        TextBlock lastTextBlockClicked; 
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock; 
            if (findingMatch == false) { 
                textBlock.Visibility = Visibility.Hidden; 
                lastTextBlockClicked = textBlock; 
                findingMatch = true; 
            } else if (textBlock.Text == lastTextBlockClicked.Text) {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden; 
                findingMatch = false; 
            } else { 
                lastTextBlockClicked.Visibility = Visibility.Visible; 
                findingMatch = false; 
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
            if (matchesFound == 8) {
                //matchesFound = 0;
                SetUpGame(); //Resetea el juego una vez que encuentras los 8 pares
            } 
        }
    }
}
