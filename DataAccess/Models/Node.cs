using System.Collections.Generic;

namespace DataAccess.Models
{
    public class Node
    {
        public string Key;
        public Node Right, Bottom, BotRight, BotLeft;
        public int PositionX, PositionY;
        public DistinctNodeComparer distinctNodeComparer;

        public Node(string item, int positionX, int positionY)
        {
            Key = item;
            Right = Bottom = BotRight = BotLeft = null;
            PositionX = positionX;
            PositionY = positionY;
            distinctNodeComparer = new DistinctNodeComparer();
        }

        public class DistinctNodeComparer : IEqualityComparer<Node>
        {
            public bool Equals(Node x, Node y)
            {
                return x.PositionX == y.PositionX &&
                        x.PositionY == y.PositionY;
            }

            public int GetHashCode(Node obj)
            {
                return obj.Key.GetHashCode() ^
                    obj.PositionX.GetHashCode() ^
                    obj.PositionY.GetHashCode();
            }
        }
    }
}
