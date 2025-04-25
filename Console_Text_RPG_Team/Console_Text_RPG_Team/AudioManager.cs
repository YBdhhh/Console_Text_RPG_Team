using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using Newtonsoft.Json;

namespace Console_Text_RPG_Team
{
	internal class AudioManager
	{
		string audioFilePath;
		
		AudioFileReader audioFile;

		WaveOutEvent outputDevice;
		public AudioManager(string filePath)
		{
				audioFilePath = filePath;
				audioFile = new AudioFileReader(audioFilePath);
				outputDevice = new WaveOutEvent();


				// 초기 필수
				outputDevice.Init(audioFile);
		}
		public void Play()
		{
			if (outputDevice.PlaybackState != PlaybackState.Stopped)
				outputDevice.Stop(); // 이전 재생 중단

				audioFile.Position = 0;  // 위치 초기화
				outputDevice.Init(audioFile); // Init은 Stop 후 재초기화 필요할 수 있음
				outputDevice.Play();    // 다시 재생
		}
		public void PlayLooping()
		{
			outputDevice.PlaybackStopped += PlaybackStoppedHandler;
			outputDevice.Play();
		}
		public void Stop()
		{
			outputDevice.PlaybackStopped -= PlaybackStoppedHandler;
			outputDevice.Stop();
		}
		public void SetVolume(float volume)
		{
			outputDevice.Volume = volume;
		}
		public void Dispose()
		{
			outputDevice.Dispose();
			audioFile.Dispose();
		}
		public void PlaybackStoppedHandler(object sender, StoppedEventArgs e)
		{

			// 오디오 파일의 위치를 처음으로 재설정
			audioFile.Position = 0;
			outputDevice.Play();
		}
	}
}
