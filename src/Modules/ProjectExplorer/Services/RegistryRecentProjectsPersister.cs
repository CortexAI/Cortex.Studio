using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace Cortex.Modules.ProjectExplorer.Services
{
    class RegistryRecentProjectsPersister : IRecentProjectsPersister
    {
        private const string KeyName = "RECENT";

        public int MaxProjects { get; set; }
        public IEnumerable<string> RecentProjects { get { return GetRecentProjects(); } }
        public void AddProject(string path)
        {
            var subkey = Registry.CurrentUser.OpenSubKey("Software", true);
            if (subkey == null)
                throw new Exception("No software key found in registry");
            
            var key = subkey.OpenSubKey(AppDomain.CurrentDomain.FriendlyName, true);
            var projects = new List<string>();

            // Create a new key if not exists
            if (key == null)
            {
                key = subkey.CreateSubKey(AppDomain.CurrentDomain.FriendlyName);
                
                if(key == null)
                    throw new Exception("Cannot create a registry key");

                projects.Add(path);
            }
            else
            {
                var recentData = key.GetValue(KeyName) as string[];
                if(recentData != null)
                { 
                    if(!recentData.First().Equals(path))
                        projects.Add(path);    
                    projects.AddRange(recentData);
                }
                else
                    projects.Add(path);
            }

            if (projects.Count > MaxProjects)
                projects = projects.GetRange(0, MaxProjects);

            key.SetValue(KeyName, projects.ToArray(), RegistryValueKind.MultiString);
            key.Close();
            subkey.Close();
        }

        public void Clear()
        {
            var subkey = Registry.CurrentUser.OpenSubKey("Software");
            if (subkey != null)
            {
                var key = subkey.OpenSubKey(AppDomain.CurrentDomain.FriendlyName);
                if (key == null)
                    return;
                key.DeleteSubKey("RECENT");
                key.Close();
                subkey.Close();
            }
        }

        private static IEnumerable<string> GetRecentProjects()
        {
            var subkey = Registry.CurrentUser.OpenSubKey("Software");
            if (subkey != null)
            {
                var key = subkey.OpenSubKey(AppDomain.CurrentDomain.FriendlyName);
                if (key == null)
                    return new List<string>();
                var values = key.GetValue("RECENT") as string[];
                
                key.Close();
                subkey.Close();

                if (values != null)
                    return new List<string>(values);
            }

            return new List<string>();
        }
    }
}