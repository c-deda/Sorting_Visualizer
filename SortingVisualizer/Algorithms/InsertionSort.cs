using SortingVisualizer.Animations;

namespace SortingVisualizer.Algorithms
{
    public class InsertionSort : Algorithm
    {
        public override void Sort()
        {
            animation = new Animation();

            for (int i = 0; i < ArrayLength - 1; ++i)
            {
                int j = i + 1;

                while (j > 0)
                {
                    animation.frames.Add(new AnimationFrame(FrameType.Comparison, j - 1, j));
                    if (Array[j - 1] > Array[j])
                    {
                        int temp = Array[j - 1];
                        Array[j - 1] = Array[j];
                        Array[j] = temp;
                        animation.frames.Add(new AnimationFrame(FrameType.Swap, j, j - 1));
                        --j;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            OnArraySorted(animation);
        }
    }
}
