using SortingVisualizer.Animations;
using System;

namespace SortingVisualizer.Algorithms
{
    public abstract class Algorithm
    {
        private const int MinInt = 1;
        public const int MaxInt = 1000;
        private const int UniqueValues = 5;
        public const int ArrayLength = 100;

        public string Name { get; set; }

        public static int[] Array = new int[ArrayLength];
        public Animation animation { get; set; }

        public static event EventHandler<EventArgs> ArrayGenerated;
        public static event EventHandler<SortEventArgs> ArraySorted;


        public void GenerateArray(string arrayType)
        {
            switch (arrayType)
            {
                case "Random":
                    GenerateRandom();
                    break;
                case "Few Unique":
                    GenerateFewUnique();
                    break;
                case "Reversed":
                    GenerateReversed();
                    break;
                case "Almost Sorted":
                    GenerateAlmostSorted();
                    break;
            }

            OnArrayGenerated();
        }



        private void GenerateRandom()
        {
            Random random = new Random();

            for (int i = 0; i < ArrayLength; ++i)
            {
                Array[i] = random.Next(MinInt, MaxInt);
            }
        }

        private void GenerateFewUnique()
        {
            Random random = new Random();
            int randInt = 0;

            // Generate Array With Few Unique Values
            for (int i = 0; i < ArrayLength; ++i)
            {
                if (i % (ArrayLength / UniqueValues) == 0)
                {
                    randInt = random.Next(MinInt, MaxInt);
                }

                Array[i] = randInt;
            }

            // Shuffle Values
            for (int i = 0; i < ArrayLength; ++i)
            {
                randInt = random.Next(ArrayLength);
                int temp = Array[i];
                Array[i] = Array[randInt];
                Array[randInt] = temp;
            }
        }

        private void GenerateReversed()
        {
            GenerateRandom();
            System.Array.Sort(Array);
            System.Array.Reverse(Array);
        }

        private void GenerateAlmostSorted()
        {
            // Generate New Array And Sort It
            GenerateRandom();
            System.Array.Sort(Array);

            // Pick A Random Index That Isn't The Last Index
            Random random = new Random();
            int randIndex = random.Next(ArrayLength - 2);

            // Swap Value At Random Index With Last Index
            int temp = Array[ArrayLength - 1];
            Array[ArrayLength - 1] = Array[randIndex];
            Array[randIndex] = temp;
        }

        public abstract void Sort();

        protected virtual void OnArrayGenerated()
        {
            if (ArrayGenerated != null)
            {
                ArrayGenerated(this, new EventArgs() { });
            }
        }

        protected virtual void OnArraySorted(Animation animation)
        {
            if (ArraySorted != null)
            {
                ArraySorted(this, new SortEventArgs() { Animation = animation });
            }
        }
    }

    public class SortEventArgs : EventArgs
    {
        public Animation Animation { get; set; }
    }
}