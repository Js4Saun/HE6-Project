using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Plugin.XamarinFormsSaveOpenPDFPackage;

namespace FilePickerDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilePickerPage : ContentPage
    {
        public FilePickerPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // FOR PDF Files

            var customFileType =
                new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "application/pdf" } },
                });

            var pickImage = await FilePicker.PickAsync(new PickOptions()
            {
                FileTypes = customFileType,
                PickerTitle = "Pick a Page"
            });
            
            if(pickImage != null)
            {
                var stream = await pickImage.OpenReadAsync();

                using (var memorystream = new MemoryStream())
                {
                    await stream.CopyToAsync(memorystream);
                    await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView("myFile.pdf", "application/pdf", memorystream,PDFOpenContext.InApp);
                }
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var pickImages = await FilePicker.PickMultipleAsync(new PickOptions()
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an Images"
            });

            if(pickImages != null)
            {
                var imageList = new List<ImageSource>();
                foreach (var img in pickImages)
                {
                    var stream = await img.OpenReadAsync();
                    imageList.Add(ImageSource.FromStream(() => stream));
                }

                cvImage.ItemsSource = imageList;
            }
        }
    }
}