using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Plugin.SimpleAudioPlayer;
using Plugin.AudioRecorder;
using System.Timers;
using System.Reflection;
using System.Collections;

namespace App7
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Consumo : ContentPage
	{
        ArrayList listaArquiv = new ArrayList();
        public Consumo ()
		{
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            //listaArquivo.ItemsSource = lista;
        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }


        protected override void OnAppearing()
        {
           // var meuarquivo = new List<Arquivo>();

            var arquivos = Directory.EnumerateFiles(App.PastaDiretorio, "*.wav");
            foreach (var nomearquivo in arquivos)
            {
                //File.Delete(nomearquivo);
                int indice = nomearquivo.LastIndexOf('/');
                listaArquiv.Add(nomearquivo.Substring(indice + 1));
               // meuarquivo.Add(listaArquiv);
                
               
            }
            listaArquivo.ItemsSource = listaArquiv;
            //listaArquivo.ItemsSource = App.listaRecorder;
        }

    }
}