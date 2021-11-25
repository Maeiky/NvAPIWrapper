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
using static NvAPIWrapper.Native.GPU.Structures.PerformanceStates20ClockEntryV1;

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

        private void btnSelected_Click(object sender,EventArgs e) {
          int val_core =  0;
          int val_mem =  0;
          int.TryParse(  tbOverclock_Mem.Text , out val_core);
          int.TryParse(  tbOverclock_Mem.Text , out val_mem);

           PhysicalGPU[] aGPU =  PhysicalGPU.GetPhysicalGPUs();
          foreach (PhysicalGPU gpu in aGPU) { 
                
               // gpu.
           IPerformanceStates20Info state =  GPUApi.GetPerformanceStates20(gpu.Handle);

          //PerformanceStates20InfoV3 state = new  PerformanceStates20InfoV3();
          

          //    int freq = 
    
           IPerformanceStates20ClockEntry[] entry = state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced];
            

           IPerformanceStates20ClockDependentSingleFrequency singfreq = entry[1].SingleFrequency;
           uint freq =  singfreq.FrequencyInkHz;


          entry[1].SingleFrequency =  new PerformanceStates20ClockDependentSingleFrequency(8052525);

           state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced][1].SingleFrequency =  new PerformanceStates20ClockDependentSingleFrequency(8052525);
          // state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced].SetValue(entry[1],1 );
           state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced].Clone();
        state.Clocks.Clear();


          state.Clocks = new Dictionary<NvAPIWrapper.Native.GPU.PerformanceStateId, IPerformanceStates20ClockEntry[]>();
        //8052525
        //8100000

        state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced] = null;
        //  GPUApi.SetPerformanceStates20(gpu.Handle, state);

      //  state.Clocks.Clear();

        state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced] = entry;
        


      // entry[1] = new IPerformanceStates20ClockEntry();


      //  PerformanceStates20ClockDependentSingleFrequency f = new PerformanceStates20ClockDependentSingleFrequency(80000);

       // state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced][1].SingleFrequency = new IPerformanceStates20ClockDependentSingleFrequency(800000);

        //[1].SingleFrequency
                break;
          }

         




            
        }
    }
}



/*
 * It's now possible to change these setting using the low-level APIs. The reason why all these configurations are not available directly from the high-level API is the fact that it takes a lot of time to wrap all and every one of them as well as the fact that majority of these functions are private and therefore I am not particularly eager to port them all the way.

Overclock and overvoltage
To get clock speed you can try the high level GPU.PhysicalGPU.GPUPerformanceStatesInformation property and to change these settings you should be able to use Native.GPUApi.GetPerformanceStates20() and Native.GPUApi.SetPerformanceStates20() methods.

Pascal+ overclock and overvoltage
In addition to the methods and properties described above; for certain GPUs including Pascal series of GPUs it is possible to retrieve and change the additional available settings. Followings are the methods that might be interesting to you:
Native.GPUApi.GetVFPCurve()
Native.GPUApi.GetClockBoostLock()
Native.GPUApi.SetClockBoostLock()
Native.GPUApi.GetClockBoostTable()
Native.GPUApi.SetClockBoostTable()
Native.GPUApi.GetCoreVoltageBoostPercent()
Native.GPUApi.SetCoreVoltageBoostPercent()
Native.GPUApi.GetClockBoostMask()
Native.GPUApi.GetClockBoostRanges()
Native.GPUApi.GetCurrentVoltage()

Cooler (fan) control
Full cooler control functionalities are available via the high-level API via the GPU.PhysicalGPU.CoolerInformation property.

Power targets
Power target and performance cap status can be monitored using the high-level API available via the GPU.PhysicalGPU.PerformanceControl property.
To change these settings the following low-level methods might be used:
Native.GPUApi.ClientPowerPoliciesGetStatus()
Native.GPUApi.ClientPowerPoliciesSetStatus()

Thermal targets
Thermal target and performance cap status can be monitored using the high-level API available via the GPU.PhysicalGPU.PerformanceControl property.
To change these settings the following low-level methods might be used:
Native.GPUApi.GetThermalPoliciesStatus()
Native.GPUApi.SetThermalPoliciesStatus()
*/