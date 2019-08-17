using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using WPReactionTest.ViewModels;

namespace WPReactionTest.Classes
{
    public class ReactionTestStore
    {
        public HighScoreList AllHighScores;
        
        private readonly IsolatedStorageSettings g_IsolatedStore;

        private const string LastHighScoreUpdateKey = "LastHighScoreUpdate";
        private const string HighScoresSaveFileName = "HighScores.xml";


        public ReactionTestStore()
        {
            this.g_IsolatedStore = IsolatedStorageSettings.ApplicationSettings;

            Initialize();
        }


        private void Initialize()
        {
            AllHighScores = new HighScoreList();

            if (g_IsolatedStore.Contains(LastHighScoreUpdateKey))
            {
                AllHighScores.LastHighScoreUpdate = (DateTime)g_IsolatedStore[LastHighScoreUpdateKey];
            }

            using (IsolatedStorageFile fileSystem = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!fileSystem.FileExists(HighScoresSaveFileName))
                {
                    AllHighScores.HighScores = new List<HighScoreItemViewModel>();
                }
                else
                {
                    using (IsolatedStorageFileStream fs = new IsolatedStorageFileStream(HighScoresSaveFileName, FileMode.Open, fileSystem))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<HighScoreItemViewModel>));
                        AllHighScores.HighScores = serializer.Deserialize(fs) as List<HighScoreItemViewModel>;
                    }
                }
            }
        }        

        private void SaveLastHighScoreUpdate()
        {
            if (g_IsolatedStore.Contains(LastHighScoreUpdateKey))
            {
                g_IsolatedStore[LastHighScoreUpdateKey] = AllHighScores.LastHighScoreUpdate;
            }
            else
            {
                g_IsolatedStore.Add(LastHighScoreUpdateKey, AllHighScores.LastHighScoreUpdate);
            }

            g_IsolatedStore.Save();
        }

        private void SaveHighScores()
        {
            using (IsolatedStorageFile filesystem = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fs = new IsolatedStorageFileStream(HighScoresSaveFileName, FileMode.Create, filesystem))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<HighScoreItemViewModel>));
                    serializer.Serialize(fs, AllHighScores.HighScores);
                }
            }
        }

        public void SaveStore()
        {
            SaveLastHighScoreUpdate();
            SaveHighScores();
        }
    }
}
