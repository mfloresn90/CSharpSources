//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using Windows.Media.MediaProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SDKTemplate;
using System;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Devices.Enumeration;
using Windows.Foundation;

namespace MediaCapture
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AudioCapture : SDKTemplate.Common.LayoutAwarePage
    {
        private Windows.Media.Capture.MediaCapture m_mediaCaptureMgr;
        private Windows.Storage.StorageFile m_recordStorageFile;
        private bool m_bRecording;
        private bool m_bSuspended;
        private bool m_bUserRequestedRaw;
        private bool m_bRawAudioSupported;
        private TypedEventHandler<SystemMediaTransportControls, SystemMediaTransportControlsPropertyChangedEventArgs> m_mediaPropertyChanged;

        private readonly String AUDIO_FILE_NAME = "audio.mp4";

        // A pointer back to the main page.  This is needed if you want to call methods in MainPage such
        // as NotifyUser()
        MainPage rootPage = MainPage.Current;

        public AudioCapture()
        {
            this.InitializeComponent();
            ScenarioInit();
            m_mediaPropertyChanged = new TypedEventHandler<SystemMediaTransportControls, SystemMediaTransportControlsPropertyChangedEventArgs>(SystemMediaControls_PropertyChanged);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            SystemMediaTransportControls systemMediaControls = SystemMediaTransportControls.GetForCurrentView();
            systemMediaControls.PropertyChanged += m_mediaPropertyChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SystemMediaTransportControls systemMediaControls = SystemMediaTransportControls.GetForCurrentView();
            systemMediaControls.PropertyChanged -= m_mediaPropertyChanged;
            ScenarioClose();
        }

        private async void ScenarioInit()
        {
            btnStartDevice3.IsEnabled = true;
            btnStartStopRecord3.IsEnabled = false;
            m_bRecording = false;
            recordRawAudio.IsChecked = false;
            recordRawAudio.IsEnabled = false;
            m_bUserRequestedRaw = false;
            m_bRawAudioSupported = false;
            playbackElement3.Source = null;
            m_bSuspended = false;
            ShowStatusMessage("");


            //Read system's raw audio stream support
            String[] propertiesToRetrieve = { "System.Devices.AudioDevice.RawProcessingSupported" };
            try
            {
                var device = await DeviceInformation.CreateFromIdAsync(MediaDevice.GetDefaultAudioCaptureId(AudioDeviceRole.Communications), propertiesToRetrieve);
                m_bRawAudioSupported = device.Properties["System.Devices.AudioDevice.RawProcessingSupported"].Equals(true);
                if (m_bRawAudioSupported)
                {
                    recordRawAudio.IsEnabled = true;
                    ShowStatusMessage("Raw audio recording is supported");
                }
                else
                {
                    ShowStatusMessage("Raw audio recording is not supported");
                }
            }
            catch (Exception e)
            {
                ShowExceptionMessage(e);
            }
        }

        private async void ScenarioClose()
        {
            if (m_bRecording)
            {
                ShowStatusMessage("Stopping Record on invisibility");

                await m_mediaCaptureMgr.StopRecordAsync();
                m_bRecording = false;
                EnableButton(true, "StartStopRecord");
                m_mediaCaptureMgr.Dispose();
            }
            if (m_mediaCaptureMgr != null)
            {
                m_mediaCaptureMgr.Dispose();
            }

        }

        private async void SystemMediaControls_PropertyChanged(SystemMediaTransportControls sender, SystemMediaTransportControlsPropertyChangedEventArgs e)
        {
            switch (e.Property)
            {
                case SystemMediaTransportControlsProperty.SoundLevel:
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        if (sender.SoundLevel != Windows.Media.SoundLevel.Muted)
                        {
                            ScenarioInit();
                        }
                        else
                        {
                            ScenarioClose();
                        }
                    });
                    break;

                default:
                    break;
            }
        }

        public async void RecordLimitationExceeded(Windows.Media.Capture.MediaCapture currentCaptureObject)
        {
            try
            {
                if (m_bRecording)
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                    {
                        try
                        {
                            ShowStatusMessage("Stopping Record on exceeding max record duration");
                            await m_mediaCaptureMgr.StopRecordAsync();
                            m_bRecording = false;
                            SwitchRecordButtonContent();
                            EnableButton(true, "StartStopRecord");
                            ShowStatusMessage("Stopped record on exceeding max record duration:" + m_recordStorageFile.Path);
                        }
                        catch (Exception e)
                        {
                            ShowExceptionMessage(e);
                        }

                    });
                }
            }
            catch (Exception e)
            {
                ShowExceptionMessage(e);
            }
        }

        public void Failed(Windows.Media.Capture.MediaCapture currentCaptureObject, MediaCaptureFailedEventArgs currentFailure)
        {
            try
            {
                var ignored = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    try
                    {
                        ShowStatusMessage("Fatal error" + currentFailure.Message);

                    }
                    catch (Exception e)
                    {
                        ShowExceptionMessage(e);
                    }
                });
            }
            catch (Exception e)
            {
                ShowExceptionMessage(e);
            }
        }

        private async void startAudioCapture()
        {
            m_mediaCaptureMgr = new Windows.Media.Capture.MediaCapture();
            var settings = new Windows.Media.Capture.MediaCaptureInitializationSettings();
            settings.StreamingCaptureMode = Windows.Media.Capture.StreamingCaptureMode.Audio;
            settings.MediaCategory = Windows.Media.Capture.MediaCategory.Other;
            settings.AudioProcessing = (m_bRawAudioSupported && m_bUserRequestedRaw) ? Windows.Media.AudioProcessing.Raw : Windows.Media.AudioProcessing.Default;
            await m_mediaCaptureMgr.InitializeAsync(settings);

            EnableButton(true, "StartPreview");
            EnableButton(true, "StartStopRecord");
            EnableButton(true, "TakePhoto");
            ShowStatusMessage("Device initialized successfully");
            m_mediaCaptureMgr.RecordLimitationExceeded += new Windows.Media.Capture.RecordLimitationExceededEventHandler(RecordLimitationExceeded); ;
            m_mediaCaptureMgr.Failed += new Windows.Media.Capture.MediaCaptureFailedEventHandler(Failed); ;
        }

        private void btnStartDevice_Click(Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                m_bUserRequestedRaw = recordRawAudio.IsChecked.Value ? true : false;
                recordRawAudio.IsEnabled = false;
                EnableButton(false, "StartDevice");
                ShowStatusMessage("Starting device");
                startAudioCapture();
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
            }
        }

        internal async void btnStartStopRecord_Click(Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                String fileName;
                EnableButton(false, "StartStopRecord");

                if (!m_bRecording)
                {
                    ShowStatusMessage("Starting Record");

                    fileName = AUDIO_FILE_NAME;

                    m_recordStorageFile = await Windows.Storage.KnownFolders.VideosLibrary.CreateFileAsync(fileName, Windows.Storage.CreationCollisionOption.GenerateUniqueName);

                    ShowStatusMessage("Create record file successful");

                    MediaEncodingProfile recordProfile = null;
                    recordProfile = MediaEncodingProfile.CreateM4a(Windows.Media.MediaProperties.AudioEncodingQuality.Auto);

                    await m_mediaCaptureMgr.StartRecordToStorageFileAsync(recordProfile, this.m_recordStorageFile);

                    m_bRecording = true;
                    SwitchRecordButtonContent();
                    EnableButton(true, "StartStopRecord");

                    ShowStatusMessage("Start Record successful");

                }
                else
                {
                    ShowStatusMessage("Stopping Record");

                    await m_mediaCaptureMgr.StopRecordAsync();

                    m_bRecording = false;
                    EnableButton(true, "StartStopRecord");
                    SwitchRecordButtonContent();

                    ShowStatusMessage("Stop record successful");
                    if (!m_bSuspended)
                    {
                        var stream = await m_recordStorageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);

                        ShowStatusMessage("Record file opened");
                        ShowStatusMessage(this.m_recordStorageFile.Path);
                        playbackElement3.AutoPlay = true;
                        playbackElement3.SetSource(stream, this.m_recordStorageFile.FileType);
                        playbackElement3.Play();

                    }

                }
            }
            catch (Exception exception)
            {
                EnableButton(true, "StartStopRecord");
                ShowExceptionMessage(exception);
                m_bRecording = false;
            }
        }

        private void ShowStatusMessage(String text)
        {
            rootPage.NotifyUser(text, NotifyType.StatusMessage);
        }

        private void ShowExceptionMessage(Exception ex)
        {
            rootPage.NotifyUser(ex.Message, NotifyType.ErrorMessage);
        }

        private void SwitchRecordButtonContent()
        {
            {
                if (m_bRecording)
                {
                    btnStartStopRecord3.Content = "StopRecord";
                }
                else
                {
                    btnStartStopRecord3.Content = "StartRecord";
                }
            }
        }

        private void EnableButton(bool enabled, String name)
        {
            if (name.Equals("StartDevice"))
            {
                btnStartDevice3.IsEnabled = enabled;
            }

            else if (name.Equals("StartStopRecord"))
            {
                btnStartStopRecord3.IsEnabled = enabled;
            }

        }
    }
}
