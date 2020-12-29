using System.Collections.Generic;

namespace lab2 {
    public class PrimeNumbersDecomposition : Algorithm {

        public PrimeNumbersDecomposition() : base("Prime Numbers Decomposition") {}

        public override List<List<int>> formParams(int numOfElements, int numOfThreads) {
            List<int> primes = getBasePrime();
            List<List<int>> ll = new List<List<int>>();

            if (primes.Count <= numOfThreads) {
                while (primes.Count > 0) {
                    ll.Add(new List<int> {primes[primes.Count-1]});
                    primes.RemoveAt(primes.Count - 1);
                }
            }
            else {
                for (int i = 0; i < numOfThreads; i++) {
                    ll.Insert(0, new List<int>());
                }

                while(primes.Count > 0) {
                    for (int i = 0; i < numOfThreads && primes.Count > 0; i++) {
                        ll[i].Add(primes[primes.Count - 1]);
                        primes.RemoveAt(primes.Count - 1);
                    }
                }
            }
            return ll;
        }
        public override void perform(object o) {
            List<int> primes = o as List<int>;

            foreach (int prime in primes) {                
                int i = sqrtn + 1;

                while (i < n - 1 && i % prime != 0) {
                    i++;
                    if (isComposite[i]) i++;
                }
                
                for (int j = i; j < n; j+= prime) {
                    if (!isComposite[j]) isComposite[j] = !isComposite[j];
                }
            }
        }
    }
}