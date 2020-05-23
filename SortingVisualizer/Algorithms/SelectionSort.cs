using SortingVisualizer.Animations;

namespace SortingVisualizer.Algorithms
{
    public class SelectionSort : Algorithm
    {
        public override void Sort()
        {
            animation = new Animation();

            for (int i = 0; i < ArrayLength; ++i)
            {
                int min = i;

                for (int j = i; j < ArrayLength; ++j)
                {
                    animation.frames.Add(new AnimationFrame(FrameType.Comparison, j, min));
                    if (Array[j] < Array[min])
                    {
                        min = j;
                    }
                }

                int temp = Array[i];
                Array[i] = Array[min];
                Array[min] = temp;

                animation.frames.Add(new AnimationFrame(FrameType.Swap, i, min));
            }

            OnArraySorted(animation);
        }
    }
}
