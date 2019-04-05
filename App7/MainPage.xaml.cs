using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Plugin.SimpleAudioPlayer;
using Plugin.AudioRecorder;
using System.Timers;
using System.Reflection;


namespace App7
{
    

    public partial class MainPage : ContentPage
    {

        AudioRecorderService recorder;
        AudioPlayer player;
        bool isTimerRunning = false;
        int seconds = 0, minutes = 0;
        string nome ;

        public MainPage()
        {
            
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
                this.Padding = new Thickness(0, 28, 0, 0);

            recorder = new AudioRecorderService
            {
                StopRecordingAfterTimeout = true,
                TotalAudioTimeout = TimeSpan.FromSeconds(15),
                AudioSilenceTimeout = TimeSpan.FromSeconds(2)
            };

            player = new AudioPlayer();
            player.FinishedPlaying += Finish_Playing;


        }


        void Felicidade_Clicked(object sender, EventArgs e)
        {
            nome = "Felicidade";
            lbl_teste.Text = "Diga: Estou Feliz";
        }


        void Tristeza_Clicked(object sender, EventArgs e)
        {
            nome = "Triteza";
            lbl_teste.Text = "Diga: Estou Triste";
        }

        void Finish_Playing(object sender, EventArgs e)
        {
            bntRecord.IsEnabled = true;
            bntRecord.BackgroundColor = Color.FromHex("#7cbb45");
            bntPlay.IsEnabled = true;
            bntPlay.BackgroundColor = Color.FromHex("#7cbb45");
            bntStop.IsEnabled = false;
            bntStop.BackgroundColor = Color.Silver;
            lblSeconds.Text = "00";
            lblMinutes.Text = "00";
        }

        async void Record_Clicked(object sender, EventArgs e)
        {
            seconds++;
            //nome = Path.GetRandomFileName() + ".wav";
            //string nomeArquivo = Path.Combine(App.PastaDiretorio, $"{nome}");

            //File.Create(nomeArquivo);
            //recorder.FilePath = nomeArquivo;

            if (!recorder.IsRecording)
            {
                seconds = 0;
                minutes = 0;
                isTimerRunning = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                    

                    if (seconds.ToString().Length == 1)
                    {
                        lblSeconds.Text = "0" + seconds.ToString();
                    }
                    else
                    {
                        lblSeconds.Text = seconds.ToString();
                    }
                    if (seconds == 60)
                    {
                        minutes++;
                        seconds = 0;

                        if (minutes.ToString().Length == 1)
                        {
                            lblMinutes.Text = "0" + minutes.ToString();
                        }
                        else
                        {
                            lblMinutes.Text = minutes.ToString();
                        }

                        lblSeconds.Text = "00";
                    }
                    return isTimerRunning;
                });

                //
                recorder.StopRecordingOnSilence = IsSilence.IsToggled;
                var audioRecordTask = await recorder.StartRecording();

          
                //label_inicial.Text = nomeArquivo;





                bntRecord.IsEnabled = false;
                bntRecord.BackgroundColor = Color.Silver;
                bntPlay.IsEnabled = true;
                bntPlay.BackgroundColor = Color.Silver;
                bntStop.IsEnabled = true;
                bntStop.BackgroundColor = Color.FromHex("#7cbb45");

                
            }
        }

        async void Stop_Clicked(object sender, EventArgs e)
        {
            StopRecording();
            await recorder.StopRecording();
            
           
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Consumo());
        }

        void StopRecording()
        {




            //var FilePath = recorder.GetAudioFilePath();
            //var File = recorder.GetAudioFilePath();

            // var FilePath = "/data/user/0/com.companyname.App7/files/.local/share/";

            // string nome = "voz1.wav";
            // string nomeArquivo = Path.Combine(App.PastaDiretorio, $"{nome}");
            //recorder.FilePath = FilePath;

            //  recorder.FilePath = recorder.GetAudioFilePath();
            // File.Create(FilePath);
            // File.Move(recorder.GetAudioFilePath(),App.PastaDiretorio);
            //string nome = "teste111.wav";
            
           

           
            //label_inicial.Text = nomeArquivo;





            //lbl_teste.Text = recorder.GetAudioFilePath();

            isTimerRunning = false;
            bntRecord.IsEnabled = true;
            bntRecord.BackgroundColor = Color.FromHex("#7cbb45");
            bntPlay.IsEnabled = true;
            bntPlay.BackgroundColor = Color.FromHex("#7cbb45");
            bntStop.IsEnabled = false;
            bntStop.BackgroundColor = Color.Silver;
            lblSeconds.Text = "00";
            lblMinutes.Text = "00";

            copyFileSound();

        }

        public void copyFileSound() {

            if (File.Exists("/data/user/0/com.companyname.App7/files/.local/share/ARS_"+nome+".wav")) { 
                File.Delete("/data/user/0/com.companyname.App7/files/.local/share/ARS_"+ nome +".wav");
            }
            
            File.Copy("/data/user/0/com.companyname.App7/cache/ARS_recording.wav", "/data/user/0/com.companyname.App7/files/.local/share/ARS_"+ nome +".wav");
        }

        void Add_Clicked(object sender, EventArgs e)
        {


                          ///data/user/0/com.companyname.App7/files/.local/share/ARS_recording.wav
            var FilePath = "/data/user/0/com.companyname.App7/files/.local/share/ARS_"+ nome +".wav";
            


            StopRecording();
            player.Play(FilePath);





        }
        void Play_Clicked(object sender, EventArgs e)
        {
            try
            {
                // string nome = "Voz1";
                // string nomeArquivo = Path.Combine(App.PastaDiretorio, $"{nome}");

                //var FilePath = recorder.GetAudioFilePath();
                //var FilePath = recorder.GetAudioFilePath();
                var FilePath = "data/user/0/com.companyname.App7/files/.local/share/ARS_recording.wav";
               // string nome = "teste111.wav";
                StopRecording();
                    player.Play(FilePath);
                
               // lbl_teste.Text = filePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
