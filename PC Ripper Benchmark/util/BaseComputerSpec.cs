namespace PC_Ripper_Benchmark.util {

    public class BaseComputerSpec {

        public BaseComputerSpec() {

        }

        public string CPUName { get => "Intel(R) Core(TM) i7-6700K CPU @ 4.00GHz"; }

        public string RAMName { get => "Crucial RAM "; }

        public string DISKName { get => ""; }

        public double TicksPerIterationCPU { get => 90; }

        public double TicksPerIterationRAM { get => 45.500006; }

        public double TicksPerIterationDISK { get => 110.3033833; }

        public byte Score { get => 50; }

    }
}
