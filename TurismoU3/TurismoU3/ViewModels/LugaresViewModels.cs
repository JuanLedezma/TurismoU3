using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using TurismoU3.Models;
using TurismoU3.Views;
using Xamarin.Forms;

namespace TurismoU3.ViewModels
{
    public class LugaresViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Lugares> Lugar { get; set; } = new ObservableCollection<Lugares>();
        public string Vacio { get; set; }
        public Lugares Lugares { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand DetallesCommand { get; set; }
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand TemporalCommand { get; set; }
        public Lugares Temp { get; set; }
        public void Notificar()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public LugaresViewModel()
        {
            Deserializar();
            EditarCommand = new Command<Lugares>(Editar);
            EliminarCommand = new Command<Lugares>(Eliminar);
            AgregarCommand = new Command(Agregar);
            GuardarCommand = new Command(Guardar);
            DetallesCommand = new Command<Lugares>(Detalles);
            CambiarVistaCommand = new Command<string>(CambiarVista);
            TemporalCommand = new Command<Lugares>(Temporal);
        }

        private void Temporal(Lugares t)
        {
            Temp= t;
        }

        private void Serializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "Lugares.json";
            File.WriteAllText(file, JsonConvert.SerializeObject(Lugar));
        }
        private void Deserializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "Lugares.json";
            if (File.Exists(file))
            {
                Lugar = JsonConvert.DeserializeObject<ObservableCollection<Lugares>>(File.ReadAllText(file));
            }
        }

        public void Eliminar(Lugares obj)
        {
            if (obj != null)
            {
                Lugar.Remove(obj);
                Serializar();
            }
        }
        int num;
        public void Editar(Lugares l)
        {
            num = Lugar.IndexOf(l);
            Lugares = new Lugares
            {
                Ciudad = l.Ciudad,
                Atraccion = l.Atraccion,
                Link = l.Link,
                Descripcion = l.Descripcion
            };
            EditarPage = new EditarView()
            {
                BindingContext = this
            };
            App.Current.MainPage.Navigation.PushAsync(EditarPage);
        }
        private void Guardar()
        {
            Lugar[num] = Lugares;
            Serializar();
            App.Current.MainPage.Navigation.PopToRootAsync();
        }
        EditarView EditarPage;
        DetallesView DetallesPage;
        AgregarView AgregarPage;


        
        private void CambiarVista( string vista)
        {
            if (vista == "Agregar")
            {
                Lugares = new Lugares(); 
                AgregarPage = new AgregarView() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(AgregarPage);
            }
            else if (vista == "Ver")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        private void Detalles(Lugares obj)
        {
            DetallesPage = new DetallesView()
            {
                BindingContext = obj
            };
            App.Current.MainPage.Navigation.PushAsync(DetallesPage);
        }



        private void Agregar()
        {
            if (Lugares != null)
            {
                Vacio = "";

                if (string.IsNullOrWhiteSpace(Lugares.Ciudad))
                {
                    Vacio = "Escriba el nombre de la ciudad";
                }

                if (string.IsNullOrWhiteSpace(Lugares.Atraccion))
                {
                    Vacio = "Escriba el nombre de la Atraccion";
                }
                if (string.IsNullOrWhiteSpace(Lugares.Descripcion))
                {
                    Vacio = "Escriba una breve descripcion del lugar";
                }
                if (string.IsNullOrWhiteSpace(Lugares.Link))
                {
                    Vacio = "Escriba el link de una foto para añadirlo al album";
                }
                if (string.IsNullOrWhiteSpace(Vacio))
                {
                    Lugar.Add(Lugares);


                    CambiarVista("Ver");
                    Serializar();

                }

                Notificar();
            }


        }
    }
}
