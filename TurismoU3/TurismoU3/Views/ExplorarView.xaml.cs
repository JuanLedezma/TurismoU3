using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoU3.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurismoU3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExplorarView : ContentPage
    {
        public ExplorarView()
        {
            InitializeComponent();

            MensajeAsync();


        }
        private async Task MensajeAsync()
        {
            await DisplayAlert("Sugerencia", "¡Para poder editar o eliminar debe hacer dos tap (clicks) seguidos en la imagen y para ver mas detalles solo uno!", "Entendido");
        }
        private async void TapGestureRecognizer_TappedAsync(object sender, EventArgs e)
        {
            
            string action = await DisplayActionSheet("Que deseas hacer con el elemento?", null, null, "Editar", "Eliminar");
            Debug.WriteLine("Action: " + action);
            switch (action)
            {
                case "Editar":
                    lvm.EditarCommand.Execute(lvm.Temp);
                   /*await Navigation.PushAsync(new EditarView());*/
                    


                    break;
                case "Eliminar":
                    if (await DisplayAlert("Por favor confirme", $"¿Está seguro de eliminar este lugar?", "Si", "No") == true)
                    {
                         lvm.EliminarCommand.Execute(lvm.Temp);
                    }
                   
                    break;
                    

            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            lvm.DetallesCommand.Execute(lvm.Temp);
        }
    }
}