using System;
namespace MSYS.Security
{   
 
  
    public class SecCheck
    {
        private const int AA1YNilqR20iU4oER62ESkJKEw26 = 70;
        private static readonly int[] ABIi = new int[] { 
            2, 3, 5, 7, 11, 13, 0x11, 0x13, 0x17, 0x1d, 0x1f, 0x25, 0x29, 0x2b, 0x2f, 0x35,
            0x3b, 0x3d, 0x43, 0x47, 0x49, 0x4f, 0x53, 0x59, 0x61, 0x65, 0x67, 0x6b, 0x6d, 0x71, 0x7f, 0x83,
            0x89, 0x8b, 0x95, 0x97, 0x9d, 0xa3, 0xa7, 0xad, 0xb3, 0xb5, 0xbf, 0xc1, 0xc5, 0xc7, 0xd3, 0xdf,
            0xe3, 0xe5, 0xe9, 0xef, 0xf1, 0xfb, 0x101, 0x107, 0x10d, 0x10f, 0x115, 0x119, 0x11b, 0x125, 0x133, 0x137,
            0x139, 0x13d, 0x14b, 0x151, 0x15b, 0x15d, 0x161, 0x167, 0x16f, 0x175, 0x17b, 0x17f, 0x185, 0x18d, 0x191, 0x199,
            0x1a3, 0x1a5, 0x1af, 0x1b1, 0x1b7, 0x1bb, 0x1c1, 0x1c9, 0x1cd, 0x1cf, 0x1d3, 0x1df, 0x1e7, 0x1eb, 0x1f3, 0x1f7,
            0x1fd, 0x209, 0x20b, 0x21d, 0x223, 0x22d, 0x233, 0x239, 0x23b, 0x241, 0x24b, 0x251, 0x257, 0x259, 0x25f, 0x265,
            0x269, 0x26b, 0x277, 0x281, 0x283, 0x287, 0x28d, 0x293, 0x295, 0x2a1, 0x2a5, 0x2ab, 0x2b3, 0x2bd, 0x2c5, 0x2cf,
            0x2d7, 0x2dd, 0x2e3, 0x2e7, 0x2ef, 0x2f5, 0x2f9, 0x301, 0x305, 0x313, 0x31d, 0x329, 0x32b, 0x335, 0x337, 0x33b,
            0x33d, 0x347, 0x355, 0x359, 0x35b, 0x35f, 0x36d, 0x371, 0x373, 0x377, 0x38b, 0x38f, 0x397, 0x3a1, 0x3a9, 0x3ad,
            0x3b3, 0x3b9, 0x3c7, 0x3cb, 0x3d1, 0x3d7, 0x3df, 0x3e5, 0x3f1, 0x3f5, 0x3fb, 0x3fd, 0x407, 0x409, 0x40f, 0x419,
            0x41b, 0x425, 0x427, 0x42d, 0x43f, 0x443, 0x445, 0x449, 0x44f, 0x455, 0x45d, 0x463, 0x469, 0x47f, 0x481, 0x48b,
            0x493, 0x49d, 0x4a3, 0x4a9, 0x4b1, 0x4bd, 0x4c1, 0x4c7, 0x4cd, 0x4cf, 0x4d5, 0x4e1, 0x4eb, 0x4fd, 0x4ff, 0x503,
            0x509, 0x50b, 0x511, 0x515, 0x517, 0x51b, 0x527, 0x529, 0x52f, 0x551, 0x557, 0x55d, 0x565, 0x577, 0x581, 0x58f,
            0x593, 0x595, 0x599, 0x59f, 0x5a7, 0x5ab, 0x5ad, 0x5b3, 0x5bf, 0x5c9, 0x5cb, 0x5cf, 0x5d1, 0x5d5, 0x5db, 0x5e7,
            0x5f3, 0x5fb, 0x607, 0x60d, 0x611, 0x617, 0x61f, 0x623, 0x62b, 0x62f, 0x63d, 0x641, 0x647, 0x649, 0x64d, 0x653,
            0x655, 0x65b, 0x665, 0x679, 0x67f, 0x683, 0x685, 0x69d, 0x6a1, 0x6a3, 0x6ad, 0x6b9, 0x6bb, 0x6c5, 0x6cd, 0x6d3,
            0x6d9, 0x6df, 0x6f1, 0x6f7, 0x6fb, 0x6fd, 0x709, 0x713, 0x71f, 0x727, 0x737, 0x745, 0x74b, 0x74f, 0x751, 0x755,
            0x757, 0x761, 0x76d, 0x773, 0x779, 0x78b, 0x78d, 0x79d, 0x79f, 0x7b5, 0x7bb, 0x7c3, 0x7c9, 0x7cd, 0x7cf
        };
        private uint[] ACdKG;
        private int AD_pXpk;

        public SecCheck()
        {
            this.ACdKG = null;
            this.ACdKG = new uint[70];
            this.AD_pXpk = 1;
        }

        public SecCheck(long value)
        {
            this.ACdKG = null;
            this.ACdKG = new uint[70];
            long num = value;
            this.AD_pXpk = 0;
            while ((value != 0L) && (this.AD_pXpk < 70))
            {
                this.ACdKG[this.AD_pXpk] = (uint) (((ulong) value) & 0xffffffffL);
                value = value >> 0x20;
                this.AD_pXpk++;
            }
            if (num > 0L)
            {
                if ((value != 0L) || ((this.ACdKG[0x45] & 0x80000000) != 0))
                {
                    throw new ArithmeticException("Positive overflow in constructor.");
                }
            }
            else if ((num < 0L) && ((value != -1L) || ((this.ACdKG[this.AD_pXpk - 1] & 0x80000000) == 0)))
            {
                throw new ArithmeticException("Negative underflow in constructor.");
            }
            if (this.AD_pXpk == 0)
            {
                this.AD_pXpk = 1;
            }
        }

        public SecCheck(ulong value)
        {
            this.ACdKG = null;
            this.ACdKG = new uint[70];
            this.AD_pXpk = 0;
            while ((value != 0L) && (this.AD_pXpk < 70))
            {
                this.ACdKG[this.AD_pXpk] = (uint) (value & 0xffffffffL);
                value = value >> 0x20;
                this.AD_pXpk++;
            }
            if ((value != 0L) || ((this.ACdKG[0x45] & 0x80000000) != 0))
            {
                throw new ArithmeticException("Positive overflow in constructor.");
            }
            if (this.AD_pXpk == 0)
            {
                this.AD_pXpk = 1;
            }
        }

        public SecCheck(SecCheck bi)
        {
            this.ACdKG = null;
            this.ACdKG = new uint[70];
            this.AD_pXpk = bi.AD_pXpk;
            for (int i = 0; i < this.AD_pXpk; i++)
            {
                this.ACdKG[i] = bi.ACdKG[i];
            }
        }

        public SecCheck(byte[] inData)
        {
            this.ACdKG = null;
            this.AD_pXpk = inData.Length >> 2;
            int num = inData.Length & 3;
            if (num != 0)
            {
                this.AD_pXpk++;
            }
            if (this.AD_pXpk > 70)
            {
                throw new ArithmeticException("Byte overflow in constructor.");
            }
            this.ACdKG = new uint[70];
            int index = inData.Length - 1;
            for (int i = 0; index >= 3; i++)
            {
                this.ACdKG[i] = (uint) ((((inData[index - 3] << 0x18) + (inData[index - 2] << 0x10)) + (inData[index - 1] << 8)) + inData[index]);
                index -= 4;
            }
            switch (num)
            {
                case 1:
                    this.ACdKG[this.AD_pXpk - 1] = inData[0];
                    break;

                case 2:
                    this.ACdKG[this.AD_pXpk - 1] = (uint) ((inData[0] << 8) + inData[1]);
                    break;

                case 3:
                    this.ACdKG[this.AD_pXpk - 1] = (uint) (((inData[0] << 0x10) + (inData[1] << 8)) + inData[2]);
                    break;
            }
            while ((this.AD_pXpk > 1) && (this.ACdKG[this.AD_pXpk - 1] == 0))
            {
                this.AD_pXpk--;
            }
        }

        public SecCheck(uint[] inData)
        {
            this.ACdKG = null;
            this.AD_pXpk = inData.Length;
            if (this.AD_pXpk > 70)
            {
                throw new ArithmeticException("Byte overflow in constructor.");
            }
            this.ACdKG = new uint[70];
            int index = this.AD_pXpk - 1;
            for (int i = 0; index >= 0; i++)
            {
                this.ACdKG[i] = inData[index];
                index--;
            }
            while ((this.AD_pXpk > 1) && (this.ACdKG[this.AD_pXpk - 1] == 0))
            {
                this.AD_pXpk--;
            }
        }

        public SecCheck(string value, int radix)
        {
            int num2;
            this.ACdKG = null;
            SecCheck check = new SecCheck(1L);
            SecCheck check2 = new SecCheck();
            value = value.ToUpper().Trim();
            int num = 0;
            if (value[0] == '-')
            {
                num = 1;
            }
            for (num2 = value.Length - 1; num2 >= num; num2--)
            {
                int num3 = value[num2];
                if ((num3 >= 0x30) && (num3 <= 0x39))
                {
                    num3 -= 0x30;
                }
                else if ((num3 >= 0x41) && (num3 <= 90))
                {
                    num3 = (num3 - 0x41) + 10;
                }
                else
                {
                    num3 = 0x98967f;
                }
                if (num3 >= radix)
                {
                    throw new ArithmeticException("Invalid string in constructor.");
                }
                if (value[0] == '-')
                {
                    num3 = -num3;
                }
                check2 += check * num3;
                if ((num2 - 1) >= num)
                {
                    check *= radix;
                }
            }
            if (value[0] == '-')
            {
                if ((check2.ACdKG[0x45] & 0x80000000) == 0)
                {
                    throw new ArithmeticException("Negative underflow in constructor.");
                }
            }
            else if ((check2.ACdKG[0x45] & 0x80000000) != 0)
            {
                throw new ArithmeticException("Positive overflow in constructor.");
            }
            this.ACdKG = new uint[70];
            for (num2 = 0; num2 < check2.AD_pXpk; num2++)
            {
                this.ACdKG[num2] = check2.ACdKG[num2];
            }
            this.AD_pXpk = check2.AD_pXpk;
        }

        public SecCheck(byte[] inData, int inLen)
        {
            this.ACdKG = null;
            this.AD_pXpk = inLen >> 2;
            int num = inLen & 3;
            if (num != 0)
            {
                this.AD_pXpk++;
            }
            if ((this.AD_pXpk > 70) || (inLen > inData.Length))
            {
                throw new ArithmeticException("Byte overflow in constructor.");
            }
            this.ACdKG = new uint[70];
            int index = inLen - 1;
            for (int i = 0; index >= 3; i++)
            {
                this.ACdKG[i] = (uint) ((((inData[index - 3] << 0x18) + (inData[index - 2] << 0x10)) + (inData[index - 1] << 8)) + inData[index]);
                index -= 4;
            }
            switch (num)
            {
                case 1:
                    this.ACdKG[this.AD_pXpk - 1] = inData[0];
                    break;

                case 2:
                    this.ACdKG[this.AD_pXpk - 1] = (uint) ((inData[0] << 8) + inData[1]);
                    break;

                case 3:
                    this.ACdKG[this.AD_pXpk - 1] = (uint) (((inData[0] << 0x10) + (inData[1] << 8)) + inData[2]);
                    break;
            }
            if (this.AD_pXpk == 0)
            {
                this.AD_pXpk = 1;
            }
            while ((this.AD_pXpk > 1) && (this.ACdKG[this.AD_pXpk - 1] == 0))
            {
                this.AD_pXpk--;
            }
        }

        private static int func_AA1YNilqR20iU4oER62ESkJKEw26(uint[] numArray1, int num1)
        {
            int num = 0x20;
            int length = numArray1.Length;
            while ((length > 1) && (numArray1[length - 1] == 0))
            {
                length--;
            }
            for (int i = num1; i > 0; i -= num)
            {
                if (i < num)
                {
                    num = i;
                }
                ulong num4 = 0L;
                for (int j = 0; j < length; j++)
                {
                    ulong num6 = numArray1[j] << num;
                    num6 |= num4;
                    numArray1[j] = (uint) (num6 & 0xffffffffL);
                    num4 = num6 >> 0x20;
                }
                if ((num4 != 0L) && ((length + 1) <= numArray1.Length))
                {
                    numArray1[length] = (uint) num4;
                    length++;
                }
            }
            return length;
        }

        private static int func_ABIi(uint[] numArray1, int num1)
        {
            int num = 0x20;
            int num2 = 0;
            int length = numArray1.Length;
            while ((length > 1) && (numArray1[length - 1] == 0))
            {
                length--;
            }
            for (int i = num1; i > 0; i -= num)
            {
                if (i < num)
                {
                    num = i;
                    num2 = 0x20 - num;
                }
                ulong num5 = 0L;
                for (int j = length - 1; j >= 0; j--)
                {
                    ulong num7 = numArray1[j] >> num;
                    num7 |= num5;
                    num5 = numArray1[j] << num2;
                    numArray1[j] = (uint) num7;
                }
            }
            while ((length > 1) && (numArray1[length - 1] == 0))
            {
                length--;
            }
            return length;
        }

        public SecCheck abs()
        {
            if ((this.ACdKG[0x45] & 0x80000000) != 0)
            {
                return -this;
            }
            return new SecCheck(this);
        }

        private bool func_ACdKG(SecCheck check1)
        {
            int num9;
            long a = 5L;
            long num2 = -1L;
            long num3 = 0L;
            bool flag = false;
            while (!flag)
            {
                int num4 = Jacobi(a, check1);
                if (num4 == -1)
                {
                    flag = true;
                }
                else
                {
                    if ((num4 == 0) && (Math.Abs(a) < check1))
                    {
                        return false;
                    }
                    if (num3 == 20L)
                    {
                        SecCheck check = check1.sqrt();
                        if ((check * check) == check1)
                        {
                            return false;
                        }
                    }
                    a = (Math.Abs(a) + 2L) * num2;
                    num2 = -num2;
                }
                num3 += 1L;
            }
            long bi = (1L - a) >> 2;
            SecCheck check2 = check1 + 1;
            int num6 = 0;
            for (int i = 0; i < check2.AD_pXpk; i++)
            {
                uint num8 = 1;
                num9 = 0;
                while (num9 < 0x20)
                {
                    if ((check2.ACdKG[i] & num8) != 0)
                    {
                        i = check2.AD_pXpk;
                        break;
                    }
                    num8 = num8 << 1;
                    num6++;
                    num9++;
                }
            }
            SecCheck check3 = check2 >> num6;
            SecCheck constant = new SecCheck();
            int index = check1.AD_pXpk << 1;
            constant.ACdKG[index] = 1;
            constant.AD_pXpk = index + 1;
            constant /= check1;
            SecCheck[] checkArray = func_AD_pXpk(1, bi, check3, check1, constant, 0);
            bool flag2 = false;
            if (((checkArray[0].AD_pXpk == 1) && (checkArray[0].ACdKG[0] == 0)) || ((checkArray[1].AD_pXpk == 1) && (checkArray[1].ACdKG[0] == 0)))
            {
                flag2 = true;
            }
            for (num9 = 1; num9 < num6; num9++)
            {
                if (!flag2)
                {
                    checkArray[1] = check1.BarrettReduction(checkArray[1] * checkArray[1], check1, constant);
                    checkArray[1] = (checkArray[1] - (checkArray[2] << 1)) % check1;
                    if ((checkArray[1].AD_pXpk == 1) && (checkArray[1].ACdKG[0] == 0))
                    {
                        flag2 = true;
                    }
                }
                checkArray[2] = check1.BarrettReduction(checkArray[2] * checkArray[2], check1, constant);
            }
            if (flag2)
            {
                SecCheck check5 = check1.gcd(bi);
                if ((check5.AD_pXpk == 1) && (check5.ACdKG[0] == 1))
                {
                    if ((checkArray[2].ACdKG[0x45] & 0x80000000) != 0)
                    {
                        checkArray[2] += check1;
                    }
                    SecCheck check6 = (bi * Jacobi(bi, check1)) % check1;
                    if ((check6.ACdKG[0x45] & 0x80000000) != 0)
                    {
                        check6 += check1;
                    }
                    if (checkArray[2] != check6)
                    {
                        flag2 = false;
                    }
                }
            }
            return flag2;
        }

        private static SecCheck[] func_AD_pXpk(SecCheck check6, SecCheck check8, SecCheck check1, SecCheck check5, SecCheck check7, int num1)
        {
            int num3;
            SecCheck[] checkArray = new SecCheck[3];
            if ((check1.ACdKG[0] & 1) == 0)
            {
                throw new ArgumentException("Argument k must be odd.");
            }
            int num = check1.bitCount();
            uint num2 = ((uint) 1) << ((num & 0x1f) - 1);
            SecCheck check = 2 % check5;
            SecCheck check2 = 1 % check5;
            SecCheck check3 = check6 % check5;
            SecCheck check4 = check2;
            bool flag = true;
            for (num3 = check1.AD_pXpk - 1; num3 >= 0; num3--)
            {
                while (num2 != 0)
                {
                    if ((num3 == 0) && (num2 == 1))
                    {
                        break;
                    }
                    if ((check1.ACdKG[num3] & num2) != 0)
                    {
                        check4 = (check4 * check3) % check5;
                        check = ((check * check3) - (check6 * check2)) % check5;
                        check3 = (check5.BarrettReduction(check3 * check3, check5, check7) - ((check2 * check8) << 1)) % check5;
                        if (flag)
                        {
                            flag = false;
                        }
                        else
                        {
                            check2 = check5.BarrettReduction(check2 * check2, check5, check7);
                        }
                        check2 = (check2 * check8) % check5;
                    }
                    else
                    {
                        check4 = ((check4 * check) - check2) % check5;
                        check3 = ((check * check3) - (check6 * check2)) % check5;
                        check = (check5.BarrettReduction(check * check, check5, check7) - (check2 << 1)) % check5;
                        if (flag)
                        {
                            check2 = check8 % check5;
                            flag = false;
                        }
                        else
                        {
                            check2 = check5.BarrettReduction(check2 * check2, check5, check7);
                        }
                    }
                    num2 = num2 >> 1;
                }
                num2 = 0x80000000;
            }
            check4 = ((check4 * check) - check2) % check5;
            check = ((check * check3) - (check6 * check2)) % check5;
            if (flag)
            {
                flag = false;
            }
            else
            {
                check2 = check5.BarrettReduction(check2 * check2, check5, check7);
            }
            check2 = (check2 * check8) % check5;
            for (num3 = 0; num3 < num1; num3++)
            {
                check4 = (check4 * check) % check5;
                check = ((check * check) - (check2 << 1)) % check5;
                if (flag)
                {
                    check2 = check8 % check5;
                    flag = false;
                }
                else
                {
                    check2 = check5.BarrettReduction(check2 * check2, check5, check7);
                }
            }
            checkArray[0] = check4;
            checkArray[1] = check;
            checkArray[2] = check2;
            return checkArray;
        }

        public SecCheck BarrettReduction(SecCheck x, SecCheck n, SecCheck constant)
        {
            int num5;
            int num = n.AD_pXpk;
            int index = num + 1;
            int num3 = num - 1;
            SecCheck check = new SecCheck();
            int num4 = num3;
            for (num5 = 0; num4 < x.AD_pXpk; num5++)
            {
                check.ACdKG[num5] = x.ACdKG[num4];
                num4++;
            }
            check.AD_pXpk = x.AD_pXpk - num3;
            if (check.AD_pXpk <= 0)
            {
                check.AD_pXpk = 1;
            }
            SecCheck check2 = check * constant;
            SecCheck check3 = new SecCheck();
            num4 = index;
            num5 = 0;
            while (num4 < check2.AD_pXpk)
            {
                check3.ACdKG[num5] = check2.ACdKG[num4];
                num4++;
                num5++;
            }
            check3.AD_pXpk = check2.AD_pXpk - index;
            if (check3.AD_pXpk <= 0)
            {
                check3.AD_pXpk = 1;
            }
            SecCheck check4 = new SecCheck();
            int num6 = (x.AD_pXpk > index) ? index : x.AD_pXpk;
            for (num4 = 0; num4 < num6; num4++)
            {
                check4.ACdKG[num4] = x.ACdKG[num4];
            }
            check4.AD_pXpk = num6;
            SecCheck check5 = new SecCheck();
            for (num4 = 0; num4 < check3.AD_pXpk; num4++)
            {
                if (check3.ACdKG[num4] != 0)
                {
                    ulong num7 = 0L;
                    int num8 = num4;
                    num5 = 0;
                    while ((num5 < n.AD_pXpk) && (num8 < index))
                    {
                        ulong num9 = ((check3.ACdKG[num4] * n.ACdKG[num5]) + check5.ACdKG[num8]) + num7;
                        check5.ACdKG[num8] = (uint) (num9 & 0xffffffffL);
                        num7 = num9 >> 0x20;
                        num5++;
                        num8++;
                    }
                    if (num8 < index)
                    {
                        check5.ACdKG[num8] = (uint) num7;
                    }
                }
            }
            check5.AD_pXpk = index;
            while ((check5.AD_pXpk > 1) && (check5.ACdKG[check5.AD_pXpk - 1] == 0))
            {
                check5.AD_pXpk--;
            }
            check4 -= check5;
            if ((check4.ACdKG[0x45] & 0x80000000) != 0)
            {
                SecCheck check6 = new SecCheck();
                check6.ACdKG[index] = 1;
                check6.AD_pXpk = index + 1;
                check4 += check6;
            }
            while (check4 >= n)
            {
                check4 -= n;
            }
            return check4;
        }

        public int bitCount()
        {
            while ((this.AD_pXpk > 1) && (this.ACdKG[this.AD_pXpk - 1] == 0))
            {
                this.AD_pXpk--;
            }
            uint num = this.ACdKG[this.AD_pXpk - 1];
            uint num2 = 0x80000000;
            int num3 = 0x20;
            while ((num3 > 0) && ((num & num2) == 0))
            {
                num3--;
                num2 = num2 >> 1;
            }
            return (num3 + ((this.AD_pXpk - 1) << 5));
        }

        public override bool Equals(object o)
        {
            SecCheck check = (SecCheck) o;
            if (this.AD_pXpk != check.AD_pXpk)
            {
                return false;
            }
            for (int i = 0; i < this.AD_pXpk; i++)
            {
                if (this.ACdKG[i] != check.ACdKG[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool FermatLittleTest(int confidence)
        {
            SecCheck check;
            if ((this.ACdKG[0x45] & 0x80000000) != 0)
            {
                check = -this;
            }
            else
            {
                check = this;
            }
            if (check.AD_pXpk == 1)
            {
                if ((check.ACdKG[0] == 0) || (check.ACdKG[0] == 1))
                {
                    return false;
                }
                if ((check.ACdKG[0] == 2) || (check.ACdKG[0] == 3))
                {
                    return true;
                }
            }
            if ((check.ACdKG[0] & 1) == 0)
            {
                return false;
            }
            int num = check.bitCount();
            SecCheck check2 = new SecCheck();
            SecCheck exp = check - new SecCheck(1L);
            Random rand = new Random();
            for (int i = 0; i < confidence; i++)
            {
                bool flag = false;
                while (!flag)
                {
                    int bits = 0;
                    while (bits < 2)
                    {
                        bits = (int) (rand.NextDouble() * num);
                    }
                    check2.genRandomBits(bits, rand);
                    int num4 = check2.AD_pXpk;
                    if ((num4 > 1) || ((num4 == 1) && (check2.ACdKG[0] != 1)))
                    {
                        flag = true;
                    }
                }
                SecCheck check4 = check2.gcd(check);
                if ((check4.AD_pXpk == 1) && (check4.ACdKG[0] != 1))
                {
                    return false;
                }
                SecCheck check5 = check2.modPow(exp, check);
                int num5 = check5.AD_pXpk;
                if ((num5 > 1) || ((num5 == 1) && (check5.ACdKG[0] != 1)))
                {
                    return false;
                }
            }
            return true;
        }

        public SecCheck gcd(SecCheck bi)
        {
            SecCheck check;
            SecCheck check2;
            if ((this.ACdKG[0x45] & 0x80000000) != 0)
            {
                check = -this;
            }
            else
            {
                check = this;
            }
            if ((bi.ACdKG[0x45] & 0x80000000) != 0)
            {
                check2 = -bi;
            }
            else
            {
                check2 = bi;
            }
            SecCheck check3 = check2;
            while ((check.AD_pXpk > 1) || ((check.AD_pXpk == 1) && (check.ACdKG[0] != 0)))
            {
                check3 = check;
                check = check2 % check;
                check2 = check3;
            }
            return check3;
        }

        public SecCheck genCoPrime(int bits, Random rand)
        {
            bool flag = false;
            SecCheck check = new SecCheck();
            while (!flag)
            {
                check.genRandomBits(bits, rand);
                SecCheck check2 = check.gcd(this);
                if ((check2.AD_pXpk == 1) && (check2.ACdKG[0] == 1))
                {
                    flag = true;
                }
            }
            return check;
        }

        public static SecCheck genPseudoPrime(int bits, int confidence, Random rand)
        {
            SecCheck check = new SecCheck();
            for (bool flag = false; !flag; flag = check.isProbablePrime(confidence))
            {
                check.genRandomBits(bits, rand);
                check.ACdKG[0] |= 1;
            }
            return check;
        }

        public void genRandomBits(int bits, Random rand)
        {
            int num3;
            int num = bits >> 5;
            int num2 = bits & 0x1f;
            if (num2 != 0)
            {
                num++;
            }
            if (num > 70)
            {
                throw new ArithmeticException("Number of required bits > maxLength.");
            }
            for (num3 = 0; num3 < num; num3++)
            {
                this.ACdKG[num3] = (uint) (rand.NextDouble() * 4294967296);
            }
            for (num3 = num; num3 < 70; num3++)
            {
                this.ACdKG[num3] = 0;
            }
            if (num2 != 0)
            {
                uint num4 = ((uint) 1) << (num2 - 1);
                this.ACdKG[num - 1] |= num4;
                int x = -1;
                num4 = ((uint) x) >> (0x20 - num2);
                this.ACdKG[num - 1] &= num4;
            }
            else
            {
                this.ACdKG[num - 1] |= 0x80000000;
            }
            this.AD_pXpk = num;
            if (this.AD_pXpk == 0)
            {
                this.AD_pXpk = 1;
            }
        }

        public byte[] getBytes()
        {
            int num = this.bitCount();
            int num2 = num >> 3;
            if ((num & 7) != 0)
            {
                num2++;
            }
            byte[] buffer = new byte[num2];
            int index = 0;
            uint num5 = this.ACdKG[this.AD_pXpk - 1];
            uint num4 = (num5 >> 0x18) & 0xff;
            if (num4 != 0)
            {
                buffer[index++] = (byte) num4;
            }
            num4 = (num5 >> 0x10) & 0xff;
            if (num4 != 0)
            {
                buffer[index++] = (byte) num4;
            }
            num4 = (num5 >> 8) & 0xff;
            if (num4 != 0)
            {
                buffer[index++] = (byte) num4;
            }
            num4 = num5 & 0xff;
            if (num4 != 0)
            {
                buffer[index++] = (byte) num4;
            }
            int num6 = this.AD_pXpk - 2;
            while (num6 >= 0)
            {
                num5 = this.ACdKG[num6];
                buffer[index + 3] = (byte) (num5 & 0xff);
                num5 = num5 >> 8;
                buffer[index + 2] = (byte) (num5 & 0xff);
                num5 = num5 >> 8;
                buffer[index + 1] = (byte) (num5 & 0xff);
                num5 = num5 >> 8;
                buffer[index] = (byte) (num5 & 0xff);
                num6--;
                index += 4;
            }
            return buffer;
        }

        public override int GetHashCode() 
        {
          return  this.ToString().GetHashCode();
        }

        public int IntValue() 
        {
            return            ((int) this.ACdKG[0]);
        }

        public bool isProbablePrime()
        {
            SecCheck check;
            if ((this.ACdKG[0x45] & 0x80000000) != 0)
            {
                check = -this;
            }
            else
            {
                check = this;
            }
            if (check.AD_pXpk == 1)
            {
                if ((check.ACdKG[0] == 0) || (check.ACdKG[0] == 1))
                {
                    return false;
                }
                if ((check.ACdKG[0] == 2) || (check.ACdKG[0] == 3))
                {
                    return true;
                }
            }
            if ((check.ACdKG[0] & 1) == 0)
            {
                return false;
            }
            for (int i = 0; i < ABIi.Length; i++)
            {
                SecCheck check2 = ABIi[i];
                if (check2 >= check)
                {
                    break;
                }
                SecCheck check3 = check % check2;
                if (check3.IntValue() == 0)
                {
                    return false;
                }
            }
            SecCheck check4 = check - new SecCheck(1L);
            int num2 = 0;
            for (int j = 0; j < check4.AD_pXpk; j++)
            {
                uint num4 = 1;
                for (int m = 0; m < 0x20; m++)
                {
                    if ((check4.ACdKG[j] & num4) != 0)
                    {
                        j = check4.AD_pXpk;
                        break;
                    }
                    num4 = num4 << 1;
                    num2++;
                }
            }
            SecCheck exp = check4 >> num2;
            int num6 = check.bitCount();
            SecCheck check7 = new SecCheck(2).modPow(exp, check);
            bool flag = false;
            if ((check7.AD_pXpk == 1) && (check7.ACdKG[0] == 1))
            {
                flag = true;
            }
            for (int k = 0; !flag && (k < num2); k++)
            {
                if (check7 == check4)
                {
                    flag = true;
                    break;
                }
                check7 = (check7 * check7) % check;
            }
            if (flag)
            {
                flag = this.func_ACdKG(check);
            }
            return flag;
        }

        public bool isProbablePrime(int confidence)
        {
            SecCheck check;
            if ((this.ACdKG[0x45] & 0x80000000) != 0)
            {
                check = -this;
            }
            else
            {
                check = this;
            }
            for (int i = 0; i < ABIi.Length; i++)
            {
                SecCheck check2 = ABIi[i];
                if (check2 >= check)
                {
                    break;
                }
                SecCheck check3 = check % check2;
                if (check3.IntValue() == 0)
                {
                    return false;
                }
            }
            return check.RabinMillerTest(confidence);
        }

        public static int Jacobi(SecCheck a, SecCheck b)
        {
            if ((b.ACdKG[0] & 1) == 0)
            {
                throw new ArgumentException("Jacobi defined only for odd integers.");
            }
            if (a >= b)
            {
                a = a % b;
            }
            if ((a.AD_pXpk == 1) && (a.ACdKG[0] == 0))
            {
                return 0;
            }
            if ((a.AD_pXpk == 1) && (a.ACdKG[0] == 1))
            {
                return 1;
            }
            if (a < 0)
            {
                if (((b - 1).ACdKG[0] & 2) == 0)
                {
                    return Jacobi(-a, b);
                }
                return -Jacobi(-a, b);
            }
            int num = 0;
            for (int i = 0; i < a.AD_pXpk; i++)
            {
                uint num3 = 1;
                for (int j = 0; j < 0x20; j++)
                {
                    if ((a.ACdKG[i] & num3) != 0)
                    {
                        i = a.AD_pXpk;
                        break;
                    }
                    num3 = num3 << 1;
                    num++;
                }
            }
            SecCheck check = a >> num;
            int num5 = 1;
            if (((num & 1) != 0) && (((b.ACdKG[0] & 7) == 3) || ((b.ACdKG[0] & 7) == 5)))
            {
                num5 = -1;
            }
            if (((b.ACdKG[0] & 3) == 3) && ((check.ACdKG[0] & 3) == 3))
            {
                num5 = -num5;
            }
            if ((check.AD_pXpk == 1) && (check.ACdKG[0] == 1))
            {
                return num5;
            }
            return (num5 * Jacobi(b % check, check));
        }

        public long LongValue()
        {
            long num = 0L;
            num = this.ACdKG[0];
            try
            {
                num |= this.ACdKG[1] << 0x20;
            }
            catch (Exception)
            {
                if ((this.ACdKG[0] & 0x80000000) != 0)
                {
                    num = this.ACdKG[0];
                }
            }
            return num;
        }

        public static SecCheck[] LucasSequence(SecCheck P, SecCheck Q, SecCheck k, SecCheck n)
        {
            if ((k.AD_pXpk == 1) && (k.ACdKG[0] == 0))
            {
                return new SecCheck[] { 0, (2 % n), (1 % n) };
            }
            SecCheck check = new SecCheck();
            int index = n.AD_pXpk << 1;
            check.ACdKG[index] = 1;
            check.AD_pXpk = index + 1;
            check /= n;
            int num2 = 0;
            for (int i = 0; i < k.AD_pXpk; i++)
            {
                uint num4 = 1;
                for (int j = 0; j < 0x20; j++)
                {
                    if ((k.ACdKG[i] & num4) != 0)
                    {
                        i = k.AD_pXpk;
                        break;
                    }
                    num4 = num4 << 1;
                    num2++;
                }
            }
            SecCheck check2 = k >> num2;
            return func_AD_pXpk(P, Q, check2, n, check, num2);
        }

        public bool LucasStrongTest()
        {
            SecCheck check;
            if ((this.ACdKG[0x45] & 0x80000000) != 0)
            {
                check = -this;
            }
            else
            {
                check = this;
            }
            if (check.AD_pXpk == 1)
            {
                if ((check.ACdKG[0] == 0) || (check.ACdKG[0] == 1))
                {
                    return false;
                }
                if ((check.ACdKG[0] == 2) || (check.ACdKG[0] == 3))
                {
                    return true;
                }
            }
            if ((check.ACdKG[0] & 1) == 0)
            {
                return false;
            }
            return this.func_ACdKG(check);
        }

        public SecCheck max(SecCheck bi)
        {
            if (this > bi)
            {
                return new SecCheck(this);
            }
            return new SecCheck(bi);
        }

        public SecCheck min(SecCheck bi)
        {
            if (this < bi)
            {
                return new SecCheck(this);
            }
            return new SecCheck(bi);
        }

        public SecCheck modInverse(SecCheck modulus)
        {
            SecCheck[] checkArray = new SecCheck[] { 0, 1 };
            SecCheck[] checkArray2 = new SecCheck[2];
            SecCheck[] checkArray3 = new SecCheck[] { 0, 0 };
            int num = 0;
            SecCheck check = modulus;
            SecCheck check2 = this;
            while ((check2.AD_pXpk > 1) || ((check2.AD_pXpk == 1) && (check2.ACdKG[0] != 0)))
            {
                SecCheck outQuotient = new SecCheck();
                SecCheck outRemainder = new SecCheck();
                if (num > 1)
                {
                    SecCheck check5 = (checkArray[0] - (checkArray[1] * checkArray2[0])) % modulus;
                    checkArray[0] = checkArray[1];
                    checkArray[1] = check5;
                }
                if (check2.AD_pXpk == 1)
                {
                    singleByteDivide(check, check2, outQuotient, outRemainder);
                }
                else
                {
                    multiByteDivide(check, check2, outQuotient, outRemainder);
                }
                checkArray2[0] = checkArray2[1];
                checkArray3[0] = checkArray3[1];
                checkArray2[1] = outQuotient;
                checkArray3[1] = outRemainder;
                check = check2;
                check2 = outRemainder;
                num++;
            }
            if ((checkArray3[0].AD_pXpk > 1) || ((checkArray3[0].AD_pXpk == 1) && (checkArray3[0].ACdKG[0] != 1)))
            {
                throw new ArithmeticException("No inverse!");
            }
            SecCheck check6 = (checkArray[0] - (checkArray[1] * checkArray2[0])) % modulus;
            if ((check6.ACdKG[0x45] & 0x80000000) != 0)
            {
                check6 += modulus;
            }
            return check6;
        }

        public SecCheck modPow(SecCheck exp, SecCheck n)
        {
            SecCheck check2;
            if ((exp.ACdKG[0x45] & 0x80000000) != 0)
            {
                throw new ArithmeticException("Positive exponents only.");
            }
            SecCheck check = 1;
            bool flag = false;
            if ((this.ACdKG[0x45] & 0x80000000) != 0)
            {
                check2 = -this % n;
                flag = true;
            }
            else
            {
                check2 = this % n;
            }
            if ((n.ACdKG[0x45] & 0x80000000) != 0)
            {
                n = -n;
            }
            SecCheck constant = new SecCheck();
            int index = n.AD_pXpk << 1;
            constant.ACdKG[index] = 1;
            constant.AD_pXpk = index + 1;
            constant /= n;
            int num2 = exp.bitCount();
            int num3 = 0;
            for (int i = 0; i < exp.AD_pXpk; i++)
            {
                uint num5 = 1;
                for (int j = 0; j < 0x20; j++)
                {
                    if ((exp.ACdKG[i] & num5) != 0)
                    {
                        check = this.BarrettReduction(check * check2, n, constant);
                    }
                    num5 = num5 << 1;
                    check2 = this.BarrettReduction(check2 * check2, n, constant);
                    if ((check2.AD_pXpk == 1) && (check2.ACdKG[0] == 1))
                    {
                        if (flag && ((exp.ACdKG[0] & 1) != 0))
                        {
                            return -check;
                        }
                        return check;
                    }
                    num3++;
                    if (num3 == num2)
                    {
                        break;
                    }
                }
            }
            if (flag && ((exp.ACdKG[0] & 1) != 0))
            {
                return -check;
            }
            return check;
        }

        public static void MulDivTest(int rounds)
        {
            Random random = new Random();
            byte[] inData = new byte[0x40];
            byte[] buffer2 = new byte[0x40];
            for (int i = 0; i < rounds; i++)
            {
                int num4;
                int inLen = 0;
                while (inLen == 0)
                {
                    inLen = (int) (random.NextDouble() * 65.0);
                }
                int num3 = 0;
                while (num3 == 0)
                {
                    num3 = (int) (random.NextDouble() * 65.0);
                }
                bool flag = false;
                while (!flag)
                {
                    num4 = 0;
                    while (num4 < 0x40)
                    {
                        if (num4 < inLen)
                        {
                            inData[num4] = (byte) (random.NextDouble() * 256.0);
                        }
                        else
                        {
                            inData[num4] = 0;
                        }
                        if (inData[num4] != 0)
                        {
                            flag = true;
                        }
                        num4++;
                    }
                }
                flag = false;
                while (!flag)
                {
                    for (num4 = 0; num4 < 0x40; num4++)
                    {
                        if (num4 < num3)
                        {
                            buffer2[num4] = (byte) (random.NextDouble() * 256.0);
                        }
                        else
                        {
                            buffer2[num4] = 0;
                        }
                        if (buffer2[num4] != 0)
                        {
                            flag = true;
                        }
                    }
                }
                while (inData[0] == 0)
                {
                    inData[0] = (byte) (random.NextDouble() * 256.0);
                }
                while (buffer2[0] == 0)
                {
                    buffer2[0] = (byte) (random.NextDouble() * 256.0);
                }
                Console.WriteLine(i);
                SecCheck check = new SecCheck(inData, inLen);
                SecCheck check2 = new SecCheck(buffer2, num3);
                SecCheck check3 = check / check2;
                SecCheck check4 = check % check2;
                SecCheck check5 = (check3 * check2) + check4;
                if (check5 != check)
                {
                    Console.WriteLine("Error at " + i);
                    Console.WriteLine(check + "\r");
                    Console.WriteLine(check2 + "\r");
                    Console.WriteLine(check3 + "\r");
                    Console.WriteLine(check4 + "\r");
                    Console.WriteLine(check5 + "\r");
                    break;
                }
            }
        }

        public static void multiByteDivide(SecCheck bi1, SecCheck bi2, SecCheck outQuotient, SecCheck outRemainder)
        {
            uint[] numArray = new uint[70];
            int num = bi1.AD_pXpk + 1;
            uint[] numArray2 = new uint[num];
            uint num2 = 0x80000000;
            uint num3 = bi2.ACdKG[bi2.AD_pXpk - 1];
            int num4 = 0;
            int num5 = 0;
            while ((num2 != 0) && ((num3 & num2) == 0))
            {
                num4++;
                num2 = num2 >> 1;
            }
            for (int i = 0; i < bi1.AD_pXpk; i++)
            {
                numArray2[i] = bi1.ACdKG[i];
            }
            func_AA1YNilqR20iU4oER62ESkJKEw26(numArray2, num4);
            bi2 = bi2 << num4;
            int num7 = num - bi2.AD_pXpk;
            int index = num - 1;
            ulong num9 = bi2.ACdKG[bi2.AD_pXpk - 1];
            ulong num10 = bi2.ACdKG[bi2.AD_pXpk - 2];
            int num11 = bi2.AD_pXpk + 1;
            uint[] inData = new uint[num11];
            while (num7 > 0)
            {
                ulong num12 = (numArray2[index] << 0x20) + numArray2[index - 1];
                ulong num13 = num12 / num9;
                ulong num14 = num12 % num9;
                bool flag = false;
                while (!flag)
                {
                    flag = true;
                    if ((num13 == 0x100000000L) || ((num13 * num10) > ((num14 << 0x20) + numArray2[index - 2])))
                    {
                        num13 -= (ulong) 1L;
                        num14 += num9;
                        if (num14 < 0x100000000L)
                        {
                            flag = false;
                        }
                    }
                }
                int num15 = 0;
                while (num15 < num11)
                {
                    inData[num15] = numArray2[index - num15];
                    num15++;
                }
                SecCheck check = new SecCheck(inData);
                SecCheck check2 = bi2 * num13;
                while (check2 > check)
                {
                    num13 -= (ulong) 1L;
                    check2 -= bi2;
                }
                SecCheck check3 = check - check2;
                for (num15 = 0; num15 < num11; num15++)
                {
                    numArray2[index - num15] = check3.ACdKG[bi2.AD_pXpk - num15];
                }
                numArray[num5++] = (uint) num13;
                index--;
                num7--;
            }
            outQuotient.AD_pXpk = num5;
            int num16 = 0;
            int num17 = outQuotient.AD_pXpk - 1;
            while (num17 >= 0)
            {
                outQuotient.ACdKG[num16] = numArray[num17];
                num17--;
                num16++;
            }
            while (num16 < 70)
            {
                outQuotient.ACdKG[num16] = 0;
                num16++;
            }
            while ((outQuotient.AD_pXpk > 1) && (outQuotient.ACdKG[outQuotient.AD_pXpk - 1] == 0))
            {
                outQuotient.AD_pXpk--;
            }
            if (outQuotient.AD_pXpk == 0)
            {
                outQuotient.AD_pXpk = 1;
            }
            outRemainder.AD_pXpk = func_ABIi(numArray2, num4);
            num16 = 0;
            while (num16 < outRemainder.AD_pXpk)
            {
                outRemainder.ACdKG[num16] = numArray2[num16];
                num16++;
            }
            while (num16 < 70)
            {
                outRemainder.ACdKG[num16] = 0;
                num16++;
            }
        }

        public static SecCheck operator +(SecCheck bi1, SecCheck bi2)
        {
            SecCheck check = new SecCheck {
                AD_pXpk = (bi1.AD_pXpk > bi2.AD_pXpk) ? bi1.AD_pXpk : bi2.AD_pXpk
            };
            long num = 0L;
            for (int i = 0; i < check.AD_pXpk; i++)
            {
                long num3 = (bi1.ACdKG[i] + bi2.ACdKG[i]) + num;
                num = num3 >> 0x20;
                check.ACdKG[i] = (uint) (((ulong) num3) & 0xffffffffL);
            }
            if ((num != 0L) && (check.AD_pXpk < 70))
            {
                check.ACdKG[check.AD_pXpk] = (uint) num;
                check.AD_pXpk++;
            }
            while ((check.AD_pXpk > 1) && (check.ACdKG[check.AD_pXpk - 1] == 0))
            {
                check.AD_pXpk--;
            }
            int index = 0x45;
            if (((bi1.ACdKG[index] & 0x80000000) == (bi2.ACdKG[index] & 0x80000000)) && ((check.ACdKG[index] & 0x80000000) != (bi1.ACdKG[index] & 0x80000000)))
            {
                throw new ArithmeticException();
            }
            return check;
        }

        public static SecCheck operator &(SecCheck bi1, SecCheck bi2)
        {
            SecCheck check = new SecCheck();
            int num = (bi1.AD_pXpk > bi2.AD_pXpk) ? bi1.AD_pXpk : bi2.AD_pXpk;
            for (int i = 0; i < num; i++)
            {
                check.ACdKG[i] = bi1.ACdKG[i] & bi2.ACdKG[i];
            }
            check.AD_pXpk = 70;
            while ((check.AD_pXpk > 1) && (check.ACdKG[check.AD_pXpk - 1] == 0))
            {
                check.AD_pXpk--;
            }
            return check;
        }

        public static SecCheck operator |(SecCheck bi1, SecCheck bi2)
        {
            SecCheck check = new SecCheck();
            int num = (bi1.AD_pXpk > bi2.AD_pXpk) ? bi1.AD_pXpk : bi2.AD_pXpk;
            for (int i = 0; i < num; i++)
            {
                check.ACdKG[i] = bi1.ACdKG[i] | bi2.ACdKG[i];
            }
            check.AD_pXpk = 70;
            while ((check.AD_pXpk > 1) && (check.ACdKG[check.AD_pXpk - 1] == 0))
            {
                check.AD_pXpk--;
            }
            return check;
        }

        public static SecCheck operator --(SecCheck bi1)
        {
            SecCheck check = new SecCheck(bi1);
            bool flag = true;
            int index = 0;
            while (flag && (index < 70))
            {
                long num = check.ACdKG[index];
                num -= 1L;
                check.ACdKG[index] = (uint) (((ulong) num) & 0xffffffffL);
                if (num >= 0L)
                {
                    flag = false;
                }
                index++;
            }
            if (index > check.AD_pXpk)
            {
                check.AD_pXpk = index;
            }
            while ((check.AD_pXpk > 1) && (check.ACdKG[check.AD_pXpk - 1] == 0))
            {
                check.AD_pXpk--;
            }
            int num3 = 0x45;
            if (((bi1.ACdKG[num3] & 0x80000000) != 0) && ((check.ACdKG[num3] & 0x80000000) != (bi1.ACdKG[num3] & 0x80000000)))
            {
                throw new ArithmeticException("Underflow in --.");
            }
            return check;
        }

        public static SecCheck operator /(SecCheck bi1, SecCheck bi2)
        {
            SecCheck outQuotient = new SecCheck();
            SecCheck outRemainder = new SecCheck();
            int index = 0x45;
            bool flag = false;
            bool flag2 = false;
            if ((bi1.ACdKG[index] & 0x80000000) != 0)
            {
                bi1 = -bi1;
                flag2 = true;
            }
            if ((bi2.ACdKG[index] & 0x80000000) != 0)
            {
                bi2 = -bi2;
                flag = true;
            }
            if (bi1 >= bi2)
            {
                if (bi2.AD_pXpk == 1)
                {
                    singleByteDivide(bi1, bi2, outQuotient, outRemainder);
                }
                else
                {
                    multiByteDivide(bi1, bi2, outQuotient, outRemainder);
                }
                if (flag2 != flag)
                {
                    return -outQuotient;
                }
            }
            return outQuotient;
        }

        public static bool operator ==(SecCheck bi1, SecCheck bi2) 
        {
           return bi1.Equals(bi2);
        }

        public static SecCheck operator ^(SecCheck bi1, SecCheck bi2)
        {
            SecCheck check = new SecCheck();
            int num = (bi1.AD_pXpk > bi2.AD_pXpk) ? bi1.AD_pXpk : bi2.AD_pXpk;
            for (int i = 0; i < num; i++)
            {
                check.ACdKG[i] = bi1.ACdKG[i] ^ bi2.ACdKG[i];
            }
            check.AD_pXpk = 70;
            while ((check.AD_pXpk > 1) && (check.ACdKG[check.AD_pXpk - 1] == 0))
            {
                check.AD_pXpk--;
            }
            return check;
        }

        public static bool operator >(SecCheck bi1, SecCheck bi2)
        {
            int index = 0x45;
            if (((bi1.ACdKG[index] & 0x80000000) == 0) || ((bi2.ACdKG[index] & 0x80000000) != 0))
            {
                if (((bi1.ACdKG[index] & 0x80000000) == 0) && ((bi2.ACdKG[index] & 0x80000000) != 0))
                {
                    return true;
                }
                int num2 = (bi1.AD_pXpk > bi2.AD_pXpk) ? bi1.AD_pXpk : bi2.AD_pXpk;
                index = num2 - 1;
                while ((index >= 0) && (bi1.ACdKG[index] == bi2.ACdKG[index]))
                {
                    index--;
                }
                if (index >= 0)
                {
                    return (bi1.ACdKG[index] > bi2.ACdKG[index]);
                }
            }
            return false;
        }

        public static bool operator >=(SecCheck bi1, SecCheck bi2)
        {
            return
            ((bi1 == bi2) || (bi1 > bi2));
        }

        public static implicit operator SecCheck(int value) 
        {
          return  new SecCheck((long) value);
        }

        public static implicit operator SecCheck(long value) 
        {
           return new SecCheck(value);
        }

        public static implicit operator SecCheck(uint value)
        {
            return new SecCheck((ulong) value);
        }

        public static implicit operator SecCheck(ulong value)
        {
            return  new SecCheck(value);
        }

        public static SecCheck operator ++(SecCheck bi1)
        {
            SecCheck check = new SecCheck(bi1);
            long num2 = 1L;
            int index = 0;
            while ((num2 != 0L) && (index < 70))
            {
                long num = check.ACdKG[index];
                num += 1L;
                check.ACdKG[index] = (uint) (((ulong) num) & 0xffffffffL);
                num2 = num >> 0x20;
                index++;
            }
            if (index > check.AD_pXpk)
            {
                check.AD_pXpk = index;
            }
            else
            {
                while ((check.AD_pXpk > 1) && (check.ACdKG[check.AD_pXpk - 1] == 0))
                {
                    check.AD_pXpk--;
                }
            }
            int num4 = 0x45;
            if (((bi1.ACdKG[num4] & 0x80000000) == 0) && ((check.ACdKG[num4] & 0x80000000) != (bi1.ACdKG[num4] & 0x80000000)))
            {
                throw new ArithmeticException("Overflow in ++.");
            }
            return check;
        }

        public static bool operator !=(SecCheck bi1, SecCheck bi2) 
        {
          return  !bi1.Equals(bi2);
        }

        public static SecCheck operator <<(SecCheck bi1, int shiftVal)
        {
            SecCheck check = new SecCheck(bi1);
            check.AD_pXpk = func_AA1YNilqR20iU4oER62ESkJKEw26(check.ACdKG, shiftVal);
            return check;
        }

        public static bool operator <(SecCheck bi1, SecCheck bi2)
        {
            int index = 0x45;
            if (((bi1.ACdKG[index] & 0x80000000) != 0) && ((bi2.ACdKG[index] & 0x80000000) == 0))
            {
                return true;
            }
            if (((bi1.ACdKG[index] & 0x80000000) != 0) || ((bi2.ACdKG[index] & 0x80000000) == 0))
            {
                int num2 = (bi1.AD_pXpk > bi2.AD_pXpk) ? bi1.AD_pXpk : bi2.AD_pXpk;
                index = num2 - 1;
                while ((index >= 0) && (bi1.ACdKG[index] == bi2.ACdKG[index]))
                {
                    index--;
                }
                if (index >= 0)
                {
                    return (bi1.ACdKG[index] < bi2.ACdKG[index]);
                }
            }
            return false;
        }

        public static bool operator <=(SecCheck bi1, SecCheck bi2) 
        {
           return ((bi1 == bi2) || (bi1 < bi2));
        }

        public static SecCheck operator %(SecCheck bi1, SecCheck bi2)
        {
            SecCheck outQuotient = new SecCheck();
            SecCheck outRemainder = new SecCheck(bi1);
            int index = 0x45;
            bool flag = false;
            if ((bi1.ACdKG[index] & 0x80000000) != 0)
            {
                bi1 = -bi1;
                flag = true;
            }
            if ((bi2.ACdKG[index] & 0x80000000) != 0)
            {
                bi2 = -bi2;
            }
            if (bi1 >= bi2)
            {
                if (bi2.AD_pXpk == 1)
                {
                    singleByteDivide(bi1, bi2, outQuotient, outRemainder);
                }
                else
                {
                    multiByteDivide(bi1, bi2, outQuotient, outRemainder);
                }
                if (flag)
                {
                    return -outRemainder;
                }
            }
            return outRemainder;
        }

        public static SecCheck operator *(SecCheck bi1, SecCheck bi2)
        {
            int num2;
            int index = 0x45;
            bool flag = false;
            bool flag2 = false;
            try
            {
                if ((bi1.ACdKG[index] & 0x80000000) != 0)
                {
                    flag = true;
                    bi1 = -bi1;
                }
                if ((bi2.ACdKG[index] & 0x80000000) != 0)
                {
                    flag2 = true;
                    bi2 = -bi2;
                }
            }
            catch (Exception)
            {
            }
            SecCheck check = new SecCheck();
            try
            {
                num2 = 0;
                while (num2 < bi1.AD_pXpk)
                {
                    if (bi1.ACdKG[num2] != 0)
                    {
                        ulong num3 = 0L;
                        int num4 = 0;
                        for (int i = num2; num4 < bi2.AD_pXpk; i++)
                        {
                            ulong num6 = ((bi1.ACdKG[num2] * bi2.ACdKG[num4]) + check.ACdKG[i]) + num3;
                            check.ACdKG[i] = (uint) (num6 & 0xffffffffL);
                            num3 = num6 >> 0x20;
                            num4++;
                        }
                        if (num3 != 0L)
                        {
                            check.ACdKG[num2 + bi2.AD_pXpk] = (uint) num3;
                        }
                    }
                    num2++;
                }
            }
            catch (Exception)
            {
                throw new ArithmeticException("Multiplication overflow.");
            }
            check.AD_pXpk = bi1.AD_pXpk + bi2.AD_pXpk;
            if (check.AD_pXpk > 70)
            {
                check.AD_pXpk = 70;
            }
            while ((check.AD_pXpk > 1) && (check.ACdKG[check.AD_pXpk - 1] == 0))
            {
                check.AD_pXpk--;
            }
            if ((check.ACdKG[index] & 0x80000000) != 0)
            {
                if ((flag != flag2) && (check.ACdKG[index] == 0x80000000))
                {
                    if (check.AD_pXpk == 1)
                    {
                        return check;
                    }
                    bool flag3 = true;
                    for (num2 = 0; (num2 < (check.AD_pXpk - 1)) && flag3; num2++)
                    {
                        if (check.ACdKG[num2] != 0)
                        {
                            flag3 = false;
                        }
                    }
                    if (flag3)
                    {
                        return check;
                    }
                }
                throw new ArithmeticException("Multiplication overflow.");
            }
            if (flag != flag2)
            {
                return -check;
            }
            return check;
        }

        public static SecCheck operator ~(SecCheck bi1)
        {
            SecCheck check = new SecCheck(bi1);
            for (int i = 0; i < 70; i++)
            {
                check.ACdKG[i] = ~bi1.ACdKG[i];
            }
            check.AD_pXpk = 70;
            while ((check.AD_pXpk > 1) && (check.ACdKG[check.AD_pXpk - 1] == 0))
            {
                check.AD_pXpk--;
            }
            return check;
        }

        public static SecCheck operator >>(SecCheck bi1, int shiftVal)
        {
            SecCheck check = new SecCheck(bi1);
            check.AD_pXpk = func_ABIi(check.ACdKG, shiftVal);
            if ((bi1.ACdKG[0x45] & 0x80000000) != 0)
            {
                int num;
                for (num = 0x45; num >= check.AD_pXpk; num--)
                {
                    check.ACdKG[num] = uint.MaxValue;
                }
                uint num2 = 0x80000000;
                for (num = 0; num < 0x20; num++)
                {
                    if ((check.ACdKG[check.AD_pXpk - 1] & num2) != 0)
                    {
                        break;
                    }
                    check.ACdKG[check.AD_pXpk - 1] |= num2;
                    num2 = num2 >> 1;
                }
                check.AD_pXpk = 70;
            }
            return check;
        }

        public static SecCheck operator -(SecCheck bi1, SecCheck bi2)
        {
            int num2;
            SecCheck check = new SecCheck {
                AD_pXpk = (bi1.AD_pXpk > bi2.AD_pXpk) ? bi1.AD_pXpk : bi2.AD_pXpk
            };
            long num = 0L;
            for (num2 = 0; num2 < check.AD_pXpk; num2++)
            {
                long num3 = (bi1.ACdKG[num2] - bi2.ACdKG[num2]) - num;
                check.ACdKG[num2] = (uint) (((ulong) num3) & 0xffffffffL);
                if (num3 < 0L)
                {
                    num = 1L;
                }
                else
                {
                    num = 0L;
                }
            }
            if (num != 0L)
            {
                for (num2 = check.AD_pXpk; num2 < 70; num2++)
                {
                    check.ACdKG[num2] = uint.MaxValue;
                }
                check.AD_pXpk = 70;
            }
            while ((check.AD_pXpk > 1) && (check.ACdKG[check.AD_pXpk - 1] == 0))
            {
                check.AD_pXpk--;
            }
            int index = 0x45;
            if (((bi1.ACdKG[index] & 0x80000000) != (bi2.ACdKG[index] & 0x80000000)) && ((check.ACdKG[index] & 0x80000000) != (bi1.ACdKG[index] & 0x80000000)))
            {
                throw new ArithmeticException();
            }
            return check;
        }

        public static SecCheck operator -(SecCheck bi1)
        {
            if ((bi1.AD_pXpk == 1) && (bi1.ACdKG[0] == 0))
            {
                return new SecCheck();
            }
            SecCheck check = new SecCheck(bi1);
            for (int i = 0; i < 70; i++)
            {
                check.ACdKG[i] = ~bi1.ACdKG[i];
            }
            long num3 = 1L;
            for (int j = 0; (num3 != 0L) && (j < 70); j++)
            {
                long num2 = check.ACdKG[j];
                num2 += 1L;
                check.ACdKG[j] = (uint) (((ulong) num2) & 0xffffffffL);
                num3 = num2 >> 0x20;
            }
            if ((bi1.ACdKG[0x45] & 0x80000000) == (check.ACdKG[0x45] & 0x80000000))
            {
                throw new ArithmeticException("Overflow in negation.");
            }
            check.AD_pXpk = 70;
            while ((check.AD_pXpk > 1) && (check.ACdKG[check.AD_pXpk - 1] == 0))
            {
                check.AD_pXpk--;
            }
            return check;
        }

        public bool RabinMillerTest(int confidence)
        {
            SecCheck check;
            if ((this.ACdKG[0x45] & 0x80000000) != 0)
            {
                check = -this;
            }
            else
            {
                check = this;
            }
            if (check.AD_pXpk == 1)
            {
                if ((check.ACdKG[0] == 0) || (check.ACdKG[0] == 1))
                {
                    return false;
                }
                if ((check.ACdKG[0] == 2) || (check.ACdKG[0] == 3))
                {
                    return true;
                }
            }
            if ((check.ACdKG[0] & 1) == 0)
            {
                return false;
            }
            SecCheck check2 = check - new SecCheck(1L);
            int num = 0;
            for (int i = 0; i < check2.AD_pXpk; i++)
            {
                uint num3 = 1;
                for (int k = 0; k < 0x20; k++)
                {
                    if ((check2.ACdKG[i] & num3) != 0)
                    {
                        i = check2.AD_pXpk;
                        break;
                    }
                    num3 = num3 << 1;
                    num++;
                }
            }
            SecCheck exp = check2 >> num;
            int num5 = check.bitCount();
            SecCheck check4 = new SecCheck();
            Random rand = new Random();
            for (int j = 0; j < confidence; j++)
            {
                bool flag = false;
                while (!flag)
                {
                    int bits = 0;
                    while (bits < 2)
                    {
                        bits = (int) (rand.NextDouble() * num5);
                    }
                    check4.genRandomBits(bits, rand);
                    int num8 = check4.AD_pXpk;
                    if ((num8 > 1) || ((num8 == 1) && (check4.ACdKG[0] != 1)))
                    {
                        flag = true;
                    }
                }
                SecCheck check5 = check4.gcd(check);
                if ((check5.AD_pXpk == 1) && (check5.ACdKG[0] != 1))
                {
                    return false;
                }
                SecCheck check6 = check4.modPow(exp, check);
                bool flag2 = false;
                if ((check6.AD_pXpk == 1) && (check6.ACdKG[0] == 1))
                {
                    flag2 = true;
                }
                for (int m = 0; !flag2 && (m < num); m++)
                {
                    if (check6 == check2)
                    {
                        flag2 = true;
                        break;
                    }
                    check6 = (check6 * check6) % check;
                }
                if (!flag2)
                {
                    return false;
                }
            }
            return true;
        }

        public static void RSATest(int rounds)
        {
            Random random = new Random(1);
            byte[] inData = new byte[0x40];
            SecCheck exp = new SecCheck("a932b948feed4fb2b692609bd22164fc9edb59fae7880cc1eaff7b3c9626b7e5b241c27a974833b2622ebe09beb451917663d47232488f23a117fc97720f1e7", 0x10);
            SecCheck check2 = new SecCheck("4adf2f7a89da93248509347d2ae506d683dd3a16357e859a980c4f77a4e2f7a01fae289f13a851df6e9db5adaa60bfd2b162bbbe31f7c8f828261a6839311929d2cef4f864dde65e556ce43c89bbbf9f1ac5511315847ce9cc8dc92470a747b8792d6a83b0092d2e5ebaf852c85cacf34278efa99160f2f8aa7ee7214de07b7", 0x10);
            SecCheck n = new SecCheck("e8e77781f36a7b3188d711c2190b560f205a52391b3479cdb99fa010745cbeba5f2adc08e1de6bf38398a0487c4a73610d94ec36f17f3f46ad75e17bc1adfec99839589f45f95ccc94cb2a5c500b477eb3323d8cfab0c8458c96f0147a45d27e45a4d11d54d77684f65d48f15fafcc1ba208e71e921b9bd9017c16a5231af7f", 0x10);
            Console.WriteLine("e =" + exp.ToString(10));
            Console.WriteLine("d =" + check2.ToString(10));
            Console.WriteLine("n =" + n.ToString(10) + "\r\n");
            for (int i = 0; i < rounds; i++)
            {
                int inLen = 0;
                while (inLen == 0)
                {
                    inLen = (int) (random.NextDouble() * 65.0);
                }
                bool flag = false;
                while (!flag)
                {
                    for (int j = 0; j < 0x40; j++)
                    {
                        if (j < inLen)
                        {
                            inData[j] = (byte) (random.NextDouble() * 256.0);
                        }
                        else
                        {
                            inData[j] = 0;
                        }
                        if (inData[j] != 0)
                        {
                            flag = true;
                        }
                    }
                }
                while (inData[0] == 0)
                {
                    inData[0] = (byte) (random.NextDouble() * 256.0);
                }
                Console.Write("Round = " + i);
                SecCheck check4 = new SecCheck(inData, inLen);
                if (check4.modPow(exp, n).modPow(check2, n) != check4)
                {
                    Console.WriteLine("Error at round " + i);
                    Console.WriteLine(check4 + "");
                    break;
                }
                Console.WriteLine(" <PASSED>.");
            }
        }

        public static void RSATest2(int rounds)
        {
            Random rand = new Random();
            byte[] inData = new byte[0x40];
            byte[] buffer2 = new byte[] { 
                0x85, 0x84, 100, 0xfd, 0x70, 0x6a, 0x9f, 240, 0x94, 12, 0x3e, 0x2c, 0x74, 0x34, 5, 0xc9,
                0x55, 0xb3, 0x85, 50, 0x98, 0x71, 0xf9, 0x41, 0x21, 0x5f, 2, 0x9e, 0xea, 0x56, 0x8d, 140,
                0x44, 0xcc, 0xee, 0xee, 0x3d, 0x2c, 0x9d, 0x2c, 0x12, 0x41, 30, 0xf1, 0xc5, 50, 0xc3, 170,
                0x31, 0x4a, 0x52, 0xd8, 0xe8, 0xaf, 0x42, 0xf4, 0x72, 0xa1, 0x2a, 13, 0x97, 0xb1, 0x31, 0xb3
            };
            byte[] buffer3 = new byte[] { 
                0x99, 0x98, 0xca, 0xb8, 0x5e, 0xd7, 0xe5, 220, 40, 0x5c, 0x6f, 14, 0x15, 9, 0x59, 110,
                0x84, 0xf3, 0x81, 0xcd, 0xde, 0x42, 220, 0x93, 0xc2, 0x7a, 0x62, 0xac, 0x6c, 0xaf, 0xde, 0x74,
                0xe3, 0xcb, 0x60, 0x20, 0x38, 0x9c, 0x21, 0xc3, 220, 200, 0xa2, 0x4d, 0xc6, 0x2a, 0x35, 0x7f,
                0xf3, 0xa9, 0xe8, 0x1d, 0x7b, 0x2c, 120, 250, 0xb8, 2, 0x55, 0x80, 0x9b, 0xc2, 0xa5, 0xcb
            };
            SecCheck check = new SecCheck(buffer2);
            SecCheck check2 = new SecCheck(buffer3);
            SecCheck modulus = (check - 1) * (check2 - 1);
            SecCheck n = check * check2;
            for (int i = 0; i < rounds; i++)
            {
                SecCheck exp = modulus.genCoPrime(0x200, rand);
                SecCheck check6 = exp.modInverse(modulus);
                Console.WriteLine("e =" + exp.ToString(10));
                Console.WriteLine("d =" + check6.ToString(10));
                Console.WriteLine("n =" + n.ToString(10) + "\r\n");
                int inLen = 0;
                while (inLen == 0)
                {
                    inLen = (int) (rand.NextDouble() * 65.0);
                }
                bool flag = false;
                while (!flag)
                {
                    for (int j = 0; j < 0x40; j++)
                    {
                        if (j < inLen)
                        {
                            inData[j] = (byte) (rand.NextDouble() * 256.0);
                        }
                        else
                        {
                            inData[j] = 0;
                        }
                        if (inData[j] != 0)
                        {
                            flag = true;
                        }
                    }
                }
                while (inData[0] == 0)
                {
                    inData[0] = (byte) (rand.NextDouble() * 256.0);
                }
                Console.Write("Round = " + i);
                SecCheck check7 = new SecCheck(inData, inLen);
                if (check7.modPow(exp, n).modPow(check6, n) != check7)
                {
                    Console.WriteLine("Error at round " + i);
                    Console.WriteLine(check7 + "\r\n");
                    break;
                }
                Console.WriteLine(" <PASSED>.");
            }
        }

        public void setBit(uint bitNum)
        {
            uint index = bitNum >> 5;
            byte num2 = (byte) (bitNum & 0x1f);
            uint num3 = ((uint) 1) << num2;
            this.ACdKG[index] |= num3;
            if (index >= this.AD_pXpk)
            {
                this.AD_pXpk = ((int) index) + 1;
            }
        }

        public static void singleByteDivide(SecCheck bi1, SecCheck bi2, SecCheck outQuotient, SecCheck outRemainder)
        {
            int num2;
            ulong num6;
            uint[] numArray = new uint[70];
            int num = 0;
            for (num2 = 0; num2 < 70; num2++)
            {
                outRemainder.ACdKG[num2] = bi1.ACdKG[num2];
            }
            outRemainder.AD_pXpk = bi1.AD_pXpk;
            while ((outRemainder.AD_pXpk > 1) && (outRemainder.ACdKG[outRemainder.AD_pXpk - 1] == 0))
            {
                outRemainder.AD_pXpk--;
            }
            ulong num3 = bi2.ACdKG[0];
            int index = outRemainder.AD_pXpk - 1;
            ulong num5 = outRemainder.ACdKG[index];
            if (num5 >= num3)
            {
                num6 = num5 / num3;
                numArray[num++] = (uint) num6;
                outRemainder.ACdKG[index] = (uint) (num5 % num3);
            }
            index--;
            while (index >= 0)
            {
                num5 = (outRemainder.ACdKG[index + 1] << 0x20) + outRemainder.ACdKG[index];
                num6 = num5 / num3;
                numArray[num++] = (uint) num6;
                outRemainder.ACdKG[index + 1] = 0;
                outRemainder.ACdKG[index--] = (uint) (num5 % num3);
            }
            outQuotient.AD_pXpk = num;
            int num7 = 0;
            num2 = outQuotient.AD_pXpk - 1;
            while (num2 >= 0)
            {
                outQuotient.ACdKG[num7] = numArray[num2];
                num2--;
                num7++;
            }
            while (num7 < 70)
            {
                outQuotient.ACdKG[num7] = 0;
                num7++;
            }
            while ((outQuotient.AD_pXpk > 1) && (outQuotient.ACdKG[outQuotient.AD_pXpk - 1] == 0))
            {
                outQuotient.AD_pXpk--;
            }
            if (outQuotient.AD_pXpk == 0)
            {
                outQuotient.AD_pXpk = 1;
            }
            while ((outRemainder.AD_pXpk > 1) && (outRemainder.ACdKG[outRemainder.AD_pXpk - 1] == 0))
            {
                outRemainder.AD_pXpk--;
            }
        }

        public bool SolovayStrassenTest(int confidence)
        {
            SecCheck check;
            if ((this.ACdKG[0x45] & 0x80000000) != 0)
            {
                check = -this;
            }
            else
            {
                check = this;
            }
            if (check.AD_pXpk == 1)
            {
                if ((check.ACdKG[0] == 0) || (check.ACdKG[0] == 1))
                {
                    return false;
                }
                if ((check.ACdKG[0] == 2) || (check.ACdKG[0] == 3))
                {
                    return true;
                }
            }
            if ((check.ACdKG[0] & 1) == 0)
            {
                return false;
            }
            int num = check.bitCount();
            SecCheck a = new SecCheck();
            SecCheck check3 = check - 1;
            SecCheck exp = check3 >> 1;
            Random rand = new Random();
            for (int i = 0; i < confidence; i++)
            {
                bool flag = false;
                while (!flag)
                {
                    int bits = 0;
                    while (bits < 2)
                    {
                        bits = (int) (rand.NextDouble() * num);
                    }
                    a.genRandomBits(bits, rand);
                    int num4 = a.AD_pXpk;
                    if ((num4 > 1) || ((num4 == 1) && (a.ACdKG[0] != 1)))
                    {
                        flag = true;
                    }
                }
                SecCheck check5 = a.gcd(check);
                if ((check5.AD_pXpk == 1) && (check5.ACdKG[0] != 1))
                {
                    return false;
                }
                SecCheck check6 = a.modPow(exp, check);
                if (check6 == check3)
                {
                    check6 = -1;
                }
                SecCheck check7 = Jacobi(a, check);
                if (check6 != check7)
                {
                    return false;
                }
            }
            return true;
        }

        public SecCheck sqrt()
        {
            uint num4;
            uint num = (uint) this.bitCount();
            if ((num & 1) != 0)
            {
                num = (num >> 1) + 1;
            }
            else
            {
                num = num >> 1;
            }
            uint num2 = num >> 5;
            byte num3 = (byte) (num & 0x1f);
            SecCheck check = new SecCheck();
            if (num3 == 0)
            {
                num4 = 0x80000000;
            }
            else
            {
                num4 = ((uint) 1) << num3;
                num2++;
            }
            check.AD_pXpk = (int) num2;
            for (int i = ((int) num2) - 1; i >= 0; i--)
            {
                while (num4 != 0)
                {
                    check.ACdKG[i] ^= num4;
                    if ((check * check) > this)
                    {
                        check.ACdKG[i] ^= num4;
                    }
                    num4 = num4 >> 1;
                }
                num4 = 0x80000000;
            }
            return check;
        }

        public static void SqrtTest(int rounds)
        {
            Random rand = new Random();
            for (int i = 0; i < rounds; i++)
            {
                int bits = 0;
                while (bits == 0)
                {
                    bits = (int) (rand.NextDouble() * 1024.0);
                }
                Console.Write("Round = " + i);
                SecCheck check = new SecCheck();
                check.genRandomBits(bits, rand);
                SecCheck check2 = check.sqrt();
                SecCheck check3 = (check2 + 1) * (check2 + 1);
                if (check3 <= check)
                {
                    Console.WriteLine("Error at round " + i);
                    Console.WriteLine(check + "\r\n");
                    break;
                }
                Console.WriteLine(" <PASSED>.");
            }
        }

        public string ToHexString()
        {
            string str = this.ACdKG[this.AD_pXpk - 1].ToString("X");
            for (int i = this.AD_pXpk - 2; i >= 0; i--)
            {
                str = str + this.ACdKG[i].ToString("X8");
            }
            return str;
        }

        public override string ToString()
        {
            return
            this.ToString(10);
        }

        public string ToString(int radix)
        {
            if ((radix < 2) || (radix > 0x24))
            {
                throw new ArgumentException("Radix must be >= 2 and <= 36");
            }
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string str2 = "";
            SecCheck check = this;
            bool flag = false;
            if ((check.ACdKG[0x45] & 0x80000000) != 0)
            {
                flag = true;
                try
                {
                    check = -check;
                }
                catch (Exception)
                {
                }
            }
            SecCheck outQuotient = new SecCheck();
            SecCheck outRemainder = new SecCheck();
            SecCheck check4 = new SecCheck((long) radix);
            if ((check.AD_pXpk == 1) && (check.ACdKG[0] == 0))
            {
                return "0";
            }
            while ((check.AD_pXpk > 1) || ((check.AD_pXpk == 1) && (check.ACdKG[0] != 0)))
            {
                singleByteDivide(check, check4, outQuotient, outRemainder);
                if (outRemainder.ACdKG[0] < 10)
                {
                    str2 = outRemainder.ACdKG[0] + str2;
                }
                else
                {
                    str2 = str[((int) outRemainder.ACdKG[0]) - 10] + str2;
                }
                check = outQuotient;
            }
            if (flag)
            {
                str2 = "-" + str2;
            }
            return str2;
        }

        public void unsetBit(uint bitNum)
        {
            uint index = bitNum >> 5;
            if (index < this.AD_pXpk)
            {
                byte num2 = (byte) (bitNum & 0x1f);
                uint num3 = ((uint) 1) << num2;
                uint num4 = uint.MaxValue ^ num3;
                this.ACdKG[index] &= num4;
                if ((this.AD_pXpk > 1) && (this.ACdKG[this.AD_pXpk - 1] == 0))
                {
                    this.AD_pXpk--;
                }
            }
        }
    }
}
