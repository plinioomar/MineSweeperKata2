using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperKataLibrary
{
    public class Field
    {
        public int Lines;
        public int Columns;
        public String[] InputGrid;
        public String[] OutputGrid;

        public Field(int N, int M)
        {
            Lines = N;
            Columns = M;
            InputGrid = new String[Lines];
            OutputGrid = new String[Lines];
        }

        public void TransformAllDotsIntoZeroes()
        {
            for (int i = 0; i < Lines; i++)
            {
                char[] chars = InputGrid[i].ToCharArray();
                CharVerifier(chars);
                OutputGrid[i] = new string(chars);
            }

        }

        public void CharVerifier(char[] chars)
        {
            for (int j = 0; j < Columns; j++)
            {
                Charcondition(chars, j);
            }
        }

        public void Charcondition(char[] chars, int j)
        {
            if (chars[j] != '*')
                chars[j] = '0';
        }

        public void GenerateOutputField()
        {
            TransformAllDotsIntoZeroes();
            for (int i = 0; i < Lines; i++)
            {
                char[] chars = OutputGrid[i].ToCharArray();
                Generator(chars, i);
            }
        }

        public void Generator(char[] chars, int i)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (chars[j] == '*')
                    UpdateSurroundingValues(i, j);
            }
        }



        private void UpdateSurroundingValues(int i, int j)
        {
            bool iValidLowerIndex = false;
            bool iValidHigherIndex = false;

            bool jValidLowerIndex = false;
            bool jValidHigherIndex = false;

            if (i - 1 >= 0)
                iValidLowerIndex = true;

            if (i + 1 < Lines)
                iValidHigherIndex = true;

            if (j - 1 >= 0)
                jValidLowerIndex = true;

            if (j + 1 < Columns)
                jValidHigherIndex = true;


            MultipleAdder(iValidLowerIndex, iValidHigherIndex, jValidLowerIndex, jValidHigherIndex, i, j);

            DiagonalAdder(iValidLowerIndex, iValidHigherIndex, jValidLowerIndex, jValidHigherIndex, i, j);
        }

        public void MultipleAdder(bool iValidLowerIndex, bool iValidHigherIndex, bool jValidLowerIndex, bool jValidHigherIndex, int i, int j)
        {
            if (iValidLowerIndex)
                DotValuePlus1(i - 1, j);

            if (iValidHigherIndex)
                DotValuePlus1(i + 1, j);

            if (jValidLowerIndex)
                DotValuePlus1(i, j - 1);

            if (jValidHigherIndex)
                DotValuePlus1(i, j + 1);


        }

        public void DiagonalAdder(bool iValidLowerIndex, bool iValidHigherIndex, bool jValidLowerIndex, bool jValidHigherIndex, int i, int j)
        {
            if (iValidLowerIndex && jValidLowerIndex)
                DotValuePlus1(i - 1, j - 1);

            if (iValidLowerIndex && jValidHigherIndex)
                DotValuePlus1(i - 1, j + 1);

            if (iValidHigherIndex && jValidLowerIndex)
                DotValuePlus1(i + 1, j - 1);

            if (iValidHigherIndex && jValidHigherIndex)
                DotValuePlus1(i + 1, j + 1);
        }

        private void DotValuePlus1(int i, int j)
        {
            char[] chars = OutputGrid[i].ToCharArray();
            if (chars[j] != '*')
            {
                int value = Convert.ToInt32(chars[j]) - 48;
                value++;
                chars[j] = Convert.ToChar(value + 48);
            }

            OutputGrid[i] = new String(chars);
        }
    }
}
