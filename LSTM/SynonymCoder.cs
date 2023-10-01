
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learningtotranslatesequences
{

    [Serializable]
    public class SynonymCoder
    {
        public List<LongLengthy> encoders = new List<LongLengthy>();
        public int hdim { get; set; }
        public int dim { get; set; }
        public int depth { get; set; }

        public SynonymCoder(int hdim, int dim, int depth )
        {
             encoders.Add(new LongLengthy(hdim, dim));
 
            //for (int i = 1; i < depth; i++)
            //{
            //   encoders.Add(new LongLengthy(hdim, hdim));
 
            //}
            this.hdim = hdim;
            this.dim = dim;
            this.depth = depth;
        }
        public void Refresh()
        {
            foreach (var item in encoders)
            {
                item.Refresh();
            }

        }

        public WeightMatrix Encode(WeightMatrix V, ComputeGraph g)
        {
            foreach (var encoder in encoders)
            {
                var e = encoder.SynonymPace(V, g); 
                    V = e; 
  
            }
            return V;
        }
        public List<WeightMatrix> Encode2(WeightMatrix V, ComputeGraph g)
        {
            List<WeightMatrix> res = new List<WeightMatrix>();
            foreach (var encoder in encoders)
            {
                var e = encoder.SynonymPace(V, g);
                V = e;
                res.Add(e);
            }
            return res;
        }

        public List<WeightMatrix> Retrieve()
        {
            List<WeightMatrix> response = new List<WeightMatrix>();

            foreach (var item in encoders)
            {

                response.AddRange(item.Retrieve());

            }



            return response;
        }

    }
}
