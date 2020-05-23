using SortingVisualizer.Animations;

namespace SortingVisualizer.Algorithms
{
    class QuickSort : Algorithm
    {
        public override void Sort()
        {
            animation = new Animation();

            QuickSortRecursive(0, ArrayLength - 1);

            OnArraySorted(animation);
        }

        private void QuickSortRecursive(int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(low, high);

                QuickSortRecursive(low, partitionIndex - 1);
                QuickSortRecursive(partitionIndex + 1, high);
            }
        }

        private int Partition(int low, int high)
        {
            int pivot = Array[high];
            int lowIndex = (low - 1);

            for (int j = low; j < high; ++j)
            {
                animation.frames.Add(new AnimationFrame(FrameType.Comparison, j, high));
                if (Array[j] <= pivot)
                {
                    ++lowIndex;

                    int temp = Array[lowIndex];
                    Array[lowIndex] = Array[j];
                    Array[j] = temp;
                    animation.frames.Add(new AnimationFrame(FrameType.Swap, j, lowIndex));
                }
            }

            int temp1 = Array[lowIndex + 1];
            Array[lowIndex + 1] = Array[high];
            Array[high] = temp1;
            animation.frames.Add(new AnimationFrame(FrameType.Swap, high, lowIndex + 1));

            return lowIndex + 1;
        }
    }
}