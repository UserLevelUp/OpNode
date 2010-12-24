using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Security.Permissions;
using pWordLib.dat;
using pWordLib.dat.registry;
using System.IO;

namespace pWordLib.mgr
{
    public class registryMgr
    {
        private pRegistry _pRegistry;
        // Check the users registry only
        public registryMgr(String version)
        {
            // get the current user
            RegistryKey pWordRegistry = Registry.CurrentUser.CreateSubKey(@"Software\SourceForge\pWord");
            using (RegistryKey
                pWordUsing = pWordRegistry.CreateSubKey(@"Version" + version))
            {
                _pRegistry = new pRegistry();


                _pRegistry.Version = (String)pWordUsing.GetValue("Version");
                _pRegistry.Filename = (String)pWordUsing.GetValue("Filename");
            }

        }

        public bool FileExist()
        {
            try
            {
                if ((_pRegistry.Filename == null) || (_pRegistry.Filename.Length == 0))
                {
                    return false;
                }
                FileInfo fi = new FileInfo(_pRegistry.Filename);
                return fi.Exists;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        // When saving be sure to update the value in the registry
        public String AutoSavePathFromRegistry(String version)
        {
            return _pRegistry.Filename;
        }

        // When saving be sure to update the value in the registry
        public void SavePathInRegistry(String version, String path)
        {
            // get the current user
            RegistryKey pWordRegistry = Registry.CurrentUser.CreateSubKey(@"Software\SourceForge\pWord");
            using (RegistryKey
                pWordUsing = pWordRegistry.CreateSubKey(@"Version" + version))
            {
                String currentVersion = (String)pWordUsing.GetValue("Version");
                _pRegistry = new pRegistry();
                pWordUsing.SetValue("Filename", path);
                _pRegistry.Filename = path;
            }
        }



        private static void ShowSecurity(RegistrySecurity security)
        {
            Console.WriteLine("\r\nCurrent access rules:\r\n");
            foreach (RegistryAccessRule ar in
                security.GetAccessRules(true, true, typeof(NTAccount)))
            {
                Console.WriteLine("        User: {0}", ar.IdentityReference);
                Console.WriteLine("        Type: {0}", ar.AccessControlType);
                Console.WriteLine("      Rights: {0}", ar.RegistryRights);
                Console.WriteLine();
            }
        }
    }
}
