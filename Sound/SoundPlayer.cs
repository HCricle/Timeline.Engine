using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace Timeline.Framework.Drawing.Sound
{
    /// <summary>
    /// 音乐播放
    /// 注意：此实例最好只有一个(一个page最后只有0-1个)
    /// </summary>
    public class SoundPlayer : IDisposable
    {
        public SoundPlayer()
        {
            player = new MediaPlayer()
            {
                AutoPlay=true
            };
        }
        private MediaPlayer player;
        private MediaSource[] sources;
        private MediaPlaybackList playingList;
        /// <summary>
        /// 当前播放的列表
        /// </summary>
        public MediaPlaybackList PlayingList => playingList;
        /// <summary>
        /// 播放器
        /// </summary>
        public MediaPlayer Player => player;   
        public double Volumn
        {
            get => Player.Volume;
            set => Player.Volume = value;
        }
        public void SetPlayList(params Uri[] uris)
        {
            if (sources != null) 
            {
                for (int i = 0; i < sources.Length; i++)
                {
                    sources[i].Dispose();
                }
            }
            sources = new MediaSource[uris.Length];
            playingList = new MediaPlaybackList()
            {
                 AutoRepeatEnabled=false
            };
            MediaSource ms;
            for (int i = 0; i < uris.Length; i++)
            {
                ms = MediaSource.CreateFromUri(uris[i]);
                playingList.Items.Add(new MediaPlaybackItem(ms));
                sources[i] = ms;
            }
            Player.Source = PlayingList;
        }
        public void Stop()
        {
            Player.Source = null;
        }
        public void Dispose()
        {
            Player.Dispose();
        }
    }
}
