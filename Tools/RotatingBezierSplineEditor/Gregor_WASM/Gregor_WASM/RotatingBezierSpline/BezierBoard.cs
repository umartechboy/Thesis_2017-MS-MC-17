using Gregor_WASM;
using SkiaSharp.Views.Blazor;
using System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RotatingBezierSplineEditor
{
    public class BezierBoard:Control
    {
        public event EventHandler OnRequestToShowAll;
        public event EventHandler OnRequestToUnlockAll;
        public event EventHandler OnRequestShowOnly;
        public event EventHandler OnRequestToUnlockOnly;
        public bool MayHaveUnsavedChanges = false;
        public delegate void MouseEventHandlerF(object sender, MouseEventArgsF e);
        public class MouseEventArgsF : EventArgs
        {
            
            public float X { get; private set; }
            public float Y { get; private set; }
            public int Delta { get; private set; }
            public MouseButtons Button { get; private set; }
            public int Clicks { get; private set; }
            public PointF Location { get { return new PointF(X, Y); } }
            public MouseEventArgsF(MouseButtons button, int clicks, float X, float Y, int delta)
            {
                this.X = X;
                this.Y = Y;
                Delta = delta;
                Button = button;
                this.Clicks = clicks;
            }
            public override string ToString()
            {
                return Location.ToString();
            }
        }
        AnchorDrawMode _AnchorDrawMode = AnchorDrawMode.Centers;
        InkDrawMode _InkDrawMode = InkDrawMode.All;
        public InkDrawMode InkDrawMode { get { return _InkDrawMode; } set { _InkDrawMode = value; Invalidate(); } }
        public AnchorDrawMode AnchorDrawMode { get { return _AnchorDrawMode; } set { _AnchorDrawMode = value; Invalidate(); } }
        List<BezierBoardItem> Objects = new List<BezierBoardItem>();

        MouseButtons MouseButtonsThatWentDown = MouseButtons.None;
        Point MouseLocationAtDown;
		public BezierBoard(Action invalidate)
        {
            this.invalidate = invalidate;
            MouseMove += BezierBoard_MouseMove;
            MouseDown += BezierBoard_MouseDown;
            MouseUp += BezierBoard_MouseUp;
			//SizeChanged += BezierBoard_SizeChanged;
			MouseClick += BezierBoard_Click;
        }
        bool _ge = true, _se = true, _xyl = true;
        public bool GridEnabled { get{ return _ge; }set { _ge = value; BezierBoard_SizeChanged(null, null);Invalidate(); } }
        public bool ScaleEnabled { get { return _se; } set { _se = value; BezierBoard_SizeChanged(null, null); Invalidate(); } }
        public bool XYLinesEnabled { get { return _xyl & GridEnabled; } set { _xyl = value; Invalidate(); } }
        public int  XAxisHeight { get { return ScaleEnabled? 30:0; } }
        public int YAxisWidth { get { return ScaleEnabled ? 50 : 0; } }

        public static float Fill { get; internal set; } = 1;

        private void BezierBoard_SizeChanged(object sender, EventArgs e)
        {
            PlotBounds = new RectangleF(0, 0, Width - YAxisWidth, Height - XAxisHeight);
            YAxisBounds = new RectangleF(Width - YAxisWidth, 0, YAxisWidth, Height - XAxisHeight);
            XAxisBounds = new RectangleF(0, Height - XAxisHeight, Width - YAxisWidth, XAxisHeight);
        }

        internal RotatingBezierSpline[] GetSplineObjects()
        {
            return Objects.FindAll(o => o is RotatingBezierSpline).FindAll(o => ((RotatingBezierSpline)o).Anchors.Count > 0).Select(o => (RotatingBezierSpline)o).ToArray();
        }
        internal ImageItem[] GetImageObjects()
        {
            return Objects.FindAll(o => o is ImageItem).Select(o => (ImageItem)o).ToArray();
        }
        public class BezierBoardItemEventArgs : EventArgs
        { public BezierBoardItem Item { get; set; } }
        public delegate void BezierBoardItemEventHandler(object sender, BezierBoardItemEventArgs e);
        public event BezierBoardItemEventHandler OnBezierBoardItemAdded;
        public event BezierBoardItemEventHandler OnBezierBoardItemRemoved;
        public BezierBoardItem AddItem(BezierBoardItem item)
        {
            if (Objects.Count > 0)
                item.Index = Objects.Max(oo => oo.Index) + 1;
            var st = DateTime.Now;
            if (item == null) return null;
            Objects.Add(item);
            item.OnSelfRemoveRequest += (s, e) =>
            {
                Objects.Remove(item);
                OnBezierBoardItemRemoved?.Invoke(this, new BezierBoardItemEventArgs() { Item = item });
                Invalidate();
            };
            if (item is RotatingBezierSpline)
            {
                var sitem = (RotatingBezierSpline)item;
                // TBD
                //sitem.OnShowCurveMenuRequest += (s, e) =>
                //{
                //    CurveMenuItem cmi = new CurveMenuItem();
                //    cmi.cms.Items.Add("Duplicate").Click += (ss, ee) => {
                //        var spline = sitem.MakeCopy(this);
                //        AddItem(spline);
                //        Invalidate();
                //    };
                //    cmi.cms.Items.Add("Delete").Click+=(ss,ee) => {
                //        Objects.Remove(item);
                //        OnBezierBoardItemRemoved?.Invoke(this, new BezierBoardItemEventArgs() { Item = (RotatingBezierSpline)item });
                //        Invalidate();
                //    };
                //    cmi.OnRequestShowOnly += (s_, e_) => OnRequestShowOnly?.Invoke(sitem, new EventArgs());
                //    cmi.OnRequestToUnlockOnly += (s_, e_) => OnRequestToUnlockOnly?.Invoke(sitem, new EventArgs());
                //    cmi.OnRequestToShowAll += (s_, e_) => OnRequestToShowAll?.Invoke(sitem, new EventArgs());
                //    cmi.OnRequestToUnlockAll += (s_, e_) => OnRequestToUnlockAll?.Invoke(sitem, new EventArgs());
                //    cmi.cms.Show(Cursor.Position);
                //    };
            
                //sitem.OnSelected += (s, e) => { foreach (var obj in Objects)
                //        if (obj is RotatingBezierSpline)
                //        {
                //            if (obj == sitem)
                //                continue;
                //            ((RotatingBezierSpline)obj).UnselectAllAnchors();
                //        }
                //};
            }
            OnBezierBoardItemAdded?.Invoke(this, new BezierBoardItemEventArgs() { Item = item });
            return item;
        }
        
        private void BezierBoard_MouseUp(object sender, MouseEventArgs e2)
        {
            if (MouseAtDownForScale != null)
            { MouseAtDownForScale = null; return; }
            if (BoardMoveStartedAt != null)
            { BoardMoveStartedAt = null; return; }
            var pT = GtoV(e2.Location);
            var e = new MouseEventArgsF(e2.Button, e2.Clicks, pT.X, pT.Y, e2.Delta);
            for (int i = Objects.Count - 1; i >= 0; i--)
                Objects[i].ProcessMouseUp(sender, new PointF(OffsetG.X, -Height + OffsetG.Y + XAxisHeight), PPU, e, currentControlUnderMouse, null, this);
            currentControlUnderMouse = null;
            Invalidate();
        }
        public new Cursor DefaultCursor = Cursors.Default;
        PointF OffsetGAtMouseDown; // for panning
        object BoardMoveStartedAt = null;
        object MouseAtDownForScale = null; // for scaling
        PointF VatMouseDown; // for scaling
        Point lastMouseG;
        float ppuAtMouseDown = 1;
        public bool clickIsForAdding = false;
        private void BezierBoard_MouseDown(object sender, MouseEventArgs e2)
        {
            MouseButtonsThatWentDown = e2.Button;
            MouseLocationAtDown = e2.Location;
            var pT = GtoV(e2.Location);
            var eBkp = new Point(e2.X, e2.Y);
            var e = new MouseEventArgsF(e2.Button, e2.Clicks, pT.X, pT.Y, e2.Delta);
            var controls = Objects;
            for (int i = controls.Count - 1; i >= 0; i--)
            {
                currentControlUnderMouse = controls[i].ProcessMouseDown(e, new PointF(OffsetG.X, -Height + OffsetG.Y + XAxisHeight), PPU, InkDrawMode, AnchorDrawMode, null, this);
                if (currentControlUnderMouse != null)
                {
                    Invalidate();
                    MayHaveUnsavedChanges = true;
                    return;
                }
            }
            // we reach here only only when no controls are under the mouse.
            foreach(var o in Objects)
            {
                if (o is RotatingBezierSpline)
                {
                    var so = (RotatingBezierSpline)o;
                    if ((so.CanReceiveAnchorAtEnd || so.CanReceiveAnchorAtStart) && (clickIsForAdding && so.Visible && !so.Locked))
                    {
                        if (e.Button == MouseButtons.Left ) // this is not an append operation, but a cancel op.
                        {
                            continuousAnchorAddition(e, so);
                            return;
                        }
                        else
                        {
                            foreach (var o_ in Objects)
                            {
                                if (o_ is RotatingBezierSpline)
                                { ((RotatingBezierSpline)o_).UnselectAllAnchors(); }
                            }
                            return;
                        }
                    }
                }
            }
            // we move here only when no spline is being appended to
            if (e.Button == MouseButtons.Right) // initiate scale
            {
                //GAtMouseDown = eG.X;
                VatMouseDown = GtoV(eBkp);
                MouseAtDownForScale = eBkp;
                ppuAtMouseDown = PPU;
            } // we move here only when no spline is being appended to
            else if (e.Button == MouseButtons.Middle) // initiate pan
            {
                BoardMoveStartedAt = eBkp;
                OffsetGAtMouseDown = OffsetG;
            }
            else // move or add new curve
            {
                if (clickIsForAdding)
                {
                    MayHaveUnsavedChanges = true;
                    var so = new RotatingBezierSpline(this, true);
                    AddItem(so);
                    continuousAnchorAddition(e, so);
                    Invalidate();
                    //RotatingBezierSpline newSpline = ;
                    //var newSpline.AddAnchor(new RotatingBezierSplineAnchor(e.Location))
                }
                else
                {
                    BoardMoveStartedAt = eBkp;
                    OffsetGAtMouseDown = OffsetG;
                }
            }
            lastMouseG = eBkp;
        }
        public void ForceBeginDragItem(BezierBoardItem item)
        {
            // TBD
            //Cursor.Position = new Point(0, 0);
            ForceBeginDragItem(item, new MouseEventArgsF(MouseButtons.Left, 1, 0, 0, 0));
        }
        public void ForceBeginDragItem(BezierBoardItem item, MouseEventArgsF e)
        {
            if (item == null) return;
            currentControlUnderMouse = item;
            item.NotifyMouseDown(this, e);
            item.MouseState = MouseState.Held;
        }
        void continuousAnchorAddition(MouseEventArgsF e, RotatingBezierSpline so)
        {
            var a1 = e.Location;
            a1 = new PointF(a1.X + 1, a1.Y + 1);
            anchorToAdd = new RotatingBezierSplineAnchor(e.Location, a1, 0, 0, so.FlatTipWidth,  true);
            anchorToAdd.BindCurvatureHandlesLength = true;

            if (so.CanReceiveAnchorAtStart && so.Anchors.Count > 1)
            {
                var r = anchorToAdd.R1.DistanceFrom(anchorToAdd.P);
                anchorToAdd.R1.X = (float)(anchorToAdd.P.X + r * Math.Cos(so.Anchors.First().R1.AngleAbout(so.Anchors.First().P)));
                anchorToAdd.R1.Y = (float)(anchorToAdd.P.Y + r * Math.Sin(so.Anchors.First().R1.AngleAbout(so.Anchors.First().P)));
            }
            else
            {
                if (so.Anchors.Count > 1)
                {
                    var r = anchorToAdd.R1.DistanceFrom(anchorToAdd.P);
                    anchorToAdd.R1.X = (float)(anchorToAdd.P.X + r * Math.Cos(so.Anchors[so.Anchors.Count - 2].R1.AngleAbout(so.Anchors[so.Anchors.Count - 2].P)));
                    anchorToAdd.R1.Y = (float)(anchorToAdd.P.Y + r * Math.Sin(so.Anchors[so.Anchors.Count - 2].R1.AngleAbout(so.Anchors[so.Anchors.Count - 2].P)));
                }
            }
            CurvatureHandlePoint pointToMoveWhenAdding = so.AddAnchor(anchorToAdd);
            so.UnselectAllAnchors();

            ForceBeginDragItem(pointToMoveWhenAdding, e);
            void unbindEv(object ss, EventArgs ee)
            {
                anchorToAdd.BindCurvatureHandlesLength = false;
                pointToMoveWhenAdding.OnMouseUp -= unbindEv;
                anchorToAdd.P.MouseState = MouseState.Selected;
            }
            pointToMoveWhenAdding.OnMouseUp += unbindEv;
        }
        BezierBoardItem currentControlUnderMouse = null;
        RotatingBezierSplineAnchor anchorToAdd;

        public float VxtoG(float v, float offsetG, float ppu)
        {
            return v * ppu + offsetG;
        }
        public float GxtoV(float vG, float offsetG, float ppu)
        {
            return (vG - offsetG) / ppu;
        }
        public float VytoG(float v, float offsetG, float ppu)
        {
            return -(v * ppu + offsetG) + Height - XAxisHeight;
        }
        public float GytoV(float vG, float offsetG, float ppu)
        {
            return ((Height - XAxisHeight - vG) - offsetG) / ppu;
        }
        public PointF GtoV(Point p)
        {
            return new PointF(
                GxtoV(p.X, OffsetG.X, PPU), 
                GytoV(p.Y, OffsetG.Y, PPU));
        }
        public PointF VtoG(PointF p)
        {
            return new PointF(
                VxtoG(p.X, OffsetG.X, PPU), 
                VytoG(p.Y, OffsetG.Y, PPU));
        }

        public void ScaleChanged(PointF latestPoint, PointF lastPoint)
        {
            OffsetG = new PointF(
                ((Point)MouseAtDownForScale).X - VatMouseDown.X * PPU,
                (Height - XAxisHeight - ((Point)MouseAtDownForScale).Y) - VatMouseDown.Y * PPU);
        }

        private void BezierBoard_Click(object sender, EventArgs e)
        {
            if (currentControlUnderMouse != null)
                currentControlUnderMouse.NotifyMouseClick(MouseLocationAtDown, MouseButtonsThatWentDown);
        }

        private void BezierBoard_MouseMove(object sender, MouseEventArgs e2)
        {
            if (MouseAtDownForScale != null)
            {
                var ec = ((Point)MouseAtDownForScale);
                var er = new Point((Width - (int)YAxisWidth) / 2, (Height - XAxisHeight) / 2);
                var refD = RBSPoint.DistanceBetween(ec.X, ec.Y, er.X, er.Y);
                PPU = ppuAtMouseDown * (float)(RBSPoint.DistanceBetween(e2.X, e2.Y, er.X, er.Y) / refD);
                if (PPU < .01F)
                    PPU = 0.01F;
                else if (PPU > 100)
                    PPU = 100;
                ScaleChanged(e2.Location, lastMouseG);
                lastMouseG = e2.Location;
                Invalidate();
                return;
            }
            if (BoardMoveStartedAt != null)
            {
                OffsetG = new PointF(
                    OffsetGAtMouseDown.X + e2.X - ((Point)BoardMoveStartedAt).X,
                    OffsetGAtMouseDown.Y - e2.Y + ((Point)BoardMoveStartedAt).Y
                    );
                lastMouseG = e2.Location;
                Invalidate();
                return;
            }
            
            var pT = GtoV(e2.Location);
            var e = new MouseEventArgsF(e2.Button, e2.Clicks, pT.X, pT.Y, e2.Delta);
            var controls = Objects;
            for (int i = controls.Count - 1; i >= 0; i--)
            {
                if (controls[i] == null)
                    continue;
                if (controls[i].ProcessMouseMove(e, new PointF(OffsetG.X, -Height + OffsetG.Y + XAxisHeight), PPU, currentControlUnderMouse, InkDrawMode, AnchorDrawMode, null, this))
                {
                    break;
                }
            }
            Invalidate();
        }

        internal void ImportObjects(string fileName)
        {
            LoadFile(fileName, this);
        }
        static List<BezierBoardItem> LoadFile(string fileName, BezierBoard board)
        {
            List<BezierBoardItem> objects = new List<BezierBoardItem>();
            string fileToDelete = "";
            if (fileName.ToLower().EndsWith(".rbs"))
            {
                fileToDelete = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Path.GetFileNameWithoutExtension(fileName) + "_temp.xml");
                DecompressFile(fileName, fileToDelete);
                fileName = fileToDelete;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            int index = 1;
            foreach (XmlElement obj in doc.GetElementsByTagName("image"))
            {
                var ii = ImageItem.Parse(board, obj);
                board?.AddItem(ii);
                objects.Add(ii);
                ii.Index = index++;
            }
            var emptys = new List<RotatingBezierSpline>();
            int unnamedIndex = 1;
            foreach (XmlElement obj in doc.GetElementsByTagName("spline"))
            {
                var sp = (RotatingBezierSpline)RotatingBezierSpline.Parse(obj, board);
                if (sp.Label == "")
                    sp.Label = "Spline " + (unnamedIndex++);
                sp.Index = index++;
                if (sp.Anchors.Count <= 1)
                    emptys.Add(sp);
                else
                {
                    board?.AddItem(sp);
                    objects.Add(sp);
                }
            }
            if (emptys.Count > 0)
            {
                // TBD
                //if (MessageBox.Show("There are " + emptys.Count + " splines with 1 or fewer anchor points. Do you want to import them too?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //    foreach (var sp in emptys)
                //    {
                //        board?.AddItem(sp);
                //        objects.Add(sp);
                //    }
            }
            if (fileToDelete != "")
                File.Delete(fileToDelete);
            return objects;
        }
        public static List<BezierBoardItem> LoadFile(string fileName)
        {
            return LoadFile(fileName, null);
        }
        public static List<BezierBoardItem> LoadFile()
        {
            //var ofd = new OpenFileDialog();
            //ofd.Filter = "Rotating Bezier Spline Files (*.rbs)|*.rbs|XML documents (*.xml)|*.xml";
            //if (ofd.ShowDialog() != DialogResult.OK)
            //    return null;
            //return LoadFile(ofd.FileName);
            throw new NotImplementedException();
        }

        internal void ClearObjects()
        {
            Objects.Clear();
        }

        internal void SaveObjects(string fileName, params int [] indices)
        {
            if (indices == null)
                indices = new int[0];
            if (indices.Length == 0)
            {
                indices = new int[Objects.Count];
                for (int i = 0; i < indices.Length; i++)
                    indices[i] = i;
            }
            indices = indices.Distinct().ToArray();

            XmlDocument doc = new XmlDocument();

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            var main = doc.CreateElement("objects");
            List<BezierBoardItem> sortedItems = new List<BezierBoardItem>();
            foreach (var ind in indices)
                sortedItems.Add(Objects[ind]);
            sortedItems = sortedItems.OrderBy(i => i.Index).ToList();
            foreach (var item in sortedItems)
                item.Save(main);
            doc.AppendChild(main);
            if (fileName.ToLower().EndsWith(".xml"))
            {
                doc.Save(fileName);
            }
            else
            {
                doc.Save(fileName + "_uncompressed");
                CompressFile(fileName + "_uncompressed", fileName);
                File.Delete(fileName + "_uncompressed");
            }
        }
        public static void CompressFile(string path, string destFileName)
        {
            FileStream sourceFile = File.OpenRead(path);
            FileStream destinationFile = File.Create(destFileName);

            byte[] buffer = new byte[sourceFile.Length];
            sourceFile.Read(buffer, 0, buffer.Length);

            using (GZipStream output = new GZipStream(destinationFile,
                CompressionMode.Compress))
            {
                Console.WriteLine("Compressing {0} to {1}.", sourceFile.Name,
                    destinationFile.Name, false);

                output.Write(buffer, 0, buffer.Length);
            }

            // Close the files.
            sourceFile.Close();
            destinationFile.Close();
        }
        public static void DecompressFile(string path, string newFileName)
        {
            using (FileStream originalFileStream = File.OpenRead(path))
            {
                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
        }
        protected void OnPaint(SKPaintGLSurfaceEventArgs e)
        {
            var g = Gregor_WASM.Graphics.FromCanvas(e.Surface.Canvas);
            DrawAxisAndGrid_Horizontal(g);
            DrawAxisAndGrid_Vertical(g);

            g.SetClip(new RectangleF(0, 0, Width - YAxisWidth, Height - XAxisHeight));
            g.DrawRectangle(new Gregor_WASM.Pen(System.Drawing.Color.DarkSlateBlue), 0, 0, PlotBounds.Width - 1, PlotBounds.Height - 1);

            g.ScaleTransform(1, -1);
            g.TranslateTransform(0 + OffsetG.X, -Height + OffsetG.Y + XAxisHeight);
            g.ScaleTransform(PPU, PPU);
            foreach (var obj in Objects.FindAll(o => o is ImageItem))
                obj?.Draw(g, new PointF(), 1, InkDrawMode, AnchorDrawMode, null, this);

            g.ResetTransform();
            g.ResetClip();

            g.SetClip(new RectangleF(0, 0, Width - YAxisWidth, Height - XAxisHeight));
            g.ScaleTransform(1, -1);
            //g.TranslateTransform(0 + OffsetG.X, -Height + OffsetG.Y + XAxisHeight);
            //g.ScaleTransform(PPU, PPU);
            foreach (var obj in Objects.FindAll(o => !(o is ImageItem)))
                obj?.Draw(g, new PointF(OffsetG.X, -Height + OffsetG.Y+ XAxisHeight), PPU,  InkDrawMode, AnchorDrawMode, null, this);

            g.ResetTransform();
            g.ResetClip();
            if (MouseAtDownForScale != null)
            {
                var lineC = PPU < ppuAtMouseDown ? Color.DarkGreen : Color.DarkRed;
                var line = new Gregor_WASM.Pen(lineC, 3);
                var p1 = new Point((Width - YAxisWidth) / 2, (Height - XAxisHeight) / 2);
                var p2 = lastMouseG;
                var p3 = (Point)MouseAtDownForScale;
                g.DrawLine(line, p1, p2);
                g.DrawRectangle(new Gregor_WASM.Pen(lineC,1), p1.X - 3, p1.Y - 3, 6, 6);
                g.DrawRectangle(new Gregor_WASM.Pen(lineC, 1), p2.X - 3, p2.Y - 3, 6, 6);
                g.DrawLine(Color.Red, 1, p3.X, p3.Y - 20, p3.X, p3.Y + 20);
                g.DrawLine(Color.Red, 1, p3.X - 20, p3.Y, p3.X + 20, p3.Y);
                g.DrawRectangle(Color.Red, 1, p3.X - 40, p3.Y - 40, 80, 80);
                var r = (float)RBSPoint.DistanceBetween(p1.X, p1.Y, p3.X, p3.Y);
                g.DrawEllipse(Color.Black, 1, p1.X - r, p1.Y - r, 2 * r, 2 * r);
            }
            if (BoardMoveStartedAt != null)
            {
                var p1 = (Point)BoardMoveStartedAt;
                var p2 = lastMouseG;
                g.DrawLine(Color.DarkBlue, 1, p1, p2);
                g.DrawRectangle(Color.DarkBlue, 1, p1.X - 3, p1.Y - 3, 6, 6);
                g.DrawRectangle(Color.DarkBlue, 1, p2.X - 3, p2.Y - 3, 6, 6);
            }

        }
        public float PPU { get; private set; } = 1F;
        PointF OffsetG = new PointF(100,100);
        Gregor_WASM.Font tickFont = new Gregor_WASM.Font("ARIAL", 10);
        RectangleF PlotBounds, YAxisBounds, XAxisBounds;
        public void DrawAxisAndGrid_Horizontal(Gregor_WASM.Graphics g)
        {
            if (!ScaleEnabled && !GridEnabled)
                return;
            float xs = PPU;
            float xog = OffsetG.X;
            // X Axis

            var axisP = new Gregor_WASM.Pen(Color.Black, 1.5F);
            var majLine = new Gregor_WASM.Pen(Color.DarkGray, 1F);
            var minLine = new Gregor_WASM.Pen(Color.LightGray, 1F);

            double unitX = 1e-8F;
            double multF = 5;
            // determine scale first
            while (unitX * xs < tickFont.Height * 2.2F)
            {
                unitX *= multF;
                multF = multF == 2 ? 5 : 2;
            }

            double minX = 0, maxX = 0;
            while (minX * xs < -xog)
            {
                if (double.IsPositiveInfinity(minX + unitX))
                { minX = float.MaxValue; break; }
                minX += unitX;
            }
            while (minX * xs > -xog)
            {
                if (double.IsNegativeInfinity(minX - unitX))
                { minX = double.MinValue; break; }
                minX -= unitX;
            }

            while (maxX * xs > PlotBounds.Width - xog)
            {
                if (double.IsNegativeInfinity(maxX - unitX))
                { minX = double.MinValue; break; }
                maxX -= unitX;
            }
            while (maxX * xs < PlotBounds.Width - xog)
            {
                if (double.IsPositiveInfinity(maxX + unitX))
                { maxX = float.MaxValue; break; }
                maxX += unitX;
            }



            int xaHei = (tickFont.Height * 15 / 10);
            bool isMinLine = false;

            var xSigFiguresAfterD = 0;
            var totalFigs = (unitX / 2 - Math.Floor(unitX / 2)).ToString().Length - 2;

            while (Math.Round(unitX, xSigFiguresAfterD) == Math.Round(unitX / 2, xSigFiguresAfterD)
                && xSigFiguresAfterD <= totalFigs)
                xSigFiguresAfterD++;

            for (double i = minX; i <= maxX; i += unitX / 2)
            {
                //PointF drawableMid = VtoG(new PointF(i, 0), xog / xs, xs, 1, 0);
                //drawableMid = new PointF(drawableMid.X, h);

                PointF drawable1 = new PointF((float)i * xs + xog + PlotBounds.X, PlotBounds.Y);
                PointF drawable2 = new PointF((float)i * xs + xog + PlotBounds.X, PlotBounds.Y + PlotBounds.Height);
                //if (grid)
                //drawable1 = new PointF(drawable1.X, 0);
                //if (grid)
                //drawable2 = new PointF(drawable2.X, h - xaHei);
                string sNum = NumberUtils.roundedFrac((float)i, xSigFiguresAfterD);
                var xyo = g.MeasureString(sNum, tickFont);
                PointF drawableStrPos = new PointF(drawable2.X - xyo.Width / 2, XAxisBounds.Y + 8);
                if (!isMinLine)
                {
                    drawable2 = new PointF((float)i * xs + xog + PlotBounds.X, PlotBounds.Y + PlotBounds.Height + 5);
                    if (drawable1.X < PlotBounds.Width && drawable1.X >= 0)
                    {
                        if (GridEnabled)
                            g.DrawLine(majLine, drawable1, drawable2);
                        g.DrawString(sNum, tickFont, Color.Black, drawableStrPos.X, drawableStrPos.Y);
                    }
                }
                else
                {
                    if (drawable1.X < PlotBounds.Width && drawable1.X > 0)
                    {
                        if (GridEnabled)
                            g.DrawLine(minLine, drawable1, drawable2);
                    }
                }
                isMinLine = !isMinLine;
            }
            if (XYLinesEnabled)
                if (xog < PlotBounds.Width && xog > 0)
                    g.DrawLine(axisP, xog, 0, xog, PlotBounds.Height);

            //g.DrawLine(axisP, AxisBounds.X, AxisBounds.Y, AxisBounds.X + AxisBounds.Width, AxisBounds.Y);

            //// axis labels are buttons now. Dont draw their strings
            //var unitStr = new DXString() { Text = Unit, Color = Color.Black, DXFont = tickFont };
            //var unitSize = g.MeasureString(unitStr);
            //g.DrawString(unitStr, AxisBounds.X + AxisBounds.Width / 2 - unitSize.Width / 2, AxisBounds.Y + tickFont.Height * 0.9F);
        }

        public void DrawAxisAndGrid_Vertical(Gregor_WASM.Graphics g)
        {
            if (!ScaleEnabled && !GridEnabled)
                return;
            YAxisBounds.Height = PlotBounds.Height;
            YAxisBounds.Y = PlotBounds.Y;
            float ys = PPU;
            float yog = OffsetG.Y;

            // X Axis
            var axisP = new Gregor_WASM.Pen(Color.Black, 1.5F);
            var majLine = new Gregor_WASM.Pen(Color.DarkGray, 1F);
            var minLine = new Gregor_WASM.Pen(Color.LightGray, 1F);

            // Y Axis
            double unitY = 1e-8F;
            double multF = 5;
            multF = 5;
            // determine scale first
            while (unitY * ys < tickFont.Height * 1.5F)
            {
                if (double.IsNegativeInfinity(unitY * multF))
                { unitY = double.MinValue; break; }
                if (double.IsPositiveInfinity(unitY * multF))
                { unitY = double.MaxValue; break; }
                unitY *= multF;
                multF = multF == 2 ? 5 : 2;
            }
            //if (unitY < 1e-7 || unitY > 1e7)
            //    return drawingRect;

            double minY = 0, maxY = 0;
            while (minY * ys < -yog)
            {
                if (double.IsPositiveInfinity(minY + unitY))
                { minY = double.MaxValue; break; }
                minY += unitY;
            }
            while (minY * ys + yog > 0)
            {
                if (double.IsNegativeInfinity(minY - unitY))
                { minY = double.MinValue; break; }
                minY -= unitY;
            }

            while (maxY * ys + yog > YAxisBounds.Height)
            {
                if (double.IsNegativeInfinity(maxY - unitY))
                { minY = double.MinValue; break; }
                maxY -= unitY;
            }
            while (maxY * ys + yog < YAxisBounds.Height)
            {
                if (double.IsPositiveInfinity(maxY + unitY))
                { maxY = double.MaxValue; break; }
                maxY += unitY;
            }


            bool isMinLine = false;
            var ySigFiguresAfterD = 0;
            var totalFigs = (unitY / 2 - Math.Floor(unitY / 2)).ToString().Length - 2;

            while (Math.Round(unitY, ySigFiguresAfterD) == Math.Round(unitY / 2, ySigFiguresAfterD)
                && ySigFiguresAfterD <= totalFigs)
                ySigFiguresAfterD++;
            for (double i = minY; i <= maxY; i += unitY / 2)
            {
                //PointF drawableMid = VtoG(new PointF(0, i), 1, 1, yog / ys, ys, PlotBounds.Height);
                //drawableMid = new PointF(PlotBounds.Width, drawableMid.Y);

                PointF drawable1 = new PointF(PlotBounds.X, (float)PlotBounds.Height - (float)(PlotBounds.Y + i * ys + yog));
                PointF drawable2 = new PointF(PlotBounds.X + PlotBounds.Width, (float)PlotBounds.Height - (float)(PlotBounds.Y + i * ys + yog));
                if (!isMinLine)
                {
                    drawable2 = new PointF(PlotBounds.X + PlotBounds.Width + 5, (float)PlotBounds.Height - (float)(PlotBounds.Y + i * ys + yog));

                    string s = NumberUtils.roundedFrac((float)i, ySigFiguresAfterD);
                    var xyo = g.MeasureString(s, tickFont);
                    PointF drawableStrPos = new PointF(YAxisBounds.X + 6, drawable2.Y - xyo.Height / 2);
                    if (drawable2.Y < PlotBounds.Y + PlotBounds.Height && drawable2.Y > PlotBounds.Y)
                    {
                        if (GridEnabled)
                            g.DrawLine(majLine, drawable1, drawable2);
                        g.DrawString(s, tickFont, Color.Black, drawableStrPos.X, drawableStrPos.Y);
                    }
                }
                else
                {
                    if (drawable2.Y < PlotBounds.Height && drawable2.Y > 0)
                        if (GridEnabled)
                            g.DrawLine(minLine, drawable1, drawable2);
                }
                isMinLine = !isMinLine;
            }

            // zero line
            if (XYLinesEnabled)
                if (yog < YAxisBounds.Height && yog > 0)
                    g.DrawLine(axisP, PlotBounds.X, PlotBounds.Y + PlotBounds.Height - yog, PlotBounds.X + PlotBounds.Width, PlotBounds.Y + PlotBounds.Height - yog);

            // draw border
            //g.DrawLine(axisP, AxisBounds.X, AxisBounds.Y, AxisBounds.X, AxisBounds.Y + AxisBounds.Height);

            // axis labels are buttons now. Dont draw their strings
            //var unitStr = new DXString() { Text = Unit, Color = Color.Black, DXFont = Font };
            //var unitSize = g.MeasureString(unitStr);

            //g.DrawString(unitStr, AxisBounds.X + Font.Height * 2.5F, AxisBounds.Y + AxisBounds.Height/2 + unitSize.Width/2, -90);
        }
        public static FlatTipRenderAlgorithm FlatTipRenderAlgorithm { get; set; }
        public static Color ForcedInkColor { get; set; } = Color.Black;
        public static bool ForceSingleColorSplines { get; set; } = false;
        public static bool SplinesCanBeSelected { get; internal set; } = true;
    }
    
    public enum InkDrawMode
    {
        None = 0,
        Spline = 1,
        Ink = 2,
        Images = 4,
        CompleteSpline = Spline | Ink,
        All = CompleteSpline | Images
    }
    public enum FlatTipRenderAlgorithm
    {
        Polygon,
        Rectangle
    }
    public enum AnchorDrawMode
    {
        None = 0,
        Centers = 1,
        RotaionHandles = 2 ,
        CurvatureHandles = 4,
        All = Centers | RotaionHandles| CurvatureHandles,
    }
    public class NumberUtils
    {
        static string prefixes = "afpum kMTPA";
        public static string roundedFrac(float number, int significantFigures)
        {
            var isNeg = number < 0;
            if (isNeg)
                number = -number;

            if (number == 0)
                return "0";
            int multi = 5;
            while (number < 1 && multi > 0)
            { number *= 1000; multi--; }
            while (number >= 1000 && multi < 10)
            { number /= 1000; multi++; }

            return ((Math.Round(number, 3) * (isNeg ? -1 : 1)) + (multi != 5 ? prefixes[multi].ToString() : "").ToString());
        }
    }
}
