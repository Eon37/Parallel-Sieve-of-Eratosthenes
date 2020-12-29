using System.Threading;

namespace lab2 {

    public class PoolAlgorithm : Algorithm {

        public PoolAlgorithm() : base("Thread Pool Algorithm") {}
        
        public override void perform(object o) {
            int prime = (int)((object[])o)[0];
            CountdownEvent cnt = (CountdownEvent)((object[])o)[1];

            int i = sqrtn + 1;

            while (i < n - 1 && i % prime != 0) {
                i++;
                if (isComposite[i]) i++;
            }
                
            for (int j = i; j < n; j+= prime) {
                if (!isComposite[j]) isComposite[j] = !isComposite[j];
            }

            cnt.Signal();
        }
    }
}