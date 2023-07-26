using System;
using System.Windows.Forms;
using System.Linq;

namespace CursesWork
{
    class Generation
    {

        public static void GetMassive0(MainForm form,int[] mas)
        {
            int a = int.Parse(form.textBox1.Text);
            for (int i = 0; i < a; i++)
            {
                mas[i] = i;
            }
        }

        public static void GetMassive1(MainForm form,int[] mas)
        {
            int a = int.Parse(form.textBox1.Text);
            for (int i = 0; i < a; i++)
            {
                mas[i] = a - i;
            }
        }

        public static void GetMassive2(MainForm form,int[] mas)
        {
            int a = int.Parse(form.textBox1.Text);
            for (int i = 0; i < a; i++)
            {
                mas[i] = 1;
            }
        }

        public static void GetMassive3(MainForm form,int[] mas)
        {
            int a = int.Parse(form.textBox1.Text);
            Random r = new Random();
            for (int i = 0; i < a; i++)
            {
                mas[i] = r.Next(0, 101);
            }
        }

        public static void GetMassive4(MainForm form,int[] mas)
        {
            int a = int.Parse(form.textBox1.Text);
            Random r = new Random();
            for (int i = 0; i < a; i++)
            {
                if (i % 5 == 0)
                {
                    mas[i] = r.Next(i - 4, i + 4);
                }
                else
                {
                    mas[i] = i;
                }
            }
        }
    }
}
