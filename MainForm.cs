using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace CursesWork
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox1.MaxLength = 7;
        }

        public int NumberOfString = 0;

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
            {
                e.KeyChar = '\0';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.Items.Count == 0)
            {
                MessageBox.Show("Ни один элемент не был выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                label8.Text = "No Errors";
                for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                {
                    string path = checkedListBox1.CheckedItems[i].ToString() + ".txt";
                    FileInfo fileInf = new FileInfo(path);
                    if (fileInf.Exists)
                    {
                        fileInf.Delete();
                    }
                }
                for (int i = checkedListBox1.Items.Count - 1; i >= 0; i--)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        checkedListBox1.Items.RemoveAt(i);
                        NumberOfString--;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.Items.Count == 0)
            {
                MessageBox.Show("Ни один элемент не был выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                label8.Text = "No Errors";
                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                chart2.Series[0].Points.Clear();
                chart2.Series[1].Points.Clear();
                chart3.Series[0].Points.Clear();
                chart3.Series[1].Points.Clear();

                for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                {
                    string fileName = checkedListBox1.CheckedItems[i].ToString();
                    string originalFilePath = fileName + ".txt";
                    string sortedFilePath = fileName + "_sorted.txt";

                    StreamReader reader = new StreamReader(originalFilePath);
                    int size = Convert.ToInt32(reader.ReadLine());
                    int[] mas = new int[size];
                    string line = reader.ReadLine();
                    Experiment.LineReader(this, mas, line, 0);
                    reader.Close();

                    try
                    {
                        Experiment.StartSort(this, mas, size, fileName);
                        WriteSequenceToFile(originalFilePath, mas);
                        WriteSequenceToFile(sortedFilePath, mas.OrderBy(x => x).ToArray());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при сортировке массива " + fileName + ": " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void IsGenerated_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength != '\0' && !(comboBox1.SelectedIndex == -1))
            {
                int caseSwitch = comboBox1.SelectedIndex;

                if (Convert.ToInt32(textBox1.Text) <= 1 || Convert.ToInt32(textBox1.Text) > 1000000)
                {
                    MessageBox.Show("Недопустимое количество элементов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (NumberOfString <= 10)
                    {
                        bool isHave = false;

                        // Очищение поля "ошибки" и диаграммы
                        label8.Text = "No Errors";

                        int[] mas = new int[int.Parse(textBox1.Text)];
                        string name = "";

                        switch (caseSwitch)
                        {
                            case 0:
                                Generation.GetMassive0(this,mas);
                                name = "up" + textBox1.Text;
                                break;
                            case 1:
                                Generation.GetMassive1(this, mas);
                                name = "down" + textBox1.Text;
                                break;
                            case 2:
                                Generation.GetMassive2(this,mas);
                                name = "similar" + textBox1.Text;
                                break;
                            case 3:
                                Generation.GetMassive3(this,mas);
                                name = "random" + textBox1.Text;
                                break;
                            case 4:
                                Generation.GetMassive4(this, mas);
                                name = "half" + textBox1.Text;
                                break;
                        }

                        foreach (string s in checkedListBox1.Items.OfType<string>())
                        {
                            if (s == name)
                                isHave = true;
                        }

                        if (!isHave)
                        {
                            checkedListBox1.Items.Insert(NumberOfString, name);
                            // Создание файла
                            WriteSequenceToFile(name + ".txt", mas);

                            NumberOfString++;
                        }
                        else
                            MessageBox.Show("Такой файл уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Превышено количество массивов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Вид последовательности не был выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CursesWork_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        public void WriteSequenceToFile(string filePath, int[] sequence)
        {
            StreamWriter writer = new StreamWriter(filePath);
            writer.WriteLine(sequence.Length);
            for (int i = 0; i < sequence.Length; i++)
            {
                writer.Write(sequence[i].ToString() + " ");
            }
            writer.Close();
        }
    }
}
