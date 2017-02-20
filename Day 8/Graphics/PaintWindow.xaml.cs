using System;
using System.Collections.Generic;
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
        }

        /// <summary>
        /// Draw shapes using each dash style as the black border.
        /// </summary>
        /// <param name="brush">The brush to use for each square.</param>
        public void DrawShapes(SolidColorBrush brush, TheShape shape, bool isFilled)
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
                        DrawShape(new System.Windows.Shapes.Rectangle(), brush, top, left, bottom, right, dashStyle, isFilled);
                        break;
                    case TheShape.Circle:
                        DrawShape(new Ellipse(), brush, top, left, bottom, right, dashStyle, isFilled);
                        break;
                    default:
                        break;
                }
                left += horizontal;
                right = left + width;
            }
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
        private void DrawShape(Shape shape, SolidColorBrush brush, double top, double left, double bottom, double right, DashStyle dashStyle, bool isFilled)
        {
            shape.Width = right - left;
            shape.Height = bottom - top;
            if (isFilled)
                shape.Fill = brush;
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

        public void DrawImage(System.Windows.Controls.Image image)
        {
            Canvas.Children.Clear();
            Canvas.Children.Add(image);
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
    }
}
