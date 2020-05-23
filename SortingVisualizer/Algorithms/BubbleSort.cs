using SortingVisualizer.Animations;

namespace SortingVisualizer.Algorithms
{
    public class BubbleSort : Algorithm
    {
        public override void Sort()
        {
            animation = new Animation();

            for (int i = 0; i < ArrayLength; ++i)
            {
                for (int j = 1; j < ArrayLength - i; ++j)
                {
                    animation.frames.Add(new AnimationFrame(FrameType.Comparison, j, j - 1));
                    if (Array[j] < Array[j - 1])
                    {
                        int temp = Array[j];
                        Array[j] = Array[j - 1];
                        Array[j - 1] = temp;
                        animation.frames.Add(new AnimationFrame(FrameType.Swap, j, j - 1));
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            OnArraySorted(animation);
        }
    }
}
