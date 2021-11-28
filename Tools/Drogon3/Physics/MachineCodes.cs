using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSim
{
    public class MachineProgram
    {
        public int Index { get; set; } = -1;
        public List<MachineCode> Codes { get; set; } = new List<MachineCode>();
        public double TotalTimeSpent = 0;
        public GCode LastSolvedTarget { get; set; } = null;

        public void Begin()
        {
            Index = 0;
        }
        public MachineCode UnderWorking
        {
            get
            {
                if (Index < 0 || Index >= Codes.Count) return null;
                return Codes[Index];
            }
        }
    }
    public class ToolChangeCommand : MachineCode
    {
        public double ToolSize { get; set; }
        public ToolChangeCommand(double toolSize)
        {
            this.ToolSize = toolSize;
        }
    }
    public class MachineCode
    {
        
    }
    public class GCode:MachineCode
    {
    }
    public class G90 : GCode
    {
        public EulerAngleOrientation Target { get; set; }
        public double FeedRate { get; set; }
        //public double AllocatedTime { get; set; }
        public override string ToString()
        {
            return "G90 X" + Target.X + " Y" + Target.Y + " Z" + Target.Z + " A" + Target.A + " B" + Target.B + " G" + Target.G;
        }
    }
}
