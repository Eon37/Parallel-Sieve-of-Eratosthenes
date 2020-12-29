using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

namespace lab2 {
    class Program {
        static int amnt = 5;
        static double[,] elapsedTime;
        static Stopwatch sw = new Stopwatch();

        static int min = 100000;
        static int step = 10;
        static int max;
        

        static int numOfTests = 5;
        static double[] tests = new double[numOfTests];

        static void Main(string[] args) {
            min = Int32.Parse(args[0]);
            step = Int32.Parse(args[1]);
            amnt = Int32.Parse(args[2]);

            elapsedTime = new double[amnt, 8];
            max = min * (int)Math.Pow(step, elapsedTime.GetUpperBound(0));

            int k = min;

            for (int i = 0; i <= elapsedTime.GetUpperBound(0); i++) {
                elapsedTime[i, 0] = k;
                k *= step;
            }

            run();
        }

        static void run() {    

            //Sequential
            calculate(new Sequential());
            printTime("Sequential");

            //ThreadPool
            calculate(new PoolAlgorithm());
            printTime("Thread pool");

            //DataDecomposition
            calculate(new DataDecomposition());
            printTime("Data decomposition");

            //PrimeNumbersDecomp
            calculate(new PrimeNumbersDecomposition());
            printTime("Prime numbers decompostition");


            //SequentialMultyThread
            calculate(new MultyThreadSequential());
            printTime("Multy thread sequential");
        }

        static void calculate(Sequential alg) {
            for (int i = min, ii = 0; i <= max; i *= step, ii++) {
                for (int k = 0; k < numOfTests; k++) {
                    tests[k] = measure(i, alg);
                }
                elapsedTime[ii, 1] = Math.Round(calcAvg(tests), 3);
            }
        }

        static void calculate(PoolAlgorithm alg) {
            for(int i = min, ii = 0; i <= max; i *= step, ii++) {
                    for (int k = 0; k < numOfTests; k++) { 
                        tests[k] = measure(i, alg); 
                    }
                    elapsedTime[ii, 2] = Math.Round(calcAvg(tests), 3);
            }
        }

        static void calculate(Algorithm alg) {
            for(int i = min, ii = 0; i <= max; i *= step, ii++) {
                for(int j = 2, jj = 3 ; j <= 10; j += 2, jj++) {
                    for (int k = 0; k < numOfTests; k++) { 
                        tests[k] = measure(i, j, alg); 
                    }
                    elapsedTime[ii, jj] = Math.Round(calcAvg(tests), 3);
                }                
            }
        }

        static void calculate(MultyThreadSequential alg) {
            for(int i = min, ii = 0; i <= max; i *= step, ii++) {
                for(int j = 2, jj = 3 ; j <= 10; j += 2, jj++) {
                    for (int k = 0; k < numOfTests; k++) { 
                        alg.annihilateIndex();
                        tests[k] = measure(i, j, alg); 
                    }
                    elapsedTime[ii, jj] = Math.Round(calcAvg(tests), 3);
                }                
            }
        }

        static double measure(int numOfElements, Sequential alg) {
            alg.initArr(numOfElements);

            sw.Restart();
            alg.perform();
            sw.Stop();

            return sw.Elapsed.TotalMilliseconds;
        }

        static double measure(int numOfelements, int numOfThreads, Algorithm alg) {
            Thread[] thrds = new Thread[numOfThreads];
            alg.initArr(numOfelements);

            alg.findBase();

            List<List<int>> ll = alg.formParams(numOfelements, numOfThreads);
            
                sw.Restart();
                for (int i = 0; i < numOfThreads && (ll == null || i < ll.Count); i++) {
                    thrds[i] = new Thread(alg.perform);
                    thrds[i].Start(ll != null? ll[i] : null);
                }
                for (int i =0; i < numOfThreads && (ll == null || i < ll.Count); i++) {
                    thrds[i].Join();
                }
                sw.Stop();

            return sw.Elapsed.TotalMilliseconds + alg.sw.Elapsed.TotalMilliseconds;
        }

        static double measure(int numOfElements, PoolAlgorithm alg) {
            alg.initArr(numOfElements);

            alg.findBase();

            List<int> basePrimes = alg.getBasePrime();

            CountdownEvent cnt = new CountdownEvent(basePrimes.Count);

            sw.Restart();
            for(int i = 0; i < basePrimes.Count; i++) {
                ThreadPool.QueueUserWorkItem(alg.perform, new object[] {basePrimes[i], cnt});
            }

            cnt.Wait();
            sw.Stop();

            return sw.Elapsed.TotalMilliseconds + alg.sw.Elapsed.TotalMilliseconds;
        }

//nums.Length > 2 !!! | numOfTests >= 3 !!!
        static double calcAvg(double[] nums) {
            for (int i = 2; i < nums.Length; i++) {
                nums[1] += nums[i];
            }

            return nums[1]/nums.Length;
        }

        static void printTime(string algName) {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Data for " + algName + " algorithm");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ".PadLeft(10, ' ') + "|");
            Console.Write("0".PadLeft(10, ' ') + "|" + "1".PadLeft(10, ' ') + "|");
            for(int i = 2; i <= 10; i += 2) {
                Console.Write(i.ToString().PadLeft(10, ' ') + "|");
            }

            Console.WriteLine();

            for (int i = 0; i <= elapsedTime.GetUpperBound(0); i++) {
                for (int j = 0; j <= elapsedTime.GetUpperBound(1); j++) {
                    Console.Write(elapsedTime[i, j].ToString().PadLeft(10, ' ') + "|");
                }
                Console.WriteLine();
            }
        }

    }
}
