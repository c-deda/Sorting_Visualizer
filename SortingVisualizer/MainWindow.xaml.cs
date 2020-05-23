using SortingVisualizer.Algorithms;
using SortingVisualizer.Animations;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SortingVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Algorithm _selectedAlgorithm;
        private string _selectedArrayType;

        public ObservableCollection<Algorithm> Algorithms { get; set; } = new ObservableCollection<Algorithm>();
        public ObservableCollection<string> ArrayTypes { get; set; } = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Algorithms.Add(new BubbleSort() { Name = "Bubble Sort" });
            Algorithms.Add(new SelectionSort() { Name = "Selection Sort" });
            Algorithms.Add(new InsertionSort() { Name = "Insertion Sort" });
            Algorithms.Add(new MergeSort() { Name = "Merge Sort" });
            Algorithms.Add(new QuickSort() { Name = "Quick Sort" });
            Algorithms.Add(new ShellSort() { Name = "Shell Sort" });

            ArrayTypes.Add("Random");
            ArrayTypes.Add("Few Unique");
            ArrayTypes.Add("Reversed");
            ArrayTypes.Add("Almost Sorted");

            Algorithm.ArrayGenerated += OnArrayGenerated;
            Algorithm.ArraySorted += OnArraySorted;
        }

        private void AlgorithmsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedAlgorithm = (Algorithm)AlgorithmsComboBox.SelectedItem;
            Generate();
        }

        private void ArrayTypesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedArrayType = (string)ArrayTypesComboBox.SelectedItem;
            Generate();
        }

        private void Generate()
        {
            _selectedAlgorithm.GenerateArray(_selectedArrayType);
            Animation.isStopped = true;
            SortButton.IsEnabled = true;
            ResizeMode = ResizeMode.CanResize;
        }
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            Generate();
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedAlgorithm.Sort();
            SortButton.IsEnabled = false;
            ResizeMode = ResizeMode.NoResize;
        }

        private void AdjustSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Animation.ChangeSpeed((int)e.NewValue);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeRectangles();
        }

        private void OnArrayGenerated(object sender, EventArgs e)
        {
            DrawArray();
        }

        private void OnArraySorted(object sender, SortEventArgs e)
        {
            e.Animation.Play(ArrayCanvas);
        }

        private void DrawArray()
        {
            ArrayCanvas.Children.Clear();

            double rectWidth = (ArrayCanvas.ActualWidth / Algorithm.ArrayLength);

            for (int i = 0; i < Algorithm.ArrayLength; ++i)
            {
                Rectangle newRect = new Rectangle()
                {
                    Fill = Brushes.Black,
                    StrokeThickness = 4,
                    Width = rectWidth,
                    Height = (Algorithm.Array[i] / (double)Algorithm.MaxInt) * ArrayCanvas.ActualHeight
                };
                Canvas.SetLeft(newRect, (rectWidth * i));
                Canvas.SetBottom(newRect, 0);

                ArrayCanvas.Children.Add(newRect);
            }
        }

        private void ResizeRectangles()
        {
            double newWidth = (ArrayCanvas.ActualWidth / Algorithm.ArrayLength);

            for (int i = 0; i < ArrayCanvas.Children.Count; ++i)
            {
                double newHeight = (Algorithm.Array[i] / (double)Algorithm.MaxInt) * ArrayCanvas.ActualHeight;

                ArrayCanvas.Children[i].SetValue(WidthProperty, newWidth);
                ArrayCanvas.Children[i].SetValue(Shape.HeightProperty, newHeight);
                Canvas.SetLeft(ArrayCanvas.Children[i], (newWidth * i));
            }
        }
    }
}
