using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Graphics
{
    /// <summary>
    /// Interaction logic for PaintWindow.xaml
    /// </summary>
    public partial class PaintWindow : Window
    {
        enum Viewing { Nothing, Shape, Image, Line};
        private Viewing viewing = Viewing.Nothing;
        private SolidColorBrush lastBrush = null;
        private TheShape lastShape;
        private bool lastIsFilled;
        private System.Windows.Controls.Image lastImage = null;

        private DashStyle[] dashStyles = new DashStyle[] { DashStyles.Solid, DashStyles.Dot, DashStyles.Dash, DashStyles.DashDot, DashStyles.DashDotDot };

        public PaintWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Draw lines in each possible dast sytle.
        /// </summary>
        /// <param name="brush">The brush to use to draw the lines.</param>
        public void DrawLines(SolidColorBrush brush)
        {
            if (brush is null)
                throw new ArgumentNullException(nameof(brush));

            Canvas.Children.Clear(); 
            System.Windows.Point start = new System.Windows.Point(0, 0);
            double distance = Height / (dashStyles.Length + 1);
            System.Windows.Point end = new System.Windows.Point(Width, start.Y);
            foreach (DashStyle dashStyle in dashStyles)
            {
                start = new System.Windows.Point(start.X, start.Y + distance);
                end = new System.Windows.Point(end.X, start.Y);
                DrawLine(brush, ref start, ref end, dashStyle);
            }

            lastBrush = brush;
            viewing = Viewing.Line;
        }

        /// <summary>
        /// Draw shapes using each dash style as the black border.
        /// </summary>
        /// <param name="brush">The brush to use for each square.</param>
        public void DrawShapes(SolidColorBrush brush, TheShape shape, bool isFilled, BitmapImage bitmapImage = null)
        {
            Canvas.Children.Clear();
            double vertical = Height / 2;
            double height = vertical - 10;
            double horizontal = Width / (dashStyles.Length + 1);
            double width = horizontal - 10;
            double top = 5;
            double left = 5;
            double bottom = top + height;
            double right = left + width;
            foreach (DashStyle dashStyle in dashStyles)
            {
                switch (shape)
                {
                    case TheShape.Square:
                        DrawShape(new System.Windows.Shapes.Rectangle(), brush, top, left, bottom, right, dashStyle, isFilled, bitmapImage);
                        break;
                    case TheShape.Circle:
                        DrawShape(new Ellipse(), brush, top, left, bottom, right, dashStyle, isFilled, bitmapImage);
                        break;
                    default:
                        break;
                }
                left += horizontal;
                right = left + width;
            }

            lastBrush = brush;
            lastShape = shape;
            lastIsFilled = isFilled;
            viewing = Viewing.Shape;
        }

        /// <summary>
        /// Draw a shape with the given properties.
        /// </summary>
        /// <param name="brush">The brush to use to fill the shape.</param>
        /// <param name="top">The location of the top of the shape.</param>
        /// <param name="left">The location of the left side of the shape.</param>
        /// <param name="bottom">The location of the bottom of the shape.</param>
        /// <param name="right">The location of the right side of the shape.</param>
        /// <param name="dashStyle">The dash style of the black border.</param>
        /// <param name="isFilled">If true, fill with the brush; otherwise outline dashes with the brush.</param>
        private void DrawShape(Shape shape, SolidColorBrush brush, double top, double left, double bottom, double right, DashStyle dashStyle, bool isFilled, BitmapImage bitmapImage)
        {
            if (shape is null)
                throw new ArgumentNullException(nameof(shape));
            if (top < 0)
                throw new ArgumentOutOfRangeException(nameof(top), top, "Must be non-negstive");
            if (left < 0)
                throw new ArgumentOutOfRangeException(nameof(left), left, "Must be non-negstive");
            if (bottom < 0)
                throw new ArgumentOutOfRangeException(nameof(bottom), bottom, "Must be non-negstive");
            if (right < 0)
                throw new ArgumentOutOfRangeException(nameof(right), right, "Must be non-negstive");
            if (right <= left)
                throw new ArgumentException("Width must be positive");
            if (bottom <= top)
                throw new ArgumentException("Height must be positive");

            shape.Width = right - left;
            shape.Height = bottom - top;
            if (isFilled)
                shape.Fill = brush;
            else if (bitmapImage != null)
                shape.Fill = new ImageBrush(bitmapImage);
            else
            {
                shape.Stroke = brush;
                shape.StrokeThickness = 1;
                shape.StrokeDashArray = dashStyle.Dashes;
                shape.StrokeDashCap = PenLineCap.Round;
            }

            Canvas.Children.Add(shape);
            Canvas.SetLeft(shape, left);
            Canvas.SetTop(shape, top);
        }

        /// <summary>
        /// Draw the image on the canvas.
        /// </summary>
        /// <param name="image">The image to draw.</param>
        public void DrawImage(System.Windows.Controls.Image image)
        {
            if (image is null)
                throw new ArgumentNullException(nameof(image));

            image.Height = Height;
            image.Width = Width;
            Canvas.Children.Clear();
            Canvas.Children.Add(image);
            lastImage = image;
            viewing = Viewing.Image;
        }

        /// <summary>
        /// Draw a line.
        /// </summary>
        /// <param name="brush">The brush to use for the line.</param>
        /// <param name="start">The starting point for the line.</param>
        /// <param name="end">The ending point of the line.</param>
        /// <param name="dashStyle">The dash style for the line.</param>
        private void DrawLine(SolidColorBrush brush, ref System.Windows.Point start, ref System.Windows.Point end, DashStyle dashStyle)
        {
            Line line = new Line()
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                SnapsToDevicePixels = true,
                Stroke = brush,
                StrokeThickness = 5,
                StrokeDashArray = dashStyle.Dashes,
                StrokeDashCap = PenLineCap.Round,
            };
            Canvas.Children.Add(line);
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            switch (viewing)
            {
                case Viewing.Nothing:
                    break;
                case Viewing.Line:
                    DrawLines(lastBrush);
                    break;
                case Viewing.Shape:
                    DrawShapes(lastBrush, lastShape, lastIsFilled);
                    break;
                case Viewing.Image:
                    DrawImage(lastImage);
                    break;
                default:
                    Debug.Fail("Unexpected Viewing value");
                    break;
            }
        }
    }
}
