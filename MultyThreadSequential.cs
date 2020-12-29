using System.Collections.Generic;

namespace lab2 {
    class MultyThreadSequential : Algorithm {

        public MultyThreadSequential() : base("Multy Thread Sequential") {}
        
        private List<int> basePrimes;
        private int currentIndex = 0;

        public override List<List<int>> formParams(int numOfElements, int numOfThreads) {
            basePrimes = getBasePrime();
            return null;
        }
        
        public override void perform(object o) {
            int currentPrime;

            while(true) {
                lock("here?") {
                    if(currentIndex >= basePrimes.Count) break;
                
                    currentPrime = basePrimes[currentIndex];
                    currentIndex++;
                }

                int i = sqrtn + 1;

                while (i < n - 1 && i % currentPrime != 0) {
                    i++;
                    if (isComposite[i]) i++;
                }
                    
                for (int j = i; j < n; j+= currentPrime) {
                    if (!isComposite[j]) isComposite[j] = !isComposite[j];
                }
            }
        }
        public void annihilateIndex() {
            currentIndex = 0;
        }

    }
}