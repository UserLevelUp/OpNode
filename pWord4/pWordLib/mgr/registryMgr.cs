using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Security.Permissions;

namespace pWordLib.mgr
{
    public class registryMgr
    {

        private String fileName;

        public String FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        // Check the users registry only
        public registryMgr(String version)
        {
            // get the current user
            RegistryKey key = Registry.CurrentUser;
            int hash = key.GetHashCode();
            RegistryKey HKEYCU_SoftwareKey = key.OpenSubKey("Software");
            RegistryKey HKEYCU_SourceForgeKey = HKEYCU_SoftwareKey.OpenSubKey("SourceForge");
            if (HKEYCU_SourceForgeKey != null)
            {
                RegistryKey HKEYCU_pWordKey = HKEYCU_SourceForgeKey.OpenSubKey("pWord");
                if (HKEYCU_pWordKey != null)
                {
                    RegistryKey pwordVersionKey = HKEYCU_pWordKey.OpenSubKey(version);
                    if ((pwordVersionKey != null) && (pwordVersionKey.SubKeyCount <= 0))
                    {
                        String[] names = pwordVersionKey.GetValueNames();
                        if (names.Length > 0)
                        {
                            foreach (String name in names)
                            {
                                pwordVersionKey.GetValue(name, "empty", RegistryValueOptions.None);
                            }
                        }
                        else
                        {
                            // Nothing is there so continue 
                        }
                    }
                    else
                    {
                        // it doesn't exists, so go ahead and create just the part that is missing ie... the key Version0_0_06B
                        // Note: the current user only cares about where his pWord file is located.  Multiple users can access multiple version of pWord
                        string user = "";
                        try
                        {
                            user =  Environment.UserDomainName + @"\" + Environment.UserName;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        HKEYCU_pWordKey = HKEYCU_SourceForgeKey.OpenSubKey("pWord");
                        
                        try
                        {
                            RegistryKey HKEYCU_V6B = HKEYCU_pWordKey.OpenSubKey("Version" + version, false);
                            RegistryKey HKeyCU_FN = HKEYCU_pWordKey.OpenSubKey("FileName", false);
                            String PWordVersion = (String)HKEYCU_V6B.GetValue("Version");
                            HKEYCU_pWordKey.Close();
                        }
                        catch (
                            Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        }

                    }

                }

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
