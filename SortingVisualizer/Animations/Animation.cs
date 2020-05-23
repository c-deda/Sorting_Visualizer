using SortingVisualizer.Algorithms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SortingVisualizer.Animations
{
    public class Animation
    {
        private const int DefaultSpeed = 1;

        public static int Speed = DefaultSpeed;
        public static bool isStopped;

        public List<AnimationFrame> frames { get; }

        public Animation()
        {
            frames = new List<AnimationFrame>();
        }

        public static void ChangeSpeed(int newSpeed)
        {
            Speed = newSpeed;
        }

        public async void Play(Canvas Canvas)
        {
            isStopped = false;

            int lastComparison = -1;
            int lastMerge = -1;

            for (int i = 0; i < frames.Count && !isStopped; ++i)
            {
                switch (frames[i].type)
                {
                    case FrameType.Comparison:
                        Comparison(Canvas, i, lastComparison);
                        lastComparison = i;
                        if (i % Speed == 0) { await Task.Delay(1); }
                        break;
                    case FrameType.Swap:
                        Swap(Canvas, i);
                        if (i % Speed == 0) { await Task.Delay(1); }
                        break;
                    case FrameType.Merge:
                        Merge(Canvas, i, lastMerge);
                        lastMerge = i;
                        if (i % Speed == 0) { await Task.Delay(1); }
                        break;
                }
            }

            if (!isStopped)
            {
                SetAllColor(Canvas, Brushes.Green);
            }

            frames.Clear();
        }

        private void Comparison(Canvas Canvas, int i, int lastComparison)
        {
            if (lastComparison >= 0)
            {
                Canvas.Children[frames[lastComparison].value1].SetValue(Shape.FillProperty, Brushes.Black);
                Canvas.Children[frames[lastComparison].value2].SetValue(Shape.FillProperty, Brushes.Black);
            }

            Canvas.Children[frames[i].value1].SetValue(Shape.FillProperty, Brushes.Red);
            Canvas.Children[frames[i].value2].SetValue(Shape.FillProperty, Brushes.Red);
        }

        private void Swap(Canvas Canvas, int i)
        {
            double height1 = (double)Canvas.Children[frames[i].value1].GetValue(FrameworkElement.HeightProperty);
            double height2 = (double)Canvas.Children[frames[i].value2].GetValue(FrameworkElement.HeightProperty);

            Canvas.Children[frames[i].value1].SetValue(FrameworkElement.HeightProperty, height2);
            Canvas.Children[frames[i].value2].SetValue(FrameworkElement.HeightProperty, height1);
        }

        private void Merge(Canvas Canvas, int i, int lastMerge)
        {
            if (lastMerge >= 0)
            {
                Canvas.Children[frames[lastMerge].value1].SetValue(Shape.FillProperty, Brushes.Black);
            }

            double newHeight = (frames[i].value2 / (double)Algorithm.MaxInt) * Canvas.ActualHeight;
            Canvas.Children[frames[i].value1].SetValue(FrameworkElement.HeightProperty, newHeight);

            Canvas.Children[frames[i].value1].SetValue(Shape.FillProperty, Brushes.Red);
        }

        private void SetAllColor(Canvas Canvas, SolidColorBrush color)
        {
            for (int i = 0; i < Algorithm.ArrayLength; ++i)
            {
                Canvas.Children[i].SetValue(Shape.FillProperty, color);
            }
        }
    }
}