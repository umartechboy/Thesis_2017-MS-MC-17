using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.IO;
using System.IO.Ports;
using RoboSim;
using Physics;
using SplineDesigner;

namespace RoboSim
{
    public partial class Form1 : Form
    {
        ChainTranformationMatrix eeH = new ChainTranformationMatrix();
        SphericalRobot robot = new SphericalRobot();
        
        int simulationStepMillis = 45;
        double SimSubStepping = 10;
        MainWpfPrevWindow wpfPrevForm2 = new MainWpfPrevWindow();
        Ink ink = new Ink(0.1);
        WorkSpace workspace = new WorkSpace();
        WindowToForm formWpfPrev2;
        RobotEditor RobotEditorF = new RobotEditor();

        Dictionary<string, ShowCloseForm> analysisForms = new Dictionary<string, ShowCloseForm>();
        public Form1()
        {
            InitializeComponent();
            robot.Initialize();
            robot.OnDebugPoint += Robot_OnDebugPoint;
            // create the test point at robot EE
            eeH.Children.Add(new TranslateTransformationMatrix(0, 0, robot.L0));
            eeH.Children.Add(new RotateTransformationMatrix(robot.Actuators[0].CurrentPosition, Axis.Z));
            eeH.Children.Add(new RotateTransformationMatrix(robot.Actuators[1].CurrentPosition, Axis.X));
            eeH.Children.Add(new TranslateTransformationMatrix(0, 0, robot.L1));
            eeH.Children.Add(new RotateTransformationMatrix(robot.Actuators[2].CurrentPosition, Axis.X));
            eeH.Children.Add(new RotateTransformationMatrix(robot.Actuators[3].CurrentPosition, Axis.Z));
            eeH.Children.Add(new TranslateTransformationMatrix(0, 0, robot.L2));
            eeH.Children.Add(new RotateTransformationMatrix(robot.Actuators[4].CurrentPosition, Axis.X));
            eeH.Children.Add(new RotateTransformationMatrix(robot.Actuators[5].CurrentPosition, Axis.Z));
            eeH.Children.Add(new TranslateTransformationMatrix(0, 0, robot.L3));

            formWpfPrev2 = new WindowToForm(wpfPrevForm2, robot, ink, workspace);
            formWpfPrev2.Text = "3D Animation";
            RobotEditorF.Text = "Robot Editor";
            analysisForms.Add("wpf", formWpfPrev2);
            analysisForms.Add("robotEditor", RobotEditorF);
            RobotEditorF.Robot = robot;
            robot.lastState = new SphericalRobot();
            robot.lastState.Initialize();
            analysisToolStripMenuItem.DropDownItems.Clear();
            foreach (var aForm in analysisForms)
            {
                analysisToolStripMenuItem.DropDownItems.Add(aForm.Value.Text);
                analysisToolStripMenuItem.DropDownItems[analysisToolStripMenuItem.DropDownItems.Count - 1].Click +=
                    analysisToolStripSubMenuItem_Click;
                analysisToolStripMenuItem.DropDownItems[analysisToolStripMenuItem.DropDownItems.Count - 1].Name =
                    aForm.Key;
            }
            Point lastLoc = Location;
            periodicSaveT.Enabled = periodicallySaveOutputToolStripMenuItem.Checked;

            // populate manual control TBs
            for (int i = 0; i < robot.Actuators.Length; i++)
            {
                var tb = new TrackBar();
                tb.Name = "mcTB" + i;
                tb.Location = new Point(50, tb.Height * i);
                tb.Width = manualControlP.Width - 50;
                tb.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                tb.TabIndex = i + 6;

                tb.Minimum = (int)(robot.Actuators[i].MinimumPosition * 180 / Math.PI);
                tb.Maximum = (int)(Math.Round((robot.Actuators[i].PositionRange + robot.Actuators[i].MinimumPosition) * 180 / Math.PI));
                tb.Value = (int)(robot.Actuators[i].Target * 180 / Math.PI);

                tb.ValueChanged += mcTb_ValueChanged;
                manualControlP.Controls.Add(tb);
                var t = new TextBox();
                t.TabIndex = i;
                t.Name = "mcL" + i;
                t.Location = new Point(0, tb.Height * i + tb.Height / 2 - t.Height / 2);
                t.Text = Math.Round(robot.Actuators[i].Target * 180 / Math.PI, 1).ToString();
                t.Leave += T_Leave;
                manualControlP.Controls.Add(t);
            }
            robot.OnTargetChanged += Robot_OnTargetChanged;
            manualControlP.Height = manualControlP.Controls[11].Top + manualControlP.Controls[11].Height;
            splitContainer1.SplitterDistance = manualControlP.Height;

        }
                                                
        private void Robot_OnTargetChanged(int ind, double value)
        {                                                  
            robot.OnTargetChanged -= Robot_OnTargetChanged;

            var tb = (TrackBar)manualControlP.Controls.Find("mcTB" + ind, false)[0];
            var l = (TextBox)manualControlP.Controls.Find("mcL" + ind, false)[0];

            l.Leave -= T_Leave;
            tb.ValueChanged -= mcTb_ValueChanged;
            l.Text = Math.Round(value * 180 / Math.PI, 2).ToString();
            if (!double.IsNaN(value))
                tb.Value = (int)Math.Round(value * 180 / Math.PI, 0);

            l.Leave += T_Leave;
            tb.ValueChanged += mcTb_ValueChanged;
            robot.OnTargetChanged += Robot_OnTargetChanged;
        }
               
        private void Robot_OnDebugPoint(System.Windows.Media.Media3D.Point3D p, int ind)
        {
            wpfPrevForm2.TestPointLocations[ind].Offset = p;
        }

        private void T_Leave(object sender, EventArgs e)
        {
            robot.OnTargetChanged -= Robot_OnTargetChanged;
            int ind = Convert.ToInt16(((TextBox)sender).Name.Substring(3));
            try
            {
                robot.Actuators[ind].Target = Convert.ToDouble(((TextBox)sender).Text) * Math.PI / 180; ;
            }
            catch { ((TextBox)sender).Text = "Nil"; return; }
            var l = (TrackBar)manualControlP.Controls.Find("mcTB" + ind, false)[0];
            l.Value = (int)Convert.ToDouble(((TextBox)sender).Text);
                                                    
            robot.OnTargetChanged += Robot_OnTargetChanged;
        }

        private void mcTb_ValueChanged(object sender, EventArgs e)
        {
            robot.OnTargetChanged -= Robot_OnTargetChanged;
            int ind = Convert.ToInt16(((TrackBar)sender).Name.Substring(4));
            robot.Actuators[ind].Target = ((TrackBar)sender).Value * Math.PI / 180;
            eeH.Rotations[ind].Th = ((TrackBar)sender).Value * Math.PI / 180;
            var l = (TextBox)manualControlP.Controls.Find("mcl" + ind, false)[0];
            l.Leave -= T_Leave;
            l.Text = ((TrackBar)sender).Value.ToString();

            currentPosL.Text = Math.Round(wpfPrevForm2.TestPointLocations[0].Offset.X, 3).ToString() +  ", " +
                Math.Round(wpfPrevForm2.TestPointLocations[0].Offset.Y, 3).ToString() + ", " +
                Math.Round(wpfPrevForm2.TestPointLocations[0].Offset.Z, 3).ToString();
            l.Leave += T_Leave;
            robot.OnTargetChanged += Robot_OnTargetChanged;
        }
                               

        private void saveScriptMI_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "All Files (*.*)|(*.*)";
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(sfd.FileName))
                        File.Delete(sfd.FileName);
                    var f = File.OpenWrite(sfd.FileName);
                    using (var sr = new StreamWriter(f))
                    {
                    }            
                    f.Close();
                    MessageBox.Show("File saved.");
                }
                catch
                {
                    MessageBox.Show("Error while saving the file.");
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var ofd = new OpenFileDialog();
            ofd.Filter = "All Files (*.*)|*.*";
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if (!File.Exists(ofd.FileName))
                        return;
                    var f = File.OpenRead(ofd.FileName);
                    using (var sr = new StreamReader(f))
                    {
                    }
                    f.Close();                
                }
                catch
                {
                    MessageBox.Show("Error while loading the file.");
                }
            }
        }

        bool[,] defaultActuationSignals = new bool[,]{
            { false, false, false},
            { false, false, false},
            { false, false, false},
            { false, false, false},
            { false, false, false},
            { false, false, false}
        };
        private void Form1_Load(object sender, EventArgs e)
        {              
            simulationStepT.Enabled = realTimeSimulation_MI.Checked;
            //RobotEditorF.Show2();
            showDotPosTB.Select();  
        }

        void SimStep()
        {
            for (double i = 0; i < SimSubStepping; i++)
                robot.ThreadStep(simulationStepMillis / 1000.0D / SimSubStepping);
            
            robot.sync3DView();
        }
        // this is the real simulation tick. Timing does matter
        private void simulationStepT_Tick(object sender, EventArgs e)
        {
            SimStep();
        }

        private void realTimeSimulationT_MI_Click(object sender, EventArgs e)
        {
            simulationStepT.Enabled = !simulationStepT.Enabled;
            realTimeSimulation_MI.Checked = simulationStepT.Enabled;
        }

        // this is just a graphics refresher. Timing doesn't matter
        private void animated3DRefresh_t_Tick(object sender, EventArgs e)
        {
            if (formWpfPrev2.Visible)
            {
                if (AnimateSolutions.Count > 0)
                {
                    showSolution(AnimateSolutions[0]);
                    AnimateSolutions.RemoveAt(0);
                }
            }
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formWpfPrev2.Face.Visibility == System.Windows.Visibility.Visible)
                formWpfPrev2.Face.Close();
            if (sp != null)
                sp.Close();
        }

        private void updatePlotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            plotsRefreshT.Enabled = updatePlotsToolStripMenuItem.Checked;
        }
                     
        private void plotsRefreshT_Tick(object sender, EventArgs e)
        {
            //var cogF = (PanelPlotForm)analysisForms["cog"];
            //var topTrajF = (PanelPlotForm)analysisForms["traj"];
            //var ladderF = (PanelPlotForm)analysisForms["ladder"];   
            //if (cogF.Visible)
            //{
            //    robot.DrawCog(cogF.G, cogF.dispP.BackgroundImage.Width, cogF.dispP.BackgroundImage.Height, cogF.explorer.Translation, (double)cogF.ZoomTB.Value);
            //    cogF.dispP.Invalidate();
            //}
        }

        private void simStepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimStep();
        }
        
        SerialPort sp;
        private void connectToProteusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connectToProteusToolStripMenuItem.Checked)
            {
                sp.Close();
                sp = null;
                connectToProteusToolStripMenuItem.Checked = false;
                return;
            }
            sp = new SerialPort("COM1", 9600);
            sp.Open();
            sp.NewLine = "\n";
            SyncProteus();
            connectToProteusToolStripMenuItem.Checked = true;
        }
        bool SyncProteus()
        {
            //if (sp == null)
            //    return false;
            //if (!sp.IsOpen)
            //    return false;
            //for (int i = 0; i < 4; i++)
            //    sp.WriteLine("O" + i + "=" + writeBytes[i]);
            return true;
        }

        private void analysisToolStripSubMenuItem_Click(object sender, EventArgs e)
        {
            var mi = (ToolStripMenuItem)sender;
            var form = analysisForms[mi.Name];
            if (!mi.Checked)
            {
                form.Width = 800;
                form.Height = 500;
                form.Show2();                  
                mi.Checked = true;
            }
            else
            {
                form.Hide2();
                mi.Checked = false;
            }
        }
        
        Point lastLoc = new Point();
        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            foreach (var form in analysisForms)
            {
                form.Value.Location = new Point(
                                    form.Value.Location.X + Location.X - lastLoc.X,
                                    form.Value.Location.Y + Location.Y - lastLoc.Y
                    );
            }
            lastLoc = Location;
        }
                       
        private void periodicallySaveOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            periodicSaveT.Enabled = !periodicallySaveOutputToolStripMenuItem.Checked;   
            periodicallySaveOutputToolStripMenuItem.Checked = periodicSaveT.Enabled;
        }

        string dirSeed = "";
        bool PeriodicSave()
        {
            if (dirSeed == "")
            {
                dirSeed = DateTime.Now.Ticks.ToString();
                Directory.CreateDirectory(dirSeed);
            }
            string fSeed = DateTime.Now.Ticks.ToString();
            if (formWpfPrev2.Visible)
            {
                try
                {
                    var bmp = new System.Windows.Media.Imaging.RenderTargetBitmap(
                        (int)wpfPrevForm2.Width,
                        (int)wpfPrevForm2.Height,
                        96, 96, System.Windows.Media.PixelFormats.Pbgra32);
                    bmp.Render(wpfPrevForm2);
                    var pngEnc = new System.Windows.Media.Imaging.PngBitmapEncoder();
                    pngEnc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bmp));
                    string fname = dirSeed + "\\wpf_" + fSeed + ".png";
                    using (var f = File.Create(fname))
                    {
                        pngEnc.Save(f);
                        f.Flush();
                        f.Close();
                    }
                }
                catch { }
            }

            var cogF = (PanelPlotForm)analysisForms["cog"];
            if (cogF.Visible)
            {
                cogF.dispP.BackgroundImage.Save(dirSeed + "\\cog_" + fSeed + ".png");
            }
            return true;
        }
        private void periodicSaveT_Tick(object sender, EventArgs e)
        {
            PeriodicSave();

        }

        private void forceAScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PeriodicSave();
        }
                                
        private void showDotTB_Leave(object sender, EventArgs e)
        {
            
        }

        private void solveTargetB_Click(object sender, EventArgs e)
        {
            computeSolutions(showDotPosTB.Text, showDotOrientationTB.Text);
        }

        private void applySolutionB_Click(object sender, EventArgs e)
        {
            showSolution((RobotSolution)solutionsCB.SelectedItem);
        }
        void showTarget(string pos, string orient)
        {
            try
            {
                showTarget(System.Windows.Media.Media3D.Point3D.Parse(pos), System.Windows.Media.Media3D.Vector3D.Parse(orient));
            }
            catch { }
        }
        void showTarget(System.Windows.Media.Media3D.Point3D p, System.Windows.Media.Media3D.Vector3D o)
        {
            wpfPrevForm2.TestPointLocations[0].Offset = p;
            wpfPrevForm2.TestPointLocations[0].Orientation = o;
        }
        List<RobotSolution> computeSolutions(string pos, string orientation, bool putToCB = true)
        {
            try
            {
                var p = System.Windows.Media.Media3D.Point3D.Parse(pos);
                var o = System.Windows.Media.Media3D.Vector3D.Parse(orientation);

                var solutions = SphericalRobotSolution.Solve(robot, new EulerAngleOrientation(p, o.X, o.Y, o.Z));
                if (solutions.Count == 0)
                {
                    MessageBox.Show("No solutions were found.");
                    return solutions;
                }

                solutionsCB.Items.Clear();
                foreach (var sol in solutions)
                {
                    solutionsCB.Items.Add(sol);
                }
                solutionsCB.SelectedIndex = 0;
                return solutions;
            }
            catch { return new List<RobotSolution>(); }
        }
        void showSolution(RobotSolution solution)
        {                  
            robot.ShowSolution(solution);
        }

        private void showDotPosTB_TextChanged(object sender, EventArgs e)
        {
            computeSolutions(showDotPosTB.Text, showDotOrientationTB.Text);
            showTarget(showDotPosTB.Text, showDotOrientationTB.Text);
            if (robot.TargetAchievable((RobotSolution)solutionsCB.SelectedItem))
            {
                showSolution((RobotSolution)solutionsCB.SelectedItem);
            }
            else
            { }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void setAnimLimitsPosControls_TextChanged(object sender, EventArgs e)
        {
            var pc = (Control)sender;                                           
            var oc = tabPage2.Controls.Find(pc.Name.Substring(0, pc.Name.IndexOf("P")) + "OrientAnimTB", false)[0];
            showTarget(pc.Text, oc.Text);
            var solutions = computeSolutions(pc.Text, oc.Text, false);
            if (solutions.Count > 0)       
                showSolution(solutions[0]);
        }

        private void setAnimLimitsOrientationControls_TextChanged(object sender, EventArgs e)
        {        
            var oc = (Control)sender;
            var pc = tabPage2.Controls.Find(oc.Name.Substring(0, oc.Name.IndexOf("O")) + "PosAnimTB", false)[0];
            showTarget(pc.Text, oc.Text);
            var solutions = computeSolutions(pc.Text, oc.Text, false);
            if (solutions.Count > 0)
                showSolution(solutions[0]);
        }

        public List<SphericalRobotSolution> AnimateSolutions = new List<SphericalRobotSolution>();
        private void animateB_Click(object sender, EventArgs e)
        {
            simulationStepT.Enabled = false;
            realTimeSimulation_MI.Checked = false;

            List<SphericalRobotSolution> solutions = new List<SphericalRobotSolution>();
            var p1 = System.Windows.Media.Media3D.Point3D.Parse(fromPosAnimTB.Text);
            var p2 = System.Windows.Media.Media3D.Point3D.Parse(toPosAnimTB.Text);
            var o1 = System.Windows.Media.Media3D.Point3D.Parse(fromOrientAnimTB.Text);
            var o2 = System.Windows.Media.Media3D.Point3D.Parse(toOrientAnimTB.Text);
            var divs = Convert.ToDouble(animDivsTB.Text);
            for (double i = 0; i <= divs; i++)
            {
                var p = new System.Windows.Media.Media3D.Point3D(
                    p1.X + (-p1.X + p2.X) / divs * i, p1.Y + (-p1.Y + p2.Y) / divs * i, p1.Z + (-p1.Z + p2.Z) / divs * i);

                var o = new System.Windows.Media.Media3D.Point3D(
                    o1.X + (-o1.X + o2.X) / divs * i, o1.Y + (-o1.Y + o2.Y) / divs * i, o1.Z + (-o1.Z + o2.Z) / divs * i);

                solutions.Add((SphericalRobotSolution)computeSolutions(p.ToString(), o.ToString(), false)[0]);
            }
            AnimateSolutions = solutions;
        }

        List<EulerAngleOrientation[]> ComputedTargetsForWritting = new List<EulerAngleOrientation[]>();
        private void sendTooWorkspaceB_Click(object sender, EventArgs e)
        {
            ComputedTargetsForWritting = new List<EulerAngleOrientation[]>();
            Cursor = Cursors.WaitCursor;
            var ss = splineEditor.GetSplines();
            List<EulerAngleOrientation> targets = new List<EulerAngleOrientation>();
            EulerAngleOrientation planeOrientation = new EulerAngleOrientation();
            try
            {
                planeOrientation.Offset = System.Windows.Media.Media3D.Point3D.Parse(centerOfWrittingPlaneTB.Text);
                planeOrientation.Orientation = System.Windows.Media.Media3D.Vector3D.Parse(orientationOfWrittingPlaneTB.Text);

                var minX = float.PositiveInfinity;
                var maxX = float.NegativeInfinity;
                var minY = float.PositiveInfinity;
                var maxY = float.NegativeInfinity;
                foreach (var s in ss)
                {
                    var b = s.GetBounds();
                    if (b.X < minX) minX = b.X;
                    if (b.Y < minY) minY = b.Y;
                    if (b.X + b.Width > maxX) maxX = b.X + b.Width;
                    if (b.Y + b.Height > maxY) maxY = b.Y + b.Height;
                }
                var ps = System.Windows.Media.Media3D.Point3D.Parse(sizeOfWrittingPlaneTB.Text+",0");
                var bounds = new RectangleF(0, 0, (float)ps.X, (float)ps.Y);
                double scaleX = bounds.Width / (maxX - minX);
                double scaleY = bounds.Height / (maxY - minY);
                double scale = scaleX;
                if (scaleY < scale)
                    scale = scaleY;
                var midX = (maxX + minX) / 2 + (bounds.X) / scale;
                var midY = (maxY + minY) / 2 + (bounds.Y) / scale;

                ink.Clear();
                int prog = 0;
                int ssi = 0;
                foreach (var s in ss)
                {
                    prog = 0;
                    targets = s.MakeTargets(Convert.ToDouble(resolutionForScriptTB.Text), scale, midX, midY);

                    List<EulerAngleOrientation> targetsTransformed = new List<EulerAngleOrientation>();
                    foreach (var o in targets)
                    {
                        o.Y *= -1;
                        o.G = -o.G;
                        o.G = o.G + Math.PI / 2;
                        ChainTranformationMatrix m = planeOrientation;
                        m.Children.Add(o);
                        var ea = EulerAngleOrientation.FromTransformationMatrix(m);
                        targetsTransformed.Add(ea);
                        o.G = o.G - Math.PI / 2;
                    }
                    ComputedTargetsForWritting.Add(targetsTransformed.ToArray());
                    ink.Thickness = s.FlatTipThickness * scale;
                    foreach (var o in targets)
                    {
                        prog++;
                        if (((prog * 100) / targets.Count) / ss.Count + ssi * 100 / ss.Count != scriptMakePB.Value)
                        {
                            scriptMakePB.Value = ((prog * 100) / targets.Count) / ss.Count + ssi * 100 / ss.Count;
                            Application.DoEvents();                                
                        }
                        ink.AppendFlatTipPoint(o, planeOrientation);
                    }
                    ink.NewChar();
                    ssi++;
                }
                ink.SetBoard(1, 0.5, planeOrientation);

                planTrajectoryButton.Enabled = true;
            }
            catch
            {
                planTrajectoryButton.Enabled = false;
            }
            Cursor = Cursors.Default;
            scriptMakePB.Value = 0;
        }

        private void planTrajectoryButton_Click(object sender, EventArgs e)
        {
            if (ComputedTargetsForWritting.Count == 0)
                return;
            trajectoryStepT.Enabled = true;

            double dt = simulationStepMillis / 1000.0D / SimSubStepping;
            TrajForWritting = DiscretePlannedTrajectory.FromTargets(ComputedTargetsForWritting, robot, dt, 0.2);
            SimStepsForWritting = 0;     
            trajIndexForWritting = 0;
        }
        int trajIndexForWritting = 0;
        int trajSubIndexForWritting = 0;
        DiscretePlannedTrajectory TrajForWritting;
        long SimStepsForWritting = 0;
        long waitForSimStepStartedAt = 0;

        private void trajectoryStepT_Tick(object sender, EventArgs e)
        {                   
            double dt = simulationStepMillis / 1000.0D / SimSubStepping;

            for (int sc = 0; sc < (trajectoryStepT.Interval / 1000.0D) / dt; sc++)
            {
                if (trajIndexForWritting >= TrajForWritting.Actions.Count)
                {
                    trajectoryStepT.Enabled = false;
                    continue;
                }
                var step = TrajForWritting.Actions[trajIndexForWritting];
                if (step is GoToHome)
                {
                    var s = (GoToHome)step;

                    if (trajSubIndexForWritting == 0)
                    {
                        for (int i = 0; i < 6; i++)
                            robot.Actuators[i].Target = s.Targets[i];
                        trajSubIndexForWritting = 1;
                        continue;
                    }
                    bool allOnTarget = true;
                    for (int j = 0; j < 6; j++)
                    {
                        if (!robot.Actuators[j].IsOnTarget)
                        {
                            allOnTarget = false;
                            break;
                        }
                    }
                    if (allOnTarget)
                    {
                        trajIndexForWritting++;
                        trajSubIndexForWritting = 0;
                        continue;
                    }
                    else
                    {
                        robot.ThreadStep(dt);
                        SimStepsForWritting++;
                        continue;
                    }
                }
                else if (step is ChangeTarget)
                {
                    var s = (ChangeTarget)step;
                    for (int i = 0; i < 6; i++)
                        robot.Actuators[i].Target = s.Target[i];
                    trajIndexForWritting++;
                }
                else if (step is WaitTarget)
                {             
                    bool allOnTarget = true;
                    for (int j = 0; j < 6; j++)
                    {
                        if (!robot.Actuators[j].IsOnTarget)
                        {
                            allOnTarget = false;
                            break;
                        }
                    }
                    if (allOnTarget)
                    {
                        trajIndexForWritting++;     
                        continue;
                    }
                    else
                        robot.ThreadStep(dt);
                }
                else if (step is WaitSimSteps)
                {
                    var s = (WaitSimSteps)step;
                    if (trajSubIndexForWritting == 0)
                    {
                        waitForSimStepStartedAt = SimStepsForWritting;
                        trajSubIndexForWritting = 1;
                        continue;
                    }
                    if (trajSubIndexForWritting == 1)
                    {
                        if (SimStepsForWritting - waitForSimStepStartedAt < s.TotalSteps)
                        {
                            if (SimStepsForWritting - waitForSimStepStartedAt > 5)
                            { }
                            robot.ThreadStep(dt);
                            SimStepsForWritting++;
                        }
                        else
                        {
                            trajSubIndexForWritting = 0;
                            trajIndexForWritting++;
                        }
                    }
                }
            }
        }

        bool busy = false;
        private void computeWorkspace_Click(object sender, EventArgs e)
        {
            if (busy)
            {
                busy = false;
                return;
            }
            busy = true;
            var start = System.Windows.Media.Media3D.Point3D.Parse(workSpaceStartTB.Text);
            var range = System.Windows.Media.Media3D.Point3D.Parse(workSpaceRangeTB.Text);
            double res = Convert.ToDouble(linearResWsTB.Text), aRes = Convert.ToDouble(aResWsTB.Text);
            workspace.GenerateMap(res, start.X, range.X, start.Y, range.Y, start.Z, range.Z);
            double Va = 0;
            double Vt = 0;
            for (double z = start.Z; z < start.Z + range.Z && busy; z += res)
                for (double y = start.Y; y < start.Y + range.Y && busy; y += res)
                    for (double x = start.X; x < start.X + range.X && busy; x += res)
                    {
                        int Marks = 0;
                        int total = 0;
                        for (double g = -Math.PI; g < Math.PI; g += aRes)
                        {
                            for (double b = -Math.PI; b < Math.PI; b += aRes)
                            {
                                for (double a = -Math.PI; a < Math.PI; a += aRes)
                                {
                                    var s = SphericalRobotSolution.Solution1(robot, new EulerAngleOrientation(a, b, g, x, y, z));
                                    bool allRight = true;
                                    for (int j = 0; j < 6 && allRight; j++)
                                    {
                                        if (double.IsNaN(s.MotorAngles[j]) || s.MotorAngles[j] < -20 || s.MotorAngles[j] > 20)
                                            allRight = false;
                                    }
                                    if (allRight)
                                        Marks++;
                                    total++;
                                }
                            }
                        }

                        if (Marks == total)
                            Va += res * res * res;
                        Vt += res * res * res;
                        totalWorkableAreaWsL.Text = Math.Round(Va, 3).ToString();
                        totalWsAreaL.Text = Math.Round(Vt, 3).ToString();
                        successRatioWsl.Text = Math.Round(Va / Vt * 100).ToString();
                        workspace.SetMap(new System.Windows.Media.Media3D.Point3D(x, y, z), (double)Marks / total);

                        wsProgX.Value = (int)Math.Round(((x - start.X) / range.X) * 100);
                        wsProgY.Value = (int)Math.Round(((y - start.Y) / range.Y) * 100);
                        wsProgZ.Value = (int)Math.Round(((z - start.Z) / range.Z) * 100);
                        Application.DoEvents();
                    }

            wsProgX.Value = 0;
            wsProgY.Value = 0;
            wsProgZ.Value = 0;
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }
    }
}
