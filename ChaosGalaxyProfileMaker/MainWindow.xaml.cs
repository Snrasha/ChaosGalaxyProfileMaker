
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChaosGalaxyProfileMaker
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProfileCG profileCG;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            profileCG=new ProfileCG();
        }

        string OpenImageDialog(string title)
        {

            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Title = title;
            dialog.FileName = "Image"; // Default file name
            dialog.DefaultExt = ".png"; // Default file extension
            dialog.Filter = "PNG Files (.png)|*.png"; // Filter files by extension

            // Show open file dialog box
           
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                if(dialog.FileName.Trim().Length == 0)
                {
                    return null;
                }
                // Open document
                return dialog.FileName;
            }

            return null;
        }
        string ImportProfileDialog()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Title = "Import your profile save";
            dialog.FileName = "save"; // Default file name
            dialog.DefaultExt = ".cgpm"; // Default file extension
            dialog.Filter = "CGPM Files (.cgpm)|*.cgpm"; // Filter files by extension

            // Show open file dialog box

            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                if (dialog.FileName.Trim().Length == 0)
                {
                    return null;
                }
                // Open document
                return dialog.FileName;
            }

            return null;
        }
        string ExportProfileDialog(string currentfilename)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Title = "Save the profile in cgpm format";
            if (currentfilename != null)
            {
                string[] inifilesplit = currentfilename.Split('/');
                string inifile = inifilesplit[inifilesplit.Length - 1];
                int idx = inifile.LastIndexOf(".");
                if (idx != -1)
                {
                    inifile = inifile.Substring(0, idx);
                }
                if (!inifile.EndsWith(".cgpm"))
                {
                    inifile += ".cgpm";
                }
                dialog.FileName = inifile; // Default file name
            }
            dialog.Filter = "CGPM Files (.cgpm)|*.cgpm"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                string resultpath = dialog.FileName;
                if (resultpath.Trim().Length == 0)
                {
                    return null;
                }
                else if (!resultpath.EndsWith(".cgpm"))
                {
                    resultpath += ".cgpm";
                }
                // Open document
                return resultpath;
            }

            return null;
        }
        string SaveProfileDialog(string currentfilename)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Title = "Save the profile in jpg";
            if (currentfilename != null)
            {
                string[] inifilesplit = currentfilename.Split('/');
                string inifile = inifilesplit[inifilesplit.Length - 1];
                int idx = inifile.LastIndexOf(".");
                if (idx != -1)
                {
                    inifile = inifile.Substring(0, idx);
                }
                if (!inifile.EndsWith(".jpg"))
                {
                    inifile += ".jpg";
                }
                dialog.FileName = inifile; // Default file name
            }
            dialog.Filter = "JPG Files (.jpg)|*.jpg"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                string resultpath = dialog.FileName;
                if (resultpath.Trim().Length == 0)
                {
                    return null;
                }
                else if (!resultpath.EndsWith(".jpg"))
                {
                    resultpath += ".jpg";
                }
                // Open document
                return resultpath;
            }

            return null;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        int NextStop(string[] lines,int currentIdx)
        {
            while (lines.Length>currentIdx && !lines[currentIdx].StartsWith("####"))
            {
                currentIdx++;
            }
            return currentIdx + 1;
        }
        string NextStopGetString(string[] lines, int currentIdx)
        {
            string txt = null;
            while (lines.Length > currentIdx && !lines[currentIdx].StartsWith("####"))
            {
                if (txt == null)
                {
                    txt = lines[currentIdx];
                }
                else
                {
                    txt = txt + '\n' + lines[currentIdx];
                }
                currentIdx++;
            }
            return txt;
        }

        void OnClickEdit(object sender, RoutedEventArgs e)
        {
            string filename = ImportProfileDialog();
            if (filename == null)
            {
                return;
            }
            
            string[] lines = File.ReadAllLines(filename, Encoding.UTF8);
            int currentIdx = 0;

            currentIdx = NextStop(lines, currentIdx);
            profileCG.LoadPortrait(lines[currentIdx]);
            currentIdx = NextStop(lines, currentIdx);
            profileCG.LoadUnit(lines[currentIdx]);
            currentIdx = NextStop(lines, currentIdx);
            text_box_talent_hero.Text= NextStopGetString(lines, currentIdx);
            currentIdx = NextStop(lines, currentIdx);
            text_box_strategy_hero.Text= NextStopGetString(lines, currentIdx);
            currentIdx = NextStop(lines, currentIdx);
            text_box_tactics_hero.Text= NextStopGetString(lines, currentIdx);

            currentIdx = NextStop(lines, currentIdx);
            text_box_story_hero.Text = NextStopGetString(lines, currentIdx);
            currentIdx = NextStop(lines, currentIdx);
            text_box_conditions_hero.Text = NextStopGetString(lines, currentIdx);

            currentIdx = NextStop(lines, currentIdx);
            text_box_military_hero.Text = NextStopGetString(lines, currentIdx);
            currentIdx = NextStop(lines, currentIdx);
            text_box_intellect_hero.Text = NextStopGetString(lines, currentIdx);
            currentIdx = NextStop(lines, currentIdx);
            text_box_admin_hero.Text = NextStopGetString(lines, currentIdx);
            currentIdx = NextStop(lines, currentIdx);
            text_box_character_hero.Text = NextStopGetString(lines, currentIdx);

            currentIdx = NextStop(lines, currentIdx);
            text_box_power_unit.Text = NextStopGetString(lines, currentIdx);
            currentIdx = NextStop(lines, currentIdx);
            text_box_energy_unit.Text = NextStopGetString(lines, currentIdx);
            currentIdx = NextStop(lines, currentIdx);
            text_box_agility_unit.Text = NextStopGetString(lines, currentIdx);
            currentIdx = NextStop(lines, currentIdx);
            text_box_movement_unit.Text = NextStopGetString(lines, currentIdx);

            currentIdx = NextStop(lines, currentIdx);
            text_box_name_hero.Text = NextStopGetString(lines, currentIdx);
            currentIdx = NextStop(lines, currentIdx);
            text_box_name_unit.Text = NextStopGetString(lines, currentIdx);


            currentIdx = NextStop(lines, currentIdx);
            text_box_type_unit.Text = NextStopGetString(lines, currentIdx);
            currentIdx = NextStop(lines, currentIdx);
            string weaponstxt= NextStopGetString(lines, currentIdx);
            string[] weapons= weaponstxt.Split('\n');
            if (weapons.Length > 0) {
                text_box_weapons_unit_1.Text = weapons[0];
            }
            if (weapons.Length > 1)
            {
                text_box_weapons_unit_2.Text = weapons[1];
            }
            if (weapons.Length > 2)
            {
                text_box_weapons_unit_3.Text = weapons[2];
            }
            currentIdx = NextStop(lines, currentIdx);
            text_box_shield_unit.Text = NextStopGetString(lines, currentIdx);
            currentIdx = NextStop(lines, currentIdx);
            string abilitiestxt = NextStopGetString(lines, currentIdx);
            string[] abilities = abilitiestxt.Split('\n');
            if (abilities.Length > 0)
            {
                text_box_abilities_unit_1.Text = abilities[0];
            }
            if (abilities.Length > 1)
            {
                text_box_abilities_unit_2.Text = abilities[1];
            }
            if (abilities.Length > 2)
            {
                text_box_abilities_unit_3.Text = abilities[2];
            }

            profileCG.SetFilePath(filename);

        }
        void OnClickSave(object sender, RoutedEventArgs e)
        {
            string filename = ExportProfileDialog(profileCG.GetFileName());
            if (filename == null)
            {
                return;
            }
            profileCG.SetNames(text_box_name_hero.Text, text_box_name_unit.Text);
            profileCG.SetUnitStats(text_box_power_unit.Text, text_box_energy_unit.Text, text_box_agility_unit.Text, text_box_movement_unit.Text);

            string[] weapons = new string[] { text_box_weapons_unit_1.Text.Trim(),
            text_box_weapons_unit_2.Text.Trim(),
            text_box_weapons_unit_3.Text.Trim()};

            string[] abilities = new string[] { text_box_abilities_unit_1.Text.Trim(),
            text_box_abilities_unit_2.Text.Trim(),
            text_box_abilities_unit_3.Text.Trim()};

            profileCG.SetUnitEquipment(text_box_type_unit.Text, weapons, text_box_shield_unit.Text, abilities);

            profileCG.SetStartingStats(text_box_military_hero.Text, text_box_intellect_hero.Text, text_box_admin_hero.Text, text_box_character_hero.Text);
            profileCG.SetStoryAndConditions(text_box_story_hero.Text, text_box_conditions_hero.Text);
            profileCG.SetSkills(text_box_talent_hero.Text, text_box_strategy_hero.Text, text_box_tactics_hero.Text);
            profileCG.ExportProfile(filename);
        }
        void OnClickExport(object sender, RoutedEventArgs e)
        {
            string filename = SaveProfileDialog(profileCG.GetFileName());
            if (filename == null)
            {
                return;
            }
            profileCG.SetNames(text_box_name_hero.Text, text_box_name_unit.Text);
            profileCG.SetUnitStats(text_box_power_unit.Text, text_box_energy_unit.Text, text_box_agility_unit.Text, text_box_movement_unit.Text);

            string[] weapons = new string[] { text_box_weapons_unit_1.Text.Trim(),
            text_box_weapons_unit_2.Text.Trim(),
            text_box_weapons_unit_3.Text.Trim()};

            string[] abilities = new string[] { text_box_abilities_unit_1.Text.Trim(),
            text_box_abilities_unit_2.Text.Trim(),
            text_box_abilities_unit_3.Text.Trim()};

            profileCG.SetUnitEquipment(text_box_type_unit.Text, weapons, text_box_shield_unit.Text, abilities);

            profileCG.SetStartingStats(text_box_military_hero.Text, text_box_intellect_hero.Text, text_box_admin_hero.Text, text_box_character_hero.Text);
            profileCG.SetStoryAndConditions(text_box_story_hero.Text, text_box_conditions_hero.Text);
            profileCG.SetSkills(text_box_talent_hero.Text, text_box_strategy_hero.Text, text_box_tactics_hero.Text);
            profileCG.SaveProfile(filename);
        }
        void OnClickPortrait(object sender, RoutedEventArgs e)
        {
            string filename = OpenImageDialog("Select the CO portrait (240x200)");
            if (filename == null)
            {
                return;
            }
            profileCG.LoadPortrait(filename);

        }

        void OnClickUnit(object sender, RoutedEventArgs e)
        {
            string filename = OpenImageDialog("Select the CO unit spritesheet (128x64 face and behind)");
            if (filename == null)
            {
                return;
            }
            profileCG.LoadUnit(filename);

        }
        void OnClickDismiss(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None && e.Key == Key.V)
            {
                OnClickExport(null, null);
            }
            else if (Keyboard.Modifiers == ModifierKeys.None && e.Key == Key.D)
            {
                OnClickEdit(null, null);
            }
            else if (Keyboard.Modifiers == ModifierKeys.None && e.Key == Key.S)
            {
                OnClickSave(null, null);
            }
            else if (Keyboard.Modifiers == ModifierKeys.None && e.Key == Key.Escape)
            {
                this.Close();
            }
        }


    }

}
