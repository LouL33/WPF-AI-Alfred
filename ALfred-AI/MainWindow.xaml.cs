using System;
using System.Collections.Generic;
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
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;

namespace ALfred_AI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechRecognitionEngine speechRecognionEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer ALFRED = new SpeechSynthesizer();

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                // hook to events
                speechRecognionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);
                speechRecognionEngine.SpeechHypothesized += new EventHandler<SpeechHypothesizedEventArgs>(engine_SpeechRecongnized);
                // load dictonnary
                LoadGrammerAndCommands();
                // use the system's default microphone
                speechRecognionEngine.SetInputToDefaultAudioDevice();
                // start Listening
                speechRecognionEngine.RecognizeAsync(RecognizeMode.Multiple);
                ALFRED.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(ALFRED_SpeakCompleted);

                if (ALFRED.State == SynthesizerState.Speaking)
                    ALFRED.SpeakAsyncCancelAll();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "voice recognition failed");
            }
        }

        private void ALFRED_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            if (ALFRED.State == SynthesizerState.Speaking)
                ALFRED.SpeakAsyncCancelAll();
        }

        private void engine_SpeechRecongnized(object sender, SpeechHypothesizedEventArgs e)
        {
            string Speech = e.Result.Text;
            switch (Speech)
            {
                case "hello Alfred":
                    ALFRED.SpeakAsync("hello master lou");
                    break;
                case "open google":
                    ALFRED.SpeakAsync("opening google");
                    System.Diagnostics.Process.Start("https://www.google.com/");
                    break;
            }
        }

        private void engine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            progress.Value = e.AudioLevel;
        }

        private void LoadGrammerAndCommands()
        {
            try
            {
                Choices Text = new Choices();
                string[] Lines = File.ReadAllLines(Environment.CurrentDirectory + "\\Commands.txt");
                Text.Add(Lines);
                Grammar WordsList = new Grammar(new GrammarBuilder(Text));
                speechRecognionEngine.LoadGrammar(WordsList);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

       
    }
}
