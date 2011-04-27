using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace BinaryClock
{
    public partial class MainPage : PhoneApplicationPage
    {

        public MainPage()
        {
            InitializeComponent();

            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;
            SizeChanged += new SizeChangedEventHandler(BinaryClock_SizeChanged);
            Loaded += new RoutedEventHandler(BinaryClock_Loaded);
        }


        void BinaryClock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Draw();
        }

        void BinaryClock_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
        }

        void Draw()
        {
            ContentCanvas.Children.Clear();

            DateTime time = System.DateTime.Now;

            int appletHeight = (int) ContentCanvas.ActualHeight;
            int appletWidth = (int) ContentCanvas.ActualWidth;
            int dots = 6;

            int rectSize = Math.Max(1, Math.Min((appletHeight - 3) / 4, (appletWidth - 3) / dots));

            int yPos = ((appletHeight - 4 * rectSize) / 2) + (int) ContentCanvas.MaxHeight;
            int xPos = ((appletWidth - (rectSize * dots) - 5) / 2) + (int)ContentCanvas.MinWidth;

            if (yPos < 0)
            {
                yPos = 0;
            }


            int[] timeDigits = { time.Hour / 10, time.Hour % 10, time.Minute / 10, time.Minute % 10, time.Second / 10, time.Second % 10 };

            for (int i = 0; i < dots; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((timeDigits[i] & (1 << (3 - j))) != 0)
                    {
                        Rectangle rec = new Rectangle();
                        rec.Width = rectSize;
                        rec.Height = rectSize;
                        rec.Stroke = new SolidColorBrush(Colors.White);
                        rec.Fill = new SolidColorBrush(Colors.White);

                        Canvas.SetLeft(rec, xPos + (i * (rectSize + 1)));
                        Canvas.SetTop(rec, yPos + (j * (rectSize + 1)));

                        ContentCanvas.Children.Add(rec);
                    }
                }
            }
            storyboard.Begin();
        }

        protected void storyboard_Completed(object sender, EventArgs e)
        {
            Draw();
        }
    }
}

