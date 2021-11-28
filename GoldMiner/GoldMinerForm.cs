using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App;
using NvAPIWrapper;
using NvAPIWrapper.Display;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Mosaic;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.GPU;
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
           
/*
 * 
 for those interested, I found how to manually tweak in the start.bat with
nvidia-smi -i 0 --lock-gpu-clocks=minimuclock,maxclock
before the line starting the miner of course.
Here's my actual settings, I would appreciate if testing people could share theirs so we can help to improve each others:
#reset all core settings
nvidia-smi -rgc

  /// nvidia-smi -i 0 -pl 125
  /// 


    nvidia-persistenced --persistence-mode  #first run deamon
nvidia-smi -pm ENABLED  #2nd enable persistenced mode
nvidia-smi -pl 130  #limit TDP if u want

        //nvidia-smi -i 0 --lock-gpu-clocks=1140,1140
 // nvidia-smi -i 0  --lock-memory-clocks=8100,8100 -> nmot work
-lmc 
 */





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


        private bool overclock_gpu(int gpu_id, int val_core) {

             if (val_core < 500 || val_core > 9000) {
                Log.print("Invalid Core clock");
                return false;
            }
            int core_min = val_core;
            int core_max = val_core;

            LauchTool smi= new LauchTool();
            smi.bRunInThread = false;
            smi.bWaitEndForOutput = true;
            smi.bReturnBoth = true;
            smi.bHidden = false;

            string _result = smi.fLauchExe("cmd.exe", "/C nvidia-smi -i " + gpu_id + " --lock-gpu-clocks=" + core_min  + "," + core_max + "");
            Log.print(_result);
            //TODO detect failure
            return true;
        }

          private bool overclock_mem(int gpu_id, int val_mem) {

             if (val_mem < 500 || val_mem > 9000) {
                Log.print("Invalid mem clock");
                return false;
            }

            LauchTool NVoc= new LauchTool();
            NVoc.bRunInThread = false;
            NVoc.bWaitEndForOutput = true;
            NVoc.bReturnBoth = true;
            NVoc.bHidden = false;

            string[] _result = NVoc.fLauchExe("NVoc.exe", gpu_id + " 0 "  + val_mem).Split('\n');
            //string _result = NVoc.fLauchExe("cmd.exe", "/C nvidia-smi -i " + gpu_id + " --lock-gpu-clocks=" + core_min  + "," + core_max + "");
            foreach (string res in _result) {
                Log.print(res);
                if (res.IndexOf("VRAM OC OK") != -1) {
                    return true;
                }
            }
                   
            return false;
        }


        private void btnSelected_Click(object sender,EventArgs e) { 
            int val_core =  0;
            int val_mem =  0;
            int.TryParse(  tbOverclock_Core.Text , out val_core);
            int.TryParse(  tbOverclock_Mem.Text , out val_mem);
            
            overclock_gpu(0, val_core);
            overclock_mem(0, val_mem);


        }

        private void btnSelected_Click_ollldd(object sender,EventArgs e) { //Not uded

           PhysicalGPU[] aGPU =  PhysicalGPU.GetPhysicalGPUs();
          foreach (PhysicalGPU gpu in aGPU) { 
           IPerformanceStates20Info state =  GPUApi.GetPerformanceStates20(gpu.Handle);

          //PerformanceStates20InfoV3 state = new  PerformanceStates20InfoV3();
          


        state.Clocks[PerformanceStateId.P2_Balanced][1].SingleFrequency =  new PerformanceStates20ClockDependentSingleFrequency(8052525);



             PrivatePCIeInfoV2 pcieInformation = GPUApi.GetPCIEInfo(gpu.Handle);


            var clocks = state.Clocks;
            var baseVoltages = state.Voltages;


             GPUPerformanceState[]  PerformanceStates = state.PerformanceStates.Select((state20, i) =>
            {
                PCIeInformation statePCIeInfo = null;

                if ( pcieInformation.PCIePerformanceStateInfos.Length > i)
                {
                    statePCIeInfo = new PCIeInformation(pcieInformation.PCIePerformanceStateInfos[i]);
                }

                return new GPUPerformanceState(
                    i,
                    state20,
                    clocks[state20.StateId],
                    baseVoltages[state20.StateId],
                    statePCIeInfo
                );
            }).ToArray();


        /*
        GPUPerformanceState test =  new GPUPerformanceState(
                    i,
                    state20,
                    clocks[state20.StateId],
                    baseVoltages[state20.StateId],
                    statePCIeInfo
                );

        */
           state.Clocks.Clear(); 
        /*
        Dictionary<PerformanceStateId, PerformanceStates20ClockEntryV1[]> _clk =  state.GetClockV1();

         IPerformanceStates20ClockDependentSingleFrequency singfreq = _clk[PerformanceStateId.P2_Balanced][1].SingleFrequency;
           uint freq =  singfreq.FrequencyInkHz;


        _clk[PerformanceStateId.P2_Balanced][1].SingleFrequency =  new PerformanceStates20ClockDependentSingleFrequency(8052525);

     //  singfreq =  new PerformanceStates20ClockDependentSingleFrequency(8052525);

        
        state.GetClockV1()[PerformanceStateId.P2_Balanced][1].SingleFrequency =  new PerformanceStates20ClockDependentSingleFrequency(8052525);


         state.SetClockV1(_clk);
        */



          break;


          //    int freq = 
    
        /*
          // IPerformanceStates20ClockEntry[] entry = state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced];
           IPerformanceStates20ClockEntry[] entry = _clk[PerformanceStateId.P2_Balanced];
           IPerformanceStates20ClockDependentSingleFrequency singfreq = entry[1].SingleFrequency;
        */

         //  uint freq =  singfreq.FrequencyInkHz;

        /*

          entry[1].SingleFrequency =  new PerformanceStates20ClockDependentSingleFrequency(8052525);

           state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced][1].SingleFrequency =  new PerformanceStates20ClockDependentSingleFrequency(8052525);
          // state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced].SetValue(entry[1],1 );
           state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced].Clone();
        state.Clocks.Clear();

   
          state.Clocks = new Dictionary<NvAPIWrapper.Native.GPU.PerformanceStateId, IPerformanceStates20ClockEntry[]>();
        //8052525
        //8100000

        state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced] = null;     */
        //  GPUApi.SetPerformanceStates20(gpu.Handle, state);

      //  state.Clocks.Clear();

        //state.Clocks[NvAPIWrapper.Native.GPU.PerformanceStateId.P2_Balanced] = entry;
        


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