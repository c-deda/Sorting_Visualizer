using SortingVisualizer.Animations;

namespace SortingVisualizer.Algorithms
{
    public class MergeSort : Algorithm
    {
        public override void Sort()
        {
            animation = new Animation();

            MergeSortRecursive(0, ArrayLength - 1);

            OnArraySorted(animation);
        }

        private void MergeSortRecursive(int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSortRecursive(left, middle);
                MergeSortRecursive(middle + 1, right);

                Merge(left, middle, right);
            }
        }

        private void Merge(int left, int middle, int right)
        {
            int[] leftArray = new int[middle - left + 1];
            int[] rightArray = new int[right - middle];

            System.Array.Copy(Array, left, leftArray, 0, middle - left + 1);
            System.Array.Copy(Array, middle + 1, rightArray, 0, right - middle);

            int i = 0;
            int j = 0;

            for (int k = left; k < right + 1; ++k)
            {
                if (i == leftArray.Length)
                {
                    Array[k] = rightArray[j];
                    animation.frames.Add(new AnimationFrame(FrameType.Merge, k, rightArray[j]));
                    ++j;
                }
                else if (j == rightArray.Length)
                {
                    Array[k] = leftArray[i];
                    animation.frames.Add(new AnimationFrame(FrameType.Merge, k, leftArray[i]));
                    ++i;
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    Array[k] = leftArray[i];
                    animation.frames.Add(new AnimationFrame(FrameType.Merge, k, leftArray[i]));
                    ++i;
                }
                else
                {
                    Array[k] = rightArray[j];
                    animation.frames.Add(new AnimationFrame(FrameType.Merge, k, rightArray[j]));
                    ++j;
                }
            }
        }
    }
}