// ***********************************************************************
// Assembly         : Xm.Sicep.Audit.Common
// Author           : Julieth Gil
// Created          : 09-05-2021
//
// ***********************************************************************
// <copyright file="Node.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary></summary>

using System.Collections.Generic;

namespace BusinessLayer.Dtos
{
    public class NodeDto
    {
        public string Key;
        public NodeDto Right, Bottom, BottomRight, BottomLeft;
        public int PositionX, PositionY;
        public DistinctNodeComparer distinctNodeComparer;

        public NodeDto(string item, int positionX, int positionY)
        {
            Key = item;
            Right = Bottom = BottomRight = BottomLeft = null;
            PositionX = positionX;
            PositionY = positionY;
            distinctNodeComparer = new DistinctNodeComparer();
        }

        public class DistinctNodeComparer : IEqualityComparer<NodeDto>
        {
            public bool Equals(NodeDto x, NodeDto y)
            {
                return x.PositionX == y.PositionX &&
                        x.PositionY == y.PositionY;
            }

            public int GetHashCode(NodeDto obj)
            {
                return obj.Key.GetHashCode() ^
                    obj.PositionX.GetHashCode() ^
                    obj.PositionY.GetHashCode();
            }
        }
    }
}
