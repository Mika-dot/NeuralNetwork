using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace Learningtotranslatesequences
{

    public class GradientDescent
    {
        public double decay_rate = 0.999; // 1
        public double smooth_eps = 1e-8; // 0
        List<WeightMatrix> step_cache = new List<WeightMatrix>();

        public void move(List<WeightMatrix> model, double step_size, double regc, double clipval)
        {
            var num_clipped = 0;
            var num_tot = 0;
            foreach (var k in model)
            {
                if (k==null)
                {
                    continue;
                }
                var m = k;
                var s = k.Cash;
                for (int i = 0, n = m.Weight.Length; i < n; i++)
                {

                    var mdwi = m.Gradient[i];
                    s[i] = s[i] * this.decay_rate + (1.0 - this.decay_rate)
                        * mdwi * mdwi;

                    if (mdwi > clipval)
                    {
                        mdwi = clipval;
                        num_clipped++;
                    }
                    if (mdwi < -clipval)
                    {
                        mdwi = -clipval;
                        num_clipped++;
                    }
                    num_tot++;

                    m.Weight[i] += -step_size *
                        mdwi / Math.Sqrt(s[i] + this.smooth_eps) -
                        regc * m.Weight[i];
                    m.Gradient[i] = 0;
                }

            }
        }
    }
}
