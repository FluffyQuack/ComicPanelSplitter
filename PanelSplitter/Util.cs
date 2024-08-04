﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace PanelSplitter
{
    public class Util
    {


        public static string GetOutputFilePath(string outputDirectory, string originalFileName, int counter)
        {
            if (String.IsNullOrEmpty(originalFileName))
            {
                string filename = string.Format("panel{0}.png", counter); //Fluffy: Changed to PNG because JPEG icky ick
				return Path.Combine(outputDirectory, filename);
            }
            else
            {
                string filename = string.Format("{0}_Panel{1}.png", Path.GetFileNameWithoutExtension(originalFileName), counter); //Fluffy: Changed to PNG because JPEG icky ick
				return Path.Combine(outputDirectory, filename);
            }         
        }


        public static Coordinate[,] PixelsToCoordinates(Bitmap image)
        {
            Coordinate[,] coords = new Coordinate[image.Width, image.Height];

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    {
                        Coordinate coordinate = new Coordinate(x, y, image.GetPixel(x, y));
                        coords[x, y] = coordinate;
                    }
                }
            }
            return coords;
        }


        public static void CutandWriteToFile(List<FloodFilledRegion> regions, Bitmap bitmap, string exportpath, string originalFileName)
        {
            int counter = 1;
            foreach (FloodFilledRegion region in regions)
            {
                string outputFilePath = Util.GetOutputFilePath(exportpath, originalFileName, counter);
                CutAndWriteToFile(bitmap, region.Left,  region.Top, region.Right, region.Down, outputFilePath);
                counter++;
            }
        }

        public static void CutAndWriteToFile(Bitmap bitmap, int left, int top, int right, int bottom, string outputfilePath)
        {
            int width = right - left;
            int height = bottom - top;
            Rectangle region = new Rectangle(left, top, width, height);
            Bitmap panel = bitmap.Clone(region, PixelFormat.DontCare);
            panel.Save(outputfilePath, ImageFormat.Png); //Fluffy: Changed to PNG because JPEG icky ick
        }


    }
}
