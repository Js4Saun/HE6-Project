using FilePickerDemo.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FilePickerDemo.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}