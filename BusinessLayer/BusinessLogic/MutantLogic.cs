using BusinessLayer.Constants;
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
            int lengthY = dna.Count;
            DnaContainsData(lengthY);
            int lengthX = dna[0].Length;
            IsCorrectFormatDna(dna, lengthX);
            HasSequenceMinimum(dna, lengthY);
            ValidateNitrogenousBase(dna);
            ValidateDnaSequence(dna, lengthX, lengthY);

            return true;
        }

        private void DnaContainsData(int lengthY)
        {
            if (lengthY <= 0)
                throw new InvalidOperationException($"{nameof(MutantLogic)}: No DNA.");
        }

        private void IsCorrectFormatDna(List<string> dna, int lengthX)
        {
            List<string> wrongChains = dna.Where(x => x.Length != lengthX).ToList();
            if (wrongChains.Count > 0)
                throw new InvalidOperationException($"{nameof(MutantLogic)}: The DNA format is wrong.");
        }

        private void HasSequenceMinimum(List<string> dna, int lengthY)
        {
            List<string> ChainsThree = dna.Where(x => x.Length < ConstantsService.NumberEqualLetters).ToList();
            if (ChainsThree.Count > 0 && lengthY < ConstantsService.NumberEqualLetters)
                throw new InvalidOperationException($"{nameof(MutantLogic)}: DNA does not meet the minimum sequence to be a mutant.");
        }

        private void ValidateNitrogenousBase(List<string> dna)
        {
            string regexFormat = "^[ATCG]+$";
            Regex regex = new Regex(regexFormat);

            foreach (string chain in dna)
            {
                MatchCollection matchDna = regex.Matches(chain);
                if (matchDna.Count <= 0)
                    throw new InvalidOperationException($"{nameof(MutantLogic)}: The nitrogenous base of DNA has invalid data.");
            }
        }

        private void ValidateDnaSequence(List<string> dna, int lengthY, int lengthX)
        {
            List<Node> nodesList = new List<Node>();
            Node parent = new Node(dna[0][0].ToString(), 0, 0);
            MappingNode(ref parent, dna, 0, 0, ref nodesList);


            int countChains = 0;
            List<Node> nodesClean = nodesList.Distinct(parent.distinctNodeComparer).ToList();
            foreach (Node node in nodesClean)
            {
                if (lengthX >= ConstantsService.NumberEqualLetters && node.Bottom != null && node.Bottom.Key == node.Key)
                {
                    if (node.Bottom.Bottom != null && node.Bottom.Bottom.Key == node.Key)
                    {
                        if (node.Bottom.Bottom.Bottom != null && node.Bottom.Bottom.Bottom.Key == node.Key)
                            countChains++;
                    }
                }

                if (lengthY >= ConstantsService.NumberEqualLetters && node.Right != null && node.Right.Key == node.Key)
                {
                    if (node.Right.Right != null && node.Right.Right.Key == node.Key)
                    {
                        if (node.Right.Right.Right != null && node.Right.Right.Right.Key == node.Key)
                            countChains++;
                    }
                }

                if (ValidateBottomRight(lengthY, lengthX, countChains, node))
                {
                    if (node.BottomRight.BottomRight != null && node.BottomRight.BottomRight.Key == node.Key)
                    {
                        if (node.BottomRight.BottomRight.BottomRight != null && node.BottomRight.BottomRight.BottomRight.Key == node.Key)
                            countChains++;
                    }
                }

                if (ValidateBottomLeft(lengthY, lengthX, countChains, node))
                {
                    if (node.BottomLeft.BottomLeft != null && node.BottomLeft.BottomLeft.Key == node.Key)
                    {
                        if (node.BottomLeft.BottomLeft.BottomLeft != null && node.BottomLeft.BottomLeft.BottomLeft.Key == node.Key)
                            countChains++;
                    }
                }
            }

            if (countChains >= ConstantsService.AmountMutantSequence)
                return;

            throw new InvalidOperationException($"{nameof(MutantLogic)}: DNA is not from a mutant.");
        }

        private static bool ValidateBottomRight(int lengthY, int lengthX, int countChains, Node node)
        {
            return countChains < ConstantsService.AmountMutantSequence
                && lengthX >= ConstantsService.NumberEqualLetters
                && lengthY >= ConstantsService.NumberEqualLetters
                && node.BottomRight != null
                && node.BottomRight.Key == node.Key;
        }

        private static bool ValidateBottomLeft(int lengthY, int lengthX, int countChains, Node node)
        {
            return countChains < ConstantsService.AmountMutantSequence
                && lengthX >= ConstantsService.NumberEqualLetters
                && lengthY >= ConstantsService.NumberEqualLetters
                && node.BottomLeft != null
                && node.BottomLeft.Key == node.Key;
        }

        private void MappingNode(ref Node parent, List<string> dna, int positionY, int positionX, ref List<Node> nodes)
        {
            if (positionX < dna[positionY].Length - 1)
            {
                parent.Right = new Node(dna[positionY][positionX + 1].ToString(), positionX + 1, positionY);
                MappingNode(ref parent.Right, dna, positionY, positionX + 1, ref nodes);
            }

            if (positionY < dna.Count - 1)
            {
                parent.Bottom = new Node(dna[positionY + 1][positionX].ToString(), positionX, positionY + 1);
                MappingNode(ref parent.Bottom, dna, positionY + 1, positionX, ref nodes);
            }

            if (positionX < dna[positionY].Length - 1 && positionY < dna.Count - 1)
            {
                parent.BottomRight = new Node(dna[positionY + 1][positionX + 1].ToString(), positionX + 1, positionY + 1);
                MappingNode(ref parent.BottomRight, dna, positionY + 1, positionX + 1, ref nodes);
            }

            if (positionX > 0 && positionX < dna[positionY].Length && positionY < dna.Count - 1)
            {
                parent.BottomLeft = new Node(dna[positionY + 1][positionX - 1].ToString(), positionX - 1, positionY + 1);
                MappingNode(ref parent.BottomLeft, dna, positionY + 1, positionX - 1, ref nodes);
            }

            nodes.Add(parent);
        }
    }
}
