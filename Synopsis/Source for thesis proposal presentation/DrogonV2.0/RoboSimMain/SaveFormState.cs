using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Forms;

namespace RoboSim
{
    public class SaveFormState
    {
        public static void SaveState(Form f, string address = "")
        {
            if (address == "")
                address = f.Name;

            string userName = Environment.UserName.Replace("@", "").Replace(".", "");
            address = "Software\\WinFormSave\\" + address + "_" + userName;
            RegistryKey main = Registry.CurrentUser.OpenSubKey(address, RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (main == null)
                main = Registry.CurrentUser.CreateSubKey(address, RegistryKeyPermissionCheck.ReadWriteSubTree);

            saveFormProps(f, ref main);
            foreach (Control c in f.Controls)
                saveControl(c, ref main);
        }
        private static void saveControl(Control c, ref RegistryKey reg)
        {
            saveProps(c, ref reg);
            if (c.Controls.Count > 0)
            {
                var sub = reg.OpenSubKey(c.Name, true);
                if (sub == null)
                    sub = reg.CreateSubKey(c.Name, RegistryKeyPermissionCheck.ReadWriteSubTree);

                foreach (Control c2 in c.Controls)
                    saveControl(c2, ref sub);
            }
        }

        private static void restoreControl(Control c, ref RegistryKey reg)
        {
            restoreProps(c, ref reg);
            if (c.Controls.Count > 0)
            {
                var sub = reg.OpenSubKey(c.Name, true);
                if (sub == null)
                    return;

                foreach (Control c2 in c.Controls)
                    restoreControl(c2, ref sub);
            }
        }
        private static void saveProps(Control c, ref RegistryKey reg)
        {
            var sub = reg.OpenSubKey(c.Name, true);
            if (sub == null)
                sub = reg.CreateSubKey(c.Name, RegistryKeyPermissionCheck.ReadWriteSubTree);

            string[] props = new string[] { "Text", "Value", "Top", "Left", "Width", "Height", "Minimum", "Maximum", "Enabled", "Checked" };
            foreach (var prop in props)
            {
                var v = c.GetType().GetProperty(prop);
                if (v != null)
                    sub.SetValue(prop, v.GetValue(c));
            }
            if (c is ListBox)
            {
                var v = c.GetType().GetProperty("Items");
                var items = (ListBox.ObjectCollection)v.GetValue(c);

                var itemsSub = reg.OpenSubKey(c.Name + "\\" + "Items", true);

                if (itemsSub == null)
                    itemsSub = reg.CreateSubKey(c.Name + "\\" + "Items", RegistryKeyPermissionCheck.ReadWriteSubTree);
                foreach (var val in itemsSub.GetValueNames())
                    itemsSub.DeleteValue(val);

                int ind = 0;
                foreach (var item in items)
                {
                    itemsSub.SetValue("Item" + ind++, item);
                }
            }

            if (c is ComboBox)
            {
                var v = c.GetType().GetProperty("Items");
                var items = (ComboBox.ObjectCollection)v.GetValue(c);

                var itemsSub = reg.OpenSubKey(c.Name + "\\" + "Items", true);

                if (itemsSub == null)
                    itemsSub = reg.CreateSubKey(c.Name + "\\" + "Items", RegistryKeyPermissionCheck.ReadWriteSubTree);
                foreach (var val in itemsSub.GetValueNames())
                    itemsSub.DeleteValue(val);

                int ind = 0;
                foreach (var item in items)
                {
                    itemsSub.SetValue("Item" + ind++, item);
                }
            }
        }
        static void saveFormProps(Form f, ref RegistryKey reg)
        {                                
            string[] props = new string[] { "Text", "Width", "Height", "Enabled" };
            foreach (var prop in props)
            {
                var v = f.GetType().GetProperty(prop);
                reg.SetValue(prop, v.GetValue(f));
            }     
        }
        static void restoreFormProps(Form f, ref RegistryKey reg)
        {                                                                          
            foreach (var val in reg.GetValueNames())
            {
                var v = f.GetType().GetProperty(val);
                if (val == "Enabled")
                    v.SetValue(f, bool.Parse(reg.GetValue(val).ToString()));
                else
                    v.SetValue(f, reg.GetValue(val));
            }
        }
        private static void restoreProps(Control c, ref RegistryKey reg)
        {
            var sub = reg.OpenSubKey(c.Name, true);
            if (sub == null)
                return;
                                                                                                                                            
            foreach (var prop in sub.GetValueNames())
            {
                var v = c.GetType().GetProperty(prop);
                if (v != null)
                {
                    try
                    {
                        v.SetValue(c, sub.GetValue(prop));
                    }
                    catch (ArgumentException)
                    {
                        v.SetValue(c, bool.Parse(sub.GetValue(prop).ToString()));
                    }
                }
            }
            if (c is ListBox)
            {
                var v = c.GetType().GetProperty("Items");
                var items = (ListBox.ObjectCollection)v.GetValue(c);

                var itemsSub = reg.OpenSubKey(c.Name + "\\" + "Items", true);

                if (itemsSub == null)
                    return;
                foreach (var val in itemsSub.GetValueNames())      
                    ((ListBox)c).Items.Add(itemsSub.GetValue(val));
            }

            if (c is ComboBox)
            {
                var v = c.GetType().GetProperty("Items");
                var items = (ComboBox.ObjectCollection)v.GetValue(c);

                var itemsSub = reg.OpenSubKey(c.Name + "\\" + "Items", true);

                if (itemsSub == null)
                    return;
                foreach (var val in itemsSub.GetValueNames())
                    ((ComboBox)c).Items.Add(itemsSub.GetValue(val));
            }
        }

        public static void RestoreState(Form f, string address = "")
        {
            if (address == "")
                address = f.Name;

            string userName = Environment.UserName.Replace("@", "").Replace(".", "");
            address = "Software\\WinFormSave\\" + address + "_" + userName;
            RegistryKey main = Registry.CurrentUser.OpenSubKey(address, RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (main == null)
                return;

            restoreFormProps(f, ref main);
            foreach (Control c in f.Controls)
                restoreControl(c, ref main);
        }
    }
}
