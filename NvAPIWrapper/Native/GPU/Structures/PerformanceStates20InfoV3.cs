using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;
using static NvAPIWrapper.Native.GPU.Structures.PerformanceStates20ClockEntryV1;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <inheritdoc cref="IPerformanceStates20Info" />
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(3)]
    public struct PerformanceStates20InfoV3 : IInitializable, IPerformanceStates20Info
    {
        internal const int MaxPerformanceStates = PerformanceStates20InfoV2.MaxPerformanceStates;

        internal const int MaxPerformanceStates20BaseVoltages =
            PerformanceStates20InfoV2.MaxPerformanceStatesBaseVoltages;

        internal StructureVersion _Version;
        internal uint _Flags;
        internal uint _NumberOfPerformanceStates;
        internal uint _NumberOfClocks;
        internal uint _NumberOfBaseVoltages;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxPerformanceStates)]
        internal PerformanceStates20InfoV1.PerformanceState20[] _PerformanceStates;

        internal PerformanceStates20InfoV2.PerformanceStates20OverVoltingSetting _OverVoltingSettings;

        /// <summary>
        ///     Creates a new instance of <see cref="PerformanceStates20InfoV3" />
        /// </summary>
        /// <param name="performanceStates">The list of performance states and their settings.</param>
        /// <param name="clocksCount">Number of clock frequencies per each performance state.</param>
        /// <param name="baseVoltagesCount">Number of base voltage per each performance state.</param>
        public PerformanceStates20InfoV3(
            PerformanceStates20InfoV1.PerformanceState20[] performanceStates,
            uint clocksCount,
            uint baseVoltagesCount)
        {
            if (performanceStates?.Length > PerformanceStates20InfoV1.MaxPerformanceStatesClocks)
            {
                throw new ArgumentException(
                    $"Maximum of {MaxPerformanceStates} performance states are configurable.",
                    nameof(performanceStates)
                );
            }

            if (performanceStates == null)
            {
                throw new ArgumentNullException(nameof(performanceStates));
            }

            this = typeof(PerformanceStates20InfoV3).Instantiate<PerformanceStates20InfoV3>();
            _NumberOfClocks = clocksCount;
            _NumberOfBaseVoltages = baseVoltagesCount;
            _NumberOfPerformanceStates = (uint) performanceStates.Length;
            Array.Copy(performanceStates, 0, _PerformanceStates, 0, performanceStates.Length);
        }

        /// <summary>
        ///     Creates a new instance of <see cref="PerformanceStates20InfoV3" />
        /// </summary>
        /// <param name="performanceStates">The list of performance states and their settings.</param>
        /// <param name="clocksCount">Number of clock frequencies per each performance state.</param>
        /// <param name="baseVoltagesCount">Number of base voltage per each performance state.</param>
        /// <param name="generalVoltages">The list of general voltages and their settings.</param>
        // ReSharper disable once TooManyDependencies
        public PerformanceStates20InfoV3(
            PerformanceStates20InfoV1.PerformanceState20[] performanceStates,
            uint clocksCount,
            uint baseVoltagesCount,
            PerformanceStates20BaseVoltageEntryV1[] generalVoltages) :
            this(performanceStates, clocksCount, baseVoltagesCount)
        {
            _OverVoltingSettings = new PerformanceStates20InfoV2.PerformanceStates20OverVoltingSetting(generalVoltages);
        }

        /// <summary>
        ///     Gets the list of general over-volting settings
        /// </summary>
        public PerformanceStates20BaseVoltageEntryV1[] GeneralVoltages
        {
            get => _OverVoltingSettings.Voltages.ToArray();
        }

        /// <inheritdoc />
        IPerformanceStates20VoltageEntry[] IPerformanceStates20Info.GeneralVoltages
        {
            get => GeneralVoltages.Cast<IPerformanceStates20VoltageEntry>().ToArray();
        }

        /// <inheritdoc />
        public bool IsEditable
        {
            get => _Flags.GetBit(0);
        }

        /// <summary>
        ///     Gets an array of valid power states for the GPU
        /// </summary>
        public PerformanceStates20InfoV1.PerformanceState20[] PerformanceStates
        {
            get => _PerformanceStates.Take((int) _NumberOfPerformanceStates).ToArray();
        }

        /// <inheritdoc />
        IPerformanceState20[] IPerformanceStates20Info.PerformanceStates
        {
            get => PerformanceStates.Cast<IPerformanceState20>().ToArray();
        }

        /// <summary>
        ///     Gets a dictionary for valid power states and their clock frequencies
        /// </summary>
        public Dictionary<PerformanceStateId, PerformanceStates20ClockEntryV1[]> Clocks {
            get {
            var clocks = (int)_NumberOfClocks;

            return PerformanceStates.ToDictionary(
                state20 => state20.StateId,
                state20 => state20._Clocks.Take(clocks).ToArray()
            );
            }
            set { 
              //Clocks=value;
                  Clocks[PerformanceStateId.P2_Balanced][1].SingleFrequency =  new PerformanceStates20ClockDependentSingleFrequency(8052525);

           //   Clocks=value;
            }
   
        }


        //

        /// <summary>
        ///     Gets a dictionary for valid power states and their voltage settings
        /// </summary>
        public IReadOnlyDictionary<PerformanceStateId, PerformanceStates20BaseVoltageEntryV1[]> Voltages
        {
            get
            {
                var baseVoltages = (int) _NumberOfBaseVoltages;

                return PerformanceStates.ToDictionary(
                    state20 => state20.StateId,
                    state20 => state20._BaseVoltages.Take(baseVoltages)
                        .ToArray()
                );
            }
        }



       public Dictionary<PerformanceStateId, PerformanceStates20ClockEntryV1[]> GetClockV1() {
         Clocks[PerformanceStateId.P2_Balanced][1].SingleFrequency =  new PerformanceStates20ClockDependentSingleFrequency(8052525);

            return Clocks;
        }
        public void  SetClockV1(Dictionary<PerformanceStateId, PerformanceStates20ClockEntryV1[]> _clk) {
            Clocks = _clk;
        }   
    


        /// <inheritdoc />
       Dictionary<PerformanceStateId, PerformanceStates20ClockEntryV1[]> IPerformanceStates20Info.Clocks
        {
            get
            {
        //    Clocks = null;
      //      Clocks.Add(PerformanceStateId.P11,null);
                return Clocks;
            /*
                return Clocks.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.Cast<IPerformanceStates20ClockEntry>().ToArray()
                );*/
            }
            set { 
                 Clocks = value;

               
            }
        }

        
 public void MySet(PerformanceStateId key, PerformanceStates20ClockEntryV1[] value)
{
    if (Clocks.ContainsKey(key))
    {
        Clocks[key] =value;
    }
    else
    {
        Clocks.Add(key, value);
    }
}

        /// <inheritdoc />
        IReadOnlyDictionary<PerformanceStateId, IPerformanceStates20VoltageEntry[]> IPerformanceStates20Info.Voltages
        {
            get
            {
                return Voltages.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.Cast<IPerformanceStates20VoltageEntry>().ToArray()
                );
            }
        }
    }
}