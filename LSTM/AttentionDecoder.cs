
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learningtotranslatesequences
{


    [Serializable]
    public class AttentionDecrypt
    {
        public List<LSTMAttentionDecryptUnit> decoders = new List<LSTMAttentionDecryptUnit>(); 
        public int hdim { get; set; }
        public int dim { get; set; }
        public int depth { get; set; }
        public AttentionUnit Attention { get; set; }
        public AttentionDecrypt(int hdim, int dim, int depth)
        {
             decoders.Add(new LSTMAttentionDecryptUnit(hdim, dim));
             for (int i = 1; i < depth; i++)
             {
                 decoders.Add(new LSTMAttentionDecryptUnit(hdim, hdim));
  
             }
             Attention = new AttentionUnit(hdim);
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
        public WeightMatrix Decode(WeightMatrix input, List<WeightMatrix> encoderOutput, ComputeGraph g)
        {
            var V = input;
            var lastStatus = this.decoders.FirstOrDefault().ct;
            var context = Attention.Perform(encoderOutput, lastStatus, g);
            foreach (var encoder in decoders)
            {
                var e = encoder.SynonymPace(context, V, g);
                V = e;
            }

            return V;
        } 
       
        public WeightMatrix Decode(WeightMatrix input, WeightMatrix encoderOutput, ComputeGraph g)
        {
            var V = input;
            var lastStatus = this.decoders.FirstOrDefault().ct;
            var context = Attention.Perform(encoderOutput, lastStatus, g);
            foreach (var encoder in decoders)
            {
                var e = encoder.SynonymPace(context, V, g);
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
            response.AddRange(Attention.Retrieve());
            return response;
        }

    }
}
