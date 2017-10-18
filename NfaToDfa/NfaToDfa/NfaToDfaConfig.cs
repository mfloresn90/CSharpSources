using System;
using System.IO;
using System.Collections.Generic;

namespace NfaToDfa
{
    public class NfaToDfaConfig
    {

        String absolutePath;
        int cardQ;
        int cardF;
        int[] f;
        int[] cardSn;
        int[,] positionDeltaN;
        List<int[]> deltaN;
        List<int[]> transitionT;
        List<int> preLclosure = new List<int>();

        public NfaToDfaConfig() { }

        public NfaToDfaConfig(String absolutePath)
        {
            this.absolutePath = absolutePath;
            readNfaData();
        }

        public int getCardQ()
        {
            return cardQ;
        }

        public int getCardF()
        {
            return cardF;
        }

        public int[] getF()
        {
            return f;
        }

        public List<int[]> getDeltaN()
        {
            return deltaN;
        }

        public int[] getCardSn()
        {
            return cardSn;
        }

        public int[,] getPositionDeltaN()
        {
            return positionDeltaN;
        }

        public List<int[]> getTransitionT()
        {
            return transitionT;
        }

        public void currentPath()
        {
            string startupPath = Directory.GetCurrentDirectory();
            Console.WriteLine("CurrentPath: " + startupPath);
        }

        public void readNfaData()
        {
            int counter = 0;
            int[] tmp;
            string[] separators = { ",", " " };

            try
            {
                using (StreamReader sr = new StreamReader(absolutePath))
                {

                    for (String line; (line = sr.ReadLine()) != null;)
                    {
                        if (counter == 0)
                        {
                            cardQ = int.Parse(line);
                        }
                        else if (counter == 1)
                        {
                            cardF = int.Parse(line);
                        }
                        else if (counter == 2)
                        {
                            String[] infoF = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            int fValue = 0;
                            f = new int[infoF.Length];
                            for (int i = 0; i < infoF.Length; i++)
                            {
                                Int32.TryParse(infoF[i], out fValue);
                                f[i] = fValue;
                            }
                        }
                        else if (counter == 3)
                        {
                            String[] infoSa = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            cardSn = new int[infoSa.Length];
                            int SaValue = 0;
                            for (int i = 0; i < infoSa.Length; i++)
                            {
                                Int32.TryParse(infoSa[i], out SaValue);
                                cardSn[i] = SaValue;
                            }
                            positionDeltaN = new int[cardSn[0], cardSn[1]];
                            int position = 0;
                            for (int x = 0; x < cardSn[0]; x++)
                            {
                                for (int y = 0; y < cardSn[1]; y++)
                                {
                                    positionDeltaN[x, y] = position;
                                    position++;
                                }
                            }
                        }
                        else if (counter == 4)
                        {
                            deltaN = new List<int[]>();
                            transitionT = new List<int[]>();
                            String[] sa = line.Split('|');
                            for (int i = 0; i < sa.Length; i++)
                            {
                                String con = sa[i];
                                String[] sap = con.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                                tmp = new int[sap.Length];
                                int sapValue = 0;
                                for (int j = 0; j < sap.Length; j++)
                                {
                                    Int32.TryParse(sap[j], out sapValue);
                                    tmp[j] = sapValue;
                                }
                                deltaN.Add(tmp);
                            }
                            for (int i = 0; i < sa.Length; i += 3)
                            {
                                String con = sa[i + 2];
                                String[] sap = con.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                                tmp = new int[sap.Length];
                                int sapValue = 0;
                                for (int j = 0; j < sap.Length; j++)
                                {
                                    Int32.TryParse(sap[j], out sapValue);
                                    tmp[j] = sapValue;
                                }
                                transitionT.Add(tmp);
                            }
                        }
                        counter++;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("readNfaData.IOException Message: " + e.Message);
            }
        }

        public List<int[]> getLambdaClosure(int cardQ)
        {
            List<int[]> result = new List<int[]>();
            Stack<int> lifo = new Stack<int>();
            int[] getInfo;
            for (int i = cardQ - 1; i >= 0; i--)
            {
                lifo.Push(i);
            }
            while (lifo.Count > 0)
            {
                preLclosure.Clear();
                int lifoValue = lifo.Pop();
                getPreLambdaClosure(lifoValue);
                getInfo = new int[preLclosure.Count];
                for (int i = 0; i < preLclosure.Count; i++)
                {
                    getInfo[i] = preLclosure[i];
                }
                result.Add(getInfo);
            }
            return result;
        }

        void getPreLambdaClosure(int state)
        {
            preLclosure.Add(state);
            int[] finded = transitionT[state];
            for (int i = 0; i < finded.Length; i++)
            {
                if (finded[i] != state)
                {
                    getPreLambdaClosure(finded[i]);
                }
            }
        }

        public int[] move(int[] s, int c)
        {
            List<int> preResult = new List<int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != deltaN[positionDeltaN[s[i], c]][0])
                {
                    preResult.Add(deltaN[positionDeltaN[s[i], c]][0]);
                }

            }
            int[] result = new int[preResult.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = preResult[i];
            }
            return result;
        }

        public int[] lClosure(int[] move, List<int[]> lambdaClosure)
        {
            List<int> preResult = new List<int>();
            int[] preRes;
            int[] result;
            for (int i = 0; i < move.Length; i++)
            {
                preRes = lambdaClosure[move[i]];
                for (int j = 0; j < preRes.Length; j++)
                {
                    preResult.Add(preRes[j]);
                }
            }
            preResult.Sort();
            result = new int[preResult.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = preResult[i];
            }
            return result;
        }

		public bool ArraysEqual<T>(T[] a1, T[] a2)
		{
			if (ReferenceEquals(a1, a2))
				return true;

			if (a1 == null || a2 == null)
				return false;

			if (a1.Length != a2.Length)
				return false;

			EqualityComparer<T> comparer = EqualityComparer<T>.Default;
			for (int i = 0; i < a1.Length; i++)
			{
				if (!comparer.Equals(a1[i], a2[i])) return false;
			}
			return true;
		}

        public bool uInDestados(List<int[]> dtran, int[] conjU)
        {
            bool flag = false;
            foreach (int[] items in dtran)
            {
                if (!items.Equals(conjU))
                {
                    flag = true;
                    //Console.WriteLine(" dtran --> " + "[" + string.Join(", ", items) + "]");
                    //Console.WriteLine(" conjU --> " + "[" + string.Join(", ", conjU) + "]");
                }
            }
            return flag;
        }

    }

}
