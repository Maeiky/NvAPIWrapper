using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NvAPIWrapper;
using NvAPIWrapper.Display;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Mosaic;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.GPU.Structures;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace GoldMiner {
    public partial class GoldMinerForm:Form {
        public GoldMinerForm() {
        InitializeComponent();
        }


        private void PrintDriverVersion()
        {
              lbDriver.Text =  "NVIDIA Driver: "  + NVIDIA.DriverBranchVersion  + " [" + NVIDIA.DriverVersion  + "]: " +   NVIDIA.InterfaceVersionString;
        }


        private void GoldMinerForm_Load(object sender,EventArgs e) {
           
             PrintDriverVersion();


         // find bits
           







             PhysicalGPU[] aGPU =  PhysicalGPU.GetPhysicalGPUs();

            foreach (PhysicalGPU gpu in aGPU) { 
                 string tVRAM = "";
                 string tGPU = "";

                ///////// Get Temp / VRAM /////////
                var maxBit = 0;
                for (; maxBit < 32; maxBit++){
                    try { GPUApi.QueryThermalSensors(gpu.Handle, 1u << maxBit);}catch{break;}
                }
                if (maxBit != 0){
                    var temp = GPUApi.QueryThermalSensors(gpu.Handle, (1u << maxBit) - 1);
                    GPUThermalSensor[] GetThermalSettings = gpu.ThermalInformation.ThermalSensors.ToArray();
                   string[]  QueryThermalSensors = temp.Temperatures.Take(maxBit).Select((t) => t.ToString("F2")).ToArray();
                    tVRAM = QueryThermalSensors[QueryThermalSensors.Length-1];
                    tGPU = QueryThermalSensors[0];
                }
                /////////////////////////////////
             
                GPUCooler[] aCoolers =  gpu.CoolerInformation.Coolers.ToArray();
             
                string fan = "Fans: ";
                foreach (GPUCooler cooler in aCoolers) {
                  // fan += cooler.CurrentFanSpeedInRPM + "% ";
                   fan += cooler.CurrentLevel + "% ";
                }
                
              //  gpu.BaseClockFrequencies
               IClockFrequencies clk =  gpu.CurrentClockFrequencies;
               ClockDomainInfo core_clk = clk.ProcessorClock;
               ClockDomainInfo mem_clk = clk.MemoryClock;
               ClockDomainInfo graph_clk = clk.GraphicsClock;
               string _core_clk = core_clk.Frequency.ToString();
               string _mem_clk = mem_clk.Frequency.ToString();
               string _graph_clk = graph_clk.Frequency.ToString();

              // string _core_clk =  gpu.clock;
                

               lsbGPU.Items.Add(  gpu.FullName  + " _core_clk[" + _graph_clk + "]"  + " _mem_clk[" + _mem_clk + "]"  +  " :" + " tGPU:" + tGPU + " tVRAM: " + tVRAM  + " " + gpu.BusInformation  + " : " +  " " + fan +  " " + gpu.ArchitectInformation);

         //   gpu.GPUId
               // lsbGPU.Items.Add(coolers.);
            }
        }

        private void lsbGPU_SelectedIndexChanged(object sender,EventArgs e) {

        }
    }
}
