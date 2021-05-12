﻿// ***********************************************************************
// Assembly         : Xm.Sicep.Audit.Common
// Author           : Julieth Gil
// Created          : 09-05-2020
//
// ***********************************************************************
// <copyright file="Node.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary></summary>

using System.Collections.Generic;

namespace DataAccess.Models
{
    public class Node
    {
        public string Key;
        public Node Right, Bottom, BottomRight, BottomLeft;
        public int PositionX, PositionY;
        public DistinctNodeComparer distinctNodeComparer;

        public Node(string item, int positionX, int positionY)
        {
            Key = item;
            Right = Bottom = BottomRight = BottomLeft = null;
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