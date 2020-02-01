using SkiaSharp;

namespace CourseEditor.Drawing.Tools
{
    public class SKPointEnentArgs :ValueEventArgs<SKPoint>
    {
        public SKPointEnentArgs(SKPoint value)
            : base(value)
        {
        }

        public SKPoint Point => Value;
    }
}
