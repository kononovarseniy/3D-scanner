using Ar.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D
{
    /// <summary>
    /// Detects highlited points on the image.
    /// One point per line.
    /// <para>&lt;joke>Detects N points in each image line. (Cases when N > 1 while not supported.)&lt;/joke></para>
    /// </summary>
    class HighlitedPointDetector
    {
        /// <summary>
        /// Minimal highlited pixel brightnes. Defeult is 120.
        /// </summary>
        public int Threshold { get; set; } = 120;
        /// <summary>
        /// Maximal gap between two highlited pixels, in image line, belonging to the one highlited curve.
        /// </summary>
        public int MaxSpace { get; set; } = 3;
        /// <summary>
        /// Number of lines to determine closest continuation of curve.
        /// </summary>
        public int AverageItems { get; set; } = 5;

        /// <summary>
        /// Finds all highlited points on the grayscaled image.
        /// </summary>
        /// <param name="imageData">Pointer to image data (8bpp grayscale).</param>
        /// <param name="width">Width of image.</param>
        /// <param name="height">Height of image.</param>
        /// <returns>All highlited points, ordered by Y coordinate.</returns>
        public Vector2[] FindHighlitedPoints(IntPtr imageData, int width, int height)
        {
            int[] xCoord;
            int detectedPointsCount;
            Find(imageData, width, height, out xCoord, out detectedPointsCount);

            Vector2[] result = new Vector2[detectedPointsCount];
            // Filter empty lines.
            int ind = 0;
            for (int y = 0; y < height; y++)
            {
                int x = xCoord[y];
                if (x != -1)
                {
                    result[ind++] = new Vector2(x, y);
                }
            }

            return result;
        }
        
        private unsafe void Find(IntPtr imageData, int width, int height, out int[] xCoord, out int detectedPointsCount)
        {
            byte* scan0 = (byte*)imageData;
            int stride = width;

            xCoord = new int[height];
            detectedPointsCount = 0;
            AverageQueue prevX = new AverageQueue(AverageItems);
            List<int> left = new List<int>();
            List<int> right = new List<int>();
            // detection
            for (int y = 0; y < height; y++)
            {
                left.Clear();
                right.Clear();
                int count = 0;
                int tmpLeft = -1;
                int tmpRight = -1;
                int lineOffset = stride * y;
                for (int x = 0; x < width; x++)
                {
                    bool isLine = scan0[lineOffset + x] > Threshold;
                    if (tmpLeft == -1)
                    {
                        if (isLine)
                        {
                            tmpLeft = x;
                            tmpRight = x;
                        }
                        else { }
                    }
                    else
                    {
                        if (isLine)
                        {
                            tmpRight = x;
                        }
                        else
                        {
                            if (x - tmpLeft > MaxSpace)
                            {
                                left.Add(tmpLeft);
                                right.Add(tmpRight);
                                count++;
                                tmpLeft = -1;
                                tmpRight = -1;
                            }
                        }
                    }
                } // end for x
                if (tmpLeft != -1)
                {
                    left.Add(tmpLeft);
                    right.Add(tmpRight);
                    count++;
                }

                int nearestX = -1;
                int minDist = int.MaxValue;
                int prev = (int)prevX.Value;
                for (int i = 0; i < count; i++)
                {
                    int x = (right[i] + left[i]) / 2;
                    int dist = Math.Abs(prev - x);
                    if (dist < minDist)
                    {
                        nearestX = x;
                        minDist = dist;
                    }
                }

                xCoord[y] = nearestX;
                if (nearestX != -1)
                {
                    detectedPointsCount++;
                    prevX.Enqueue(nearestX);
                }
            } // end for y
        }
    }
}
