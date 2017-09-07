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
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void engine_SpeechRecongnized(object sender, SpeechHypothesizedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void engine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            throw new NotImplementedException();
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
            catch (Exception)
            {

                MessageBox.Show(ex.Message);
            }
        }

       
    }
}
