using Physics;
using RoboSim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drogon3
{
    public partial class RoutePlanner : UserControl
    {
        private SphericalRobot robot;
        public RoutePlanner()
        {
            InitializeComponent();
        }
        public RoutePlanner(SphericalRobot robot):this()
        {
            robot.BeforeThreadStep += Robot_BeforeThreadStep;
            this.robot = robot;
            int yo = solveB.Bottom + 10;
            int xo = x.Left, dx = y.Left - x.Left;
            //var currentOutputs = new SpeedChangingTextBox[robot.Actuators.Length];
            //for (int i = 0; i < robot.Actuators.Length; i++)
            //{
            //    var lb = new Label();
            //    lb.Text = "A" + i;
            //    lb.AutoSize = false;
            //    lb.Width = x.Width;
            //    lb.Height = 15;
            //    lb.Left = xo;
            //    lb.Top = yo;

            //    var tb = new SpeedChangingTextBox();
            //    //tb.ReadOnly = true;
            //    tb.Width = x.Width;
            //    tb.Left = xo;
            //    tb.Top = lb.Bottom + 5;
            //    Controls.Add(lb);
            //    Controls.Add(tb);
            //    currentOutputs[i] = tb;
            //    xo += dx;
            //}
            //Timer t = new Timer();
            //t.Interval = 50;
            //t.Tick += (s, e) =>
            //{
            //    for (int i = 0; i < robot.Actuators.Length; i++)
            //        currentOutputs[i].Text = robot.Actuators[i].Current.ToString();
            //};
            //t.Enabled = true;
            //yo += 50;
            //xo = x.Left;
        }

        private void Robot_BeforeThreadStep(object sender, EventArgs e)
        {
            if (tEnabledRB.Checked)
            {

            }
        }

        private void Robot_AfterThreadStep(object sender, EventArgs e)
        {
            if (tEnabledRB.Checked)
            { 

            }
        }

        DateTime lastTime = DateTime.Now;
        double lastD = 0;
        EulerAngleOrientation CurrentTarget;
        SphericalRobotSolution[] lastSolutions = new SphericalRobotSolution[2];
        private SphericalRobotSolution[] solutions;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (object.ReferenceEquals(CurrentTarget, null)) return;
            var dt = (DateTime.Now - lastTime).TotalSeconds;
            lastTime = DateTime.Now;
            var pos = robot.CurrentEndEffectorPosition;
            var tar = CurrentTarget.Offset;
            var d = (tar - pos).Length;
            pErrorTB.Text = d.ToString();
            var dd = d - lastD;
            var v = dd / dt;
            speedTB.Text = v.ToString();
            lastD = d;
        }

        private void solveB_Click(object sender, EventArgs e)
        {
        }

		//bool SetCourseForXY(double sToTravel, double tAllowed)
		//{
		//	double aMax = 1000;
		//	double vMax = 100;
		//	// Fig 1
		//	double vStart = 0;
		//	double vEnd = 0;

		//	// syms('P2x', 'P2y', 'a_m', 'c1', 'c2', 'v_s', 'v_e', 't_m', 'v_m');
		//	// L1 = P2y == a_m * P2x + v_s;
		//	// L2 = P2y == -a_m * P2x + v_e + a_m * t_m;
		//	// P2 = solve([L1, L2], [P2x, P2y]);

		//	// x2 = P2.P2x;
		//	// y2 = P2.P2y;

		//	double x2 = (vEnd - vStart + aMax * tAllowed) / (2 * aMax);
		//	double y2 = vEnd / 2 + vStart / 2 + (aMax * tAllowed) / 2;

		//	double t_1b = 0, t_2b = 0, t_3b = 0;

		//	if (sToTravel == 0) // hardStop
		//	{
		//	}
		//	else if (y2 > vMax) // trapezium
		//	{
		//		//Serial.println(F("Its a trapezium"));
		//		// Fig 2
		//		double x3 = (vMax - vStart) / aMax;
		//		double y3 = vMax;
		//		double x4 = tAllowed - (vEnd - vMax) / (-aMax);
		//		double y4 = vMax;
		//		double t_1 = x3;
		//		double t_2 = x4 - x3;
		//		double t_3 = tAllowed - x4;
		//		double a1 = 0.5 * t_1 * (vMax - vStart);
		//		double a2 = t_1 * vStart;
		//		double a3 = t_2 * vMax;
		//		double a4 = 0.5 * t_3 * (vMax - vEnd);
		//		double a5 = t_3 * vEnd;
		//		double area = a1 + a2 + a3 + a4 + a5;
		//		if (area < sToTravel)
		//		{
		//			//Serial.println(F("Travel not possible 1"));
		//			if (!dontApply)
		//			{
		//				double additionalTimeNeeded = (sToTravel - area) / vMax;
		//				if (isNeg)
		//					sToTravel = -sToTravel;
		//				return SetCourseForXY(channel, sToTravel, tAllowed + additionalTimeNeeded, dontApply);
		//			}
		//			else
		//				return false;
		//		}
		//		// find out the maxV we are going to use
		//		// syms('a_1', 'a_2', 'a_3', 'a_4', 'a_5', '_vTravel', 'sTravel', 't_1', 't_2', 't_3')
		//		// ee6 = 0.5 * t_1 * (_vTravel - v_s) + t_1 * v_s + t_2 * _vTravel + 0.5 * t_3 * (_vTravel - v_e) + t_3 * v_e == sTravel;
		//		// solve(ee6, _vTravel)
		//		double _vTravel = vEnd / 2 + vStart / 2 + (aMax * tAllowed) / 2 - sqrt(aMax * aMax * tAllowed * tAllowed + 2 * aMax * tAllowed * vEnd + 2 * aMax * tAllowed * vStart - 4 * sToTravel * aMax - vEnd * vEnd + 2 * vEnd * vStart - vStart * vStart) / 2;

		//		// recalculate the trapezium
		//		double x3b = (_vTravel - vStart) / aMax;
		//		double y3b = _vTravel;
		//		double x4b = tAllowed - (vEnd - _vTravel) / (-aMax);
		//		double y4b = _vTravel;
		//		t_1b = x3b;
		//		t_2b = x4b - x3b;
		//		t_3b = tAllowed - x4b;
		//			vTravelx = _vTravel;
		//		else if (channel == 1)
		//			vTravely = _vTravel;
		//	}
		//	else// its a triangle
		//	{
		//		//Serial.println(F("Its a triangle"));
		//		// lets see if acc or dec is possible
		//		if (vEnd > vStart)
		//		{
		//			if ((vEnd - vStart) / tAllowed > aMax)
		//			{
		//				//Serial.println(F("Travel not possible 2"));
		//				double timeNeeded = (vEnd - vStart) / aMax;
		//				if (!dontApply)
		//				{
		//					if (isNeg)
		//						sToTravel = -sToTravel;
		//					return SetCourseForXY(channel, sToTravel, timeNeeded, dontApply);
		//				}
		//				return false;
		//			}
		//		}
		//		else
		//		{
		//			if ((vEnd - vStart) / tAllowed < -aMax)
		//			{
		//				//Serial.println(F("Travel not possible 3"));
		//				double timeNeeded = (vEnd - vStart) / -aMax;
		//				if (!dontApply)
		//				{
		//					if (isNeg)
		//						sToTravel = -sToTravel;
		//					return SetCourseForXY(channel, sToTravel, timeNeeded, dontApply);
		//				}
		//				return false;
		//				return false;
		//			}
		//		}
		//		// figure 3
		//		double a1 = 0.5 * x2 * (y2 - vStart);
		//		double a2 = x2 * vStart;
		//		double a3 = 0.5 * (tAllowed - x2) * (y2 - vEnd);
		//		double a4 = (tAllowed - x2) * vEnd;
		//		double area = a1 + a2 + a3 + a4;
		//		if (area < sToTravel)
		//		{
		//			//Serial.println(F("Travel not possible 4"));
		//			if (dontApply)
		//				return false;
		//			double areaMax = (vMax * vMax - vStart * vStart) / 2 / aMax + (vEnd * vEnd - vMax * vMax) / 2 / -aMax;
		//			if (areaMax >= sToTravel)
		//			{
		//				double travelLeft = sToTravel - area;
		//				double additionalTime1 = -(y2 - sqrt(y2 * y2 + 2 * aMax * travelLeft)) / aMax;
		//				if (isNeg)
		//					sToTravel = -sToTravel;
		//				return SetCourseForXY(channel, sToTravel, tAllowed + additionalTime1 * 1.05, dontApply);
		//			}
		//			else
		//			{
		//				double tInTriangle = (vMax - vStart) / aMax + (vEnd - vMax) / -aMax;
		//				double tAtvMax = (sToTravel - areaMax) / vMax;
		//				if (isNeg)
		//					sToTravel = -sToTravel;
		//				return SetCourseForXY(channel, sToTravel, tAtvMax + tInTriangle, dontApply);
		//			}
		//		}
		//		double _vTravel = vEnd / 2 + vStart / 2 + (aMax * tAllowed) / 2 - sqrt(aMax * aMax * tAllowed * tAllowed + 2 * aMax * tAllowed * vEnd + 2 * aMax * tAllowed * vStart - 4 * sToTravel * aMax - vEnd * vEnd + 2 * vEnd * vStart - vStart * vStart) / 2;
		//		// vTravel2 = vEnd / 2 + vStart / 2 + (aMax * tAllowed) / 2 + (aMax ^ 2 * tAllowed ^ 2 + 2 * aMax * tAllowed * vEnd + 2 * aMax * tAllowed * vStart - 4 * sToTravel * aMax - vEnd ^ 2 + 2 * vEnd * vStart - vStart ^ 2) ^ (1 / 2) / 2
		//		// calculate the trapezium
		//		double x3b = (_vTravel - vStart) / aMax;
		//		double y3b = _vTravel;
		//		double x4b = tAllowed - (vEnd - _vTravel) / (-aMax);
		//		double y4b = _vTravel;
		//		t_1b = x3b;
		//		t_2b = x4b - x3b;
		//		t_3b = tAllowed - x4b;
		//		if (!dontApply)
		//		{
		//			if (channel == 0)
		//				vTravelx = _vTravel;
		//			else if (channel == 1)
		//				vTravely = _vTravel;
		//		}
		//	}
		//	if (!dontApply)
		//	{
		//		if (channel == 0)
		//		{
		//			if (isNeg)
		//				targetXPosition = currentPositions[0] - sToTravel * mmPerStep[0];
		//			else
		//				targetXPosition = currentPositions[0] + sToTravel * mmPerStep[0];
		//		}
		//		else if (channel == 0)
		//		{
		//			if (isNeg)
		//				targetYPosition = currentPositions[1] - sToTravel * mmPerStep[1];
		//			else
		//				targetYPosition = currentPositions[1] + sToTravel * mmPerStep[1];
		//		}
		//		if (sToTravel == 0) // hardStop
		//		{
		//			if (channel == 0)
		//			{
		//				requiredCountx = currentCountx;
		//				// We can now force an overdue.
		//				time0x = (millis() - 1) / 1000.0F;
		//				time1x = time0x;
		//				time2x = time0x;
		//				time3x = time0x;
		//				s3x = currentCountx;
		//			}
		//			else
		//			{
		//				requiredCounty = currentCounty;
		//				// We can now force an overdue.
		//				time0y = (millis() - 1) / 1000.0F;
		//				time1y = time0y;
		//				time2y = time0y;
		//				time3y = time0y;
		//				s3y = currentCounty;
		//			}
		//			//Serial.println(F("Hard Stop"));
		//		}
		//		else
		//		{
		//			if (channel == 0)
		//			{
		//				time0x = millis() / 1000.0F;
		//				time1x = time0x + t_1b;
		//				time2x = time1x + t_2b;
		//				time3x = time2x + t_3b;
		//				if (!isNeg)
		//				{
		//					s0x = currentCountx;
		//					s1x = s0x + 0 + 0.5F * aMax * t_1b * t_1b;
		//					s2x = s1x + vTravelx * t_2b;
		//					s3x = s0x + sToTravel;
		//				}
		//				else
		//				{
		//					s0x = currentCountx;
		//					s1x = s0x - (0 + 0.5F * aMax * t_1b * t_1b);
		//					s2x = s1x - vTravelx * t_2b;
		//					s3x = s0x - sToTravel;
		//				}
		//				reversex = isNeg;
		//				lastControllerMicrosx = micros();
		//			}
		//			else if (channel == 1)
		//			{
		//				time0y = millis() / 1000.0F;
		//				time1y = time0y + t_1b;
		//				time2y = time1y + t_2b;
		//				time3y = time2y + t_3b;
		//				if (!isNeg)
		//				{
		//					s0y = currentCounty;
		//					s1y = s0y + 0 + 0.5F * aMax * t_1b * t_1b;
		//					s2y = s1y + vTravely * t_2b;
		//					s3y = s0y + sToTravel;
		//				}
		//				else
		//				{
		//					s0y = currentCounty;
		//					s1y = s0y - (0 + 0.5F * aMax * t_1b * t_1b);
		//					s2y = s1y - vTravely * t_2b;
		//					s3y = s0y - sToTravel;
		//				}
		//				reversey = isNeg;
		//				lastControllerMicrosy = micros();
		//			}
		//		}
		//	}
		//	/*if (channel == 0)
		//	{
		//		Serial.print(F("applicaitonChannel ")); Serial.println(channel);
		//		Serial.print(F("reverse ")); Serial.println(reversex);
		//		Serial.print(F("vTravel ")); Serial.println(vTravelx);
		//		Serial.print(F("time0 ")); Serial.println(time0x);
		//		Serial.print(F("time1 ")); Serial.println(time1x);
		//		Serial.print(F("time2 ")); Serial.println(time2x);
		//		Serial.print(F("time3 ")); Serial.println(time3x);
		//		Serial.print(F("s0 ")); Serial.println(s0x);
		//		Serial.print(F("s1 ")); Serial.println(s1x);
		//		Serial.print(F("s2 ")); Serial.println(s2x);
		//		Serial.print(F("s3 ")); Serial.println(s3x);
		//	}
		//	else if (channel == 1)
		//	{
		//		Serial.print(F("applicaitonChannel ")); Serial.println(channel);
		//		Serial.print(F("reverse ")); Serial.println(reversey);
		//		Serial.print(F("vTravel ")); Serial.println(vTravely);
		//		Serial.print(F("time0 ")); Serial.println(time0y);
		//		Serial.print(F("time1 ")); Serial.println(time1y);
		//		Serial.print(F("time2 ")); Serial.println(time2y);
		//		Serial.print(F("time3 ")); Serial.println(time3y);
		//		Serial.print(F("s0 ")); Serial.println(s0y);
		//		Serial.print(F("s1 ")); Serial.println(s1y);
		//		Serial.print(F("s2 ")); Serial.println(s2y);
		//		Serial.print(F("s3 ")); Serial.println(s3y);
		//	}*/
		//	lastVelocityControlMillis = millis() - 1;
		//	lastVelocityControlMillis = millis() - 1;
		//	return true;
		//}
	}
}
