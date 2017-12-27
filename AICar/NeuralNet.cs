using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AICar
{
    public class NeuralNet
    {
        public class NNConnection
        {
            public int n1;
            public int n2;
            public float w;
            public NNConnection(int a, int b, float c)
            {
                n1 = a;
                n2 = b;
                w = c;
            }

        }

        public List<List<NNConnection>> layers;

        public int maxCon = 40;

        public NeuralNet()
        {
            layers = new List<List<NNConnection>>();
            List<NNConnection> list = new List<NNConnection>();
            for (int i = 0; i < maxCon; i++)
                list.Add(new NNConnection(Helper.rnd.Next(7), Helper.rnd.Next(8), (float)Helper.rnd.NextDouble() * 2 - 1));
            layers.Add(list);
            for (int i = 0; i < 6; i++)
            {
                List<NNConnection> l = new List<NNConnection>();
                for (int j = 0; j < maxCon; j++)
                    l.Add(new NNConnection(Helper.rnd.Next(8), Helper.rnd.Next(8), (float)Helper.rnd.NextDouble() * 2 - 1)); 
                layers.Add(l);
            } 
            list = new List<NNConnection>();
            for (int i = 0; i < maxCon; i++)
                list.Add(new NNConnection(Helper.rnd.Next(7), Helper.rnd.Next(2), (float)Helper.rnd.NextDouble() * 2 - 1));
            layers.Add(list);
        }

        public void CalcOutput(float[] input, out float[] result)
        {
            result = new float[2];
            if (layers.Count != 8) return;
            List<NNConnection> l = layers[0];
            float[] nodes = new float[64];
            foreach (NNConnection con in l)
                nodes[con.n2] += (con.w * input[con.n1]);
            for (int i = 0; i < 8; i++)
                nodes[i] = Helper.Sigmoid(nodes[i]);
            for (int i = 0; i < 7; i++)
            {
                List<NNConnection> list = layers[i];
                foreach (NNConnection con in list)
                    nodes[con.n2 + (i + 1) * 8] += con.w * nodes[con.n1 + i * 8];
                for (int j = 0; j < 8; j++)
                    nodes[j + (i + 1) * 8] = Helper.Sigmoid(nodes[j + (i + 1) * 8]);
            }
            l = layers[7];
            foreach (NNConnection con in l)
                result[con.n2] += con.w * nodes[con.n1 + 56];
            result[0] = Helper.Sigmoid(result[0]);
            result[1] = Helper.Sigmoid(result[1]);
        }

        public void Randomize()
        {
            float rndTresh = 0.9f;
            List<NNConnection> l = layers[0];
            if (Helper.rnd.NextDouble() > rndTresh)
            {
                l.RemoveAt(Helper.rnd.Next(l.Count));
                l.Add(new NNConnection(Helper.rnd.Next(7), Helper.rnd.Next(8), (float)Helper.rnd.NextDouble()));
            }
            foreach (NNConnection con in l)
                if (Helper.rnd.NextDouble() > rndTresh)
                    con.w = (float)(Helper.rnd.NextDouble() * 2 - 1);
            for (int i = 1; i < 7; i++)
            {
                List<NNConnection> list = layers[i];
                if (Helper.rnd.NextDouble() > rndTresh)
                {
                    list.RemoveAt(Helper.rnd.Next(list.Count));
                    list.Add(new NNConnection(Helper.rnd.Next(8), Helper.rnd.Next(8), (float)Helper.rnd.NextDouble()));
                }
                foreach (NNConnection con in list)
                    if (Helper.rnd.NextDouble() > rndTresh)
                        con.w = (float)(Helper.rnd.NextDouble() * 2 - 1);
            }
            l = layers[7];
            if (Helper.rnd.NextDouble() > rndTresh)
            {
                l.RemoveAt(Helper.rnd.Next(l.Count));
                l.Add(new NNConnection(Helper.rnd.Next(8), Helper.rnd.Next(2), (float)Helper.rnd.NextDouble()));
            }
            foreach (NNConnection con in l)
                if (Helper.rnd.NextDouble() > rndTresh)
                    con.w = (float)(Helper.rnd.NextDouble() * 2 - 1);
        }

        public NeuralNet Clone()
        {
            NeuralNet result = new NeuralNet();
            result.layers = new List<List<NNConnection>>();
            foreach (List<NNConnection> layer in layers)
            {
                List<NNConnection> copy = new List<NNConnection>();
                foreach (NNConnection con in layer)
                    copy.Add(new NNConnection(con.n1, con.n2, con.w));
                result.layers.Add(copy);
            }
            return result;
        }

        public void Load(string filename)
        {
            MemoryStream m = new MemoryStream(File.ReadAllBytes(filename));
            m.Seek(0, 0);
            int count = Helper.ReadInt(m);
            layers = new List<List<NNConnection>>();
            for (int i = 0; i < count; i++)
            {
                List<NNConnection> layer = new List<NNConnection>();
                int count2 = Helper.ReadInt(m);
                for (int j = 0; j < count2; j++)
                    layer.Add(new NNConnection(Helper.ReadInt(m), Helper.ReadInt(m), Helper.ReadFloat(m)));
                layers.Add(layer);
            }
        }

        public void Save(string filename)
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteInt(m, layers.Count);
            foreach (List<NNConnection> layer in layers)
            {
                Helper.WriteInt(m, layer.Count);
                foreach (NNConnection con in layer)
                {
                    Helper.WriteInt(m, con.n1);
                    Helper.WriteInt(m, con.n2);
                    Helper.WriteFloat(m, con.w);
                }
            }
            File.WriteAllBytes(filename, m.ToArray());
        }
    }
}
