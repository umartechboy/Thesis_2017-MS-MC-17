using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{
	public class PID
    {
        public PID()
        { }

        public PID(double Kp_, double Ki_, double Kd_)
		{
			Kp = Kp_; Ki = Ki_; Kd = Kd_;
            Reset();
		}
		public double Signal(double setpoint, double pv, double dt)
        {
            // Calculate error
            double error = setpoint - pv;

            // Proportional term
            double Pout = Kp * error;

            // Integral term
            _integral += error * dt;
            // Restrict to Max/Min
            double Iout = Ki * _integral;

            if (Ki != 0)
            {
                if (_integral * Ki > Max)
                    _integral = Max / Ki;
                else if (_integral * Ki < Min)
                    _integral = Min / Ki;
            }
            // Derivative term
            double derivative = (error - _pre_error) / dt;
            double Dout = Kd * derivative;

            // Calculate total output
            double output = Pout + Iout + Dout;

            // Restrict to Max/Min
            if (output > Max)
                output = Max;
            else if (output < Min)
                output = Min;

            // Save error to previous error
            _pre_error = error;

            return output;
        }
		public double Kp = 0, Ki = 0, Kd = 0, Min = 0, Max = 0;
		double _pre_error = 0;
		double _integral = 0;

        public void Reset()
        {
            _integral = 0; _pre_error = 0;
        }
    }
}
