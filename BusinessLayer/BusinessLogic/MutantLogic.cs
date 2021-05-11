using BusinessLayer.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLayer.BusinessLogic
{
    public class MutantLogic : IMutantLogic
    {

        public bool IsMutant(List<string> dna)
        {
            int lengthX = dna[0].Length;
            int lengthY = dna.Count;

            DnaContainsData(lengthY);
            IsCorrectMatrixFormat(dna, lengthX);
            Matriz(dna, lengthY);
            ValidateLetters(dna);
            ValidateDnaSequence(dna, lengthX, lengthY);

            return true;
        }

        void DnaContainsData(int lengthY)
        {
            if (lengthY <= 0)
                throw new Exception();
        }

        void IsCorrectMatrixFormat(List<string> dna, int lengthX)
        {
            List<string> wrongChains = dna.Where(x => x.Length != lengthX).ToList();
            if (wrongChains.Count > 0)
                throw new Exception();
        }

        void Matriz(List<string> dna, int lengthY)
        {
            List<string> ChainsThree = dna.Where(x => x.Length <= 3).ToList();
            if (ChainsThree.Count > 0 && lengthY <= 3)
                throw new Exception();
        }

        void ValidateLetters(List<string> dna)
        {
            string regexFormat = "^[ATCG]+$";
            Regex regex = new Regex(regexFormat);

            foreach (string chain in dna)
            {
                MatchCollection matchDna = regex.Matches(chain);
                if (matchDna.Count <= 0)
                    throw new Exception();
            }
        }

        void ValidateDnaSequence(List<string> dna, int lengthY, int lengthX)
        {
            List<Node> nodesList = new List<Node>();
            Node parent = new Node(dna[0][0].ToString(), 0, 0);
            MappingNode(ref parent, dna, 0, 0, ref nodesList);


            int countChains = 0;
            List<Node> nodesClean = nodesList.Distinct(parent.distinctNodeComparer).ToList();
            foreach (Node node in nodesClean)
            {
                if (lengthX > 3 && node.Bottom != null && node.Bottom.Key == node.Key)
                {
                    if (node.Bottom.Bottom != null && node.Bottom.Bottom.Key == node.Key)
                    {
                        if (node.Bottom.Bottom.Bottom != null && node.Bottom.Bottom.Bottom.Key == node.Key)
                            countChains++;
                    }
                }

                if (lengthY > 3 && node.Right != null && node.Right.Key == node.Key)
                {
                    if (node.Right.Right != null && node.Right.Right.Key == node.Key)
                    {
                        if (node.Right.Right.Right != null && node.Right.Right.Right.Key == node.Key)
                            countChains++;
                    }
                }

                if (countChains < 2 && lengthX > 3 && lengthY > 3 && node.BotRight != null && node.BotRight.Key == node.Key)
                {
                    if (node.BotRight.BotRight != null && node.BotRight.BotRight.Key == node.Key)
                    {
                        if (node.BotRight.BotRight.BotRight != null && node.BotRight.BotRight.BotRight.Key == node.Key)
                            countChains++;
                    }
                }

                if (countChains < 2 && lengthX > 3 && lengthY > 3 && node.BotLeft != null && node.BotLeft.Key == node.Key)
                {
                    if (node.BotLeft.BotLeft != null && node.BotLeft.BotLeft.Key == node.Key)
                    {
                        if (node.BotLeft.BotLeft.BotLeft != null && node.BotLeft.BotLeft.BotLeft.Key == node.Key)
                            countChains++;
                    }
                }
            }

            if (countChains >= 2)
                return;

            throw new Exception();
        }

        void MappingNode(ref Node parent, List<string> dna, int positionY, int positionX, ref List<Node> nodesList)
        {
            if (positionX < dna[positionY].Length - 1)
            {
                parent.Right = new Node(dna[positionY][positionX + 1].ToString(), positionX + 1, positionY);
                MappingNode(ref parent.Right, dna, positionY, positionX + 1, ref nodesList);
            }

            if (positionY < dna.Count - 1)
            {
                parent.Bottom = new Node(dna[positionY + 1][positionX].ToString(), positionX, positionY + 1);
                MappingNode(ref parent.Bottom, dna, positionY + 1, positionX, ref nodesList);
            }

            if (positionX < dna[positionY].Length - 1 && positionY < dna.Count - 1)
            {
                parent.BotRight = new Node(dna[positionY + 1][positionX + 1].ToString(), positionX + 1, positionY + 1);
                MappingNode(ref parent.BotRight, dna, positionY + 1, positionX + 1, ref nodesList);
            }

            if (positionX > 0 && positionX < dna[positionY].Length && positionY < dna.Count - 1)
            {
                parent.BotLeft = new Node(dna[positionY + 1][positionX - 1].ToString(), positionX - 1, positionY + 1);
                MappingNode(ref parent.BotLeft, dna, positionY + 1, positionX - 1, ref nodesList);
            }

            nodesList.Add(parent);
        }
    }
}
