using Physics;
using PhysLogger2;
using PhysLogger2.Hardware;
using PhysLogger2.PhysLoggerMath.Quantity;
using RoboSim;
using RotatingBezierSplineEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drogon3
{
    public partial class MainForm : Form
    {
        double SimStep { get; set; } = .01;
        SphericalRobot robot = new SphericalRobot();
        WindowToForm formWpfPrev2;
        RobotEditor RobotEditor;
        PIDPerformanceControl pidPerformance;
        SplinePainter splinePainter;
        InverseKinematicsSolver inverseKinematicsSolver;
        MainWpfRobotPrevWindow wpfPrevWindow;
        Ink ink = new Ink();
        WorkSpace workspace = new WorkSpace();
        //RoutePlanner routePlanner;
        EndEffectorControl endEffectorControl;
        Control CurrentControlInFocus;
        //Point lastFormLocation;
        public MainForm()
        {
            InitializeComponent();
            _3dPrevT.SetImage(Properties.Resources._3DPrev, 60);
            editRobotT.SetImage(Properties.Resources.editing, 60);
            realTimeTickT.SetImage(Properties.Resources.time, 60);
            inverseKinematicsT.SetImage(Properties.Resources.kinematics, 60);
            pidPerformanceT.SetImage(Properties.Resources.pid, 60);
            splinePainterT.SetImage(Properties.Resources.greg, 60);
            //routePlannerT.SetImage(Properties.Resources.PathPlanning, 60);
            endEffectorControlT.SetImage(Properties.Resources.joystick, 60);

            robot.Initialize();
            robot.OnDebugPoint += (p, ind) => wpfPrevWindow.TestPointLocations[ind].Offset = p;


            RobotEditor = new RobotEditor(robot) { Text = "Robot Editor" };
            inverseKinematicsSolver = new InverseKinematicsSolver(robot) { Text = "Inverse Kinematics Solver"};
            inverseKinematicsSolver.OnTargetPreviewRequested += (s, target) =>
            {
                if (CurrentControlInFocus == inverseKinematicsSolver)
                    showTarget((EulerAngleOrientation)target);
              };

            wpfPrevWindow = new MainWpfRobotPrevWindow();
            wpfPrevWindow.Intialize(robot, ink, workspace);
            ink.SetBoard(1, 1);
            ink.InkOrientation = new EulerAngleOrientation(0, 0, 0, 1, 1, 1);
            formWpfPrev2 = new WindowToForm(wpfPrevWindow, robot, ink, workspace) { Text = "3D Animation" };
            formWpfPrev2.Width = 800;
            formWpfPrev2.Height = 500;
            pidPerformance = new PIDPerformanceControl();
            splinePainter = new SplinePainter(robot, ink);
            splinePainter.OnWritingPadOrientationChangeRequested += (s, orientation) =>
            {
                ink.InkOrientation = (EulerAngleOrientation)orientation;
            };
            //routePlanner = new RoutePlanner(robot) { Text = "Path Planner" };
            endEffectorControl = new EndEffectorControl(robot) { Text = "End Effector Control" };
            endEffectorControl.OnTargetPreviewRequested += (s, target) =>
            {
                if (CurrentControlInFocus == endEffectorControl)
                    showTarget((EulerAngleOrientation)target);
            };
            ; initAnalysisFormControls(formWpfPrev2, _3dPrevT, dSimulationToolStripMenuItem);
            initAnalysisFormControls(inverseKinematicsSolver, inverseKinematicsT, dSimulationToolStripMenuItem);
            initAnalysisFormControls(RobotEditor, editRobotT, inverseKinematicsSolverToolStripMenuItem);
            initAnalysisFormControls(pidPerformance, pidPerformanceT, pIDPerformanceToolStripMenuItem);
            initAnalysisFormControls(splinePainter, splinePainterT, splinePainterToolStripMenuItem);
            initAnalysisFormControls(endEffectorControl, endEffectorControlT, routePlannerToolStripMenuItem);
            //LocationChanged += (s, e) => lastFormLocation = Location;
        }
        void initAnalysisFormControls(Control control, ToolControl toolIcon, ToolStripMenuItem toolStripItem)
        {
            //LocationChanged += (s2, e2) =>
            //{
            //    form.Left += (int)((Left - lastFormLocation.X));
            //    form.Top += (int)((Top - lastFormLocation.Y));
            //};
            toolIcon.OnActivated += (s, e) =>
            {
                control.Dock = DockStyle.Fill;
                tools.Controls.Clear();
                if (toolIcon.Active)
                    tools.Controls.Add(control);
                if (control.Parent != null)
                    control.Size = control.Parent.Size;
                toolStripItem.Checked = toolIcon.Active;
                CurrentControlInFocus = control;
            };
            toolStripItem.Click += (s, e) => toolIcon.Active = !toolStripItem.Checked;
        }
        void showTarget(EulerAngleOrientation target)
        {
            wpfPrevWindow.TargetModel.Transform = target;
        }
        void showTarget(System.Windows.Media.Media3D.Point3D p, System.Windows.Media.Media3D.Vector3D o)
        {
            wpfPrevWindow.TestPointLocations[0].Offset = p;
            wpfPrevWindow.TestPointLocations[0].Orientation = o;
        }
        void initAnalysisFormControls(ShowCloseForm form, ToolControl tool, ToolStripMenuItem item)
        {
            //LocationChanged += (s2, e2) =>
            //{
            //    form.Left += (int)((Left - lastFormLocation.X));
            //    form.Top += (int)((Top - lastFormLocation.Y));
            //};
            tool.OnActivated += (s, e) =>
            {
                form.Show2(tool.Active);
                //if (tool.Active)
                //{
                //    form.Left = (int)(this.Right * 0.79F);
                //    form.Top = (int)(this.Top * 0.79F);
                //}
                item.Checked = tool.Active;
                form.OnHidden += (s2, e2) => tool.Active = false;
            };
        }
        private void analysisToolStripSubMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void _3dPrev_OnActivated(object sender, EventArgs e)
        {
            
        }

        private void realTimeTick_OnActivated(object sender, EventArgs e)
        {
            robot.RealTimeTickEnabled = realTimeTickT.Active;
        }

        uint did = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!robot.RealTimeTickEnabled)
                return;
            for (double t = 0; t < .00001; t += SimStep)
            {
                robot.ThreadStep(SimStep);
                pidPerformance.timeQ.cache = (float)(robot.SimLifeinMillis / 1000);
                for (int i = 0; i < robot.Actuators.Length; i++)
                {
                    pidPerformance.motorPosQs[i].cache = (float)(robot.Actuators[i].Current);
                    pidPerformance.motorRefQs[i].cache = (float)(robot.Actuators[i].Reference);
                }
                pidPerformance.pidPerformanceXYP.FeedData(did++);
            robot.sync3DView();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pidPerformance.Setup(robot);
        }

        private void importSplineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new RotatingBezierSplineEditor.TraceAnalyzer().Show();   
        }

        private void routePlannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new RotatingBezierSplineEditor.MainForm().Show();
        }
    }
}
