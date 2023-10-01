
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learningtotranslatesequences
{


    [Serializable]
    public class ContextDecrypt
    {
        public List<LongShortTermMemoryContextDecryptCell> decoders = new List<LongShortTermMemoryContextDecryptCell>(); 
        public int hdim { get; set; }
        public int dim { get; set; }
        public int depth { get; set; }

        public ContextDecrypt(int hdim, int dim, int depth)
        {
             decoders.Add(new LongShortTermMemoryContextDecryptCell(hdim, dim));
             for (int i = 1; i < depth; i++)
             {
                 decoders.Add(new LongShortTermMemoryContextDecryptCell(hdim, hdim));
  
             }
            this.hdim = hdim;
            this.dim = dim;
            this.depth = depth;
        }
        public void Refresh()
        {
            foreach (var item in decoders)
            {
                item.Refresh();
            }

        } 
        public WeightMatrix Decode(WeightMatrix input, WeightMatrix encoderOutput, ComputeGraph g)
        {
            var V = new WeightMatrix();
            
            foreach (var encoder in decoders)
            { 
                var e = encoder.SynonymPace(encoderOutput, input, g);
                V = e; 
            }

            return V;
        } 
        public List<WeightMatrix> Retrieve()
        {
            List<WeightMatrix> response = new List<WeightMatrix>();

            foreach (var item in decoders)
            {

                response.AddRange(item.Retrieve());
            }
            
            return response;
        }

    }
}
