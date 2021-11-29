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




        




        public class GPUobj {
            public PhysicalGPU gpu;
            public Label id;
            public Label name;
            public Label tgpu;
            public Label tvram;
            public Label fan;
            public Label core;
            public Label vram;
            public ComboBox miner;
            public ComboBox wallet;
            public Label crypto;
            public Label pool_fee;
            public Label miner_fee;
            
        }
       List<GPUobj> aGPU = new List<GPUobj>();
        private bool bFinishUpdateAllGPU = true;


        public int CreateGPU(int id, int offsetY, PhysicalGPU gpu) {
            GPUobj g = new GPUobj();
            aGPU.Add(g);
            g.gpu = gpu;

            int posX=   5;
            int posY=  offsetY + 5;

             int curr_offX=   posX;

            ///ID////
            g.id = new Label(); 
            g.id.Text = id.ToString();
            g.id.Location = new Point(posX, posY);
        
            g.id.Font = new Font("Calibri", 18);
            g.id.ForeColor = Color.Green;
            g.id.Padding = new Padding(0);
            g.id.AutoSize= true;
            gbGPUList.Controls.Add(g.id);
            curr_offX += 20;
             //////////

             ///Name////
            g.name = new Label(); 
            g.name.Text = "GPU name";
            g.name.Location = new Point(curr_offX, posY);
        
            g.name.Font = new Font("Calibri", 12);
            g.name.ForeColor = Color.Black;
            g.name.Padding = new Padding(0);
            g.name.AutoSize= true;
            gbGPUList.Controls.Add(g.name);
            curr_offX += 150;
             //////////
  
             ///GPU temp////
            g.tgpu = new Label(); 
            g.tgpu.Text = "tGPU";
            g.tgpu.Location = new Point(curr_offX, posY-8);
        
            g.tgpu.Font = new Font("Calibri", 10);
            g.tgpu.ForeColor = Color.Black;
            g.tgpu.Padding = new Padding(0);
            g.tgpu.AutoSize= true;
            gbGPUList.Controls.Add( g.tgpu);
             //////////
             

           ///VRAM temp////
            g.tvram = new Label(); 
            g.tvram.Text = "tVRAM";
            g.tvram.Location = new Point(curr_offX, posY+5);
        
            g.tvram.Font = new Font("Calibri", 10);
            g.tvram.ForeColor = Color.Black;
            g.tvram.Padding = new Padding(0);
            g.tvram.AutoSize= true;
            gbGPUList.Controls.Add( g.tvram);
            curr_offX += 100;
            //////////
             
        
            ///Fan temp////
            g.fan = new Label(); 
            g.fan.Text = "Fan";
            g.fan.Location = new Point(curr_offX, posY+5);
        
            g.fan.Font = new Font("Calibri", 10);
            g.fan.ForeColor = Color.Black;
            g.fan.Padding = new Padding(0);
            g.fan.AutoSize= true;
            gbGPUList.Controls.Add( g.fan);
            curr_offX += 100;
            //////////
            
            
            ///CoreClk ////
            g.core = new Label(); 
            g.core.Text = "Fan";
            g.core.Location = new Point(curr_offX, posY);
        
            g.core.Font = new Font("Calibri", 12);
            g.core.ForeColor = Color.Black;
            g.core.Padding = new Padding(0);
            g.core.AutoSize= true;
            gbGPUList.Controls.Add( g.core);
             curr_offX += 100;
            //////////
            g.vram = new Label(); 
            g.vram.Text = "Fan";
            g.vram.Location = new Point(curr_offX, posY);
        
            g.vram.Font = new Font("Calibri", 12);
            g.vram.ForeColor = Color.Black;
            g.vram.Padding = new Padding(0);
            g.vram.AutoSize= true;
            gbGPUList.Controls.Add( g.vram);
            curr_offX += 100;


            ///Miner////
            g.miner = new ComboBox (); 
            g.miner.Text = "NbMiner";
            g.miner.Items.Add("NbMiner");

            g.miner.Location = new Point(curr_offX, posY+5);
        
            g.miner.Font = new Font("Calibri", 10);
            g.miner.ForeColor = Color.Black;
            g.miner.Width = 100;
            gbGPUList.Controls.Add( g.miner);

            curr_offX += 100;
            //////////
            
             ///wallet////
            g.miner = new ComboBox (); 
            g.miner.Text = "wallet";
            g.miner.Items.Add("wallet");

            g.miner.Location = new Point(curr_offX, posY+5);
        
            g.miner.Font = new Font("Calibri", 10);
            g.miner.ForeColor = Color.Black;
            g.miner.Width = 100;
            gbGPUList.Controls.Add( g.miner);

            curr_offX += 100;
            //////////
            ///

            ///Crypto ////
            g.crypto = new Label(); 
            g.crypto.Text = "Crypto";
            g.crypto.Location = new Point(curr_offX, posY+5);
        
            g.crypto.Font = new Font("Calibri", 10);
            g.crypto.ForeColor = Color.Black;
            g.crypto.Padding = new Padding(0);
            g.crypto.AutoSize= true;
            gbGPU.Controls.Add( g.crypto);
            curr_offX += 80;
            //////////
                
            ///Pool fee ////
            g.pool_fee = new Label(); 
            g.pool_fee.Text = "Crypto";
            g.pool_fee.Location = new Point(curr_offX, posY-8);
        
            g.pool_fee.Font = new Font("Calibri", 10);
            g.pool_fee.ForeColor = Color.Black;
            g.pool_fee.Padding = new Padding(0);
            g.pool_fee.AutoSize= true;
            gbGPUList.Controls.Add( g.pool_fee);
            //////////
            ///
            ///Miner fee ////
            g.miner_fee = new Label(); 
            g.miner_fee.Text = "Crypto";
            g.miner_fee.Location = new Point(curr_offX, posY+5);
        
            g.miner_fee.Font = new Font("Calibri", 10);
            g.miner_fee.ForeColor = Color.Black;
            g.miner_fee.Padding = new Padding(0);
            g.miner_fee.AutoSize= true;
            gbGPUList.Controls.Add( g.miner_fee);
            curr_offX += 80;
            //////////

            return offsetY+30;
        }



        private void GoldMinerForm_Load(object sender,EventArgs e) {
           
             PrintDriverVersion();
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

             int offset = 20;
             int i = 0;
            foreach (PhysicalGPU gpu in aGPU) { 
                
                offset = CreateGPU(i, offset, gpu);
                i++;


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
               IClockFrequencies base_clk =  gpu.BaseClockFrequencies;
               IClockFrequencies boost_clk =  gpu.BoostClockFrequencies;


            //   IPerformanceStates20Info state =  GPUApi.GetPerformanceStates20(gpu.Handle);

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

            PoolAllGPU();

        }

        private void PoolAllGPU() {
           Thread winThreadUpdate = new Thread(new ThreadStart(() => {
                while (true) { 
                    /////////
                    ///
                    UpdateAllGPU();

                     //////
                    Thread.Sleep(500);
                }
            }));
            winThreadUpdate.Start();

        }

        private void UpdateAllGPU() {

            if (bFinishUpdateAllGPU) { 
                bFinishUpdateAllGPU=false;
                PhysicalGPU[] aGPU =  PhysicalGPU.GetPhysicalGPUs();
                 int i = 0;
                    foreach (PhysicalGPU gpu in aGPU) {
                         ///////// Get Temp / VRAM /////////
                        string tVRAM = "";
                        string tGPU = "";
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
             
                        string fan = "Fan: ";
                        foreach (GPUCooler cooler in aCoolers) {
                            // fan += cooler.CurrentFanSpeedInRPM + "% ";
                            fan += cooler.CurrentLevel + "% ";
                        }
                
                        //  gpu.BaseClockFrequencies
                        IClockFrequencies base_clk =  gpu.BaseClockFrequencies;
                        IClockFrequencies boost_clk =  gpu.BoostClockFrequencies;

                        IClockFrequencies clk =  gpu.CurrentClockFrequencies;
                        ClockDomainInfo core_clk = clk.ProcessorClock;
                        ClockDomainInfo mem_clk = clk.MemoryClock;
                        ClockDomainInfo graph_clk = clk.GraphicsClock;
                        string _core_clk = core_clk.Frequency.ToString();

                        double fmem_clk =  Math.Round(mem_clk.Frequency/1000.0);
                        double fgraph_clk=  Math.Round(graph_clk.Frequency/1000.0);

                        string _mem_clk = fmem_clk.ToString();
                        string _graph_clk = fgraph_clk.ToString();
                        
                        string name =  gpu.FullName.Replace("NVIDIA ", "");

                        UpdateGPU(i, name, tGPU,tVRAM, fan, _graph_clk, _mem_clk );

                        i++;
                    }
                    
                    bFinishUpdateAllGPU =true;
            }
        }



        public void UpdateGPU(int id, string name, string tGPU, string tVRAM, string fan, string _graph_clk, string _mem_clk) {
            this.BeginInvoke((MethodInvoker)delegate {
                GPUobj g = aGPU[id];
 
            //   gbGPUList.Location = new Point(  gbGPUList.Location.X,   gbGPUList.Location.Y - 1);  
        
                g.name.Text = name;
                g.tgpu.Text =    "GPU: " +tGPU;
                g.tvram.Text =   "VRAM: " +tVRAM;
                g.fan.Text =   fan;
                g.core.Text =  "Core: "+ _graph_clk;
                g.vram.Text =   "Mem: " + _mem_clk;
                g.crypto.Text =   "ETH: 0MH" ;
                g.miner_fee.Text =  "Pool_fee: 0%" ;
                g.pool_fee.Text =   "Miner_fee: 0%" ;
            });
        }


        private void lsbGPU_SelectedIndexChanged(object sender,EventArgs e) {

        }

        private string[] LaunchSmi(string arg) {
            LauchTool smi= new LauchTool();
            smi.bRunInThread = false;
            smi.bWaitEndForOutput = true;
            smi.bReturnBoth = true;
            smi.bHidden = true;

            string[] _result =  smi.fLauchExe("cmd.exe", "/C nvidia-smi " + arg).Split('\n');

            foreach (string res in _result) {
                Log.print(res);
            }
            return _result;
        }

        
        private string[] LaunchNVoc(string arg) {
            LauchTool NVoc= new LauchTool();
            NVoc.bRunInThread = false;
            NVoc.bWaitEndForOutput = true;
            NVoc.bReturnBoth = true;
            NVoc.bHidden = true;

            string[] _result = NVoc.fLauchExe("NVoc.exe", arg).Split('\n');

            foreach (string res in _result) {
                Log.print(res);
            }
            return _result;
        }


        private void btReset_Click(object sender,EventArgs e) {
            //-r, --gpu-reset //hard reset on linux?
            //nvidia-smi -rgc //reset all core

           LaunchSmi("-rgc");
        }
        private bool overclock_gpu(int gpu_id, int val_core) {

             if (val_core < 500 || val_core > 9000) {
                Log.print("Invalid Core clock");
                return false;
            }
            int core_min = val_core;
            int core_max = val_core;
            
            LaunchSmi("-i " + gpu_id + " --lock-gpu-clocks=" + core_min  + "," + core_max );

            return true;
        }

          private bool overclock_mem(int gpu_id, int val_mem) {

             if (val_mem < 500 || val_mem > 9000) {
                Log.print("Invalid mem clock");
                return false;
            }
          
            string[] _result = LaunchNVoc( gpu_id + " 0 "  + val_mem);

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

        private void groupBox1_Enter(object sender,EventArgs e) {

        }

        private void GoldMinerForm_FormClosing(object sender,FormClosingEventArgs e) {
          Hide();
        }

        private void GoldMinerForm_FormClosed(object sender,FormClosedEventArgs e) {
          System.Environment.Exit(1);
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