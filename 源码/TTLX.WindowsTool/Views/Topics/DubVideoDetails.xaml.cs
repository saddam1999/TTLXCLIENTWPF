using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.Win32;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Controls.Common;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Topics
{
	// Token: 0x02000020 RID: 32
	public partial class DubVideoDetails : UserControl, ITopicDetails
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006A85 File Offset: 0x00004C85
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00006A7C File Offset: 0x00004C7C
		public Topic TopicInfo { get; set; }

		// Token: 0x0600012F RID: 303 RVA: 0x00006A8D File Offset: 0x00004C8D
		public DubVideoDetails(Topic topic)
		{
			this.InitializeComponent();
			this.TopicInfo = topic;
			base.DataContext = this;
			this.XQuestionLst.Init(topic.Questions);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006ABC File Offset: 0x00004CBC
		private async void XVideoPlayer_OnMediaFileDownloaded(string path)
		{
			string fileName = Path.GetFileName(path);
			string newValue = "C0" + fileName;
			string track0Path = path.Replace(fileName, newValue);
			if (!File.Exists(track0Path))
			{
				if (await MediaHelper.ConvertMediaToSingleTrackMp4(path, track0Path, 0))
				{
					this.TopicInfo.MediaUrl = track0Path;
				}
			}
			else
			{
				this.TopicInfo.MediaUrl = track0Path;
			}
			foreach (Question question in this.TopicInfo.Questions)
			{
				question.AudioUrl = track0Path;
			}
			await AppData.Current.InitTopicInfo();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006AFD File Offset: 0x00004CFD
		private void XSelectImg_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.TopicInfo.ImgUrl = p;
			}, 16, 9, 1920));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006B20 File Offset: 0x00004D20
		private void XBtnAddQuestion_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.XVideoPlayer.MediaFileName))
			{
				MessengerHelper.ShowToast("请先选择视频");
				return;
			}
			Question item = new Question
			{
				TopicId = this.TopicInfo.Id,
				Idx = this.TopicInfo.Questions.Count + 1,
				Type = QuestionTypeEnum.时间轴,
				AnswerType = AnswerTypeEnum.配音答案,
				AudioUrl = this.XVideoPlayer.MediaFileName
			};
			this.TopicInfo.Questions.Add(item);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006BAC File Offset: 0x00004DAC
		private void XBtnSelVideo_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.TopicInfo.Questions.Count > 0)
			{
				MessengerHelper.ShowToast("更新视频前请删除相关问题");
				return;
			}
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "选择视频";
			openFileDialog.Filter = "视频文件|*.mp4;*.wmv;*.avi;*.mkv";
			if (openFileDialog.ShowDialog() == true)
			{
				DialogHelper.ShowDialog(new VideoEditWnd(openFileDialog.FileName, delegate(string p)
				{
					this.TopicInfo.MediaUrl = p;
				}));
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006C30 File Offset: 0x00004E30
		private async void XBtnSelAudio_OnClick(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "选择音频";
			openFileDialog.Filter = "音频文件|*.mp2;*.mp3;*.mp4;*.aac;*.wav;*.cda";
			if (openFileDialog.ShowDialog() == true)
			{
				string outputPath = Helper.GetTempMp3Path();
				if (await MediaHelper.ConvertAudioToMp3(openFileDialog.FileName, outputPath))
				{
					this.XAudioPlayer.MediaFileName = outputPath;
				}
				outputPath = null;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006C6C File Offset: 0x00004E6C
		private async void XBtnPlayVoix_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.IsQuestionsRangeValidated())
			{
				if (this.XBtnPlayVoix.IsChecked == true)
				{
					if (this.XCkbMergeAudio.IsChecked == true)
					{
						if (await this.MergeVideoAudio())
						{
							this.XMergedVideoPlayer.Play();
						}
						else
						{
							MessengerHelper.ShowToast("合成消音视频发生异常");
						}
					}
					else if (await this.CustomMergeVideoAudio())
					{
						this.XMergedVideoPlayer.Play();
					}
					else
					{
						MessengerHelper.ShowToast("合成消音视频发生异常");
					}
					this.XViewer.ScrollToRightEnd();
				}
				else
				{
					this.XMergedVideoPlayer.Stop();
					this.XViewer.ScrollToLeftEnd();
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006CA8 File Offset: 0x00004EA8
		private bool IsQuestionsRangeValidated()
		{
			List<Question> list = this.TopicInfo.Questions.ToList<Question>();
			list.Sort((Question q1, Question q2) => q1.Timeline.Start.CompareTo(q2.Timeline.Start));
			for (int i = 0; i < list.Count - 1; i++)
			{
				if (list[i].Timeline.Stop > list[i + 1].Timeline.Start)
				{
					MessengerHelper.ShowToast("有重复消音的范围");
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006D34 File Offset: 0x00004F34
		private async Task<bool> CustomMergeVideoAudio()
		{
			bool result;
			if (string.IsNullOrWhiteSpace(this.XVideoPlayer.MediaFileName))
			{
				result = false;
			}
			else
			{
				string videoPath = this.XVideoPlayer.MediaFileName;
				double videoLength = this.XVideoPlayer.MediaLength.Value;
				List<string> concatPaths = new List<string>();
				int pos = 0;
				List<Question> questions = this.TopicInfo.Questions.ToList<Question>();
				for (int i = 0; i < questions.Count; i++)
				{
					Question q = questions[i];
					if (q.Timeline.Start > pos)
					{
						string pathInter = Helper.GetTempWavPath();
						if (!(await MediaHelper.CutMediaToStereoWav(videoPath, pathInter, (double)pos, (double)q.Timeline.Start)))
						{
							return false;
						}
						concatPaths.Add(pathInter);
						pathInter = null;
					}
					int frontEase = 1000;
					if (q.Timeline.Start < frontEase)
					{
						frontEase = q.Timeline.Start;
					}
					string pathWavQuestion = Helper.GetTempWavPath();
					if (!(await MediaHelper.CutMediaToStereoWav(videoPath, pathWavQuestion, (double)(q.Timeline.Start - frontEase), (double)q.Timeline.Stop)))
					{
						return false;
					}
					string pathTemp = Helper.GetTempWavPath();
					if (!(await MediaHelper.RemoveAudioVoice(pathWavQuestion, pathTemp, q.VoixLower, q.VoixUpper, q.VoixVolumeIncrease)))
					{
						return false;
					}
					string pathRemoved = Helper.GetTempWavPath();
					if (!(await MediaHelper.CutMediaToStereoWav(pathTemp, pathRemoved, (double)frontEase, (double)(frontEase + q.Timeline.Stop - q.Timeline.Start))))
					{
						return false;
					}
					concatPaths.Add(pathRemoved);
					pos = q.Timeline.Stop;
					if (i == questions.Count - 1 && (double)pos < videoLength)
					{
						string pathLast = Helper.GetTempWavPath();
						if (!(await MediaHelper.CutMediaToStereoWav(videoPath, pathLast, (double)pos, videoLength)))
						{
							return false;
						}
						concatPaths.Add(pathLast);
						pathLast = null;
					}
					q = null;
					pathWavQuestion = null;
					pathTemp = null;
					pathRemoved = null;
				}
				string vocalRemovedPath = Helper.GetTempMp3Path();
				if (concatPaths.Count > 0)
				{
					if (!(await MediaHelper.ConcatWavToMp3(concatPaths, vocalRemovedPath)))
					{
						return false;
					}
				}
				else
				{
					string wavPath = Helper.GetTempWavPath();
					if (!(await MediaHelper.ConvertMediaToStereoWav(videoPath, wavPath)))
					{
						return false;
					}
					string tmpPath = Helper.GetTempWavPath();
					if (!(await MediaHelper.RemoveAudioVoice(wavPath, tmpPath, 100, 12000, 0)))
					{
						return false;
					}
					if (!(await MediaHelper.ConvertAudioToMp3(tmpPath, vocalRemovedPath)))
					{
						return false;
					}
					wavPath = null;
					tmpPath = null;
				}
				string resultPath = Helper.GetTempMp4Path();
				if (!(await MediaHelper.MergeVideoAudio(videoPath, vocalRemovedPath, resultPath)))
				{
					result = false;
				}
				else
				{
					this.XMergedVideoPlayer.MediaFileName = resultPath;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006D7C File Offset: 0x00004F7C
		private async Task<bool> MergeVideoAudio()
		{
			bool result;
			if (string.IsNullOrWhiteSpace(this.XVideoPlayer.MediaFileName) || string.IsNullOrWhiteSpace(this.XAudioPlayer.MediaFileName))
			{
				result = false;
			}
			else
			{
				string mediaFileName = this.XVideoPlayer.MediaFileName;
				string mediaFileName2 = this.XAudioPlayer.MediaFileName;
				string outputPath = Helper.GetTempMp4Path();
				if (!(await MediaHelper.MergeVideoAudio(mediaFileName, mediaFileName2, outputPath)))
				{
					result = false;
				}
				else
				{
					this.XMergedVideoPlayer.MediaFileName = outputPath;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006DC4 File Offset: 0x00004FC4
		private void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.TopicInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddFileInfo("imgFile", this.TopicInfo.ImgUrl, "image/jpeg");
			requestInfo.AddFileInfo("mediaFile", this.XMergedVideoPlayer.MediaFileName, "video/mp4");
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006E8C File Offset: 0x0000508C
		private void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.TopicInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddFileInfo("imgFile", this.TopicInfo.ImgUrl, "image/jpeg");
			requestInfo.AddFileInfo("mediaFile", this.XMergedVideoPlayer.MediaFileName, "video/mp4");
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006F54 File Offset: 0x00005154
		private async Task<bool> IsMergeVideoAudioValidated()
		{
			bool result;
			if (!this.IsQuestionsRangeValidated())
			{
				result = false;
			}
			else
			{
				if (this.XCkbMergeAudio.IsChecked == true)
				{
					if (!(await this.MergeVideoAudio()))
					{
						MessengerHelper.ShowToast("合成消音视频发生异常");
						return false;
					}
				}
				else if (!(await this.CustomMergeVideoAudio()))
				{
					MessengerHelper.ShowToast("合成消音视频发生异常");
					return false;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006F9C File Offset: 0x0000519C
		private async Task<bool> IsQuestionsValidated()
		{
			return await this.XQuestionLst.IsValidated();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006FE4 File Offset: 0x000051E4
		private async Task<bool> SaveQuestions()
		{
			return await this.XQuestionLst.Save();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000702C File Offset: 0x0000522C
		public async Task<bool> Save()
		{
			bool result;
			if (string.IsNullOrWhiteSpace(this.TopicInfo.ImgUrl))
			{
				MessengerHelper.ShowToast("请添加图片");
				result = false;
			}
			else if (!(await this.IsQuestionsValidated()))
			{
				result = false;
			}
			else
			{
				if (this.TopicInfo.IsSaved)
				{
					this.InitUpdateRequest();
					if (await AppData.Current.UpdateTopic())
					{
						return await this.SaveQuestions();
					}
				}
				else
				{
					if (!(await this.IsMergeVideoAudioValidated()))
					{
						return false;
					}
					this.InitCreateRequest();
					if (await AppData.Current.CreateTopic())
					{
						return await this.SaveQuestions();
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007074 File Offset: 0x00005274
		private async void XBtnMerge_OnClick(object sender, RoutedEventArgs e)
		{
			await this.IsMergeVideoAudioValidated();
		}
	}
}
