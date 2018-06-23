using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MineSweeperKataLibrary
{
    public class FieldCollector
    {
        public String infoBlock;
        public List<Field> fields;

        public FieldCollector()
        {
            fields = new List<Field>();
        }
        public void GetFields(string path)
        {
            int i = 0;
            bool endOfStream = false;
            using (StreamReader reader = new StreamReader(path))
            {
                GetFieldloop(reader, endOfStream, i);
            }

            GetFields();
        }

        public void GetFieldloop(StreamReader reader, bool endOfStream, int i)
        {

            String curLine;
            while (!endOfStream)
            {

                curLine = reader.ReadLine();
                if (curLine.Equals("0 0"))
                    endOfStream = true;
                else if (i == 0)
                {
                    infoBlock = curLine;
                    i++;
                }
                else
                    infoBlock = infoBlock + System.Environment.NewLine + curLine;
            }
        }

        private void GetFields()
        {
            String BLANK = System.Environment.NewLine;
            String[] values = infoBlock.Split(BLANK.ToCharArray());

            for (int i = 0; i < values.Length; i += 2)
            {
                Verifier(values, i);
            }


        }
        private void Verifier(String[] values, int i)
        {
            if (values[i].ToCharArray()[0] != '.' && values[i].ToCharArray()[0] != '*')
            {
                int N = Convert.ToInt32(values[i].ToCharArray()[0]) - 48;
                int M = Convert.ToInt32(values[i].ToCharArray()[2]) - 48;

                Field field = new Field(N, M);

                for (int j = i + 2, k = 0; j <= i + (N * 2) && k < field.Lines; j += 2, k++)
                {
                    field.InputGrid[k] = values[j];
                }
                fields.Add(field);
                i += (N * 2);
            }
        }

        public void GenerateOutput(string path)
        {
            GetFields(path);
            foreach (Field field in fields)
            {
                field.GenerateOutputField();
            }

            using (StreamWriter writer = new StreamWriter("C:\\Users\\plini\\Desktop\\Test_Cases_Text_Files\\Output.txt"))
            {
                for (int i = 0; i < fields.Count; i++)
                {
                    FieldWriter(writer, i);
                }
            }
        }

        public void FieldWriter(StreamWriter writer, int i)
        {
            writer.WriteLine("Field #" + (i + 1) + ":");
            foreach (String line in fields[i].OutputGrid)
                writer.WriteLine(line);
            if (!(i + 1 >= fields.Count))
                writer.WriteLine();
        }
    }
}
