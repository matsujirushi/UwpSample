using System;
using System.Diagnostics;
using System.Linq;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace SpeechSynthesizerApp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var voice in SpeechSynthesizer.AllVoices.OrderBy(a => a.Id))
            {
                Debug.WriteLine($"{voice.Id}\t{voice.DisplayName}\t{voice.Gender}");
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesisStream stream;
            using (var ss = new SpeechSynthesizer())
            {
                ss.Voice = (SpeechSynthesizer.AllVoices.First(x => x.Id.EndsWith("\\MSTTS_V110_enUS_ZiraM")));
                stream = await ss.SynthesizeTextToStreamAsync(txtScript.Text);
            }
            Media.SetSource(stream, stream.ContentType);
            Media.Play();
        }
    }
}
