using Microcharts.Forms;
using Microcharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Entry = Microcharts.ChartEntry;
using SkiaSharp;
using System.IO;

namespace BudgetApp
{
    public partial class MainPage : ContentPage
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        List<Entry> entries = new List<Entry>
        {
            new Entry(200)
            {
                Color=SKColor.Parse("#FF1493"),
                Label = "Janaury",
                ValueLabel="200",
            }
        };
        void UpdateFileList()
        {
            // получаем все файлы
            opList.ItemsSource = Directory.GetFiles(folderPath).Select(f => Path.GetFileName(f));
            // снимаем выделение
            opList.SelectedItem = null;
        }
        public MainPage()
        {
            InitializeComponent();
            budgetChart.Chart=new DonutChart {Entries = entries};
        }
        protected override void OnAppearing()
        {
            var chart = new DonutChart { Entries = entries };
            base.OnAppearing();

            // ... our chart data and chart type here ...

            budgetChart.Chart = chart;
        }

        private void addBtn_Clicked(object sender, EventArgs e)
        {

        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Подтвердить действие", "Вы хотите удалить заметку?", "Удалить", "Отмена");
            if (result == true)
            {
                // получаем имя файла
                string filename = (string)((MenuItem)sender).BindingContext;
                // удаляем файл из списка
                File.SetAttributes(folderPath, FileAttributes.Normal);
                File.Delete(Path.Combine(folderPath, filename));
                // обновляем список файлов
                UpdateFileList();
                //await this.DisplayToastAsync("This is a Toast Message");
                //await this.DisplayToastAsync("This is a Toast Message for 5 seconds", 5000);

            }
        }
    }
}
