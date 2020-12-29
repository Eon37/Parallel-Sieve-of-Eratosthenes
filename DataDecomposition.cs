using System.Collections.Generic;

namespace lab2 {
    public class DataDecomposition : Algorithm {

        public DataDecomposition() : base("Data Decomposition") {}

        public override List<List<int>> formParams(int numOfElements, int numOfThreads) {
            List<List<int>> ll = new List<List<int>>();

            int block = (numOfElements - 2 + 1)/numOfThreads;
            int start = 2;
            int end = block;
            
            for (int i = 0; i < numOfThreads; i++) {
                if(i == numOfThreads - 1) {
                    ll.Add(new List<int> {start, numOfElements});
                }
                else {
                    ll.Add(new List<int> {start, end - 1});
                }
                start += block;
                end += block;
            }
            
            return ll;
        }

        public override void perform(object o) {
            List<int> bounds = o as List<int>;
            int start = bounds[0];
            int end = bounds[1];


            foreach (int prime in getBasePrime()) {                
                int i = start;

                while (i < end && i % prime != 0) {
                    i++;
                    if (isComposite[i]) i++;
                }
                
                for (int j = i; j <= end; j+= prime) {
                    if (!isComposite[j]) isComposite[j] = !isComposite[j];
                }
            }
        }
    }
}