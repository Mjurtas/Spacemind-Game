namespace SUP_G6.Other
{
    internal class Secret
    {
        public Secret(int color, int index)
        {
            Color = color;
            Index = index;
        }

        public int Color { get; set; }
        public int Index { get; set; }
        public bool IsHandled { get; internal set; }
    }
}