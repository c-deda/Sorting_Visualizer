namespace SortingVisualizer.Animations
{
    public class AnimationFrame
    {
        public FrameType type { get; private set; }
        public int value1 { get; private set; }
        public int value2 { get; private set; }

        public AnimationFrame(FrameType type, int value1, int value2)
        {
            this.type = type;
            this.value1 = value1;
            this.value2 = value2;
        }
    }

    public enum FrameType
    {
        Comparison,
        Swap,
        Merge
    }
}
