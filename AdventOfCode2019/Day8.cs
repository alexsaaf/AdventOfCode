using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2019 {
    class Day8 {


        public static int width = 25, height = 6;
        static int chunkSize = width * height;
        
        public static long CheckCorruption(string line) {
            double k = 0;
            var res = Enumerable.Range(0, line.Length / chunkSize)
                                  .Select(i => line.Substring(i * chunkSize, chunkSize)).Aggregate((currMax, newLayer) => currMax.Count(x => x == '0') < newLayer.Count(x => x == '0') ? currMax : newLayer);
            return res.Count(x => x == '1') * res.Count(x => x == '2');
        }

        public static void DecodeImage(string line) {
            double k = 0;
            var res = Enumerable.Range(0, line.Length / chunkSize)
                                 .Select(i => line.Substring(i * chunkSize, chunkSize))
                                 .Aggregate((currImage, nextLayer) => {
                                     char[] nextImage = currImage.ToCharArray();
                                     for (int i = 0; i < currImage.Length; i++) {
                                         if (currImage[i] == '2') {
                                             nextImage[i] = nextLayer[i];
                                         }
                                     }
                                     return new string(nextImage);
                                 });

            var charArray = res.ToCharArray();
            for (int i = 0; i < height; i++) {
                var lineString = "";
                for (int y = 0; y < width; y++) {
                    lineString += charArray[i * width + y] == '1' ? "X" : " ";
                }
                Console.WriteLine(lineString);
            }
        }


    }
}
