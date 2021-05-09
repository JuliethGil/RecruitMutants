using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLayer.BusinessLogic
{
    public class MutantLogic : IMutantLogic
    {
        public bool IsMutant(List<string> dna)
        {
            string regexFormat = "^[ATCG]+$";
            Regex regex = new Regex(regexFormat);

            foreach (string chain in dna)
            {
                MatchCollection matchDNA = regex.Matches(chain);
                if (matchDNA.Count <= 0)
                    return false;
            }


            List<Node> nodesList = new List<Node>();
            Node parent = new Node(dna[0][0].ToString(),0 ,0);
            MappingNode(ref parent , dna, 0 ,0, ref nodesList);

            int countChains = 0;
            List<Node> nodesClean = nodesList.Distinct(new DistinctNodeComparer()).ToList();
            foreach(Node node in nodesClean)
            {
                if(node.bottom != null && node.bottom.key == node.key)
                {
                    if (node.bottom.bottom != null &&  node.bottom.bottom.key == node.key)
                    {
                        if (node.bottom.bottom.bottom != null && node.bottom.bottom.bottom.key == node.key)
                        {
                            countChains++;
                        }
                    }
                }

                if (node.right != null && node.right.key == node.key)
                {
                    if (node.right.right != null && node.right.right.key == node.key)
                    {
                        if (node.right.right.right != null && node.right.right.right.key == node.key)
                        {
                            countChains++;
                        }
                    }
                }

                if (node.botRight != null && node.botRight.key == node.key)
                {
                    if (node.botRight.botRight != null && node.botRight.botRight.key == node.key)
                    {
                        if (node.botRight.botRight.botRight != null && node.botRight.botRight.botRight.key == node.key)
                        {
                            countChains++;
                        }
                    }
                }

                if (node.botLeft != null && node.botLeft.key == node.key)
                {
                    if (node.botLeft.botLeft != null && node.botLeft.botLeft.key == node.key)
                    {
                        if (node.botLeft.botLeft.botLeft != null && node.botLeft.botLeft.botLeft.key == node.key)
                        {
                            countChains++;
                        }
                    }
                }
            }
            
            return countChains > 1;
        }

        void MappingNode(ref Node parent, List<string> dna,int line ,int pos, ref List<Node> nodesList)
        {
            if (pos < dna[line].Length - 1)
            {
                parent.right = new Node(dna[line][pos + 1].ToString(), pos + 1 , line);
                MappingNode(ref parent.right, dna, line ,pos + 1, ref nodesList);
            }

                
            if (line < dna.Count - 1)
            {
                parent.bottom = new Node(dna[line + 1][pos].ToString(), pos, line + 1);
                MappingNode(ref parent.bottom, dna,line + 1 ,pos, ref nodesList);
            }

            if (pos < dna[line].Length - 1 && line < dna.Count - 1)
            {
                parent.botRight = new Node(dna[line + 1][pos + 1].ToString(), pos + 1, line + 1);
                MappingNode(ref parent.botRight, dna, line + 1, pos + 1, ref nodesList);
            }

            if (pos > 0 && pos < dna[line].Length && line < dna.Count - 1)
            {
                parent.botLeft = new Node(dna[line + 1][pos - 1].ToString(), pos - 1, line + 1);
                MappingNode(ref parent.botLeft, dna, line + 1, pos - 1, ref nodesList);
            }

            nodesList.Add(parent);
            
        }
    }

   

    class Node {
        public string key;
        public Node  right, bottom, botRight, botLeft;
        public int posX, posY;

        public Node(string item, int posX, int posY)
        {
            key = item;
            right = bottom = botRight = botLeft = null;
            this.posX = posX;
            this.posY = posY;

        }
    }

    class DistinctNodeComparer : IEqualityComparer<Node>
    {

        public bool Equals(Node x, Node y)
        {
            return x.posX == y.posX &&
                    x.posY == y.posY;
        }

        public int GetHashCode(Node obj)
        {
            return obj.key.GetHashCode() ^
                obj.posX.GetHashCode() ^
                obj.posY.GetHashCode();
        }
    }
}
