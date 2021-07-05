using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RoboSim;
using Physics;

namespace RoboSim
{
    public class LookBackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new Point3D(0, 0, 0) - (Point3D)value;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWpfPrevWindow : Window
    {
        Model3DGroup TestPoints = new Model3DGroup();
        System.Windows.Forms.Timer rotTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer rotRestarter = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer simTimer = new System.Windows.Forms.Timer();

        public ModelVisual3D InkVisualModel { get; protected set; }
        public ModelVisual3D WorkSpaceVisualModel { get; protected set; }
        Model3DGroup TargetModel = new Model3DGroup();
        public List<EulerAngleOrientation> TestPointLocations { get; set; } = new List<EulerAngleOrientation>();
        public EulerAngleOrientation TargetLocation { get; set; } = new EulerAngleOrientation();
        double hscrollOffset = 0;
        //public Robot robot = new Robot();
        public MainWpfPrevWindow()
        {
            InitializeComponent();
            InkVisualModel = new ModelVisual3D();
            WorkSpaceVisualModel = new ModelVisual3D();
            this.Loaded += MainWindow_Loaded;   
            rotRestarter.Interval = 3000;
            rotRestarter.Tick += restarter_Tick;
            simTimer.Interval = 30;   
        }
                                    
        void restarter_Tick(object sender, EventArgs e)
        {
            rotRestarter.Stop();
            rotRestarter.Enabled = false;
            rotTimer.Enabled = true;
        }
          
        void hscroll_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if (rotTimer.Enabled) 
            //{
            //    rotTimer.Enabled = false;
            //    hscrollOffset = hscroll.Value - rotAngle;
            //    rotRestarter.Enabled = true;
            //    rotRestarter.Start();
            //}               
        }
                                                        
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            rotTimer.Interval = 40;
            rotTimer.Tick += animator_Tick;

            ((Transform3DGroup)((PerspectiveCamera)mainViewPort.Camera).Transform).Children.Add(new RotateTransform3D());
            ((Transform3DGroup)((PerspectiveCamera)mainViewPort.Camera).Transform).Children.Add(new TranslateTransform3D());
            camScroll = (TranslateTransform3D)((Transform3DGroup)((PerspectiveCamera)mainViewPort.Camera).Transform).Children[2];

            hscrollOffset = 200;
            var camRot = (RotateTransform3D)((Transform3DGroup)((PerspectiveCamera)mainViewPort.Camera).Transform).Children[1];
            camRot.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 1), hscrollOffset);
            AxisAngleRotation3D camAltitude = (AxisAngleRotation3D)((RotateTransform3D)((Transform3DGroup)((PerspectiveCamera)mainViewPort.Camera).Transform).Children[0]).Rotation;
            camAltitude.Angle = 23;
            camScroll.OffsetX = -0.49857739115838445;
            camScroll.OffsetY = 1.1349718002492011;
            camScroll.OffsetZ = 0.161759045218913;
            ((PerspectiveCamera)mainViewPort.Camera).FieldOfView = 38.283639439016028;
        }
        public void Intialize(SphericalRobot robot, Ink ink, WorkSpace workspace)
        {
            double robotSize = 3;
            double down = robotSize * .707;
            double up = down + robotSize * .707;

            // initialize visual models
            ModelVisual3D lights = new ModelVisual3D();
            ModelVisual3D robotVisualModel = new ModelVisual3D();
            ModelVisual3D groundVisualModel = new ModelVisual3D();
            ModelVisual3D testPointVisualModel = new ModelVisual3D();
            ModelVisual3D TargetVisualModel = new ModelVisual3D();
            InkVisualModel = new ModelVisual3D();
            WorkSpaceVisualModel = new ModelVisual3D();

            // Make Lights
            Model3DGroup lightsGp = new Model3DGroup();
            DirectionalLight dl1 = new DirectionalLight();
            dl1.Direction = new Vector3D(-1, -1, -1);
            dl1.Color = Color.FromArgb(1, 255, 255, 255);

            DirectionalLight dl2 = new DirectionalLight();
            dl2.Direction = new Vector3D(1, .5, -1);
            dl2.Color = Color.FromArgb(1, 255, 255, 255);

            DirectionalLight dl3 = new DirectionalLight();
            dl3.Direction = new Vector3D(1, .5, 1);
            dl3.Color = Color.FromArgb(1, 100, 100, 100);

            AmbientLight al = new AmbientLight();
            al.Color = Color.FromArgb(1, 1, 1, 1);

            lightsGp.Children.Add(dl1);
            lightsGp.Children.Add(dl2);
            lightsGp.Children.Add(dl3);
            lightsGp.Children.Add(al);
            lights.Content = lightsGp;

            // make world models
            var ground = new Model3DGroup();     
            TargetModel = simCalc.AxisRepMesh(1);
            // make the cross ground
            var lightRedB = new SolidColorBrush(Color.FromRgb(200, 120, 120));
            var lightGreenB = new SolidColorBrush(Color.FromRgb(120, 200, 120));
            lightRedB.Opacity = 0.5;
            lightGreenB.Opacity = 0.5;
            ground.Children.Add(simCalc.meshToGeometry(simCalc.CubeMesh(5, 5, 0.05), lightRedB));
            ground.Children[0].Transform = new TranslateTransform3D(2.5, 2.5, -0.025);
            ground.Children.Add(simCalc.meshToGeometry(simCalc.CubeMesh(5, 5, 0.05), lightGreenB));
            ground.Children[1].Transform = new TranslateTransform3D(2.5, -2.5, -0.025);
            ground.Children.Add(simCalc.meshToGeometry(simCalc.CubeMesh(5, 5, 0.05), lightRedB));
            ground.Children[2].Transform = new TranslateTransform3D(-2.5, -2.5, -0.025);
            ground.Children.Add(simCalc.meshToGeometry(simCalc.CubeMesh(5, 5, 0.05), lightGreenB));
            ground.Children[3].Transform = new TranslateTransform3D(-2.5, 2.5, -0.025);

            ground.Children.Add(simCalc.AxisRepMesh(1));
            ground.Children.Add(TargetModel);

            var b = new SolidColorBrush(Colors.Aqua);
            b.Opacity = 0.5;
            var b2 = new SolidColorBrush(Colors.Red);
            b2.Opacity = 0.5;
            var b3 = new SolidColorBrush(Colors.Green);
            b2.Opacity = 0.5;
            var tp = new GeometryModel3D[] {
                simCalc.meshToGeometry(simCalc.SphereMesh(0.1), b),
                simCalc.meshToGeometry(simCalc.SphereMesh(0.1), b2),
                simCalc.meshToGeometry(simCalc.SphereMesh(0.1), b3)};

            for(int i =0; i < tp.Length;i++)
            {
                TestPoints.Children.Add(tp[i]);
                TestPointLocations.Add(new EulerAngleOrientation());
                tp[i].Transform = TestPointLocations[i];
            }

            robotVisualModel.Content = robot.Model;
            groundVisualModel.Content = ground;
            testPointVisualModel.Content = TestPoints;
            InkVisualModel.Content = ink.Model;
            WorkSpaceVisualModel.Content = workspace.Model;


            //Add visual models to the scene     
            this.mainViewPort.Children.Clear();
            this.mainViewPort.Children.Add(lights);
            this.mainViewPort.Children.Add(robotVisualModel);
            this.mainViewPort.Children.Add(groundVisualModel);
            this.mainViewPort.Children.Add(testPointVisualModel);
            this.mainViewPort.Children.Add(InkVisualModel);
            this.mainViewPort.Children.Add(WorkSpaceVisualModel);


            AxisAngleRotation3D camAltitude = (AxisAngleRotation3D)((RotateTransform3D)((Transform3DGroup)((PerspectiveCamera)mainViewPort.Camera).Transform).Children[0]).Rotation;
            camAltitude.Angle = 0;
        }

        void animator_Tick(object sender, EventArgs e)
        {
            //hscrollOffset += 2;      
        }

        public void refresh(ref SphericalRobot robot)
        {               
            rotTimer.Enabled = true;
            hscroll_ValueChanged(null, null);   
        }

        bool mouseIsDown = false, mouseOnObject = false;  
        Point lastMovePoint = new Point();
        private void mainViewPort_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.ScrollAll;
            mouseOnObject = true;
        }

        private void mainViewPort_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!mouseIsDown)
                Cursor = Cursors.Arrow;
            mouseOnObject = false;
        }

        private void mainViewPort_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseIsDown = true;

        }
        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseIsDown = false;
            if (!mouseOnObject)
                Cursor = Cursors.Arrow;
        }


        TranslateTransform3D camScroll = null;
        private void Border_MouseMove(object sender, MouseEventArgs e)
        {                              
            var p = e.GetPosition(mainViewPort);
            if (mouseIsDown)
            {
                AxisAngleRotation3D camAltitude = (AxisAngleRotation3D)((RotateTransform3D)((Transform3DGroup)((PerspectiveCamera)mainViewPort.Camera).Transform).Children[0]).Rotation;
                AxisAngleRotation3D camAzimuth = (AxisAngleRotation3D)((RotateTransform3D)((Transform3DGroup)((PerspectiveCamera)mainViewPort.Camera).Transform).Children[1]).Rotation;
                double cAzimuth = camAzimuth.Angle * Math.PI / 180;
                double cAltitude = camAltitude.Angle * Math.PI / 180;
                double dx = (p.X - lastMovePoint.X);
                double dy = (p.Y - lastMovePoint.Y);
                var zoom = ((PerspectiveCamera)mainViewPort.Camera).FieldOfView;
                if (Keyboard.IsKeyDown(Key.LeftShift))
                {
                    var scale = 1 / Math.Sin(zoom * Math.PI / 180) * mainViewPort.ActualWidth / 2000;
                    camAltitude.Angle += dy / scale;
                    hscrollOffset += -dx / scale;
                }
                else if(Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    var scale = 1 / Math.Sin(zoom * Math.PI / 180) * mainViewPort.ActualWidth / 2000;
                    camAzimuth.Angle -= dx / scale;
                }
                else if (Keyboard.IsKeyDown(Key.LeftAlt))
                {
                    var scale = 1 / Math.Sin(zoom * Math.PI / 180) * mainViewPort.ActualWidth / 2000;
                    camAzimuth.Angle -= dx / scale;
                    camAltitude.Angle += dy / scale;
                    hscrollOffset += -dx / scale;  
                }
                else
                {                    
                    var scale = 1 / Math.Sin(zoom * Math.PI / 180) * mainViewPort.ActualWidth / 13.78;
                    //double scale = 100;
                    double r = Math.Sqrt(dx * dx + dy * dy) / scale;

                    var slope = Math.Atan2(dy, dx);
                    camScroll.OffsetX -=
                        r * Math.Cos(slope) * Math.Sin(cAzimuth) -
                        r * Math.Sin(slope) * Math.Cos(cAzimuth) * Math.Sin(cAltitude);

                    camScroll.OffsetY -=
                        -r * Math.Sin(slope) * Math.Sin(cAzimuth) * Math.Sin(cAltitude)
                        - r * Math.Cos(slope) * Math.Cos(cAzimuth);
                    camScroll.OffsetZ -= -r * Math.Cos(cAltitude) * Math.Sin(slope);

                }
            }
            lastMovePoint = p;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double v = Math.Tan(e.Delta / (double)6000 * Math.PI / 2) * 100;
            double newValue = ((PerspectiveCamera)mainViewPort.Camera).FieldOfView - v;
            if (newValue < 1)
                newValue = 1;
            if (newValue > 90)
                newValue = 90;
            ((PerspectiveCamera)mainViewPort.Camera).FieldOfView = newValue;
        }

        private void mainViewPort_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.ScrollAll;
            mouseOnObject = true;
        }
    }
}