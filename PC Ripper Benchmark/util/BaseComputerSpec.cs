namespace PC_Ripper_Benchmark.util {

    public class BaseComputerSpec {

        public BaseComputerSpec() {

        }

        public string CPUName { get => "Intel(R) Core(TM) i7-6700K CPU @ 4.00GHz"; }

        public string RAMName { get => "Crucial RAM "; }

        public string DISKName { get => ""; }

        public double IterationsPerTickCPU { get => 90; }

        public double IterationsPerTickRAM { get => 45.500006; }

        public double IterationsPerTickDISK { get => 110.3033833; }

        public byte Score { get => 50; }

    }
}
