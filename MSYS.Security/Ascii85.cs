namespace MSYS.Security
{
    using System;
    using System.IO;
    using System.Text;

    public class Ascii85
    {
        private const int AA1YNilqR20iU4oER62ESkJKEw26 = 0x21;
        private byte[] ABIi = new byte[5];
        private byte[] ACdKG = new byte[4];
        private uint AD_pXpk = 0;
        private int AEOFASF = 0;
        private uint[] AFPA = new uint[] { 0x31c84b1, 0x95eed, 0x1c39, 0x55, 1 };
        public bool EnforceMarks = true;
        public int LineLength = 0x4b;
        public string PrefixMark = "<~";
        public string SuffixMark = "~>";

        private void func_AA1YNilqR20iU4oER62ESkJKEw26(StringBuilder builder1)
        {
            this.func_func_ABIi(this.ABIi.Length, builder1);
        }

        private void func_func_ABIi(int num1, StringBuilder builder1)
        {
            int num;
            for (num = this.ABIi.Length - 1; num >= 0; num--)
            {
                this.ABIi[num] = (byte) ((this.AD_pXpk % 0x55) + 0x21);
                this.AD_pXpk /= 0x55;
            }
            for (num = 0; num < num1; num++)
            {
                char ch = (char) this.ABIi[num];
                this.func_AFPA(builder1, ch);
            }
        }

        private void func_func_ACdKG()
        {
            this.func_func_AD_pXpk(this.ACdKG.Length);
        }

        private void func_func_AD_pXpk(int num1)
        {
            for (int i = 0; i < num1; i++)
            {
                this.ACdKG[i] = (byte) (this.AD_pXpk >> (0x18 - (i * 8)));
            }
        }

        private void func_AEOFASF(StringBuilder builder1, string text1)
        {
            if ((this.LineLength > 0) && ((this.AEOFASF + text1.Length) > this.LineLength))
            {
                this.AEOFASF = 0;
                builder1.Append('\n');
            }
            else
            {
                this.AEOFASF += text1.Length;
            }
            builder1.Append(text1);
        }

        private void func_AFPA(StringBuilder builder1, char ch1)
        {
            builder1.Append(ch1);
            this.AEOFASF++;
            if ((this.LineLength > 0) && (this.AEOFASF >= this.LineLength))
            {
                this.AEOFASF = 0;
                builder1.Append('\n');
            }
        }

        public byte[] Decode(string s)
        {
            if (this.EnforceMarks && (!s.StartsWith(this.PrefixMark) | !s.EndsWith(this.SuffixMark)))
            {
                throw new Exception("ASCII85 encoded data should begin with '" + this.PrefixMark + "' and end with '" + this.SuffixMark + "'");
            }
            if (s.StartsWith(this.PrefixMark))
            {
                s = s.Substring(this.PrefixMark.Length);
            }
            if (s.EndsWith(this.SuffixMark))
            {
                s = s.Substring(0, s.Length - this.SuffixMark.Length);
            }
            MemoryStream stream = new MemoryStream();
            int index = 0;
            bool flag = false;
            foreach (char ch in s)
            {
                switch (ch)
                {
                    case '\b':
                    case '\t':
                    case '\n':
                    case '\f':
                    case '\r':
                    case '\0':
                        flag = false;
                        break;

                    case 'z':
                        if (index != 0)
                        {
                            throw new Exception("The character 'z' is invalid inside an ASCII85 block.");
                        }
                        this.ACdKG[0] = 0;
                        this.ACdKG[1] = 0;
                        this.ACdKG[2] = 0;
                        this.ACdKG[3] = 0;
                        stream.Write(this.ACdKG, 0, this.ACdKG.Length);
                        flag = false;
                        break;

                    default:
                        if ((ch < '!') || (ch > 'u'))
                        {
                            throw new Exception("Bad character '" + ch + "' found. ASCII85 only allows characters '!' to 'u'.");
                        }
                        flag = true;
                        break;
                }
                if (flag)
                {
                    this.AD_pXpk += (uint)(ch - '!') * this.AFPA[index];
                    index++;
                    if (index == this.ABIi.Length)
                    {
                        this.func_func_ACdKG();
                        stream.Write(this.ACdKG, 0, this.ACdKG.Length);
                        this.AD_pXpk = 0;
                        index = 0;
                    }
                }
            }
            if (index != 0)
            {
                if (index == 1)
                {
                    throw new Exception("The last block of ASCII85 data cannot be a single byte.");
                }
                index--;
                this.AD_pXpk += this.AFPA[index];
                this.func_func_AD_pXpk(index);
                for (int i = 0; i < index; i++)
                {
                    stream.WriteByte(this.ACdKG[i]);
                }
            }
            return stream.ToArray();
        }

        public string Encode(byte[] ba)
        {
            StringBuilder builder = new StringBuilder(ba.Length * (this.ABIi.Length / this.ACdKG.Length));
            this.AEOFASF = 0;
            if (this.EnforceMarks)
            {
                this.func_AEOFASF(builder, this.PrefixMark);
            }
            int num = 0;
            this.AD_pXpk = 0;
            foreach (byte num2 in ba)
            {
                if (num >= (this.ACdKG.Length - 1))
                {
                    this.AD_pXpk |= num2;
                    if (this.AD_pXpk == 0)
                    {
                        this.func_AFPA(builder, 'z');
                    }
                    else
                    {
                        this.func_AA1YNilqR20iU4oER62ESkJKEw26(builder);
                    }
                    this.AD_pXpk = 0;
                    num = 0;
                }
                else
                {
                    this.AD_pXpk |= (uint) (num2 << (0x18 - (num * 8)));
                    num++;
                }
            }
            if (num > 0)
            {
                this.func_func_ABIi(num + 1, builder);
            }
            if (this.EnforceMarks)
            {
                this.func_AEOFASF(builder, this.SuffixMark);
            }
            return builder.ToString();
        }
    }
}
