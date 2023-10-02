using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassWork24._09._2023NetworkProgrmming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        string str;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(textBoxAdress.Text);
                StringBuilder content = new StringBuilder();
                content.Append( await response.Content.ReadAsStringAsync());
                content.Append("\n\nПОШЛИ ЗАГОЛОВКИ\n");
                content.Append( response.Headers); // просмотр заголовков страницы
                textBoxContent.Text = content.ToString();
                str = content.ToString();
                var statusCode = response.StatusCode;
                MessageBox.Show($"Код ответа: {(int)statusCode} {statusCode}");
                
                response.Dispose();
                client.Dispose();
            }
           
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show("Неверный запрос"+ioe.Message);
            }            
            catch (HttpRequestException hre)
            {
                MessageBox.Show("Отсутствует подключение к интернету" + hre.Message);
            }


        }

        

        private void textBoxAdress_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxAdress.Text == "Адрес")
                textBoxAdress.Text = "";
        }

        private async void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => 
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = " (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string path = saveFileDialog.FileName;
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.OpenFile(), System.Text.Encoding.Default))//"text.txt", true, System.Text.Encoding.GetEncoding(1251)))
                    {
                        sw.WriteLine(str);
                        sw.Close();
                    }
                    
                }

            });
            


        }
    }
}
