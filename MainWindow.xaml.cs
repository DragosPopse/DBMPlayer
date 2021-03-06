﻿using System;
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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;
using System.IO;
using System.Xml.Linq;


using NAudio.Wave;
using NAudio.Wave.SampleProviders;

using Notifications.Wpf;


namespace DBMPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GlobalKeyboardHook _keyHook;
        private DispatcherTimer _timer;
        private NotificationManager _notifManager;

        private List<MusicTrack> _libraryTracks;
        private List<string> _libraryFolders;
        private List<Playlist> _playlists;

        public MainWindow() 
        {
            InitializeComponent();
            _notifManager = new NotificationManager();
            _keyHook = new GlobalKeyboardHook();
            _libraryTracks = new List<MusicTrack>();
            _libraryFolders = new List<string>();
            _playlists = new List<Playlist>();
            _timer = new DispatcherTimer();

            AddKeyboardCallbacks();
            
            _timer.Tick += new EventHandler(UpdateKeyboardHook);
            _timer.Interval += new TimeSpan(0, 0, 0, 0, 34);
            _timer.Start();

            LoadMusicLibraries();
            LoadPlaylists();
        }

        private void AddKeyboardCallbacks()
        {
            _keyHook.AddCallback("HideShow", Keys.NumPad2,
                () =>
                {

                    if (Visibility != Visibility.Hidden)
                    {
                        Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        Visibility = Visibility.Visible;
                    }
                });

            //_keyHook.AddCallback("Play", Keys.NumPad5, Play);
        }

        private void UpdateKeyboardHook(object sender, EventArgs e)
        {
            _keyHook.Update();
        }

        private void OnExit(object sender, EventArgs e)
        {

        }

        private void AddTracksFromFolder(string path)
        {
            string[] trackPaths = Directory.GetFiles(path, "*.mp3");
            for (int i = 0; i < trackPaths.Length; i++)
            {
                _libraryTracks.Add(new MusicTrack(trackPaths[i]));
            }
        }

        private void LoadMusicLibraries()
        {
            string xmlFile = @"data\libraries.xml";
            if (File.Exists(xmlFile))
            {
                XElement doc = XElement.Load(xmlFile);
                var folders = doc.Elements("folder");
                foreach (var folder in folders)
                {
                    string folderPath = folder.Attribute("path").Value;
                    if (Directory.Exists(folderPath))
                    {
                        _libraryFolders.Add(folderPath);
                        AddTracksFromFolder(folderPath);
                    }
                    else
                    {
                        _notifManager.ShowError("Couldn't find directory " + folderPath);
                    }
                }
            }
            else
            {
                _notifManager.ShowError("Couldn't find file " + xmlFile);
            }
        }

        private void LoadPlaylists()
        {
            string xmlFile = @"data\playlists.xml";
            if (File.Exists(xmlFile))
            {
                XElement doc = XElement.Load(xmlFile);
                var playlists = doc.Elements("playlist");
                foreach (var element in playlists)
                {
                    string name = element.Attribute("name").Value;
                    var tracks = element.Elements("track");
                    Playlist playlist = new Playlist(name);
                    foreach (var t in tracks)
                    {
                        string trackPath = t.Attribute("path").Value;
                        MusicTrack track = new MusicTrack(trackPath);
                        playlist.AddTrack(track);
                    }
                    _playlists.Add(playlist);
                }
            }
            else
            {
                _notifManager.ShowError("Couldn't find file " + xmlFile);
            }
        }

        private void SavePlaylists()
        {
            XDocument doc = new XDocument();
            doc.Add(
                new XElement(
                    "playlists"
                ));
            XElement playlistsElement = doc.Element("playlists");
            int n = _playlists.Count;
            for (int i = 0; i < n; i++)
            {
                playlistsElement.Add(new XElement("playlist", 
                    new XAttribute("name", _playlists[i].Name)));
                XElement current = playlistsElement.Elements("playlist").Last();
                var playlistTracks = _playlists[i].Tracks;
                int tracksCount = playlistTracks.Count;
                for (int j = 0; j < tracksCount; j++)
                {
                    current.Add(
                        new XElement(
                                "track",
                                new XAttribute("path", playlistTracks[j].Path)
                            ));
                }
            }
            doc.Save(@"data\playlists.xml");
        }

        private void SaveMusicLibraries()
        {
            XDocument doc = new XDocument();
            doc.Add(
                new XElement(
                    "libraries"
                ));
            XElement librariesElement = doc.Element("libraries");
            int n = _libraryFolders.Count;
            for (int i = 0; i < n; i++)
            {
                librariesElement.Add(
                        new XElement("folder", 
                            new XAttribute("path", _libraryFolders[i])
                            )
                    );
            }
            doc.Save(@"data\libraries.xml");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveMusicLibraries();
            SavePlaylists();
        }
    }
}
