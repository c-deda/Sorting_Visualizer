using SortingVisualizer.Animations;

namespace SortingVisualizer.Algorithms
{
    class ShellSort : Algorithm
    {
        public override void Sort()
        {
            animation = new Animation();

            for (int gap = ArrayLength / 2; gap > 0; gap /= 2)
            {
                for (int i = 0; i < ArrayLength - gap; ++i)
                {
                    int j = i + gap;

                    while (j >= gap)
                    {
                        animation.frames.Add(new AnimationFrame(FrameType.Comparison, j - gap, j));
                        if (Array[j - gap] > Array[j])
                        {
                            int temp = Array[j - gap];
                            Array[j - gap] = Array[j];
                            Array[j] = temp;
                            animation.frames.Add(new AnimationFrame(FrameType.Swap, j, j - gap));
                            j = j - gap;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            OnArraySorted(animation);
        }
    }
}
