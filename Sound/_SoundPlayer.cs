using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.Layers;
using Timeline.Framework.SpriteObject;
using Timeline.Framework.SpriteObject.SpriteBase;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Timeline.Framework.Drawing.Sound
{
    /// <summary>
    /// TODO:以后要改
    /// </summary>
    [Obsolete("有问题的，暂时用其它代替")]
    internal class _SoundPlayer 
    {
        /*
        public _SoundPlayer(Layer layer) : base(layer)
        {
            Device = new XAudio2();
            Device.StartEngine();
            var mc = new MasteringVoice(Device);
        }

        private XAudio2 Device;
        private SourceVoice Voice;
        private WaveFormat currentFormat;
        private AudioBuffer currentBuffer;
        private uint[] PacketsInfo;
        public AudioBuffer CurrentBuffer => currentBuffer;

        public WaveFormat CurrentFormat => currentFormat;

        public void Play(float volume=1f)
        {
            Voice?.Stop();

            Voice?.SetVolume(volume);
            Voice?.Start(0);
        }
        public void Stop()
        {
            Voice?.Stop();         
        }
        public async Task<bool> LoadSound(Uri uri)
        {
            try
            {
                Voice = await CreateVoice(uri);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected void LoadBuffer()
        {
            Voice?.FlushSourceBuffers();
            Voice?.SubmitSourceBuffer(CurrentBuffer, PacketsInfo);
        }

        protected async Task<SourceVoice> CreateVoice(Uri uri)
        {
            SourceVoice sv;
            var s = await StorageFile.GetFileFromApplicationUriAsync(uri);
            using (var stream = await s.OpenAsync(FileAccessMode.Read)) 
            {
                sv = CreateVoice(stream);
            }
            return sv;
        }
        protected SourceVoice CreateVoice(IRandomAccessStream stream)//不会自动释放
        {
            var s = stream.AsStreamForRead();
            using (var ss=new SoundStream(s))
            {
                currentFormat = ss.Format;
                currentBuffer = new AudioBuffer()
                {
                    Stream = ss.ToDataStream(),
                    AudioBytes = (int)ss.Length,
                    Flags = BufferFlags.EndOfStream
                };
                PacketsInfo = ss.DecodedPacketsInfo;
            }
            return new SourceVoice(Device, CurrentFormat, true);
        }
        protected override void Destory()
        {
            Voice?.DestroyVoice();
            Voice?.Dispose();
            GC.SuppressFinalize(this);
        }
        protected override void Draw()
        {
            
        }
        */
    }
    
}
