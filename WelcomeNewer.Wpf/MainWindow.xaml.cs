using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
using XJMSCognitive.CustomVision.Prediction;

namespace WelcomeNewer.Wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*
            using (var fileStream = File.OpenText("testjson.json"))
            {
                all = fileStream.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(all);
            }*/
        }

        private async void btnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            /*
            UrlPredictionManager urlPredictionManager = new UrlPredictionManager
            {
                Uri = "https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/Prediction/f79e8f4f-4776-410c-aec0-1ccefdd116b6/url?",
                PredictionKey = "03198e92ab7d4bdd8858def5b965ebb1",
                IterationId = "70e87612-cb65-4893-a39b-085e6bbc128b"
            };
            var result = await urlPredictionManager.Infer("http://a0.att.hudong.com/12/91/01300000027077119949915547455.jpg");
            System.Diagnostics.Debug.WriteLine(result.ToString());*/

            ImagePredictionManager imagePredictionManager = new ImagePredictionManager
            {
                Uri = "https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/Prediction/f79e8f4f-4776-410c-aec0-1ccefdd116b6/image?",
                PredictionKey = "03198e92ab7d4bdd8858def5b965ebb1",
                IterationId = "70e87612-cb65-4893-a39b-085e6bbc128b"
            };
            Response result;
            using (FileStream fileStream = File.Open("C:\\Users\\cjj19\\Pictures\\90E9C9A678EF743241E383BA6CBCFA36.jpg", FileMode.Open))
            {
                result = await imagePredictionManager.Infer(fileStream);
            }
            System.Diagnostics.Debug.WriteLine(result.ToString());

        }

        private async void btnImg1Ana_Click(object sender, RoutedEventArgs e)
        {
            /*OpenFileDialog openFileDialog = new OpenFileDialog();
             openFileDialog.Title = "选择文件";
             openFileDialog.Filter = "jpg|*.jpg|jpeg|*.jpeg";
             openFileDialog.FileName = string.Empty;
             openFileDialog.FilterIndex = 1;
             openFileDialog.RestoreDirectory = true;
             openFileDialog.DefaultExt = "jpg";
             DialogResult result = openFileDialog.ShowDialog();
             if (result == System.Windows.Forms.DialogResult.Cancel)
             {
                 return;
             }
             string fileName = openFileDialog.FileName;
             this.textBox1.Text = fileName;*/
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileOk += _OnImg1FileSelected;
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "jpg|*.jpg|jpeg|*.jpeg|png|*.png";
            openFileDialog.ShowDialog();
        }

        private async void _OnImg1FileSelected(object sender, CancelEventArgs e)
        {
            
            string fileName = (sender as OpenFileDialog).FileName;
            //Show PIC
            
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.UriSource = new Uri(fileName);
            bitmapImage.EndInit();
            
            img1.Source = bitmapImage;
            
            /*
            var bitmap = new BitmapImage();
            var stream = File.OpenRead(fileName);
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.StreamSource = stream;
            bitmap.EndInit();
            stream.Close();
            stream.Dispose();*/

            //img1.Source = bitmap;

            ImagePredictionManager imagePredictionManager = new ImagePredictionManager
            {
                Uri = "https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/Prediction/cc155d7e-6d62-441a-844a-7e49d4452eea/image?",
                PredictionKey = "f70c59c84cb441429c47257a6cfcade5",
                IterationId = "d29c79ba-4766-4ed0-aaf8-6029d8082240"
            };
            Response result;
            using (FileStream fileStream = File.Open(fileName, FileMode.Open))
            {
                result = await imagePredictionManager.Infer(fileStream);
            }
            //labelImg1.Content = result.Predictions[0].TagName;
            labelImg1.Content = GetPolluteIndex(result);

        }

        private void btnImg2Ana_Click(object sender, RoutedEventArgs e)
        {

        }

        public double GetPolluteIndex(Response response)
        {
            double finalIndex = 0;
            foreach(var prediction in response.Predictions)
            {
                int level = int.Parse(prediction.TagName.Remove(0, 5));
                finalIndex = finalIndex + level * prediction.Probability;
            }
            return finalIndex;
        }
    }
}
