// ***********************************************************************
// Assembly         : BusinessLayer
// Author           : Julieth Gil
// Created          : 09-05-2021
//
// ***********************************************************************
// <copyright file="Node.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary></summary>

using System.Collections.Generic;

namespace DataAccess.Dtos
{
    public class NodeModel
    {
        public string Key;
        public NodeModel Right, Bottom, BottomRight, BottomLeft;
        public int PositionX, PositionY;
        public DistinctNodeComparer distinctNodeComparer;

        public NodeModel(string item, int positionX, int positionY)
        {
            Key = item;
            Right = Bottom = BottomRight = BottomLeft = null;
            PositionX = positionX;
            PositionY = positionY;
            distinctNodeComparer = new DistinctNodeComparer();
        }

        public class DistinctNodeComparer : IEqualityComparer<NodeModel>
        {
            public bool Equals(NodeModel x, NodeModel y)
            {
                return x.PositionX == y.PositionX &&
                        x.PositionY == y.PositionY;
            }

            public int GetHashCode(NodeModel obj)
            {
                return obj.Key.GetHashCode() ^
                    obj.PositionX.GetHashCode() ^
                    obj.PositionY.GetHashCode();
            }
        }
    }
}
