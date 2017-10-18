using System;
using System.Collections.Generic;
using System.IO;

namespace NfaToDfa
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            NfaToDfaConfig nfadfa = new NfaToDfaConfig("nfa.txt");
            List<int[]> dTran = new List<int[]>();

            Console.WriteLine("\nConversión de un AFN a AFD (AFN-a-AFD v1.0)\n");
            Console.WriteLine("cardQ = " + nfadfa.getCardQ());
            Console.WriteLine("cardF = " + nfadfa.getCardF());
            Console.WriteLine("F = " + "[" + string.Join(", ", nfadfa.getF()) + "]");
            Console.WriteLine("DeltaN = {");
            for (int i = 0; i < nfadfa.getDeltaN().Count; i += 3)
            {
                Console.Write("\t");
                Console.Write("[" + string.Join(", ", nfadfa.getDeltaN()[i]) + "]\t");
                Console.Write("[" + string.Join(", ", nfadfa.getDeltaN()[i + 1]) + "]\t");
                Console.WriteLine("[" + string.Join(", ", nfadfa.getDeltaN()[i + 2]) + "]");
            }
            Console.WriteLine("}");
            List<int[]> lambdaClosure = nfadfa.getLambdaClosure(nfadfa.getCardQ());
            Console.WriteLine("LambdaCerradura = {");
            foreach (int[] getData in lambdaClosure)
            {
                Console.Write("\t");
                Console.WriteLine("[" + string.Join(", ", getData) + "]");
            }
            Console.WriteLine("}\n");

            dTran.Add(lambdaClosure[0]);
            //Console.WriteLine("Estado inicial  (q0) --> Lambda --> " + "[" + string.Join(", ", dTran[0]) + "]");

            int[] language = { 0, 1 };
            int[] conjuntoU = null;
            for (int i = 0; i < dTran.Count; i++)
            {
                for (int j = 0; j < language.Length; j++)
                {
                    int[] move = nfadfa.move(dTran[i], language[j]);
                    //Console.Write(" --> Move with [" + language[j] + "] --> " + "[" + string.Join(", ", move) + "]");
                    conjuntoU = nfadfa.lClosure(move, lambdaClosure);
                    //Console.WriteLine(" --> Lambda --> " + "[" + string.Join(", ", conjuntoU) + "]");

                    Console.WriteLine("\nNFA CURRENT STATES");
                    foreach (int[] items in dTran)
                    {
                        Console.WriteLine("[" + string.Join(", ", items) + "]");
                    }
                    Console.WriteLine("\n");

                    Console.Write("Agregar estado: [" + string.Join(", ", conjuntoU) + "]? (y/n): ");

                    switch (Console.Read())
                    {
                        case 'y':
                            Console.Write("\nAgregando a estados actuales");
                            dTran.Add(conjuntoU);
                            break;
                        case 'n':
                            Console.WriteLine("\nOmitiendo conjunto...");
                            break;
                    }
                }
            }

            Console.WriteLine("\nFINAL STATES");
            foreach (int[] items in dTran)
            {
                Console.WriteLine("[" + string.Join(", ", items) + "]");
            }
            Console.WriteLine("\n");

            int index = 0;
            List<int> dfaTran = new List<int>();
            for (int i = 0; i < dTran.Count; i++)
            {
                for (int j = 0; j < language.Length; j++)
                {
                    int[] move = nfadfa.move(dTran[i], language[j]);
                    //Console.Write(" --> Move with [" + language[j] + "] --> " + "[" + string.Join(", ", move) + "]");
                    conjuntoU = nfadfa.lClosure(move, lambdaClosure);
                    //Console.WriteLine(" --> Lambda --> " + "[" + string.Join(", ", conjuntoU) + "]");

                    for (int k = 0; k < dTran.Count; k++)
                    {
                        if (nfadfa.ArraysEqual<int>(conjuntoU, dTran[k]))
                        {
                            index = k;
                            dfaTran.Add(k);
                        }
                    }

                    Console.Write("[" + j + "]: ");
                    Console.Write("[" + string.Join(", ", conjuntoU) + "] = " + index);
                    Console.Write("\t");
                }
                Console.WriteLine("\n");
            }

            int cardQ = dTran.Count;
            Console.WriteLine("Ingresa cardF: ");
            int cardF = Convert.ToInt32(Console.ReadLine());
            int[] statesF = new int[cardF];
            for (int i = 0; i < cardF; i++)
            {
                Console.Write("F[" + i + "]: ");
                statesF[i] = Convert.ToInt32(Console.ReadLine());
            }
            int[] cardStates = { cardQ, 2 };



            using (StreamWriter outputFile = new StreamWriter("dfa.txt"))
            {
                outputFile.WriteLine(cardQ);
                outputFile.WriteLine(cardF);
                outputFile.WriteLine(string.Join(", ", statesF));
                outputFile.WriteLine(string.Join(", ", cardStates));
                for (int i = 0; i < dfaTran.Count; i++)
                {
                    if (i < dfaTran.Count - 1)
                    {
                        outputFile.Write(dfaTran[i] + ", ");
                    }
                    else if (i == dfaTran.Count - 1)
                    {
                        outputFile.WriteLine(dfaTran[i]);
                    }
                }
                outputFile.WriteLine("No disponible...");
            }

            Console.WriteLine("Archivo generado...");

        }
    }
}