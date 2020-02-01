namespace CourseplayEditor.Controls.Drawing.Controllers
{
    public class PointT<TValue>
    {
        public TValue X { get; set; }
        public TValue Y { get; set; }
    }

    public class PointDouble : PointT<double>
    {

    }
}
