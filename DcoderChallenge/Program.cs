using System;

namespace DcoderChallenge
{
    sealed public class Fourier
    {
        private struct FrequencyFunction
        {
            public double mamplitude;
            public double mperiod;
        }
        private struct NodeVector
        {
            public double mcurTime;
            public double mcenter_x;
            public double mcenter_y;
            public double mvector_x;
            public double mvector_y;
            public double mdot_x;
            public double mdot_y;
        }
        private enum ColorType
        {
            None = 0,
            White = 1,
            Yellow = 2,
            Magenta = 3,
            Red = 4,
            Cyan = 5,
            Green = 6,
            Blue = 7,
            DarkGray = 8,
            Gray = 9,
            DarkYellow = 10,
            DarkMagenta = 11,
            DarkRed = 12,
            DarkGreen = 13,
            DarkBlue = 14,
            Black = 15,

            Text = 77
        };

        private FrequencyFunction[] mnodeInfos;
        private NodeVector[] mnodeVectors;

        private ColorType[,] mmap;
        private ColorType[,] mtextMap;

        private int mmaxVectorNum;
        private int mmaxAmplitude;
        private int mmaxPeriod;
        private double mfrequency;
        private int mfirstN;

        private const int MAP_LENGTH = 80;
        private const int MAP_CHAR_LENGTH = 60;
        private const int CHAR_LENGTH = 5;
        private const int MAP_DIV = 2;

        private const double PI2 = Math.PI * 2.0f;

        Random random;

        //  public method
        public Fourier(int coord_x, int coord_y, int maxVectorNum, int maxAmplitude, int maxPeriod, double frequency, int firstN)
        {
            mmaxVectorNum = maxVectorNum;
            mmaxAmplitude = maxAmplitude;
            mmaxPeriod = maxPeriod;
            mfrequency = frequency;
            mfirstN = firstN;

            mnodeInfos = new FrequencyFunction[mmaxVectorNum];
            mnodeVectors = new NodeVector[mmaxVectorNum];
            mmap = new ColorType[MAP_LENGTH, MAP_LENGTH / MAP_DIV];
            mtextMap = new ColorType[MAP_LENGTH, MAP_CHAR_LENGTH];

            random = new Random();

            SetNodeInfos();
            MakeFourier(coord_x, coord_y);
            ComputeText();
        }
        public void Print()
        {
            ColorType backGroundColor = (ColorType)random.Next(1, 16);

            for (int coord_y = 0; coord_y < MAP_LENGTH; coord_y++)
            {
                for (int coord_x = 0; coord_x < MAP_LENGTH / MAP_DIV; coord_x++)
                {
                    if (mmap[coord_y, coord_x] != ColorType.None)
                    {
                        switch(mmap[coord_y, coord_x])
                        {
                            case ColorType.Black:
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("■");
                                break;
                            case ColorType.Blue:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("■");
                                break;
                            case ColorType.Cyan:
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("■");
                                break;
                            case ColorType.DarkBlue:
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write("■");
                                break;
                            case ColorType.DarkGray:
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write("■");
                                break;
                            case ColorType.DarkGreen:
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("■");
                                break;
                            case ColorType.DarkMagenta:
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.Write("■");
                                break;
                            case ColorType.DarkRed:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("■");
                                break;
                            case ColorType.DarkYellow:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write("■");
                                break;
                            case ColorType.Gray:
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write("■");
                                break;
                            case ColorType.Green:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("■");
                                break;
                            case ColorType.Magenta:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write("■");
                                break;
                            case ColorType.Red:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("■");
                                break;
                            case ColorType.White:
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("■");
                                break;
                            case ColorType.Yellow:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("■");
                                break;
                        }
                    }
                    else
                    {
                        Console.BackgroundColor = (ConsoleColor)backGroundColor;
                        Console.Write("  ");
                    }
                }
                for (int coord_x = 0; coord_x < MAP_CHAR_LENGTH; coord_x++)
                {
                    if (mtextMap[coord_y, coord_x] != ColorType.None)
                    {
                        switch (mtextMap[coord_y, coord_x])
                        {
                            case ColorType.Black:
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("■");
                                break;
                            case ColorType.Blue:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("■");
                                break;
                            case ColorType.Cyan:
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("■");
                                break;
                            case ColorType.DarkBlue:
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write("■");
                                break;
                            case ColorType.DarkGray:
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write("■");
                                break;
                            case ColorType.DarkGreen:
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("■");
                                break;
                            case ColorType.DarkMagenta:
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.Write("■");
                                break;
                            case ColorType.DarkRed:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("■");
                                break;
                            case ColorType.DarkYellow:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write("■");
                                break;
                            case ColorType.Gray:
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write("■");
                                break;
                            case ColorType.Green:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("■");
                                break;
                            case ColorType.Magenta:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write("■");
                                break;
                            case ColorType.Red:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("■");
                                break;
                            case ColorType.White:
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("■");
                                break;
                            case ColorType.Yellow:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("■");
                                break;
                        }
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("  ");
                    }
                }

                Console.WriteLine("");
            }
        }

        //  private method
        ~Fourier()
        {
            mnodeInfos = null;
            mnodeVectors = null;
            mmap = null;
            random = null;
        }
        private void SetNodeInfos()
        {
            for (int i = 0; i < mmaxVectorNum; i++)
            {
                mnodeInfos[i].mamplitude = mmaxAmplitude * random.NextDouble();
                mnodeInfos[i].mperiod = mmaxPeriod * random.NextDouble();
            }
        }
        private void MakeFourier(int coord_x, int coord_y)
        {
            int flagIndex = Math.Abs(mfirstN) - 1;

            mnodeVectors[0].mcenter_x = coord_x;
            mnodeVectors[0].mcenter_y = coord_y;

            while (mnodeVectors[flagIndex].mcurTime <= PI2)
            {
                for (int index = 0; index < mmaxVectorNum; index++)
                {
                    if (mfirstN + index < 0)
                    {
                        mnodeVectors[index].mvector_x = GetFx(index) * GetCircle_x(index);
                        mnodeVectors[index].mvector_y = GetFx(index) * GetCircle_y(index, true);
                        mnodeVectors[index].mdot_x = mnodeVectors[index].mcenter_x + mnodeVectors[index].mvector_x;
                        mnodeVectors[index].mdot_y = mnodeVectors[index].mcenter_y + mnodeVectors[index].mvector_y;
                        mnodeVectors[index].mcurTime += mfrequency * Math.Abs(mfirstN + index);

                        MoveNestVectors(index);
                    }
                    else
                    {
                        mnodeVectors[index].mvector_x = GetFx(index) * GetCircle_x(index);
                        mnodeVectors[index].mvector_y = GetFx(index) * GetCircle_y(index, false);
                        mnodeVectors[index].mdot_x = mnodeVectors[index].mcenter_x + mnodeVectors[index].mvector_x;
                        mnodeVectors[index].mdot_y = mnodeVectors[index].mcenter_y + mnodeVectors[index].mvector_y;
                        mnodeVectors[index].mcurTime += mfrequency * Math.Abs(mfirstN + index);

                        MoveNestVectors(index);
                    }

                    if (index == mmaxVectorNum - 1 && (mnodeVectors[index].mdot_x >= 0 && mnodeVectors[index].mdot_x < MAP_LENGTH / MAP_DIV && mnodeVectors[index].mdot_y >= 0 && mnodeVectors[index].mdot_y < MAP_LENGTH))
                    {
                        SetColor((int)mnodeVectors[index].mdot_x, (int)mnodeVectors[index].mdot_y, mnodeVectors[index].mcurTime);
                    }
                }
            }
        }
        private double GetFx(int index)
        {
            return mnodeInfos[index].mamplitude * Math.Cos(mnodeVectors[index].mcurTime * mnodeInfos[index].mperiod);
        }
        private double GetCircle_x(int index)
        {
            return Math.Cos(mnodeVectors[index].mcurTime);
        }
        private double GetCircle_y(int index, bool isClockWay)
        {
            if (isClockWay == true)
            {
                return Math.Sin(mnodeVectors[index].mcurTime);
            }
            else
            {
                return (-1) * Math.Sin(mnodeVectors[index].mcurTime);
            }
        }
        private void MoveNestVectors(int index)
        {
            for (int i = index + 1; i < mmaxVectorNum; i++)
            {
                mnodeVectors[i].mcenter_x = mnodeVectors[i - 1].mdot_x;
                mnodeVectors[i].mcenter_y = mnodeVectors[i - 1].mdot_y;
                mnodeVectors[i].mdot_x = mnodeVectors[i].mcenter_x + mnodeVectors[i].mvector_x;
                mnodeVectors[i].mdot_y = mnodeVectors[i].mcenter_y + mnodeVectors[i].mvector_y;
            }
        }
        private void SetColor(in int x, in int y, in double time)
        {
            if(time / PI2 <= 1.0 / 15.0)
            {
                mmap[y, x] = (ColorType)1;
                if(x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)1;
                }
                if(x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)1;
                }
                if(y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)1;
                }
                if(y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)1;
                }
            }
            else if(time / PI2 <= 2.0/15.0)
            {
                mmap[y, x] = (ColorType)2;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)2;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)2;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)2;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)2;
                }
            }
            else if (time / PI2 <= 3.0 / 15.0)
            {
                mmap[y, x] = (ColorType)3;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)3;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)3;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)3;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)3;
                }
            }
            else if (time / PI2 <= 4.0 / 15.0)
            {
                mmap[y, x] = (ColorType)4;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)4;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)4;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)4;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)4;
                }
            }
            else if (time / PI2 <= 5.0 / 15.0)
            {
                mmap[y, x] = (ColorType)5;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)5;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)5;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)5;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)5;
                }
            }
            else if (time / PI2 <= 6.0 / 15.0)
            {
                mmap[y, x] = (ColorType)6;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)6;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)6;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)6;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)6;
                }
            }
            else if (time / PI2 <= 7.0 / 15.0)
            {
                mmap[y, x] = (ColorType)7;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)7;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)7;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)7;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)7;
                }
            }
            else if (time / PI2 <= 8.0 / 15.0)
            {
                mmap[y, x] = (ColorType)8;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)8;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)8;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)8;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)8;
                }
            }
            else if (time / PI2 <= 9.0 / 15.0)
            {
                mmap[y, x] = (ColorType)9;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)9;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)9;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)9;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)9;
                }
            }
            else if (time / PI2 <= 10.0 / 15.0)
            {
                mmap[y, x] = (ColorType)10;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)10;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)10;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)10;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)10;
                }
            }
            else if (time / PI2 <= 11.0 / 15.0)
            {
                mmap[y, x] = (ColorType)11;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)11;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)11;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)11;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)11;
                }
            }
            else if (time / PI2 <= 12.0 / 15.0)
            {
                mmap[y, x] = (ColorType)12;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)12;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)12;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)12;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)12;
                }
            }
            else if (time / PI2 <= 13.0 / 15.0)
            {
                mmap[y, x] = (ColorType)13;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)13;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)13;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)13;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)13;
                }
            }
            else if (time / PI2 <= 14.0 / 15.0)
            {
                mmap[y, x] = (ColorType)14;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)14;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)14;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)14;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)14;
                }
            }
            else if (time / PI2 <= 15.0 / 15.0)
            {
                mmap[y, x] = (ColorType)15;
                if (x - 1 >= 0)
                {
                    mmap[y, x - 1] = (ColorType)15;
                }
                if (x + 1 < MAP_LENGTH / MAP_DIV)
                {
                    mmap[y, x + 1] = (ColorType)15;
                }
                if (y - 1 >= 0)
                {
                    mmap[y - 1, x] = (ColorType)15;
                }
                if (y + 1 < MAP_LENGTH)
                {
                    mmap[y + 1, x] = (ColorType)15;
                }
            }
        }
        private void ComputeText()
        {
            SetText('W', 48, 1, ColorType.Green);
            SetText('E', 54, 1, ColorType.Green);

            SetText('S', 24, 7, ColorType.White);
            SetText('A', 30, 7, ColorType.White);
            SetText('L', 36, 7, ColorType.White);
            SetText('U', 42, 7, ColorType.White);
            SetText('T', 48, 7, ColorType.White);
            SetText('E', 54, 7, ColorType.White);

            SetText('C', 24, 13, ColorType.Red);
            SetText('O', 30, 13, ColorType.Red);
            SetText('R', 36, 13, ColorType.Red);
            SetText('O', 42, 13, ColorType.Red);
            SetText('N', 48, 13, ColorType.Red);
            SetText('A', 54, 13, ColorType.Red);

            SetText('W', 12, 19, ColorType.Yellow);
            SetText('A', 18, 19, ColorType.Yellow);
            SetText('R', 24, 19, ColorType.Yellow);
            SetText('R', 30, 19, ColorType.Yellow);
            SetText('I', 36, 19, ColorType.Yellow);
            SetText('O', 42, 19, ColorType.Yellow);
            SetText('U', 48, 19, ColorType.Yellow);
            SetText('S', 54, 19, ColorType.Yellow);

            SetText('F', 42, 25, ColorType.White);
            SetText('O', 48, 25, ColorType.White);
            SetText('R', 54, 25, ColorType.White);

            SetText('T', 30, 31, ColorType.White);
            SetText('H', 36, 31, ColorType.White);
            SetText('E', 42, 31, ColorType.White);
            SetText('I', 48, 31, ColorType.White);
            SetText('R', 54, 31, ColorType.White);

            SetText('D', 2, 37, ColorType.Magenta);
            SetText('E', 7, 37, ColorType.Magenta);
            SetText('D', 13, 37, ColorType.Magenta);
            SetText('I', 18, 37, ColorType.Magenta);
            SetText('C', 24, 37, ColorType.Magenta);
            SetText('A', 30, 37, ColorType.Magenta);
            SetText('T', 36, 37, ColorType.Magenta);
            SetText('I', 42, 37, ColorType.Magenta);
            SetText('O', 48, 37, ColorType.Magenta);
            SetText('N', 54, 37, ColorType.Magenta);

            SetText('T', 30, 50, ColorType.White);
            SetText('H', 36, 50, ColorType.White);
            SetText('A', 42, 50, ColorType.White);
            SetText('N', 48, 50, ColorType.White);
            SetText('K', 54, 50, ColorType.White);

            SetText('Y', 36, 56, ColorType.White);
            SetText('O', 42, 56, ColorType.White);
            SetText('U', 48, 56, ColorType.White);
            SetText('Q', 54, 56, ColorType.Red);
        }
        private void SetText(in char text, in int x, in int y, ColorType colorType)
        {
            if(text == 'W')
            {
                mtextMap[y, x] = colorType;
                mtextMap[y + 1, x] = colorType;
                mtextMap[y + 2, x] = colorType;

                mtextMap[y + 3, x + 1] = colorType;
                mtextMap[y + 4, x + 1] = colorType;

                mtextMap[y, x + 2] = colorType;
                mtextMap[y + 1, x + 2] = colorType;
                mtextMap[y + 2, x + 2] = colorType;

                mtextMap[y + 3, x + 3] = colorType;
                mtextMap[y + 4, x + 3] = colorType;

                mtextMap[y, x + 4] = colorType;
                mtextMap[y + 1, x + 4] = colorType;
                mtextMap[y + 2, x + 4] = colorType;
            }
            else if(text == 'E')
            {
                mtextMap[y, x] = colorType;
                mtextMap[y, x + 1] = colorType;
                mtextMap[y, x + 2] = colorType;
                mtextMap[y, x + 3] = colorType;
                mtextMap[y, x + 4] = colorType;

                mtextMap[y + 1, x] = colorType;

                mtextMap[y + 2, x] = colorType;
                mtextMap[y + 2, x + 1] = colorType;
                mtextMap[y + 2, x + 2] = colorType;
                mtextMap[y + 2, x + 3] = colorType;
                mtextMap[y + 2, x + 4] = colorType;

                mtextMap[y + 3, x] = colorType;

                mtextMap[y + 4, x] = colorType;
                mtextMap[y + 4, x + 1] = colorType;
                mtextMap[y + 4, x + 2] = colorType;
                mtextMap[y + 4, x + 3] = colorType;
                mtextMap[y + 4, x + 4] = colorType;
            }
            else if(text == 'S')
            {
                mtextMap[y, x] = colorType;
                mtextMap[y, x + 1] = colorType;
                mtextMap[y, x + 2] = colorType;
                mtextMap[y, x + 3] = colorType;
                mtextMap[y, x + 4] = colorType;

                mtextMap[y + 3, x + 4] = colorType;

                mtextMap[y + 2, x] = colorType;
                mtextMap[y + 2, x + 1] = colorType;
                mtextMap[y + 2, x + 2] = colorType;
                mtextMap[y + 2, x + 3] = colorType;
                mtextMap[y + 2, x + 4] = colorType;

                mtextMap[y + 1, x] = colorType;

                mtextMap[y + 4, x] = colorType;
                mtextMap[y + 4, x + 1] = colorType;
                mtextMap[y + 4, x + 2] = colorType;
                mtextMap[y + 4, x + 3] = colorType;
                mtextMap[y + 4, x + 4] = colorType;
            }
            else if(text == 'A')
            {
                mtextMap[y, x + 2] = colorType;

                mtextMap[y + 1, x + 1] = colorType;
                mtextMap[y + 1, x + 3] = colorType;

                mtextMap[y + 2, x + 1] = colorType;
                mtextMap[y + 2, x + 2] = colorType;
                mtextMap[y + 2, x + 3] = colorType;

                mtextMap[y + 3, x] = colorType;
                mtextMap[y + 3, x + 4] = colorType;

                mtextMap[y + 4, x] = colorType;
                mtextMap[y + 4, x + 4] = colorType;
            }
            else if(text == 'L')
            {
                mtextMap[y + 4, x] = colorType;
                mtextMap[y + 4, x + 1] = colorType;
                mtextMap[y + 4, x + 2] = colorType;
                mtextMap[y + 4, x + 3] = colorType;
                mtextMap[y + 4, x + 4] = colorType;

                mtextMap[y + 3, x] = colorType;

                mtextMap[y + 2, x] = colorType;

                mtextMap[y + 1, x] = colorType;

                mtextMap[y, x] = colorType;
            }
            else if(text == 'U')
            {
                mtextMap[y + 4, x] = colorType;
                mtextMap[y + 4, x + 1] = colorType;
                mtextMap[y + 4, x + 2] = colorType;
                mtextMap[y + 4, x + 3] = colorType;
                mtextMap[y + 4, x + 4] = colorType;

                mtextMap[y + 3, x] = colorType;
                mtextMap[y + 3, x + 4] = colorType;

                mtextMap[y + 2, x] = colorType;
                mtextMap[y + 2, x + 4] = colorType;

                mtextMap[y + 1, x] = colorType;
                mtextMap[y + 1, x + 4] = colorType;

                mtextMap[y, x] = colorType;
                mtextMap[y, x + 4] = colorType;
            }
            else if(text == 'T')
            {
                mtextMap[y, x] = colorType;
                mtextMap[y, x + 1] = colorType;
                mtextMap[y, x + 2] = colorType;
                mtextMap[y, x + 3] = colorType;
                mtextMap[y, x + 4] = colorType;

                mtextMap[y + 1, x + 2] = colorType;

                mtextMap[y + 2, x + 2] = colorType;

                mtextMap[y + 3, x + 2] = colorType;

                mtextMap[y + 4, x + 2] = colorType;
            }
            else if(text == 'H')
            {
                mtextMap[y + 4, x] = colorType;
                mtextMap[y + 4, x + 4] = colorType;

                mtextMap[y + 3, x] = colorType;
                mtextMap[y + 3, x + 4] = colorType;

                mtextMap[y + 2, x] = colorType;
                mtextMap[y + 2, x + 1] = colorType;
                mtextMap[y + 2, x + 2] = colorType;
                mtextMap[y + 2, x + 3] = colorType;
                mtextMap[y + 2, x + 4] = colorType;

                mtextMap[y + 1, x] = colorType;
                mtextMap[y + 1, x + 4] = colorType;

                mtextMap[y, x] = colorType;
                mtextMap[y, x + 4] = colorType;
            }
            else if(text == 'C')
            {
                mtextMap[y + 4, x] = colorType;
                mtextMap[y + 4, x + 1] = colorType;
                mtextMap[y + 4, x + 2] = colorType;
                mtextMap[y + 4, x + 3] = colorType;
                mtextMap[y + 4, x + 4] = colorType;

                mtextMap[y + 3, x] = colorType;
                mtextMap[y + 3, x + 4] = colorType;

                mtextMap[y + 2, x] = colorType;

                mtextMap[y + 1, x] = colorType;
                mtextMap[y + 1, x + 4] = colorType;

                mtextMap[y, x] = colorType;
                mtextMap[y, x + 1] = colorType;
                mtextMap[y, x + 2] = colorType;
                mtextMap[y, x + 3] = colorType;
                mtextMap[y, x + 4] = colorType;
            }
            else if(text == 'O')
            {
                mtextMap[y + 4, x] = colorType;
                mtextMap[y + 4, x + 1] = colorType;
                mtextMap[y + 4, x + 2] = colorType;
                mtextMap[y + 4, x + 3] = colorType;
                mtextMap[y + 4, x + 4] = colorType;

                mtextMap[y + 3, x] = colorType;
                mtextMap[y + 3, x + 4] = colorType;

                mtextMap[y + 2, x] = colorType;
                mtextMap[y + 2, x + 4] = colorType;

                mtextMap[y + 1, x] = colorType;
                mtextMap[y + 1, x + 4] = colorType;

                mtextMap[y, x] = colorType;
                mtextMap[y, x + 1] = colorType;
                mtextMap[y, x + 2] = colorType;
                mtextMap[y, x + 3] = colorType;
                mtextMap[y, x + 4] = colorType;
            }
            else if(text == 'R')
            {
                mtextMap[y, x] = colorType;
                mtextMap[y, x + 1] = colorType;
                mtextMap[y, x + 2] = colorType;
                mtextMap[y, x + 3] = colorType;

                mtextMap[y + 1, x] = colorType;
                mtextMap[y + 1, x + 4] = colorType;

                mtextMap[y + 2, x] = colorType;
                mtextMap[y + 2, x + 1] = colorType;
                mtextMap[y + 2, x + 2] = colorType;
                mtextMap[y + 2, x + 3] = colorType;

                mtextMap[y + 3, x] = colorType;
                mtextMap[y + 3, x + 1] = colorType;

                mtextMap[y + 4, x] = colorType;
                mtextMap[y + 4, x + 2] = colorType;
                mtextMap[y + 4, x + 3] = colorType;
            }
            else if(text == 'N')
            {
                mtextMap[y, x] = colorType;
                mtextMap[y + 1, x] = colorType;
                mtextMap[y + 2, x] = colorType;
                mtextMap[y + 3, x] = colorType;
                mtextMap[y + 4, x] = colorType;

                mtextMap[y + 1, x + 1] = colorType;

                mtextMap[y + 2, x + 2] = colorType;

                mtextMap[y + 3, x + 3] = colorType;

                mtextMap[y + 4, x + 4] = colorType;
                mtextMap[y + 3, x + 4] = colorType;
                mtextMap[y + 2, x + 4] = colorType;
                mtextMap[y + 1, x + 4] = colorType;
                mtextMap[y, x + 4] = colorType;
            }
            else if(text == 'F')
            {
                mtextMap[y, x] = colorType;
                mtextMap[y, x + 1] = colorType;
                mtextMap[y, x + 2] = colorType;
                mtextMap[y, x + 3] = colorType;
                mtextMap[y, x + 4] = colorType;

                mtextMap[y + 1, x] = colorType;

                mtextMap[y + 2, x] = colorType;
                mtextMap[y + 2, x + 1] = colorType;
                mtextMap[y + 2, x + 2] = colorType;
                mtextMap[y + 2, x + 3] = colorType;
                mtextMap[y + 2, x + 4] = colorType;

                mtextMap[y + 3, x] = colorType;

                mtextMap[y + 4, x] = colorType;
            }
            else if(text == 'I')
            {
                mtextMap[y + 4, x + 1] = colorType;
                mtextMap[y + 4, x + 3] = colorType;

                mtextMap[y + 4, x + 2] = colorType;
                mtextMap[y + 3, x + 2] = colorType;
                mtextMap[y + 2, x + 2] = colorType;
                mtextMap[y + 1, x + 2] = colorType;
                mtextMap[y, x + 2] = colorType;

                mtextMap[y, x + 1] = colorType;
                mtextMap[y, x + 3] = colorType;
            }
            else if(text == 'D')
            {
                mtextMap[y + 4, x] = colorType;
                mtextMap[y + 4, x + 1] = colorType;
                mtextMap[y + 4, x + 2] = colorType;

                mtextMap[y + 3, x] = colorType;
                mtextMap[y + 3, x + 3] = colorType;

                mtextMap[y + 2, x] = colorType;
                mtextMap[y + 2, x + 3] = colorType;

                mtextMap[y + 1, x] = colorType;
                mtextMap[y + 1, x + 3] = colorType;

                mtextMap[y, x] = colorType;
                mtextMap[y, x + 1] = colorType;
                mtextMap[y, x + 2] = colorType;
            }
            else if(text == 'K')
            {
                mtextMap[y + 4, x] = colorType;
                mtextMap[y + 3, x] = colorType;
                mtextMap[y + 2, x] = colorType;
                mtextMap[y + 1, x] = colorType;
                mtextMap[y, x] = colorType;

                mtextMap[y + 4, x + 3] = colorType;
                mtextMap[y + 3, x + 2] = colorType;
                mtextMap[y + 2, x + 1] = colorType;
                mtextMap[y + 1, x + 2] = colorType;
                mtextMap[y, x + 3] = colorType;
            }
            else if(text == 'Y')
            {
                mtextMap[y + 2, x + 2] = colorType;
                mtextMap[y + 3, x + 2] = colorType;
                mtextMap[y + 4, x + 2] = colorType;

                mtextMap[y + 1, x + 1] = colorType;
                mtextMap[y + 1, x + 3] = colorType;

                mtextMap[y, x] = colorType;
                mtextMap[y, x + 4] = colorType;
            }
            else if(text == 'Q')
            {
                mtextMap[y, x] = colorType;
                mtextMap[y + 1, x] = colorType;
                mtextMap[y + 2, x] = colorType;

                mtextMap[y, x + 1] = colorType;
                mtextMap[y + 1, x + 1] = colorType;
                mtextMap[y + 2, x + 1] = colorType;
                mtextMap[y + 3, x + 1] = colorType;

                mtextMap[y + 1, x + 2] = colorType;
                mtextMap[y + 2, x + 2] = colorType;
                mtextMap[y + 3, x + 2] = colorType;
                mtextMap[y + 4, x + 2] = colorType;

                mtextMap[y, x + 3] = colorType;
                mtextMap[y + 1, x + 3] = colorType;
                mtextMap[y + 2, x + 3] = colorType;
                mtextMap[y + 3, x + 3] = colorType;

                mtextMap[y, x + 4] = colorType;
                mtextMap[y + 1, x + 4] = colorType;
                mtextMap[y + 2, x + 4] = colorType;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Fourier fourier = new Fourier(10, 20, 10, 10, 5, 0.001, -5);
            fourier.Print();

            Console.ReadLine();
        }
    }
}
