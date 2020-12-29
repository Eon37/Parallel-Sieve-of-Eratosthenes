using System;

namespace lab2 {
    public class Sequential : Algorithm {

        public Sequential() : base("Sequential") {}
        
        public override void perform(object o = null) {
            for (int m = 2; m <= sqrtn; m++) {
                if(!isComposite[m]) markDividedByStep1(m, n - 1);
            }

        }
    }
}