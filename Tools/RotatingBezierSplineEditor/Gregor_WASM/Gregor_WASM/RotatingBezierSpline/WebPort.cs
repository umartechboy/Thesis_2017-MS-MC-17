using Microsoft.AspNetCore.Components.Web;
using SkiaSharp;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;

namespace Gregor_WASM
{
    public enum MouseButtons : byte
    {
        None = 0,
        Left,
        Right,
        Middle,
    }
    public class MouseEventArgs : EventArgs
    {
        public MouseButtons Button { get; internal set; }
        public int Clicks { get; set; }
        public int Delta { get; set; }
        public Point Location { get; set; }
        public int X { get => Location.X; }
        public int Y { get => Location.Y; }
        public MouseEventArgs()
        {
            Location = new Point();
            Clicks = 0;
        }
        public MouseEventArgs(PointerEventArgs e)
        {
            Location = new Point((int)Math.Round(e.OffsetX), (int)Math.Round(e.OffsetY));
            Clicks = 1;
            Button = e.Button == 0 ? MouseButtons.Left : e.Button == 2 ? MouseButtons.Right : MouseButtons.Middle;
        }
        public MouseEventArgs(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            Location = new Point((int)Math.Round(e.OffsetX), (int)Math.Round(e.OffsetY));
            Clicks = 1;
            Button = e.Button == 0 ? MouseButtons.Left : e.Button == 2 ? MouseButtons.Right : MouseButtons.Middle;
        }
    }
    public delegate void MouseEventHandler(object sender, MouseEventArgs e);
	public delegate Task AsyncMouseEventHandler(object sender, MouseEventArgs e);
    public class Pen
    {
        public Color Color { get; set; } = Color.Black;
        public float Width { get; set; } = 1;
        public Pen(Color color, float width = 1)
        {
            Color = color;
            Width = width;
        }
    }
    public class Font
    {
        public string FontFamily { get; set; } = "Roboto";
        public float Height{ get; set; } = 10;
        public Font(string fontFamily, float height)
        {
            FontFamily = fontFamily;
            Height = height;
        }
    }
	public class Graphics
    {
        private Graphics() { }
        public SKCanvas canvas { get; private set; }
        public static Graphics FromCanvas(SKCanvas canvas)
        {
            return new Graphics() { canvas = canvas };
        }

        SKColor ColorToSKColor(Color c)
        {
            return new SKColor(c.R, c.G, c.B, c.A);
        }
        public void FillRectangle(Color c, float x, float y, float width, float height, float radius = 0)
        {
            canvas.DrawRoundRect(x, y, width, height, radius, radius,
                new SKPaint()
                {
                    Color = ColorToSKColor(c),
                    IsAntialias = true,
                });
        }
        public void FillRectangle(SolidBrush b, float x, float y, float width, float height, float radius = 0)
        {
            canvas.DrawRoundRect(x, y, width, height, radius, radius,
                new SKPaint()
                {
                    Color = ColorToSKColor(b.Color),
                    IsAntialias = true,
                });
        }

        public void DrawEllipse(Pen p, float x, float y, float width, float height)
        {
            DrawEllipse(p.Color, p.Width, x, y, width, height);
        }
        public void DrawEllipse(Color c, float strokeWidth, float x, float y, float width, float height)
		{
			canvas.DrawOval(x + width / 2, y + height /2, width / 2, height / 2,
				new SKPaint()
				{
					Color = ColorToSKColor(c),
					StrokeWidth = strokeWidth,
					IsAntialias = true,
					IsStroke = true
				});
		}
        public void DrawLine(Pen p, float x1, float y1, float x2, float y2)
        {
            DrawLine(p.Color, p.Width, x1, y1, x2, y2);
        }
        public void DrawLine(Pen p, PointF p1, PointF p2)
        {
            DrawLine(p.Color, p.Width, p1.X, p1.Y, p2.X, p2.Y);
        }
        public void DrawLine(Color c, float width, PointF p1, PointF p2)
        {
            DrawLine(c, width, p1.X, p1.Y, p2.X, p2.Y);
        }
        public void DrawLine(Color c, float width, float x1, float y1, float x2, float y2)
        {
            canvas.DrawLine(x1, y1, x2, y2,
                new SKPaint()
                {
                    Color = ColorToSKColor(c),
                    StrokeWidth = width,
                    IsAntialias = true,
                    IsStroke = true
                });
        }

        public SizeF MeasureString(string s, Font font)
        {
            return MeasureString(s, font.FontFamily, font.Height);
        }
        public SizeF MeasureString(string s, string fontFamily, float size)
        {
            var sz = new SKPaint(new SKFont(SKTypeface.FromFamilyName(fontFamily)))
            {
                TextSize = size,
            }.MeasureText(s);
            return new SizeF(sz, size);
        }

        public void DrawString(string s, string fontFamily, float size, Color c, PointF location)
        {
            canvas.DrawText(s, location.X, location.Y, new SKPaint(new SKFont(SKTypeface.FromFamilyName(fontFamily)))
            {
                TextSize = size,
                Color = ColorToSKColor(c),
                IsAntialias = true,
            });
        }

        public void DrawString(string s, Font font, Color c, PointF location)
        {
            DrawString(s, font.FontFamily, font.Height, c, location);
        }
        public void DrawString(string s, Font font, Color c, float x, float y)
        {
            DrawString(s, font.FontFamily, font.Height, c, new PointF(x, y));
        }

		public void FillEllipse(Color c, RectangleF rect)
        {
            FillEllipse(c, rect.X, rect.Y, rect.Width, rect.Height);
        }

		public void FillEllipse(Color c, float x, float y, float w, float h)
        {
            canvas.DrawOval(x + w / 2, y + h / 2, w / 2, h / 2,
                new SKPaint()
                {
                    Color = ColorToSKColor(c),
                    IsAntialias = true,
                });
        }
        public void DrawPolygon(Pen p, PointF[] poly)
        {
            DrawPolygon(p.Color, p.Width, poly);
        }
        public void DrawLines(Pen p, PointF[] points)
        {
            DrawPolygon(p.Color, p.Width, points, false);
        }
        public void DrawPolygon(Color c, float strokeSize, PointF[] poly, bool closePath = true)
        {
            if (poly.Length < 2)
                return;
            var path = new SKPath();
            path.MoveTo(poly.First().X, poly.First().Y);
            for (int i = 1; i < poly.Length; i++)
                path.LineTo(poly[i].X, poly[i].Y);
            if (closePath)
                path.Close();
            canvas.DrawPath(path,
                new SKPaint()
                {
                    Color = ColorToSKColor(c),
                    IsAntialias = true,
                    IsStroke = true,
                    StrokeWidth = strokeSize
                });
        }
        public void FillPolygon(Color c, PointF[] poly)
        {
            if (poly.Length < 2)
                return;
            var path = new SKPath();
            path.MoveTo(poly.First().X, poly.First().Y);
            for (int i = 1; i < poly.Length; i++)
                path.LineTo(poly[i].X, poly[i].Y);
            path.Close();
            canvas.DrawPath(path,
                new SKPaint()
                {
                    Color = ColorToSKColor(c),
                    IsAntialias = true,
                });
        }
        public void FillPath(Color c, SKPath path)
        {
            canvas.DrawPath(path,
                new SKPaint()
                {
                    Color = ColorToSKColor(c),
                    IsAntialias = true,
                });
        }
        public void DrawPath(Color c, float strokeSize, SKPath path)
        {
            canvas.DrawPath(path,
                new SKPaint()
                {
                    Color = ColorToSKColor(c),
                    IsAntialias = true,
                    IsStroke = true,
                    StrokeWidth = strokeSize
                });
        }
        public void SetClip(RectangleF rect)
        {
            var skrect = new SKRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
            canvas.ClipRect(skrect);
        }
        public void ScaleTransform(float x, float y)
        {
            canvas.Scale(x, y);
        }
        public void TranslateTransform(float x, float y)
        {
            canvas.Translate(x, y);
        }
        public void ResetTransform()
        {
            canvas.ResetMatrix();
        }
        public void ResetClip ()
        {
            canvas.Restore();
        }
        public void DrawRectangle(Pen p, float x, float y, float w, float h, float radius = 0)
        {
            DrawRectangle(p.Color, p.Width, new RectangleF(x, y, w, h), radius);
        }
        public void DrawRectangle(Color c, float strokeSize, float x, float y, float w, float h, float radius = 0)
        {
            DrawRectangle(c, strokeSize, new RectangleF(x, y, w, h), radius);
        }
        public void DrawRectangle(Color c, float strokeSize, RectangleF rect, float radius = 0)
        {
            canvas.DrawRoundRect(rect.X, rect.Y, rect.Width, rect.Height, radius, radius,
                new SKPaint()
                {
                    Color = ColorToSKColor(c),
                    StrokeWidth = strokeSize,
                    IsAntialias = true,
                    IsStroke = true
                });
        }
        public void FillRectangle(Color[] colors, float[] colorPos, float x, float y, float width, float height, float radius = 0)
        {

            // Create a linear gradient shader
            SKShader shader = SKShader.CreateLinearGradient(
                new SKPoint(x, y), // Start point (top-left corner)
                new SKPoint(x + width, y), // End point (bottom-right corner)
                colors.Select(ColorToSKColor).ToArray(), // Array of colors
                colorPos, // Array of color stop positions
                SKShaderTileMode.Clamp // Shader tile mode
            );

            // Create an SKPaint object and set its shader to the linear gradient
            SKPaint paint = new SKPaint
            {
                Shader = shader
            };

            // Draw a rectangle filled with the linear gradient
            canvas.DrawRoundRect(new SKRect(x, y, x + width, y + height), radius, radius, paint);
        }
	}
	public enum Cursors
	{
		Default = 0,
		IBeam = 1,
		NoMoveHoriz = 2,
		VSplit = 3,
		No = 4,
		Hand = 5,
	}
	public class Cursor
    {
        Cursors currentValue;
        public static implicit operator Cursor(Cursors c)
        {
            return new Cursor() { currentValue = c };
        }
        public static implicit operator Cursors(Cursor c)
        {
            if (ReferenceEquals(c, null)) { return Cursors.Default; }
            return c.currentValue;
        }
    }
    public class Control
    {
        public Cursor Cursor { get; set; } = Cursors.Default;
		public event MouseEventHandler MouseEnter;
		public event MouseEventHandler MouseMove;
		public event MouseEventHandler MouseDown;
		public event MouseEventHandler MouseUp;
		public event MouseEventHandler MouseLeave;
		public event MouseEventHandler MouseClick;
		public int Width { get; set; } = 100;
		public int Height { get; set; } = 100;
		protected Action invalidate;
		public void Invalidate()
		{
			this.invalidate.Invoke();
		}
	}
}
