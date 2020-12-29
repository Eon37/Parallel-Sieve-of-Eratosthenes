using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace lab2 {
    public abstract class Algorithm{
        public Stopwatch sw = new Stopwatch();
        private String name;
        protected bool[] isComposite;
        private List<int> basePrime;
        protected int n;
        protected int sqrtn;

        protected Algorithm(string name) {
            this.name = name;
        }
        public abstract void perform(object o);

        public virtual List<List<int>> formParams(int numOfElements, int numOfThreads) { return null; }

        public void findBase() {
            basePrime = new List<int>();
            sw.Restart();
            for (int m = 2; m <= sqrtn; m++) {
                if(!isComposite[m]) {
                    sw.Stop();
                    basePrime.Add(m);
                    markDividedByStep1(m, sqrtn); 
                    sw.Start();
                }
            }
            sw.Stop();
        }

        protected void markDividedByStep1(int m, int n) {
            sw.Start();
            for (int i = m + m; i <= n; i++) {
                if (!isComposite[i]) isComposite[i] = !isComposite[i];
            }
            sw.Stop();
        }

        public void initArr(int numOfElements) {
            n = numOfElements + 1;
            isComposite = new bool[n];
            sqrtn = (int)Math.Floor(Math.Sqrt(numOfElements));
        }

        public List<int> getBasePrime() {
            return new List<int>(basePrime);
        }

        public String getName() {
            return name;
        }
    }
}