
// ***********************************************************************
// Assembly         : BusinessLayer
// Author           : Julieth Gil
// Created          : 08-05-2021
//
// ***********************************************************************
// <copyright file="Node.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary></summary>

using BusinessLayer.Constants;
using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using DataAccess.Dtos;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessLogic
{
    public class MutantLogic : IMutantLogic
    {
        private readonly IDnaSequenceQuery _dnaSequenceQuery;

        public MutantLogic(IDnaSequenceQuery dnaSequenceQuery) => _dnaSequenceQuery = dnaSequenceQuery;

        public async Task<bool> IsMutant(MutantDto mutantDto)
        {
            int lengthY = mutantDto.Dna.Count;
            DnaContainsData(lengthY);
            int lengthX = mutantDto.Dna[0].Length;
            IsCorrectFormatDna(mutantDto.Dna, lengthX);
            ValidateNitrogenousBase(mutantDto.Dna);
            
            bool isMutant = HasSequenceMinimum(mutantDto.Dna, lengthY);
            if (isMutant)
            {
                isMutant = ValidateDnaSequence(mutantDto.Dna, lengthX, lengthY);
            }

            await InsertDnaSequence(mutantDto.Dna, isMutant);


            return isMutant;
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

        private bool HasSequenceMinimum(List<string> dna, int lengthY)
        {
            List<string> ChainsThree = dna.Where(x => x.Length < ConstantsService.NumberEqualLetters).ToList();
            if (ChainsThree.Count > 0 && lengthY < ConstantsService.NumberEqualLetters)
                return false;

            return true;
        }

        private void ValidateNitrogenousBase(List<string> dna)
        {
            string regexFormat = "^[ATCG]+$";
            Regex regex = new Regex(regexFormat);

            foreach (string chain in dna)
            {
                MatchCollection matchDna = regex.Matches(chain);
                if (matchDna.Count <= 0)
                    //TODO: Insert db. false
                    throw new InvalidOperationException($"{nameof(MutantLogic)}: The nitrogenous base of DNA has invalid data.");
            }
        }

        private bool ValidateDnaSequence(List<string> dna, int lengthY, int lengthX)
        {
            List<NodeModel> nodesList = new List<NodeModel>();
            NodeModel parent = new NodeModel(dna[0][0].ToString(), 0, 0);
            MappingNode(ref parent, dna, 0, 0, ref nodesList);


            int countChains = 0;
            List<NodeModel> nodesClean = nodesList.Distinct(parent.distinctNodeComparer).ToList();
            foreach (NodeModel node in nodesClean)
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

            return countChains >= ConstantsService.AmountMutantSequence;
        }

        private static bool ValidateBottomRight(int lengthY, int lengthX, int countChains, NodeModel node)
        {
            return countChains < ConstantsService.AmountMutantSequence
                && lengthX >= ConstantsService.NumberEqualLetters
                && lengthY >= ConstantsService.NumberEqualLetters
                && node.BottomRight != null
                && node.BottomRight.Key == node.Key;
        }

        private static bool ValidateBottomLeft(int lengthY, int lengthX, int countChains, NodeModel node)
        {
            return countChains < ConstantsService.AmountMutantSequence
                && lengthX >= ConstantsService.NumberEqualLetters
                && lengthY >= ConstantsService.NumberEqualLetters
                && node.BottomLeft != null
                && node.BottomLeft.Key == node.Key;
        }

        private void MappingNode(ref NodeModel parent, List<string> dna, int positionY, int positionX, ref List<NodeModel> nodes)
        {
            if (positionX < dna[positionY].Length - 1)
            {
                parent.Right = new NodeModel(dna[positionY][positionX + 1].ToString(), positionX + 1, positionY);
                MappingNode(ref parent.Right, dna, positionY, positionX + 1, ref nodes);
            }

            if (positionY < dna.Count - 1)
            {
                parent.Bottom = new NodeModel(dna[positionY + 1][positionX].ToString(), positionX, positionY + 1);
                MappingNode(ref parent.Bottom, dna, positionY + 1, positionX, ref nodes);
            }

            if (positionX < dna[positionY].Length - 1 && positionY < dna.Count - 1)
            {
                parent.BottomRight = new NodeModel(dna[positionY + 1][positionX + 1].ToString(), positionX + 1, positionY + 1);
                MappingNode(ref parent.BottomRight, dna, positionY + 1, positionX + 1, ref nodes);
            }

            if (positionX > 0 && positionX < dna[positionY].Length && positionY < dna.Count - 1)
            {
                parent.BottomLeft = new NodeModel(dna[positionY + 1][positionX - 1].ToString(), positionX - 1, positionY + 1);
                MappingNode(ref parent.BottomLeft, dna, positionY + 1, positionX - 1, ref nodes);
            }

            nodes.Add(parent);
        }

        private async Task<int> InsertDnaSequence(List<string> dna, bool isMutant)
        {
            DnaSequenceDto dnaSequenceDto = new DnaSequenceDto
            {
                PersonDna = string.Join(",", dna),
                IsMutant = isMutant
            };

            return await _dnaSequenceQuery.InsertDnaSequence(dnaSequenceDto);
        }
    }
}
