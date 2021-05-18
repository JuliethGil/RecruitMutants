
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
    public class DnaSequenceLogic : IDnaSequenceLogic
    {
        private readonly IDnaSequenceQuery _dnaSequenceQuery;

        public DnaSequenceLogic(IDnaSequenceQuery dnaSequenceQuery) => _dnaSequenceQuery = dnaSequenceQuery;

        public async Task<bool> IsMutant(List<string> dna)
        {
            int lengthY = dna.Count;
            DnaContainsData(lengthY);
            int lengthX = dna[0].Length;
            dna = dna.ConvertAll(d => d.ToUpper());
            IsCorrectFormatDna(dna, lengthX);
            ValidateNitrogenousBase(dna);
            bool isMutant = HasSequenceMinimum(dna, lengthY);

            if (!isMutant)
            {
                await InsertDnaSequence(dna, isMutant);
                return isMutant;
            }

            int numEqualDna = 0;

            Parallel.For(0, lengthY, (y, statey) =>
            {
                if (numEqualDna > 1)
                    statey.Stop();

                Parallel.For(0, dna[y].Length, (x, statex) =>
                {
                    if (numEqualDna > 1)
                        statex.Stop();

                    if (EqualDown(dna, x, y) || EqualRight(dna, x, y) || EqualLeftDown(dna, x, y) || EqualRightDown(dna, x, y))
                        numEqualDna++;
                });
            });

            isMutant = numEqualDna > 1;
            await InsertDnaSequence(dna, isMutant);

            return isMutant;
        }

        public async Task<StatsDto> Stats()
        {
            List<DnaSequenceDto> dnaSequences = await _dnaSequenceQuery.PutDnaSequence();

            StatsDto stats = new StatsDto()
            {
                Count_mutant_dna = dnaSequences.Count(d => d.IsMutant),
                Count_human_dna = dnaSequences.Count(d => !d.IsMutant),
            };

            if (stats.Count_mutant_dna != 0 && stats.Count_human_dna != 0)
                stats.Ratio = Math.Round((decimal)stats.Count_mutant_dna / stats.Count_human_dna, 1);

            return stats;
        }

        /// <summary>
        /// Valid that the string list contains as a sequence
        /// </summary>
        /// <param name="lengthY"></param>
        private void DnaContainsData(int lengthY)
        {
            if (lengthY <= 0)
                throw new InvalidOperationException($"{nameof(DnaSequenceLogic)}: No DNA.");
        }

        /// <summary>
        /// Valid that each group of strings have the same length
        /// </summary>
        /// <param name="dna"></param>
        /// <param name="lengthX"></param>
        private void IsCorrectFormatDna(List<string> dna, int lengthX)
        {
            List<string> wrongChains = dna.Where(x => x.Length != lengthX).ToList();
            if (wrongChains.Count > 0)
                throw new InvalidOperationException($"{nameof(DnaSequenceLogic)}: The DNA format is wrong.");
        }

        /// <summary>
        /// Valid que la cadena de cadenas contiene las letras permitidas
        /// </summary>
        /// <param name="dna"></param>
        private void ValidateNitrogenousBase(List<string> dna)
        {
            string regexFormat = "^[" + ConstantsService.ValidLetters + "]+$";
            Regex regex = new Regex(regexFormat);

            foreach (string chain in dna)
            {
                MatchCollection matchDna = regex.Matches(chain);
                if (matchDna.Count <= 0)
                    throw new InvalidOperationException($"{nameof(DnaSequenceLogic)}: The nitrogenous base of DNA has invalid data.");
            }
        }

        /// <summary>
        /// Valid the minimum sequence to be considered a mutant
        /// </summary>
        /// <param name="dna"></param>
        /// <param name="lengthY"></param>
        /// <returns></returns>
        private bool HasSequenceMinimum(List<string> dna, int lengthY)
        {
            List<string> ChainsThree = dna.Where(x => x.Length < ConstantsService.NumberEqualLetters).ToList();
            if (ChainsThree.Count > 0 && lengthY < ConstantsService.NumberEqualLetters)
                return false;

            return true;
        }

        /// <summary>
        /// Validate that there are ConstantsService.NumberEqualLetters positions of the same letter down
        /// </summary>
        /// <param name="dna">list dna</param>
        /// <param name="x">position x of the list</param>
        /// <param name="y">position y of the list</param>
        /// <returns>Whether he found the letters or not</returns>
        private bool EqualDown(List<string> dna, int x, int y)
        {
            if (y + 3 > dna.Count() - 1)
                return false;

            List<char> values = new List<char> { dna[y][x], dna[y + 1][x], dna[y + 2][x], dna[y + 3][x] };

            if (values.Count(c => c == dna[y][x]) == ConstantsService.NumberEqualLetters)
                return true;

            return false;
        }

        /// <summary>
        /// Validate that there are ConstantsService.NumberEqualLetters positions of the same letter right
        /// </summary>
        /// <param name="dna">list dna</param>
        /// <param name="x">position x of the list</param>
        /// <param name="y">position y of the list</param>
        /// <returns>Whether he found the letters or not</returns>
        private bool EqualRight(List<string> dna, int x, int y)
        {
            if (x + 3 > dna[y].Length - 1)
                return false;

            List<char> values = new List<char> { dna[y][x], dna[y][x + 1], dna[y][x + 2], dna[y][x + 3] };

            if (values.Count(c => c == dna[y][x]) == ConstantsService.NumberEqualLetters)
                return true;

            return false;
        }

        /// <summary>
        /// Validate that there are ConstantsService.NumberEqualLetters positions of the same letter left down
        /// </summary>
        /// <param name="dna">list dna</param>
        /// <param name="x">position x of the list</param>
        /// <param name="y">position y of the list</param>
        /// <returns>Whether he found the letters or not</returns>
        private bool EqualLeftDown(List<string> dna, int x, int y)
        {
            if (y + 3 > dna.Count() - 1 ||  x - 3 < 0)
                return false;

            List<char> values = new List<char> { dna[y][x], dna[y + 1][x - 1], dna[y + 2][x - 2], dna[y + 3][x - 3] };

            if (values.Count(c => c == dna[y][x]) == ConstantsService.NumberEqualLetters)
                return true;


            return false;
        }

        /// <summary>
        /// Validate that there are ConstantsService.NumberEqualLetters positions of the same letter righ down
        /// </summary>
        /// <param name="dna">list dna</param>
        /// <param name="x">position x of the list</param>
        /// <param name="y">position y of the list</param>
        /// <returns>Whether he found the letters or not</returns>
        private bool EqualRightDown(List<string> dna, int x, int y)
        {
            if (y + 3 > dna.Count() - 1 || x + 3 > dna[y].Length - 1)
                return false;

            List<char> values = new List<char> { dna[y][x], dna[y + 1][x + 1], dna[y + 2][x + 2], dna[y + 3][x + 3] };

            if (values.Count(c => c == dna[y][x]) == ConstantsService.NumberEqualLetters)
                return true;

            return false;
        }

        /// <summary>
        /// Insert the list of strings into the database
        /// </summary>
        /// <param name="dna">list dna<</param>
        /// <param name="isMutant">Whether it is mutant or not</param>
        /// <returns>Id</returns>
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
