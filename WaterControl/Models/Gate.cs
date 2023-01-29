using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WaterControl.Data
{
    public class Gate
    {
        private int[] _upOnBit = new int[] { 0, 2, 4, 6, 10, 12, 14, 16, 20, 22, 24 };
        private int[] _upCheckBit = new int[] { 200, 202, 204, 206, 210, 212, 214, 216, 220, 222, 224 };
        private int[] _upOffBit = new int[] { 200, 202, 204, 206, 210, 212, 214, 216, 220, 222, 224 };
        private int[] _upStop = new int[] { 100, 102, 104, 106, 110, 112, 114, 116, 120, 122, 124 };
        private int[] _downOnBit = new int[] { 1, 3, 5, 7, 11, 13, 15, 17, 21, 23, 25 };
        private int[] _downCheckBit = new int[] { 201, 203, 205, 207, 211, 213, 215, 217, 221, 223, 225 };
        private int[] _downOffBit = new int[] { 201, 203, 205, 207, 211, 213, 215, 217, 221, 223, 225 };
        private int[] _downStop = new int[] { 101, 103, 105, 107, 111, 113, 115, 117, 121, 123, 125 };

        public int Id { get; set; } 
        public string GateName { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsEnebled { get; set; } = true;
        public int EncoderID { get; set; } 
        public int EncoderValue { get; set; } = 0;
        public bool GateBlock { get; set; } = true;
        public bool Remote { get; set; } = true;
        public bool Local { get; set; } = false;
        [Range(0, 10, ErrorMessage = "Qiymat 0 va 10 oralig'ida bo'lishi shart")]
        public int PLCOrder { get; set; }
        public int IdPLC { get; set; } = 28;
        public int DownOnBit
        {
            get { return _downOnBit[this.PLCOrder]; }
            set { _downOnBit[this.PLCOrder] = value; }
        }
        public int DownOffBit
        {
            get { return _downOffBit[this.PLCOrder]; }
            set { _downOffBit[this.PLCOrder] = value; }
        }
        public int UpOnBit
        {
            get { return _upOnBit[this.PLCOrder]; }
            set { _upOnBit[this.PLCOrder] = value; }
        }
        public int UpCheckBit
        {
            get { return _upCheckBit[this.PLCOrder]; }
            set { _upCheckBit[this.PLCOrder] = value; }
        }
        public int DownCheckBit
        {
            get { return _downCheckBit[this.PLCOrder]; }
            set { _downCheckBit[this.PLCOrder] = value; }
        }
        public int UpOffBit
        {
            get { return _upOffBit[this.PLCOrder]; }
            set { _upOffBit[this.PLCOrder] = value; }
        }
        public int UpStop
        {
            get { return _upStop[this.PLCOrder]; }
            set { _upStop[this.PLCOrder] = value; }
        }
        public int DownStop
        {
            get { return _downStop[this.PLCOrder]; }
            set { _downStop[this.PLCOrder] = value; }
        }
    }
}
